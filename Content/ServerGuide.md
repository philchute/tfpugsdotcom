# Server Guide


This guide is for setting up a new TFC server on a rented Debian server using LinuxGSM. This is how we make our servers at TFpugs. In making this public I hope to further the community to make more servers easier to set up. Please let me (Phil) know if you have any questions or suggestions about this guide.

---

## Table of Contents

* [Choosing a server provider](#choosing-a-server-provider)
* [Renting a server](#renting-a-server)
* [Connect to server via SSH](#connect-to-server-via-ssh)
  * [Putty Configuration](#putty-configuration)
  * [Logged on to the game server via SSH](#logged-on-to-the-game-server-via-ssh)
* [Setting up Linux](#setting-up-linux)
  * [Open a second Putty Window](#open-a-second-putty-window)
* [Setting up TFC](#setting-up-tfc)
  * [Installing game server files](#installing-game-server-files)
  * [Switching to legacy branch](#switching-to-legacy-branch)
  * [Edict Index error](#edict-index-error)
  * [Update and validate](#update-and-validate)
  * [ReHLDS](#rehlds)
  * [AmxX Mod](#amxx-mod)
* [Firewall](#firewall)


## Choosing a server provider

Although it is possible to self host a server from your home, I have never found this to be a good option. The networking is never good enough, even when dedicated PC hardware is used. You cannot continually send responses fast enough though your local ISP to maintain a smooth server hosted from home.

Another option is a fully-managed game server, where a company rents you the game server already set-up and ready-to-play, typically charging by the player slot. This may be a good option if you have a common game and no knowledge or experience with server hosting, but typically options are limited and prices are high, which leaves fully-managed game servers as not the best option for most games.  Through a few hours of effort you can host the same game server on a much cheaper Virtual Private Server (VPS) or a pure Dedicated Server (DS), and it's possible to host multiple game servers on the same server instance.

The largest cloud server provider is Amazon Web Services (AWS), but there are many more options, some market towards gamers and some do not.  It's good to look through the server provider's marketing information and see that they are at least aware of game servers and the priorities involved.  Many different providers would work fine here, I've had good experiences lately with <a href="https://www.serverpoint.com">www.serverpoint.com</a> and <a href="https://www.linode.com">www.linode.com</a>, but all the current TFPugs servers are hosted on <a href="https://www.vultr.com">www.vultr.com</a>.  
Generally server providers offer a number of physical locations to choose from, typically corresponding to <a href="https://en.wikipedia.org/wiki/List_of_Internet_exchange_points">internet exchange points</a>. By hosting at these locations with the server provider, you are ensuring your server is physically located and wired more directly into a high-tier internet line. Everyone else will get better ping off of a hosted server than one at your home. 

## Renting a server

Select an appropriate size server configuration. Opinions vary on what is optimal, but generally you do not need a large CPU or very much RAM to run a server for a classic video game. They were designed for systems with only a few hundred MHz CPU and a few hundred MB of RAM. Adding extra CPU and RAM will not imporove performance, so typically you'll be selecting the smallest server configuration available.  
If you have the option you should select <a href="https://en.wikipedia.org/wiki/IPv4_address_exhaustion">IPv4</a> rather than IPv6, for maximum compatibility with old games.
You will have the choice of operating systems. Although Windows seems like a comfortable option, Linux servers will typically be the more stable and supportable server.  
You'll have the choice of <a href="https://en.wikipedia.org/wiki/List_of_Linux_distributions">Linux distributions</a>. The biggest difference to you, aside from some command-line syntax, is the package distribution. Most Linux apps and tools are distributed as packages available for download via command-line, and different Linux distributions handle package distribution differently.  
Some people prefer Ubuntu for something more user-friendly.  
Some people prefer Arch for something more capable for power users.  
I prefer Debian, as it is a good balance between usability and functionality, and something near the root of the distribution tree so will typically have up-to-date packages which you install with commands like `apt get mynewpackage`. There is a lot of documentation available, so AI LLMs are typically able to answer questions about Debian intelligently. This shouldn't make much difference in the end, but this guide is written assuming Debian. Use the latest stable (whole number) version available.  
Deploy your server when you are ready. It will take about 5 minutes to start up.  
Look at the dashboard on your server provider to see the status of your server and copy the IP address down. 

## Connect to server via SSH

Secure Shell (SSH) is possible to do in the default windows terminal application but use of PuTTY for SSH connections is highly recommended.
* PuTTY is a small Windows utility application for connecting to remote servers via SSH.
* Download PuTTY at <a href="https://www.putty.org/">https://www.putty.org/</a>
* Link for latest download: <a href="https://www.chiark.greenend.org.uk/~sgtatham/putty/latest.html">https://www.chiark.greenend.org.uk/~sgtatham/putty/latest.html</a>

### Putty Configuration

<a href="/images/tfcguide/putty-configuration.png" data-lightbox="tfc-guide" data-title="Putty Configuration"><img src="/images/tfcguide/putty-configuration.png" alt="Putty Configuration" width="300" /></a>  
* Host Name: Enter the IP address of the new server
* Port: Default port is 22
* Connection Type: SSH
* Saved Sessions: Enter a name for this server location to be saved as
* Press 'Save' and it will appear in your list of Saved Sessions.

Now you are able to simply double click the name of the server and it will connect you.

<a href="/images/tfcguide/putty-login.png" data-lightbox="tfc-guide" data-title="Putty Login"><img src="/images/tfcguide/putty-login.png" alt="Putty Login" width="300" /></a>  
* Enter the username `root` and the root password copied from the server provider dashboard.
* It's good practice to **not** save this password, as you will not be logging in as root after today.

### Logged on to the game server via SSH

You will see a command prompt showing your username, which will be root at this time.
<a href="/images/tfcguide/putty-prompt.png" data-lightbox="tfc-guide" data-title="Putty Prompt"><img src="/images/tfcguide/putty-prompt.png" alt="Putty Prompt" width="300" /></a>  

## Setting up Linux 

> *To copy paste commands into PuTTy, use `SHIFT+INS` to paste.*

First you will want to update your package list to get the newest packages available since the server provider last made this OS image using the command  
`apt update`

Next you will want to upgrade all your installed packages to the latest versions available with the command  
`apt upgrade`

Next you will want to install some additional packages which will be helpful later, with the command  
`apt install git vim tmux`

Next you will want to create a linux username for your server to use, like tfcserver, with the command  
`adduser tfcserver`

You will need to enter a password, which you should write down. You do not need to enter any of the other fields like contact info.

### Open a second Putty Window

Open a second PuTTy window, connect to the same server, and login as tfcserver.  
As tfcserver, install LinuxGSM using the command  
`curl -Lo linuxgsm.sh https://linuxgsm.sh && chmod +x linuxgsm.sh && bash linuxgsm.sh tfcserver`

## Setting up TFC 

<a href="https://linuxgsm.com/servers/tfcserver/">LinuxGSM TFC Server</a>

### Installing game server files:

As tfcserver, use LinuxGSM as a front-end for steamcmd to install TFC with the command  
`./tfcserver install`

Some dependencies will not install because you are not root.  
Use your original putty window as root to run the same command again to install the missing dependencies  
`../home/tfcserver/tfcserver install` (check syntax)

<a href="https://docs.linuxgsm.com/commands/install">More information about this step here if needed</a>

### Switching to legacy branch:

As tfcserver, open the <a href="https://docs.linuxgsm.com/configuration/linuxgsm-config">LinuxGSM config file</a> for TFC in the text editor nano, or your preferred text editor  
`nano lgsm/config-lgsm/tfcserver/tfcserver.cfg`

Add a new line to the config to specify which "beta" branch to use, in this case, the legacy branch steam_legacy.  
`branch="steam_legacy"`

Exit nano with `CTRL+X` and confirm yes to save changes.

<a href="https://docs.linuxgsm.com/steamcmd/branch">More information about this step here if needed</a>

### Edict Index error

There is an error *FATAL ERROR (shutting down): Bad entity in IndexOfEdict()* which occurs in TFC servers which we can avoid, and now is a good time to open liblist.gam with the command  
`nano serverfiles/tfc/liblist.gam`

Add a new line to the file to increase the entity limit  
`edicts "2048"`

Exit nano with `CTRL+X` and confirm yes to save changes. 

### Update and validate

Update and validate TFC using the LinuxGSM as a front-end for steamcmd  
`./tfcserver update`  
`./tfcserver validate`

### ReHLDS

<a href="https://github.com/rehlds/ReHLDS/releases">ReHLDS</a>

### AmxX Mod

<a href="https://wiki.alliedmods.net/Installing_AMX_Mod_X_Manually">https://wiki.alliedmods.net/Installing_AMX_Mod_X_Manually</a>  
Plugins listed in config and placed in config dir  

## Firewall

ufw?

Server Provider Firewall