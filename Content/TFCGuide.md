# Team Fortress Classic Guide

This guide covers everything needed to play Team Fortress Classic as well as a reference for further information. Please let us know if you have any suggestions or improvements.

---

## Table of Contents
* [Introduction](#introduction)
* [TFC Match Rules](#tfc-match-rules)
  * [1v1 matches](#1v1-matches)
  * [CTF matches](#ctf-matches)
  * [AvD matches](#avd-matches)
  * [League Matches](#league-matches)
  * [Cheating and Exploits](#cheating-and-exploits)
* [Configs](#configs)
  * [Class Configs](#class-configs)
  * [Network Settings](#network-settings)
  * [Vsync Settings](#vsync-settings)
  * [HLTV View Settings](#hltv-view-settings)
* [Custom Visuals](#custom-visuals)
  * [tfc_addon folder](#tfc_addon-folder)
  * [Custom Sprites](#custom-sprites)
  * [Default Files](#default-files)
  * [Custom Crosshairs](#custom-crosshairs)
* [Other Useful Customizations](#other-useful-customizations)
  * [Lagless Grenade Timers](#lagless-grenade-timers)
  * [Steam Game Recording](#steam-game-recording)
* [Gameplay Guides](#gameplay-guides)

---
<!--
<div class="ratio ratio-16x9">
  <iframe src="https://www.youtube.com/embed/dQw4w9WgXcQ" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</div>

file: `[scout_flag_run_example.dem](path/to/your/local/demos/scout_flag_run_example.dem)`.

-->

## Introduction

[Team Fortress Classic](https://store.steampowered.com/app/20/) was originally released in 2000 by Valve Software as a first-party mod for their flagship title Half-Life when they hired the development team of the original QuakeWorld Team Fortress mod to recreate that game in the goldsrc engine. Checkout [SteamDB](https://steamdb.info/app/20/) to view price history and current player count. Or [this page](https://steamdb.info/sales/history/) to view when the next sale is; TFC now sells for $4.99 but goes on sale for 90% off that price.

We play TFC in a competitive format, where players are largely expected to already know how to play. This creates a barrier for new players which we are looking to break down. For now, playing the game will largely be outside of the scope of this guide. [nuki's TFC Learning Center](https://sites.google.com/view/nlc-tfc) is the most comprehensive resource for learning the game, and is a great place to start. 

TFC maintains several active public servers in various formats, which can be fun for a very casual experience, but our servers are password protected private servers used for 4v4 competitive matches, or pickup games in that same 4v4 format. Check out our [Servers page](/Servers) for a list of available servers. Pickup matches, tournaments, and leagues are all formed on the discord server. Please familiarize yourself with the [Server Rules](/PlayerInfo#server-rules) before joining, as well as the Match Rules from the next section.

## TFC Match Rules

Please ask your game runner or an admin if you have any questions about what is allowed in matches.

### 1v1 matches:
* Player 1 plays blue, Player 2 plays red
* Soldiers are the primary dueling class in TFC
* Demoman is also possible
* Any other class pick must be agreed upon by your opponent 

### CTF matches: 
* Team 1 will play offense first, offense is always blue
  * Medics are the primary offensive class in TFC
  * Scouts, Spies, and Soldiers are potentially viable on some maps
  * Engineer or Demoman are only used in certain specific situations
  * No Sniper, Pyro, or HW on offense
* Team 2 will play defense first, defense is always red
  * Defense are allowed any number of soldiers and a certain number of 'MIRV classes', depending on the map
  * 'MIRV classes' are HW and Demoman; a second engineer also counts as a MIRV class
  * No Sniper, Pyro, Medic, Scout, or Spy on defense
* Unless the flag is being moved there, you should not be defending where you can see the enemy base. This includes the front door or battlements on some maps, and the yard on nearly all maps. *(This means you **could** play in the front door of destroy or schtop, but not on monkey or phantom)*
* No teleporters in CTF matches

### AvD matches:
* AvD matches are played 5v5 with team 1 as blue first and team 2 as red

### League Matches:
* Any spectators should use HLTV rather than in-game spectator viewing
* Players should record a personal demo using the console command `record filename`. Use the console command `stop` if you need to stop recording one file to record another. Players should retain these demos and provide them if requested from an admin.
* Teams are allowed substitutions between halves or overtime periods. Having an additional player on standby is a good way to insure against a player having a connection or irl issue.
* No mm1 (all-chat) during the match. Conduct yourself professionally. This is our highest form of competition and your behavior and sportsmanship should reflect that. 
* Report scores and upload logs within 24 hours if playing on your own server.
* File a dispute with the admins within 24 hours if you have any issue with the match.
* Opponants should be informed as soon as possible of last-minute roster additions and need to agree to any ringers being used. Adding new players to your roster is a good way to avoid having to use a ringer from another league team.
* Rosters should be considered locked for playoffs.


### Cheating and Exploits

Banned gameplay scripts and exploits include:
* any form of cheating, including any wall-hack, added information, speed-hack, rate manipulation, aimbot, etc.
* bypassing the normal pipe detonation or weapon animation delays (such as quickdet or quickfire)
* force_centerview or cl_pitchspeed commands (such as pipedown or pipeup scripts)
* clear, reduced, sped up, or transparent explosionsprites (see example)
* any pre-match activity which affects the match (moving the flag, deactivating security, leaving detpacks or grenades, etc.)
* taking the flag back in to the water exit trigger on the map ``cranked``
* throwing the flag over the flag room wall on the map ``fry_baked``

Example of a banned clear explosion sprite:
<br/><a href="/images/tfcguide/clear-explosions.png" data-lightbox="tfc-guide" data-title="Example of a banned clear explosion sprite"><img src="/images/tfcguide/clear-explosions.png" alt="Example of a banned clear explosion sprite" width="600" /></a><br/>

## Configs

The goldsrc engine offers an excellent and expandable platform for configuration and scripting.

TODO: add more about autoexec, config.cfg and other general config stuff

### Class configs

Class based configs are enabled with the setting:
```
setinfo "ec" "1"
```
[More information on how it works](https://www.quaddicted.com/webarchive/www.planetfortress.com/tftech/tfcconfig.shtml)

### Network Settings

[Coach Suez's guide to network settings](https://www.youtube.com/watch?v=xXZtqHTllKQ)

### Vsync Settings
The "Lower input latency" option in the 25th anniversary update may be helpful for anyone playing with vsync enabled. Forcing `gl_vsync 1` can add significant latency. A common workaround is to lower your `fps_max` to a value sufficiently below your monitor's refresh rate (for example 140 fps on a 144hz monitor, or 225 on a 270hz monitor).

The "lower input latency" option appears to solve this problem, allowing you to enable the option and let your monitor's refresh rate manage your FPS without noteworthy rendering delays. `gl_vsync 0` will always be lower latency, but some people experience noticeable tearing without vsync.
<br/><a href="/images/tfcguide/steam-mouse-settings.png" data-lightbox="tfc-guide" data-title="Steam Mouse Settings"><img src="/images/tfcguide/steam-mouse-settings.png" alt="Steam Mouse Settings" width="400" /></a><br/>
**Steam Mouse Settings**

Use this version of PresentMon to test for yourself, as newer versions removed the render latency metric: [PresentMon v0.6.2 Release](https://github.com/GameTechDev/PresentMon/releases/tag/ipm-0.6-2)  
<br/><a href="/images/tfcguide/presentmon.png" data-lightbox="tfc-guide" data-title="PresentMon showing the render latency of the game"><img src="/images/tfcguide/presentmon.png" alt="PresentMon showing the render latency of the game" width="800" /></a><br/>
**PresentMon showing the render latency of the game**

### HLTV View Settings
To set a custom FOV in HLTV:
1. Right click TFC -> properties -> launch options and add `-demoedit`
2. Load TFC and open the console -> `viewdemo demoname.dem`
3. Let the demo load for a few seconds.
4. Pause and click `events` -> `add event`
5. Set your desired FOV.


## Custom Visuals
TFC is a highly customizable game and players are encouraged to adjust the game to their liking.

### tfc_addon folder
If you aren't already using the `tfc_addon` folder, now is a great time to start:
*   Create a folder named `tfc_addon` in your Half-Life folder, like this: 
```
\SteamLibrary\steamapps\common\Half-Life\tfc_addon\
```
*   Copy your `sprites` folder from `\Half-Life\tfc\` over to `\Half-Life\tfc_addon\`.
*   Any other custom content you want to keep that might be overwritten by updates should be copied into the `tfc_addon` folder as well.
*   Select the 'enable custom addon content' checkbox in TFC's options menu.

### Default Files
If you ever want access to the original/default files for TFC without having separate custom sprites/models/etc, you can use the Steam console to download the TFC file repo directly:
*   First, open steam command by entering this in a Run window (winkey+r): `steam://open/console`.
*   Then, in the 'Console' tab in Steam, enter this command: 
```
download_depot 20 21 7841127166138118042
```
<br/><a href="/images/tfcguide/steam-console-default-files.png" data-lightbox="tfc-guide" data-title="Steam Console Default Files"><img src="/images/tfcguide/steam-console-default-files.png" alt="Steam Console Default Files" width="800" /></a><br/>
*   It might take a few tries, but you should end up with a new `\steamapps\content\app_20\depot_21` folder under your steam installation folder (not your games library folder). Inside the `depot_21` folder will be a `tfc` folder with all the default files.
*   [Direct Link to zip of default files](https://cdn.discordapp.com/attachments/1000595201557008467/1207365374383755294/default_tfc.zip)


### Custom Crosshairs

TODO: add custom crosshair stuff

### Custom Sprites
[Coach Suez's guide to editing sprites](https://www.youtube.com/watch?v=_JMVugoO7G4)

[TFC Customizations (Weapon Models, Player Models, Rocket Flares, HUDs, Textures, etc.)](https://mrclan.com/avi/files.php)

[Legal Blood Sprites zip](https://cdn.discordapp.com/attachments/1000595201557008467/1340566313109557299/LegalBloodSprites_v4.zip)  
[Legal Explosion Sprites zip](https://cdn.discordapp.com/attachments/1000595201557008467/1340566313394896957/LegalExplosionSprites.zip)


## Other Useful Customizations

### Two-tap Grenade Binds
Need someone to write this up.

### Lagless Grenade Timers
[Coach Suez's guide to lagless grenade timers](https://www.youtube.com/watch?v=h-ClGTyNO0I) 

To set up the lagless timer you'll want to do two things: silence the server-side timer, and add a command to play your own client-side timer when you press your grenade keys. 
*   Go to your `\SteamLibrary\steamapps\common\Half-Life\tfc\sound\weapons\` folder, rename the file `timer.wav` to `mytimer.wav` and move it to the `vox` folder here: `\SteamLibrary\steamapps\common\Half-Life\tfc\sound\vox\`.
*   Copy this [timer_silent.wav](https://cdn.discordapp.com/attachments/1000595201557008467/1156628833001283584/timer_silent.wav) file into your `tfc\sound\weapons\` folder, and rename it to `timer.wav` (this will mute the server-side beeps).
*   Finally, add `spk mytimer` to your grenade binds in your configs. For example if your current config has  
```
bind mouse4 "+gren1"
```
that would become  
```
bind mouse4 "+gren1; spk mytimer"
```

### Steam Game Recording

Steam now has a built in game recording feature, which is a great way to record your matches. Enable this feature in Steam Settings -> Game Recording. It will automatically record your gameplay, and the last hour of video is available to save clips from in View -> Recordings & Screenshots.
Here is a [video guide](https://www.youtube.com/watch?v=jsg9Dep63Bg) to using this convienient feature.


## Gameplay Guides
* [Phil's guide to the 4v4 Team Fortress format](https://www.youtube.com/watch?v=AsL-xzc580o)
* [Coach Suez's guide to Conc jumping for the novice player](https://www.youtube.com/watch?v=7mMKpE2E3-M)
* [Coach Suez's guide to stabbing as the spy in TFC](https://www.youtube.com/watch?v=gp4i1iwFTHg)
* [Climax guide to 2fort Engineer](https://www.youtube.com/watch?v=LEWYSbU7k_0)
* [Infinity grenade maps to pracice conc jumps](https://cdn.discordapp.com/attachments/1000595201557008467/1309636276344983632/maps_infinity.zip)
* Visit [nuki's TFC Learning Center](https://sites.google.com/view/nlc-tfc) for more gameplay guides.