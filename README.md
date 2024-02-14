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
> All the infos you type into WindowsGSM have no purpose what so ever all settings of the server have to be changed in the world_1.xml!!!

You can also use this helpfull Guide in Steam if you have problems or got stuck with the server setup: https://steamcommunity.com/app/290080/discussions/6/1368380934237836945/

# Other WinGSM Plugins:
| Icon | Game Name | Link | Version |
| --- | --- | --- | --- |
| <img src="https://i.imgur.com/LI1uPIJ.png" width="100" height="100"> | Myth of Empires Dedicated Server | [GitHub Link](https://github.com/Sarpendon/WindowsGSM.MythofEmpires) | 1.9 |
| <img src="https://i.imgur.com/25x4Ohs.png" width="100" height="100"> | Valheim Dedicated Server | [GitHub Link](https://github.com/Sarpendon/WindowsGSM.Valheim) | 1.1 |
| <img src="https://i.imgur.com/A9jtLPQ.png" width="100" height="100"> | V Rising Dedicated Server | [GitHub Link](https://github.com/Sarpendon/WindowsGSM.VRising) | 1.0 |
| <img src="https://i.imgur.com/A6dCSy9.png" width="100" height="100"> | Life is Feudal Dedicated Server | [GitHub Link](https://github.com/Sarpendon/WindowsGSM.LifeIsFeudal) | 1.0 |

