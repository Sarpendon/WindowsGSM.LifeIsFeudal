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


# The Game:
- 🕹️ **Steam Site:** https://store.steampowered.com/app/1371580/Myth_of_Empires/
- 📁 **Homepage:** https://www.mythofempires.com/

# Requirements:
- 🖥️ **WindowsGSM** >= 1.21.0
- 📟 The **RAM usage is currently high**, approximately around 10-12 GB, with no active players. Please prepare accordingly.

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
