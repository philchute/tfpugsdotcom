# Admin Guide

This guide is for TFpugs admins. It covers the information needed to manage the discord server, bot, website, and game servers.

---

## Table of Contents

* [Ongoing issues / TODOs](#ongoing-issues--todos)
  * [Short Term](#short-term)
  * [Medium Term](#medium-term)
  * [Long Term](#long-term)
* [FTP connect to servers](#ftp-connect-to-servers)
  * [FileZilla Site Manager Setup](#filezilla-site-manager-setup)
  * [Connected to the remote server](#connected-to-the-remote-server)
  * [To go to another server](#to-go-to-another-server)
* [SSH connect to servers](#ssh-connect-to-servers)
  * [Putty Configuration](#putty-configuration)
  * [Logged on to the game server via SSH](#logged-on-to-the-game-server-via-ssh)
  * [Linux navigation](#linux-navigation)
  * [Server Configuration](#server-configuration)
* [Bot Server](#bot-server)
  * [Attached to tmux session](#attached-to-tmux-session)
  * [TFCELO and NFOstats bots (old bot)](#tfcelo-and-nfostats-bots-old-bot)
  * [TFCELO_rewrite bot (new bot)](#tfcelo_rewrite-bot-new-bot)
  * [MySQL server (databases)](#mysql-server-databases)
* [Website Info](#website-info)
  * [Websites running on bot](#websites-running-on-bot)
  * [Website Info](#website-info-1)
* [Maps](#maps)
  * [League Map Pools](#league-map-pools)
  * [Pug Map Pools](#pug-map-pools)
* [Discord Administration](#discord-administration)
  * [Discord Ban](#discord-ban)
  * [Discord Kick](#discord-kick)
  * [Hellban](#hellban)
  * [Mute](#mute)
  * [ForcePTT](#forceptt)
  * [Dunce](#dunce)
  * [Remove all queues](#remove-all-queues)
  * [Give Gems](#give-gems)
  * [Give XP](#give-xp)
  * [Migrate Roles](#migrate-roles)
  * [Process Log](#process-log)
  * [Test Log](#test-log)
  * [Events Cog](#events-cog)
  * [Leagues Cog](#leagues-cog)
* [Discord Server Boost info](#discord-server-boost-info)
* [Game Servers](#game-servers)
  * [Metamod](#metamod)
  * [Metamod plugins in use](#metamod-plugins-in-use)
  * [AMXmodX](#amxmodx)
  * [Base AMXmodX plugins in use](#base-amxmodx-plugins-in-use)
  * [Custom AMXmodX plugins in use](#custom-amxmodx-plugins-in-use)

---

## Ongoing issues / TODOs

These are things we're working on that you may be able to help with.

### Short Term

Switch to systemd restarts for all bots, and update user guide.
Test game selection and role selection for new bot.  
Test pug start and stop for new bot.  

### Medium Term

* Need 1v1 match config
* More 1v1 servers
* Match config?
* Example player config. Simple and detailed? 
* Contact skill server owners for partnership. We want their data without having to make new skills servers and causing splits.
  * squishies conc server - 1 person? no discord? custom plugins?
  * climb server
  * daylight's skill server
  * 420 pug server - 1 person? no discord? custom plugins

### Long Term

* Fix issue with inconsistent push entities to allow updating to 25th anniversary update
  * Currently we are using "steam_legacy" branch, but this will not receive further updates from Valve.
  * Lets check with the CS community and other goldsrc games to see what solutions they have found.


## FTP connect to servers

Use FTP (File Transfer Protocol) to upload maps, configs, and other files to the servers.  
* File transfer to remote servers via FTP is possible to do in Windows Explorer, but use of the FileZilla application for FTP transfers is recommended.  
* FileZilla is a small utility application for conveniently doing FTP file transfers. 
* Download at <a href="https://filezilla-project.org/download.php">https://filezilla-project.org/download.php</a>

### FileZilla Site Manager Setup

Go to 'File' 'Site Manager', or press the Site Manager button in the top left  
<a href="/images/tfcguide/filezilla-site-manager.png" data-lightbox="tfc-guide" data-title="FileZilla Site Manager"><img src="/images/tfcguide/filezilla-site-manager.png" alt="FileZilla Site Manager" width="100" /></a>  
Press 'New Site' and enter a name for the server location
- Protocol: SFTP - SSH File Transfer Protocol  
- Host: Enter the IP address from the IP List above  
- Port: Can be left blank, but the default is 22  
- Logon Type: Normal  
- User: Enter the username from the Username List above  
- Password: Enter the password from the Username List above  
<a href="/images/tfcguide/filezilla-connection-settings.png" data-lightbox="tfc-guide" data-title="FileZilla Connection Settings"><img src="/images/tfcguide/filezilla-connection-settings.png" alt="FileZilla Connection Settings" width="500" /></a>  
Press 'Connect' to connect to the remote server.  
<a href="/images/tfcguide/filezilla-connecting.png" data-lightbox="tfc-guide" data-title="FileZilla Connecting"><img src="/images/tfcguide/filezilla-connecting.png" alt="FileZilla Connecting" width="450" /></a>  

### Connected to the remote server

The left pane shows your local computer.  
Your steam might be *C:\Program Files (x86)\Steam\steamapps\common\Half-Life\tfc\maps*

The right pane shows the remote server.  
Maps are located in */home/tfcserver/serverfiles/tfc/maps*  
<a href="/images/tfcguide/filezilla-main.png" data-lightbox="tfc-guide" data-title="FileZilla Main Window"><img src="/images/tfcguide/filezilla-main.png" alt="FileZilla Main Window" width="1000" /></a>  
* Double-click a file in the left pane (your computer) to transfer it to the remote server (right side)
* Double-click a file in the right pane (remote server) to transfer it to your local computer (left side)
* Use the right-click menu to open a file. Remember that double-click will transfer the file.

To go to another server:  
* Reopen Site Manager from the 'File' menu or the 'Site Manager' button below it. 
* Choose your next server, it will prompt you if you want to open the new server in a new tab
* Repeat your steps on that server

> **Always be mindful of the direction of transfer, what server you are on, what directories you're in, and what you're over-writing.**

## SSH connect to servers

Use SSH to connect to the Linux server to view status, remote console, or force a restart.
Secure Shell (SSH) is possible to do in the default windows terminal application but use of PuTTY for SSH connections is highly recommended. 

PuTTY is a small Windows utility application for connecting to remote servers via SSH.  
* Download PuTTY at <a href="https://www.putty.org/">https://www.putty.org/</a>   
* Link for latest download: <a href="https://www.chiark.greenend.org.uk/~sgtatham/putty/latest.html">https://www.chiark.greenend.org.uk/~sgtatham/putty/latest.html</a>

### Putty Configuration
<a href="/images/tfcguide/putty-configuration.png" data-lightbox="tfc-guide" data-title="Putty Configuration"><img src="/images/tfcguide/putty-configuration.png" alt="Putty Configuration" width="300" /></a>  
* Host Name: Enter the IP address from the Server List above
* Port: Default port is 22
* Connection Type: SSH
* Saved Sessions: Enter a name for this server location to be saved as
* Press 'Save' and it will appear in your list of Saved Sessions. 

Now you are able to simply double click the name of the server and it will connect you. 
<a href="/images/tfcguide/putty-login.png" data-lightbox="tfc-guide" data-title="Putty Login"><img src="/images/tfcguide/putty-login.png" alt="Putty Login" width="300" /></a>  
Enter the username and password from the pinned messages in discord

### Logged on to the game server via SSH

you will see a command prompt showing the username  
<a href="/images/tfcguide/putty-prompt.png" data-lightbox="tfc-guide" data-title="Putty Prompt"><img src="/images/tfcguide/putty-prompt.png" alt="Putty Prompt" width="250" /></a>

> **Note: we are not able to use tmux while logged in as the game server because our game server manager linuxGSM already uses it internally.**

Type `./tfcserver` to see a list of game server manager commands 
<a href="/images/tfcguide/linuxgsm-menu.png" data-lightbox="tfc-guide" data-title="LinuxGSM Menu"><img src="/images/tfcguide/linuxgsm-menu.png" alt="LinuxGSM Menu" width="600" /></a>  
Key commands for usage:  
`./tfcserver details` this will display information about the server. *(remember to expand your tmux window)*  
`./tfcserver monitor` this will ping the server and restart it if there is no response. the server does this automatically every 5 minutes (not actually implemented as of 6/23/2025)  
`./tfcserver stop` forces the server to stop  
`./tfcserver start` forces the server to start  
`./tfcserver restart` forces the server to stop then start
<a href="/images/tfcguide/linuxgsm-restart.png" data-lightbox="tfc-guide" data-title="LinuxGSM Restart"><img src="/images/tfcguide/linuxgsm-restart.png" alt="LinuxGSM Restart" width="800" /></a>  

`./tfcserver console`	enter the server console remotely  
<a href="/images/tfcguide/linuxgsm-console.png" data-lightbox="tfc-guide" data-title="LinuxGSM Console"><img src="/images/tfcguide/linuxgsm-console.png" alt="LinuxGSM Console" width="400" /></a>  
This uses the internal server manager tmux session, so you must leave this screen by pressing `CTRL+B` then `D` to detach from tmux to return to your command prompt. linuxGSM warns you of this when you go to the console, so press enter to continue. Now we are in the game server console. note the green foot bar showing we are in a tmux session here
<a href="/images/tfcguide/linuxgsm-tmux.png" data-lightbox="tfc-guide" data-title="LinuxGSM Tmux"><img src="/images/tfcguide/linuxgsm-tmux.png" alt="LinuxGSM Tmux" width="800" /></a>  
Enter game server console commands here. such as `sv_map mapName`
Press `CTRL+B` then `D` to detach and return to the previous screen (or just close putty)

### Linux navigation

Those server commands must be run from the root directory, but the tfc files are found in the directory /serverfiles/tfc
Remember linux directory slashes are backwards from windows.
you can navigate here with the command  `cd serverfiles/tfc`
or you can preface the file name with that path when opening vi 
Remember there is tab autocomplete for these commands
To move up a directory use the command `cd ..`
To scroll up in console output (outside tmux) use `shift + pgup / pgdn`

To see the amount of disk space remaining use the command `df`
<a href="/images/tfcguide/linux-df.png" data-lightbox="tfc-guide" data-title="Linux Disk Space"><img src="/images/tfcguide/linux-df.png" alt="Linux Disk Space" width="600" /></a>  

To see the processing running on task manager use the command `htop`
You can kill selected processes here by highlighting it and pressing `F9`
You can change the sorting by pressing `F6`
You can quit by pressing `Q`
This should not actually be necessary on the new tmux setup.

### Server Configuration

* start parameters are located in `lgsm/config-lgsm/tfcserver/tfcserver.cfg`
* server config is located at `serverfiles/tfc/tfcserver.cfg`

If you are comfortable with linux text editing you can open them here within putty. 
* vim is the best text editor, but learning it is beyond the scope of this document
  * Open with a command like `vi serverfiles/tfc/tfcserver.cfg`
  * Enter insert mode to type text by pressing `i`
  * Exit insert mode / return to command mode by pressing `ESC`
  * Write and Quit with the command `:wq`
  * Force quit without saving with the command `:q!`
* It's possible to use a simpler editor like nano, which is like the notepad of linux
  * Open with a command like `nano serverfiles/tfc/tfcserver.cfg`
  * Commands are shown at the bottom, `CTRL+X` to Exit
  * Use `CTRL+R` or `CTRL+O` to Read or Output (open and save)
* Otherwise, you can use FTP to download the file, edit it in windows, then upload it back to the game server. 
  * Line endings shouldn't matter in this case but generally watch your line endings when doing this process. (Windows by default uses CRLF line endings but linux typically uses LF line endings, and sometimes needs those, so generally use LF when making files for linux)

## Bot Server

Bot are now ran by systemd services.

Process to move a bot to systemd:
1. Stop the current process running in tmux.
2. Move the new .service file to /etc/systemd/system/.
3. Run sudo systemctl daemon-reload to make systemd aware of the new file.
4. Run sudo systemctl start <servicename>.service to start the service.
5. Run sudo systemctl status <servicename>.service to check that it started correctly.
6. Run sudo systemctl enable <servicename>.service to make it start on boot.
* If the service is already running, you can restart it with `sudo systemctl restart <servicename>.service`
* You can also use `sudo systemctl stop <servicename>.service` to stop the service.
* You can also use `sudo systemctl disable <servicename>.service` to disable the service from starting on boot.
* You can also use `sudo systemctl enable <servicename>.service` to enable the service to start on boot.

### View Logs for a Specific Service in Real-Time:
This is the equivalent of tail -f my_log_file.log. It's the best command for live debugging.

`journalctl -u newbot.service -f`
* -u: Specifies the unit (service) you want to see logs for.
* -f: Follows the log, showing new entries as they arrive.

### View the Last N Lines of a Service's Log:
This is useful for quickly seeing the most recent activity without getting a huge wall of text.

`journalctl -u newbot.service -n 100 --no-pager`
* -n: Shows the specified number of lines.
* --no-pager: Prints the output directly to your console instead of opening it in an interactive pager like less.

### View All Errors since the last reboot

`journalctl -p err -b`
* -p: Specifies the error level to view.
* -b: Shows logs since the last reboot.

### Services List

* newbot.service
  * python virtual environment for new bot
* oldbot.service
  * python virtual environment for old bot
  * previously ran in tmux via `source ~/virtualpy/bin/activate` and `sh run.sh`
* nfostats.service
  * python part of the old bot
  * previously ran in tmux via `sh nfostats.sh`
* interlinked.service
  * javascript node for FF pub bot
  * previously ran in tmux via `node start_bot.js`
* philchutedc.service
  * dotnet dll for <a href="http://www.philchute.com">http://www.philchute.com</a>
  * previously ran in tmux via `dotnet run --project ASP-site.csproj --configuration Release --no-build`
  * `dotnet build --configuration Release` to build the latest updates
* tfpugsdotcom.service
  * dotnet dll for <a href="http://www.tfpugs.com">http://www.tfpugs.com</a>
  * previously ran in tmux via `dotnet bin/Release/net9.0/tfpugsdotcom.dll --urls "http://0.0.0.0:5176"`
  * `dotnet build --configuration Release` to build the latest updates

### Obsolete tmux stuff:

Connect to the bot server via SSH and tmux to view, manage, or restart the discord bots.  
Use the username and password from the pinned message in discord.

<a href="/images/tfcguide/botserver-login.png" data-lightbox="tfc-guide" data-title="Bot Server Login"><img src="/images/tfcguide/botserver-login.png" alt="Bot Server Login" width="800" /></a>  

DO THIS!!!—> <a href="/images/tfcguide/tmux-attach.png" data-lightbox="tfc-guide" data-title="Tmux Attach"><img src="/images/tfcguide/tmux-attach.png" alt="Tmux Attach" width="300" /></a>  
You are now connected to the bot server, but everything is running inside of a tmux session. 
You must connect to the tmux session to interact with the bots. Enter the command 
tmux a -t botserver	(you should just need to press the up arrow on your keyboard to reach this command as it will always have been the last entered command here)  
THIS!!!—> <a href="/images/tfcguide/tmux-attach.png" data-lightbox="tfc-guide" data-title="Tmux Attach"><img src="/images/tfcguide/tmux-attach.png" alt="Tmux Attach" width="300" /></a>

### Attached to tmux session

When you are in a tmux session you will see a green status bar at the bottom of the screen,
something like this:
<a href="/images/tfcguide/tmux-status.png" data-lightbox="tfc-guide" data-title="Tmux Status"><img src="/images/tfcguide/tmux-status.png" alt="Tmux Status" width="800" /></a>  

There are several windows open in this tmux session, labeled 0 through 5 in this screenshot.
The asterisk * shows which of the windows is currently displayed. 
Press `CTRL+B` then a number key (`0-9`) to look at that window.
Alternatively you can press `CTRL+B`then `N` or `CTRL+B` then `P` to cycle to the next and previous windows. 
Most of these windows will just show that bot in process. 
If there is some action needed, switch to that bot's window and complete the action (restarting the bot, etc). Note there may be some changes in the way bots are running, for example the NFOstats bot no longer runs silently in the background, it runs in its own tmux window. So no need to search for it and kill the process, just go look at it in that window and start it again there if it is stuck. However the async changes there should mean this isn't going to get stuck anyways. 
To create a new window press `CTRL+B` then `C`
Windows can be closed with `CTRL+B` then `&`, but be careful with these if you are not used to tmux. No problem to just leave it open there. I would much rather log on to the server and see some extra bash windows open than have had something running accidentally cut off. 

To scroll up in application output, press `CTRL+B` then `[` (then `ESC` to return to live feed)
To renumber the windows type `CTRL+B` then `:movew -r`  
Full tmux cheatsheet at <a href="https://tmuxcheatsheet.com/">https://tmuxcheatsheet.com/</a>  
You can just close the entire PuTTY window when you are done and tmux will keep the session running along with the bots and everything in it.  
This is also helpful for if you get accidentally disconnected, your work will still be open in the tmux session when you log on to the server again.  

Two people can log into the server at the same time and share the screen within the tmux session. If one person resizes their window smaller than the other, the other will see a field of . characters
<a href="/images/tfcguide/tmux-resize.png" data-lightbox="tfc-guide" data-title="Tmux Resize"><img src="/images/tfcguide/tmux-resize.png" alt="Tmux Resize" width="800" /></a>  

To stop a process like a bot, press `CTRL+C` to interrupt the application
To start or restart a bot, typically you just need to press up to see the last command 
<a href="/images/tfcguide/bot-cancel.png" data-lightbox="tfc-guide" data-title="Bot Cancel"><img src="/images/tfcguide/bot-cancel.png" alt="Bot Cancel" width="400" /></a>  
<a href="/images/tfcguide/bot-restart.png" data-lightbox="tfc-guide" data-title="Bot Restart"><img src="/images/tfcguide/bot-restart.png" alt="Bot Restart" width="400" /></a>  

### TFCELO and NFOstats bots (old bot)
Under the current Debian/tmux setup you must enter a python virtual environment to run the bots.
Use the command `source ~/virtualpy/bin/activate` to enter the virtual environment.
To exit the virtual environment, use the command `deactivate`

<a href="https://github.com/12souza/TFCELO">https://github.com/12souza/TFCELO</a> (base repo)
<a href="https://github.com/philchute/TFCELO">https://github.com/philchute/TFCELO</a> (recent changes)
Previously these bots had run silently in the background but now they each run on a seperate tmux screen. View the screen labeled for that bot to see the current status and output log.
TFCELO handles the match making. Starts with `run.sh`  
The window will show live matchmaking info as people add and games form:  
<a href="/images/tfcguide/bot-tfcelo.png" data-lightbox="tfc-guide" data-title="Bot TFCELO"><img src="/images/tfcguide/bot-tfcelo.png" alt="Bot TFCELO" width="400" /></a>  
NFOstats handles the in-game stats. Start script is at `TFCELO/nfostats.sh`  
`sh nfostats.sh`  
<a href="/images/tfcguide/bot-nfostats.png" data-lightbox="tfc-guide" data-title="Bot NFOstats"><img src="/images/tfcguide/bot-nfostats.png" alt="Bot NFOstats" width="400" /></a>  
The window will show live game info while people are playing:  
<a href="/images/tfcguide/bot-nfostats-feed.png" data-lightbox="tfc-guide" data-title="Bot NFOstats Feed"><img src="/images/tfcguide/bot-nfostats-feed.png" alt="Bot NFOstats Feed" width="400" /></a>  

Be sure you are in the tmux window where it crashed if you are starting a new one, or we could end up with multiple instances running. You should just need to press the up arrow to do the previous command of starting the bot. 



### TFCELO_rewrite bot (new bot)

Configuration occurs in three sequential layers:
1. Environment variables are loaded. Currently we are not using environment variables
2. Variables from secrets.json are loaded. This includes bot tokens and server passwords. All of these are in one file for convenience, and this file is ignored by git and coding AIs, who instead read secrets_template.json which is the same file with dummy values.
3. Variables from config.json are loaded. This is the final place the application looks for variables to be defined. These are all the values which can be public, which include discord room IDs, role IDs, game configuration, timer settings, and other configurations

### MySQL server (databases)

MySQL server is holding the stats also on this server. access directly with the botserver account in tmux or remotely using the login details from the pinned message in discord.

## Website Info

Standard discord redirect link is <a href="http://www.tfpugs.com/discord">http://www.tfpugs.com/discord</a>

* <a href="http://www.tfpugs.com">http://www.tfpugs.com</a>  
  * Phil registered the domain on namecheap.com 4/13/25 with a yearly renewal
  * source code of this site at <a href="https://github.com/philchute/tfpugsdotcom">https://github.com/philchute/tfpugsdotcom</a>

* <a href="http://www.philchute.com">http://www.philchute.com</a>  
  * Phil's site for other projects. source code at <a href="https://github.com/philchute/ASP-site">https://github.com/philchute/ASP-site</a>

* <a href="http://www.tfpugs.online">http://www.tfpugs.online</a>  
  * Ed registered this domain, it will expire at some point
  * domain resolves via serverless shuttle app <a href="https://tfpugs-3gc5.shuttle.app/">https://tfpugs-3gc5.shuttle.app/</a>
  * source code at <a href="https://github.com/ededdneddyfan/tfpugs_web_app">https://github.com/ededdneddyfan/tfpugs_web_app</a>

* <a href="https://www.tfcstats.com">https://www.tfcstats.com</a>  
  * Newest stats site, actively updated
  * Nest runs this site for the european server which also accepts NA games via API

## Maps

Maps are now held on the maps table of the new bot. Runners can modify map info using bot commands.  
<a href="https://www.youtube.com/watch?v=CcismZ0uZ1A">How to set up a fast DL server on github</a>


### League Map Pools

Bot selects 3 maps from the three pools to be the maps for the week. 
The map pools are tagged with "league_0", "league_1", and "league_2" which correspond to their mirv counts to give variety in each week's pool.

### Pug Map Pools

Bot selects 2 maps from the classic pool and 2 maps from the secondary pool.
Classic maps are tagged "na_primary" and secondary maps are tagged "na_secondary". 


## Discord Administration

Discord administration is the process of dealing with undesirable behavior.  
Discord Admins are responsible for maintaining a safe, secure, enjoyable, and smooth gaming experience for all users by dealing with adverse behavior. To that end, discord admins have access to the following bot commands:

### Discord Ban 

Banning is done using the Discord server moderation tools. This prevents the discord account from rejoining the server. This is typically only done for hacked or compromised accounts. Hacked accounts post links for "Free $50 gift cards" or something like that which lead to phishing sites which compromise the account and that process repeats. No discussion or consensus is needed, just instantly ban the account, even if it is someone we know. Accounts can be unbanned in the discord server settings if it's no longer applicable, or that person can join with a new account and we can even update the database to their new discord ID, so don't hesitate to ban a hacked account.  
Remember that it isn't really possible to effectively ban someone from a free internet service. Punishments, even for very offensive conduct, will typically be temporary or permanently being on another administrative status. This is the Facebook school of moderation. If someone was banned from Facebook for an offensive post, that person would be back on with a new account within an hour. If someone instead got muted and couldn't post again for a month, that person would actually just go on to not make those offensive posts again.

### Discord Kick 

Server kicking is done using the Discord moderation tools. This removes someone from the discord server but does not prevent them from rejoining. This is not typically an effective administration tool, because they will typically just instantly rejoin the server and tell you what an asshole you are for kicking them, but it can be a quick way to get someone's attention or deal with some situations. Discord server settings also has a 'Report Raid' feature but I'm not sure what all that does.

### Hellban

Hellban is done using the bot command `/hellban @username (duration) (reason)`  
This prevents the user from chatting but they can still use commands, meaning they can still join pickups (and they can still spam those messages).  
To un-hellban someone use the bot command `/unhellban @username`  
Examples:  
`/hellban @foreignguy 3d repeated non-english chat messages`  
`/hellban @crazyguy 7d repeated off-topic rants`  
`/hellban @nonsenseguy this guy shouldn't chat but he still plays`  

### Mute

Mute is done using the bot command `/mute @username (duration) (reason)`  
This prevents the user from sending attachments or links. This can be an effective punishment for posting an inappropriate link or attachment, as well as a permanent or temporary way of dealing with problematic people who tend to spam links or attachments.   
To unmute someone use the command `/unmute @username`  
Examples:  
`/mute @someidiot 3d spamming links in pug chat`  
`/mute @totalidiot 180d repeated posting gore gifs`  
`/mute @completeidiot we can never let this person post links`

### ForcePTT

ForcePTT is done with the bot command `/forcePTT @username (duration) (reason)`  
This prevents the user from using a voice activated mic in any voice room. This uses the ForcePTT discord role and the discord moderation features. This can be an effective way to deal with people with bad microphones using voice activated mics which can pick up game sounds or other background noise, or for people who are too chatty during the game, this forces them to take the extra step of pushing a button to talk.  
To allow someone to use Push-to-talk again remove the ForcePTT discord role from them.  
Examples:  
`/forcePTT @loudguy 12h are you on an airplane?`  
`/forcePTT @alsoloud 30d you need a new mic`  
`/forcePTT @loudidiot repeated chatting during match`  

### Dunce

Dunce mode is done with the bot command `/dunce @username (duration) (reason)`  
This prevents a user's achievements and rank from displaying. This is the lightest punishment in the server and can be used to enforce minor rule infractions or to remind players to maintain good behavior, even if they aren't breaking other rules.  
Examples:  
`/dunce @dumbguy 1d taking the flag out water`  
`/dunce @badplayer 5d repeated front door D`  
`/dunce @tryhard 30d playing for stats`

### Remove all queues

Players can be manually removed from all game queues with the bot command 
`/removeallqueues @username (reason)`
This can be used to help players who are wanting or needing to be removed from all the queues, or this can be used as an administrative tool if someone needs to be removed from the queues due to being afk, overly drunk, overly rude, etc.  
Examples:  
`/removeallqueues @someguy`  
`/removeallqueues @afkguy went afk`

### Give Gems

Admins have a new command `/givegems` which can be used to give gems to a player. Use this to correct any perceived missed gems from matches or any other reason to keep players happy and motivated.  
Example usage:  
`/givegems @sadplayer 1000`  
`/givegems @robbedplayer 250`  
`/givegems @inflatedplayer -300`

### Give XP

Similarly admins can give xp to players. Use this to grant any xp players are perceiving as missed, or for whatever other reasons are needed to keep players happy and motivated.  
Example usage:  
`/givexp @unhappyplayer 1000`  
`/givexp @toolucky -1000`

### Migrate Roles

Not a normal process `/migrateroles sourceroleID destinationroleID (isDryRun) defaults true`  
* It defers the response ephemerally.   
* Checks that source and destination roles are not the same.  
* Retrieves all members from the guild who have the source_role.  
* If no members are in the source role, it informs the admin.  
* It iterates through the members from the source role:  
* If a member does not already have the destination_role, they are considered a target.
* If dry_run is False, it attempts to add the destination_role to the target member.
* It keeps track of members who would get the role (dry run), who actually got the role (execution), and who already had the destination role.
* Error handling is included for discord.Forbidden (bot lacks permissions) and discord.HTTPException during role addition.

### Process Log

`/processlog logfilename gameid [matchid]`

### Test Log 

`/testlog logfilename gameid [matchid]`

### Events Cog

The events cog creates events (tournaments or league seasons) for teams to join. 
Use the event-template.json to fill in information about the event.
Use filezilla to place the json file in the bot's /src/data/imports/ directory.
Use the command /importevent to import the event into the bot.
At the scheduled registration start time the bot will post an event which allows team captains to join.
At the scheduled registration close time the bot will post the results of the registration. 
At the scheduled event start time the bot will begin the tournament or league season. 

### Leagues Cog

Todo add info

## Discord Server Boost info
<a href='https://support.discord.com/hc/en-us/articles/360028038352-Server-Boosting-FAQ'>https://support.discord.com/hc/en-us/articles/360028038352-Server-Boosting-FAQ</a>
* Level 0 (0 boosts):
  * 50 emotes
* Level 1 (2 boosts):
  * 100 emotes
  * 128 kbps audio
  * invite background
  * 720p 60fps
* Level 2 (7 boosts):
  * 150 emotes
  * 256 kbps audio
  * custom role icons
  * server banner
* Level 3 (14 boosts):
  * 250 emotes
  * 384 kbps audio



## Game Servers

Game servers are now held in the 'servers' SQL table. We could write a bot function to !addserver or !editserver but that doesn't seem worth it right now. Servers are added directly into MYSQL when needed.

### Metamod

View the status of Metamod plugs by typing in the game server console `meta list`

### Metamod plugins in use:  
* Rechecker - used for file validation, reHLDS project
* AMXModX

### AMXmodX

View the status of AMXmodX plugins by typing in the game server console `amxx plugins`
To make changes, download the compiler / AMXStudio <a href="https://www.amxmodx.org/">https://www.amxmodx.org/</a>
There's a VScode extension available to run that compiler from there.
Compile the .sma source file to .amxx file and then place in */home/tfcserver/serverfiles/tfc/addons/amxmodx/plugins*

### Base AMXmodX plugins in use

| Plugin | Description |
|--------|-------------|
| admin.amxx | Admin Base |
| adminchat.amxx | Admin Chat |
| admincmd.amxx | Admin Commands |
| adminhelp.amxx | Admin Help |
| adminslots.amxx | Slots Reservation |
| adminvote.amxx | Admin Votes |
| cmdmenu.amxx | Commands Menu |
| mapsmenu.amxx | Maps Menu |
| menufront.amxx | Menus Front-End |
| multilingual.amxx | Multi-Lingual System |
| nextmap.amxx | NextMap |
| pausecfg.amxx | Pause Plugins |
| plmenu.amxx | Players Menu |
| pluginmenu.amxx | Plugin Menu |
| statscfg.amxx | Stats Configuration |
| timeleft.amxx | TimeLeft |

### Custom AMXmodX plugins in use

 * ~~airshot.amxx~~ 
 * airshot2.amxx
    * flavor plugin for airshots
    * watch wrote the original airshot.sma in 2006 <a href="https://forums.alliedmods.net/showthread.php?t=24312">https://forums.alliedmods.net/showthread.php?t=24312</a>
    * se7en wrote a 2.0 update and the current version 2.1 was just installed 4/22/25 <a href="https://forums.alliedmods.net/showthread.php?t=343974">https://forums.alliedmods.net/showthread.php?t=343974</a>
 * bs_dmglog.amxx 
    * optional functionality, includes damage in log files
    * beees wrote? is source code available? 
 * BugFixesTFC.amxx 
    * important one that addresses lots of the vanilla tfc bugs
    * hlstriker wrote the original in 2017 <a href="https://forums.alliedmods.net/showthread.php?t=297514">https://forums.alliedmods.net/showthread.php?t=297514</a>
    * se7en wrote the current version 1.4 <a href="https://forums.alliedmods.net/showthread.php?t=333746">https://forums.alliedmods.net/showthread.php?t=333746</a>
 * ExploitBlocks.amxx
    * rules enforcement (blocks chop hop)
    * <a href="https://github.com/12souza/exploitblocks">https://github.com/12souza/exploitblocks</a>
 * flyfastspect.amxx
    * Spectator Helper 1.3 by azul (quality of life improvement for spectators)
    * would be nice if we could have source code flyfastspect.sma 
 * grenlimits.amxx
    * Grenade Stock Limiter 1.0 by azul (lets us customize grenade limits globally (when that makes more sense than per-map adjustments))
    * would be nice if we could have source code grenlimits.sma
 * hltv_autorecord.amxx 
    * Plug-in to automatically record HLTV demos when players are on the server but not when the server is empty
    * Dr. Aft wrote the current version 1.7 and source code is available <a href="https://forums.alliedmods.net/showthread.php?p=993138">https://forums.alliedmods.net/showthread.php?p=993138</a>
 * restartgame.amxx 
    * Plug-in for changing maps in chat, also sets server host name
    * Coach Suez wrote this originally and we have the source code to make changes
    * <a href="https://github.com/12souza/pugmanager">https://github.com/12souza/pugmanager</a> (base repo)
    * <a href="https://github.com/philchute/pugmanager">https://github.com/philchute/pugmanager</a> (recent changes)
 * daylightsplugin.amxx *(IN TESTING, not on match servers yet)*
    * server behavior plugin to revert push entity behavior to pre-25th anniversary levels
    * <a href="https://tfc-database.com/index.php?news=3">https://tfc-database.com/index.php?news=3</a>
    * DayLight wrote this plugin, he is currently developing / updating it

Race plugin if we need it? <a href="https://forums.alliedmods.net/showthread.php?p=213422">https://forums.alliedmods.net/showthread.php?p=213422</a>

Possible issue we are not aware of? <a href="https://github.com/Garey27/hitbox_fixer">https://github.com/Garey27/hitbox_fixer</a>