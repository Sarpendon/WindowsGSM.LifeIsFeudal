using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsGSM.Functions;
using WindowsGSM.GameServer.Engine;
using WindowsGSM.GameServer.Query;

namespace WindowsGSM.Plugins
{
    public class LifeIsFeudal : SteamCMDAgent
    {
        // - Plugin Details
        public Plugin Plugin = new Plugin
        {
            name = "WindowsGSM.LifeIsFeudal",
            author = "Sarpendon",
            description = "WindowsGSM plugin for supporting Life Is Feudal Dedicated Server",
            version = "1.2",
            url = "https://github.com/Sarpendon/WindowsGSM.LifeIsFeudal",
            color = "#8802db"
        };

        // - Settings properties for SteamCMD installer
        public override bool loginAnonymous => true;
        public override string AppId => "320850";

        // - Standard Constructor and properties
        public LifeIsFeudal(ServerConfig serverData) : base(serverData) => base.serverData = _serverData = serverData;
        private readonly ServerConfig _serverData;

        // Error and Notice are deliberately not redeclared here. SteamCMDAgent already provides
        // both, and a field of the same name hides the base property: WindowsGSM reads
        // gameServer.Error dynamically and binds to the most derived member, so everything the
        // base wrote - the reason Install() failed, for one - never reached the UI.

        // - Game server Fixed variables
        public override string StartPath => @"ddctd_cm_yo_server.exe";
        public string FullName = "Life Is Feudal Dedicated Server";

        // Capability flag only. WindowsGSM overwrites it with the server's own Embed Console
        // setting (MainWindow.Server_BeginStart) right before calling Start().
        public bool AllowsEmbedConsole = true;

        // LiF:YO uses 28000-28003 TCP/UDP, so the next server installed has to start four ports
        // further up to avoid overlapping.
        public int PortIncrements = 4;
        public object QueryMethod = new A2S();

        // - Game server default values (applied to newly installed servers only)
        //
        // These were 2456/2457 - Valheim's ports, carried over from that plugin's template. The
        // server itself takes its port from world_<id>.xml, so this only decides what WindowsGSM
        // opens in the firewall and which port it queries for the player count; pointing it at
        // the range LiF:YO actually uses makes that work out of the box.
        public string Port = "28000";
        public string QueryPort = "28001";

        // Passed straight through to the server; see BuildWorldParameter below.
        public string Defaultmap = "-world 1";
        public string Maxplayers = "64";
        public string Additional = "";

        // - Life is Feudal is configured through world_<id>.xml and config_local.cs, both of
        //   which the admin prepares by hand along with the MariaDB database, so there is no
        //   config file to generate here. WindowsGSM calls this after installing, so provide it
        //   rather than letting the call fail as a swallowed binder exception.
        public void CreateServerCFG() { }

        public async Task<Process> Start()
        {
            string shipExePath = ServerPath.GetServersServerFiles(_serverData.ServerID, StartPath);
            if (!File.Exists(shipExePath))
            {
                Error = $"{Path.GetFileName(shipExePath)} not found ({shipExePath})";
                return null;
            }

            // Prepare start parameter
            string param = BuildWorldParameter(_serverData.ServerMap);
            param += string.IsNullOrWhiteSpace(_serverData.ServerParam) ? string.Empty : $" {_serverData.ServerParam}";

            // Prepare Process
            var gameServerProcess = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = ServerPath.GetServersServerFiles(_serverData.ServerID),
                    FileName = shipExePath,
                    Arguments = param,
                    WindowStyle = ProcessWindowStyle.Minimized,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };

            // Set up Redirect Input and Output to WindowsGSM Console if EmbedConsole is on
            bool embedConsole = AllowsEmbedConsole;
            if (embedConsole)
            {
                gameServerProcess.StartInfo.CreateNoWindow = true;
                gameServerProcess.StartInfo.RedirectStandardInput = true;
                gameServerProcess.StartInfo.RedirectStandardOutput = true;
                gameServerProcess.StartInfo.RedirectStandardError = true;

                // Without this, non-ASCII characters in server and player names arrive mangled
                // in the WindowsGSM console pane.
                gameServerProcess.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                gameServerProcess.StartInfo.StandardErrorEncoding = Encoding.UTF8;

                var serverConsole = new ServerConsole(_serverData.ServerID);
                gameServerProcess.OutputDataReceived += serverConsole.AddOutput;
                gameServerProcess.ErrorDataReceived += serverConsole.AddOutput;
            }

            // Start Process
            try
            {
                gameServerProcess.Start();
            }
            catch (FileNotFoundException e)
            {
                Error = $"File not found: {e.Message}";
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                Error = $"Access denied: {e.Message}";
                return null;
            }
            catch (Exception e)
            {
                Error = e.Message;
                return null;
            }

            if (embedConsole)
            {
                gameServerProcess.BeginOutputReadLine();
                gameServerProcess.BeginErrorReadLine();
            }

            return gameServerProcess;
        }

        // The Start Map field feeds the server's -world argument, which selects the
        // world_<id>.xml to run. The default used to be the descriptive text "world ID 1", which
        // reached the server as three separate arguments - "world", "ID" and "1" - rather than a
        // world selection.
        //
        // A value that already looks like a parameter is passed through untouched, so "-world 2"
        // keeps working. Otherwise the world number is read out of the text, which means servers
        // still configured with the old default land on -world 1 as intended instead of needing
        // the field edited by hand.
        private static string BuildWorldParameter(string serverMap)
        {
            if (string.IsNullOrWhiteSpace(serverMap)) { return string.Empty; }

            string trimmed = serverMap.Trim();
            if (trimmed.StartsWith("-")) { return $" {trimmed}"; }

            var digits = new StringBuilder();
            foreach (char c in trimmed)
            {
                if (char.IsDigit(c)) { digits.Append(c); }
            }

            return digits.Length > 0 ? $" -world {digits}" : $" -world \"{trimmed}\"";
        }

        // - Graceful shutdown support
        //
        // The previous stop path sent Ctrl+C to the process window with SendKeys. That never
        // reached the server: WindowsGSM hides the server window immediately after starting it
        // (MainWindow.Server_BeginStart), so SetForegroundWindow fails, and SendKeys.SendWait is
        // global - the keystroke went to whatever window happened to have focus on the host.
        // With an embedded console there is no window at all and MainWindowHandle is IntPtr.Zero.
        // Either way the server was killed by WindowsGSM instead of shutting down.
        //
        // ddctd_cm_yo_server.exe is a console application, so it always has a console - even
        // under CREATE_NO_WINDOW. Attaching to that console and raising CTRL_C_EVENT there asks
        // the server to shut down cleanly, which matters here because it has an open MariaDB
        // connection to finish writing through.

        private delegate bool ConsoleCtrlDelegate(uint ctrlType);

        private const uint CTRL_C_EVENT = 0;

        // Server_BeginStop awaits Stop() without a timeout of its own, so a long shutdown is
        // honoured. Only wait it out when a signal was actually delivered.
        private const int GRACEFUL_EXIT_TIMEOUT_MS = 120000;
        private const int FORCED_EXIT_TIMEOUT_MS = 5000;

        // WindowsGSM clears the console pane as soon as Stop() returns, taking the shutdown
        // output with it. Set to 0 to hand back immediately.
        private const int CONSOLE_LINGER_MS = 3000;

        // Console attachment is per process, not per thread, and WindowsGSM can run two stops at
        // once (auto restart, restart crontab and update-on-start each drive their own timer).
        // Without this lock one stop can detach the console another is about to signal.
        private static readonly object _consoleSignalLock = new object();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handler, bool add);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

        public async Task Stop(Process gameServerProcess)
        {
            if (gameServerProcess == null) { return; }

            await Task.Run(() =>
            {
                // Note: WindowsGSM builds the instance behind Stop() without a ServerConfig, so
                // _serverData is null in here. Everything this method needs comes from the
                // process itself.
                try
                {
                    if (gameServerProcess.HasExited) { return; }
                }
                catch (Exception e)
                {
                    Error = e.Message;
                    return;
                }

                lock (_consoleSignalLock)
                {
                    bool attached = false;
                    bool signalled = false;

                    try
                    {
                        // AttachConsole fails while this process still owns a console of its own.
                        FreeConsole();

                        attached = AttachConsole((uint)gameServerProcess.Id);
                        if (attached)
                        {
                            // The event reaches every process on that console, which now includes
                            // WindowsGSM. Ignore it here first, or the manager goes down with the
                            // server it is trying to stop.
                            SetConsoleCtrlHandler(null, true);
                            signalled = GenerateConsoleCtrlEvent(CTRL_C_EVENT, 0);
                        }

                        if (!signalled && gameServerProcess.MainWindowHandle != IntPtr.Zero)
                        {
                            // Fallback for a server that really does have a visible window.
                            // Guarded on the handle because SendKeys is global: with no window to
                            // bring forward, the keystroke lands in whatever the user is working in.
                            ServerConsole.SetMainWindow(gameServerProcess.MainWindowHandle);
                            ServerConsole.SendWaitToMainWindow("^c");
                            signalled = true;
                        }

                        gameServerProcess.WaitForExit(signalled ? GRACEFUL_EXIT_TIMEOUT_MS : FORCED_EXIT_TIMEOUT_MS);

                        if (signalled && CONSOLE_LINGER_MS > 0 && gameServerProcess.HasExited)
                        {
                            Thread.Sleep(CONSOLE_LINGER_MS);
                        }
                    }
                    catch (Exception e)
                    {
                        Error = e.Message;
                    }
                    finally
                    {
                        if (attached)
                        {
                            try { SetConsoleCtrlHandler(null, false); } catch { /* ignore */ }
                            try { FreeConsole(); } catch { /* ignore */ }
                        }
                    }
                }
            });
        }

        public new async Task<Process> Update(bool validate = false, string custom = null)
        {
            var (p, error) = await Installer.SteamCMD.UpdateEx(serverData.ServerID, AppId, validate, custom: custom, loginAnonymous: loginAnonymous);
            Error = error;

            // UpdateEx hands back null when steamcmd could not be downloaded or the Steam account
            // is not set up. The old code dereferenced it unconditionally, so a failed update
            // threw a NullReferenceException out of WindowsGSM's update path - twice, since the
            // retry hits the same line - instead of reporting the error it was given.
            if (p == null) { return null; }

            // Auto Update restarts the server as soon as this returns, so the update has to be
            // finished by then, not merely started.
            await Task.Run(() => p.WaitForExit());
            return p;
        }

        public new bool IsInstallValid()
        {
            string installPath = ServerPath.GetServersServerFiles(_serverData.ServerID, StartPath);
            if (File.Exists(installPath)) { return true; }

            // Keep a message the installer already left behind - that one says why it failed.
            if (string.IsNullOrWhiteSpace(Error))
            {
                Error = $"Fail to find {installPath}";
            }

            return false;
        }

        public new bool IsImportValid(string path)
        {
            // This used to look for PackageInfo.bin, carried over from the ARK plugin. Life is
            // Feudal does not ship that file, so importing an existing server always failed.
            string importPath = Path.Combine(path, StartPath);
            Error = $"Invalid Path! Fail to find {Path.GetFileName(StartPath)}";
            return File.Exists(importPath);
        }

        public new string GetLocalBuild()
        {
            var steamCMD = new Installer.SteamCMD();
            return steamCMD.GetLocalBuild(_serverData.ServerID, AppId);
        }

        public new async Task<string> GetRemoteBuild()
        {
            var steamCMD = new Installer.SteamCMD();
            return await steamCMD.GetRemoteBuild(AppId);
        }
    }
}
