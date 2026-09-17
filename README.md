# WindowsGSM.LifeIsFeudal
 🧩WindowsGSM plugin that provides Life is Feudal Dedicated server support!
 
 🏷️ To be used with https://windowsgsm.com/ 

> [!CAUTION]
> Please be advised that you'll need some additional Software like MariaDB to get this Server working propperly. Also you need to work on the SQL Database and move around some files yourself!

# Basic Installation: 
1. Download  WindowsGSM from the Link above.
2. Download this Plugin as .zip container and don't unpack it.
3. Create a Folder at a Location you wan't all Server to be Installed and Run.
4. Drag WindowsGSM.Exe into previoulsy created folder and execute it.
5. Press on the Puzzle Icon in the left bottom side and install this plugin by navigating to it and select the Zip File.
6. Wait a couple of seconds then close the plugin menu and install the game server.
7. Please download and install MariaDB Version 10.2.44 [Download Link](https://mariadb.org/download/?t=mariadb&o=true&p=mariadb&r=10.2.44&os=windows&cpu=x86_64&pkg=msi)
8. During the installation process of MariaDB please Check: "Access Root Remotely"  "UTF-8 Format" and pick a Database Password.
9. After you installed MariaDB you should have now HeidiSQL to open up and create a new Session called "LIF"
10. Use that Password you created during the installation of Maria DB to log into the created Database and createa a new one called "lif_1" and select utf8_unicode_ci!
> [!CAUTION]
> Please be carefull to select utf8_unicode_ci and not utf8_general_ci
11. Locate the \SQL\new.sql file and open it up and copy & paste the whole code into HeidiSQL Query Tab and then Run the batch in one go.
12. Locate the \Docs\config_local.cs file and replace the password with the password you selected when you installed MariaDB and save the file
13. Copy and paste the file into the root directory of the server
14. Locate the \Config\world_1.xml file and open it up to make any kind of Server Settings changes. Do not forget to save after you're finished.
15. Try to start the server over WindowsGSM, if it doesn't start please try to follow the steps again from step 7.


# The Game:
- 🕹️ **Steam Site:** https://store.steampowered.com/app/290080/Life_is_Feudal_Your_Own/

# Requirements:
- 🖥️ **WindowsGSM** >= 1.21.0

# Server Settings:
> [!IMPORTANT]
> Nearly everything you type into WindowsGSM has no effect on the server itself - all server
> settings live in world_1.xml. The two exceptions:
>- **Server Start Map:** *Selects the world, and is passed to the server as `-world <id>`. The
>  default `-world 1` runs world_1.xml. Enter `-world 2` for world_2.xml, and so on.*
>- **Server Port / Server Query Port:** *These do not configure the server - it takes its port from
>  world_1.xml - but they decide what WindowsGSM opens in the firewall and which port it queries
>  for the player count, so keep them matching your xml. LiF:YO uses 28000-28003 TCP and UDP.*

You can also use this helpfull Guide in Steam if you have problems or got stuck with the server setup: https://steamcommunity.com/app/290080/discussions/6/1368380934237836945/

# Changelog:
### 1.2
- **The world is selected properly now.** The Start Map default was the descriptive text
  `world ID 1`, which reached the server as three separate arguments rather than a world
  selection. It is now `-world 1`. Servers still configured with the old text keep working - the
  world number is read out of it - and anything you enter yourself starting with `-` is passed
  through untouched.
- Default ports are 28000/28001 instead of 2456/2457, which were Valheim's, carried over from that
  plugin's template. The server takes its own port from `world_<id>.xml`, so this only decides what
  WindowsGSM opens in the firewall and queries for the player count. New servers only.
- **Stopping the server now reaches it.** The stop signal was sent to the server's window with
  SendKeys, but WindowsGSM hides that window right after starting the server - so the keystroke
  went to whatever window happened to have focus on the machine, never to the server. Every stop
  ran into the timeout and ended in a hard kill. The signal is now raised on the server's own
  console, so it shuts down properly instead of being killed.
- The shutdown output stays readable for a few seconds instead of being cleared instantly.
- **Failed installs and updates now say why.** The reason was being swallowed and shown as an
  empty `[ERROR]`; a failed update additionally crashed with a `NullReferenceException`.
- **Importing an existing server works.** It was looking for `PackageInfo.bin`, a file this game
  does not ship, so the import always failed.
- A missing server executable is reported as such instead of a generic Windows error.
- Console output is read as UTF-8, so umlauts and other non-ASCII characters are no longer mangled.

# Other WinGSM Plugins:
| Icon | Game Name | Link | Version |
| --- | --- | --- | --- |
| <img src="https://i.imgur.com/LI1uPIJ.png" width="100" height="100"> | Myth of Empires Dedicated Server | [GitHub Link](https://github.com/Sarpendon/WindowsGSM.MythofEmpires) | 2.0 |
| <img src="https://i.imgur.com/25x4Ohs.png" width="100" height="100"> | Valheim Dedicated Server | [GitHub Link](https://github.com/Sarpendon/WindowsGSM.Valheim) | 1.2 |
| <img src="https://i.imgur.com/A9jtLPQ.png" width="100" height="100"> | V Rising Dedicated Server | [GitHub Link](https://github.com/Sarpendon/WindowsGSM.VRising) | 1.1 |
| <img src="https://i.imgur.com/A6dCSy9.png" width="100" height="100"> | Life is Feudal Dedicated Server | [GitHub Link](https://github.com/Sarpendon/WindowsGSM.LifeIsFeudal) | 1.2 |

