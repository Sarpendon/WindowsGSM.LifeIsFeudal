using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WindowsGSM.Functions;
using WindowsGSM.GameServer.Query;
using WindowsGSM.GameServer.Engine;
using System.IO;
using System.Linq;
using System.Net;
using System.Collections.Generic;

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
            version = "1.1",
            url = "https://github.com/Sarpendon/WindowsGSM.LifeIsFeudal",
            color = "#8802db"
        };

        // - Settings properties for SteamCMD installer
        public override bool loginAnonymous => true;
        public override string AppId => "320850";

        // - Standard Constructor and properties
        public LifeIsFeudal(ServerConfig serverData) : base(serverData) => base.serverData = _serverData = serverData;
        private readonly ServerConfig _serverData;
        public string Error, Notice;

        // - Game server Fixed variables
        public override string StartPath => @"ddctd_cm_yo_server.exe";
        public string FullName = "Life Is Feudal Dedicated Server";
        public bool AllowsEmbedConsole = true;
        public int PortIncrements = 2;
        public object QueryMethod = new A2S();

        // - Game server default values
        public string Port = "2456";
        public string QueryPort = "2457";
        public string Defaultmap = "world ID 1";
        public string Maxplayers = "64";
        public string Additional = "";

        public async Task<Process> Start()
        {
            string shipExePath = Functions.ServerPath.GetServersServerFiles(_serverData.ServerID, StartPath);

            // Prepare start parameter
            string param = $""; 
            param += string.IsNullOrWhiteSpace(_serverData.ServerMap) ? string.Empty : $" {_serverData.ServerMap}";
            param += string.IsNullOrWhiteSpace(_serverData.ServerParam) ? string.Empty : $" {_serverData.ServerParam}";

            // Prepare Process
            var gameServerProcess = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = ServerPath.GetServersServerFiles(_serverData.ServerID),
                    FileName = ServerPath.GetServersServerFiles(_serverData.ServerID, StartPath),
                    Arguments = param,
                    WindowStyle = ProcessWindowStyle.Minimized,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };

            // Set up Redirect Input and Output to WindowsGSM Console if EmbedConsole is on
            if (AllowsEmbedConsole)
            {
                gameServerProcess.StartInfo.CreateNoWindow = true;
                gameServerProcess.StartInfo.RedirectStandardInput = true;
                gameServerProcess.StartInfo.RedirectStandardOutput = true;
                gameServerProcess.StartInfo.RedirectStandardError = true;
                var serverConsole = new ServerConsole(_serverData.ServerID);
                gameServerProcess.OutputDataReceived += serverConsole.AddOutput;
                gameServerProcess.ErrorDataReceived += serverConsole.AddOutput;

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

                gameServerProcess.BeginOutputReadLine();
                gameServerProcess.BeginErrorReadLine();
                return gameServerProcess;
            }

            // Start Process
            try
            {
                gameServerProcess.Start();
                return gameServerProcess;
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
        }

        public async Task Stop(Process gameServerProcess)
        {
            await Task.Run(() =>
            {
                Functions.ServerConsole.SetMainWindow(gameServerProcess.MainWindowHandle);
                Functions.ServerConsole.SendWaitToMainWindow("^c"); // Send Ctrl+C command
                gameServerProcess.WaitForExit(5000);
            });
        }

        public async Task<Process> Update(bool validate = false, string custom = null)
        {
            var (p, error) = await Installer.SteamCMD.UpdateEx(serverData.ServerID, AppId, validate, custom: custom, loginAnonymous: loginAnonymous);
            Error = error;
            await Task.Run(() => { p.WaitForExit(); });
            return p;
        }

        public bool IsInstallValid()
        {
            return File.Exists(Functions.ServerPath.GetServersServerFiles(_serverData.ServerID, StartPath));
        }

        public bool IsImportValid(string path)
        {
            string exePath = Path.Combine(path, "PackageInfo.bin");
            Error = $"Invalid Path! Fail to find {Path.GetFileName(exePath)}";
            return File.Exists(exePath);
        }

        public string GetLocalBuild()
        {
            var steamCMD = new Installer.SteamCMD();
            return steamCMD.GetLocalBuild(_serverData.ServerID, AppId);
        }

        public async Task<string> GetRemoteBuild()
        {
            var steamCMD = new Installer.SteamCMD();
            return await steamCMD.GetRemoteBuild(AppId);
        }
    }
}
