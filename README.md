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
>- **Server Name:** *Fill in the Name of your Server in this Section*
>- **Server IP Adress:** *Local IP of your Server there is no need to change this GSM should get the right IP adress itself*
>- **Server Port:** *Game Port of the Server*
>- **Server Query Port:** *Is actually the RCON Port of the Server*
>- **Server Maxplayer:** *Maximum Number of Players on your Server*
>- **Server Start Map:** *Enter the Name of the Map the Server should start. If there will be more maps in the future you can change them here.*
>- **Server GSLT:** *If you want to have a Server Password enter it here or leave empty for no Server Password!*
>- **Server Start Param:** *Some Parameter are already filled in by default you can add or remove them as you wish! 

> [!CAUTION]
> Keep in mind to use quotes around texts that have spaces inbetween for example:
>- **-Description="My Server is Awesome!"** 

> [!TIP]
> To give yourself and/or others Admin acces add the Steam ID's after -ServerAdminAccounts=XXXXXXXXXXXXXXXXXX and separate with ";" if there are multiple admins.

# Other Server Settings:
| Server Start Param| Description | Example Value |
| --- | --- | --- |
| `-ServerId=` | Id of the Server itself | -ServerId=100 |
| `-ClusterId=` | ID of the Cluster the Server belongs to | -ClusterId=1 |
| `-Description=` | Short Description of the Server | -Description="join my new Server" |
| `-GameServerPVPType=` | PVP(0) / PVE(1) | -GameServerPVPType=1 |
| `-NoticeSelfEnable=` | Enable/Disable Welcome Text | -NoticeSelfEnable=true |
| `-NoticeSelfEnterServer=` | Welcome text when joining the Server | NoticeSelfEnterServer="Welcome to my Server!" |
| `-MapDifficultyRate=` | Strength & HP of NPC's and Animals | -MapDifficultyRate=1 |
| `-ServerAdminAccounts=` | Steam ID of Admin Players | -ServerAdminAccounts=76561198095468380 |
| `-bCanVoiceChat=` | Enable/Disable Voice Chat | -bCanVoiceChat=true |
| `-bCanChat=` | Enable/Disable Chat | -bCanChat=true |
| `-NoticeAllEnable=` | Enable/Disable Join/Leave Message | -NoticeAllEnable=true |
| `-NoticeEnterServer=` | Joining Message | -NoticeEnterServer=" has joined the Server" |
| `-NoticeLeaveServer=` | Leaving Message | -NoticeLeaveServer=" has left the Server" |
| `-SaveGameIntervalMinute=` | Server Save Intervall in Minutes | -SaveGameIntervalMinute=10 |

> [!NOTE]
>For more Settings use this Spreadsheet: https://docs.google.com/spreadsheets/d/1xTy0iQzI6utIqVBSQ4IIOjIyrAeDzLyYpGUVKbjwudY/edit?usp=sharing

You can find all other Server Settings in the "PrivateServerTool" and try around for yourself but it seems that not all Settings work or take affect.

# Other WinGSM Plugins:
| Icon | Game Name | Link | Version |
| --- | --- | --- | --- |
| <img src="https://i.imgur.com/LI1uPIJ.png" width="100" height="100"> | Myth of Empires Dedicated Server | [GitHub Link](https://github.com/Sarpendon/WindowsGSM.MythofEmpires) | 1.9 |
| <img src="https://i.imgur.com/25x4Ohs.png" width="100" height="100"> | Valheim Dedicated Server | [GitHub Link](https://github.com/Sarpendon/WindowsGSM.Valheim) | 1.0 |
