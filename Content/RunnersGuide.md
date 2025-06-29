# Game Runner's Guide

This guide is for designated Game Runners. It covers the responsibilities and commands needed to manage pickup games (PUGs).

---

## Table of Contents
* [Introduction](#introduction)
* [Transition notes for veteran runners](#transition-notes-for-veteran-runners)
* [Notes for new runners](#notes-for-new-runners)
* [Server List](#server-list)
* [Bot commands](#bot-commands)
  * [Set Steam ID](#set-steam-id)
  * [Add Player](#add-player)
  * [Remove Player](#remove-player)
  * [Notice](#notice) 
  * [Teams command](#teams-command)
  * [Swap Players](#swap-players)
  * [Sub](#sub)
  * [Clear Queue](#clear-queue)
  * [Cancel Match](#cancel-match)
  * [Cancel Tournament](#cancel-tournament)
  * [Undo Match](#undo-match)
  * [Undo Tournament](#undo-tournament)
  * [Requeue](#requeue)
  * [ForceVote](#forcevote)
  * [Shuffle](#shuffle)
  * [Timeout -](#timeout)
  * [Global](#global)
  * [Win](#win)
  * [Draw](#draw)
  * [TFC player](#tfc-player)
  * [Stats](#stats)
  * [No ELO match](#no-elo-match)
  * [Map Test match](#map-test-match)
  * [Map information edit](#map-information-edit)

---

## Introduction

Thank you for volunteering for the responsibility of being a game runner in the TFpugs discord. We have a simple goal here, to ensure the game we love will always be playable. We accomplish that by providing the space for players to form and play pickup matches. Providing this space is not enough. Sometimes things go wrong with the bot or with the server, and someone needs to restart it for the match to take place. Sometimes one person can ruin an entire match for everyone because of their attitude or language or even their playstyle. These problems take an intelligent, mature, and pragmatic person to quickly resolve and move on from. Without this leadership and initiative, matches will collapse, become less enjoyable, and will occur less often. With the proper direction, matches will complete, be more fun, and happen continuously. This is your role in the future of this game.

## Transition notes for veteran runners

Nothing stays the same. You cannot walk through the same river twice. You can never play the same game again. Even if you run it back immediately with the same teams, you've learned things. You're better now. You're tired. You're worse now. The meta shifts. Everything changes. That's what makes it a game and not a dance. The way TFC is played now is different than it was 10 years ago and is different than how it will be in 10 years, **but TFC will be played in 10 years**, and one of the goals of this server is to ensure that. **Locking in to a specific version of the game, or even of a website or a bot, does not lock it into a working state for the rest of time. Rather, it slowly deteriorates as other things change around it.** We are blessed by gaben with a game which actually receives updates, including the recent 25th anniversary update. We are blessed by a community of people making new tools and sites and maps and ideas which can be integrated and added into something much larger than the parts, but this requires a maintainable base that adapts and lives with the changing world. The only way forward long term is to stay current on everything, including our own bots. The new bot is a significant improvement, leveraging modern python coding patterns and standards, new discord features, and concepts that have evolved over the last 25 years of gaming. One of the primary design philosophies has been to **maintain the behavior** and sometimes even the appearance of the current pug bot. **Changes were intentionally minimal**, but please familiarize yourself with these changes and try to set a positive example as we head into the next phase of TFC pugging:
* The new bot supports discord `/slash` commands. Slash commands support tab auto-completes, actual parameterized fields, a helpful description display, and many other features which improve quality of life. Every `!bang` style command from the old bot will still work in the new bot. Commands that are truly obsolete return some information about the new process. (ex. typing `!stats` will tell you just to report the game again if needed; that command was only ever to tie two parts of the bot together) Some aliases like `!add` and `++` are so familiar that they work just fine, but try to use the `/slash` commands and you will see their benefit. Please let me know if you have any feedback about the commands as we're testing and rolling this out.
* The new bot supports ephemeral messages, and uses them whenever that makes sense. You've probably seen these in other discords. *\"Only you can see this. Dismiss Message.\"* This reduces clutter in chat rooms and allows people to interact privately with the bot. 
* The new bot supports multiple games and queues. Players are encouraged to join multiple queues, and when one fills, they are seamlessly removed from any other queues they are in. 
* The first "new game" added in this way is 1v1 TFC. Team size is 1 but the queue size is 4. Two matches are formed simultaneously, and then the winners and losers play in a mini tournament. A separate stats display occurs and a separate elo is maintained. There's a few new commands for runners like `/undotournament`.
* The next "new game" offered will be 4v4 TFC with pre-made teams. Teams will maintain a separate elo and match history from players. We will combine the current 4GL league with our discord. This will unify the rules, servers, websites, and stats for both the pugs and the league, as well as offer a common and more convienient place to play in both. This will bring more TFC players into our community and make it easier for everyone to play in both the pugs and the league.
* We will be rolling out some other titles which will compliment our community. First ones will be Ricochet 1v1 in the same format as TFC 1v1,and Left4Dead2 co-op campaigns, where queue size is 4, a vote for which of the 14 base game campaigns will be played, rather than a map vote, and no server vote occurs as these are played on official servers. Players will share a lobby code (currently not a feature within the bot) and play on Valve servers. Each campaign is 2-5 connected maps and takes about 30-45 minutes. Please let me know your suggestions for what we should add next. For this reason "game" now generally refers to a different title (or format), and "match" now refers to an individual pickup. Games are often referenced by a `gameID`, where matches have a long uuid matchID and a 6 digit matchdisplayID which can be used. 
	Example usage: `/recentmatches`, `/cancelmatch 123456`, `/matchinfo 334455`
* The new bot supports multiple groups of servers, for that reason "region" now refers to a group of servers like North America or Latin America (Miami server is in both), and servers now have a specific "location" like New York or Dallas.
* The first new region added is South America, which will be served by the Miami server as well as a new Santiago server. I hope this new queue will be a success. Please help out the South American players and runners as they get settled in to our community.
I hope you understand the reason for these and any other changes. Please let me know of any questions or concerns. I am focusing on the bot writing which I hope to have completed and tested in the next week or two. Then I'll be focusing more on updating the game servers. Please let me know if you have and thoughts or feedback on the changes. 

## Notes for new runners
Welcome to the TFpugs community! We are entering a period of expansion and growth as we leverage some great new tools to expand TFC and make it easier to play than ever. Our goal is a thriving community with games happening at all skill levels and in all regions of the world. You're joining an established community of players and runners who have been playing this game for many years. I hope you enjoy the game as it is played here, and I hope you are able to help contribute to the success of many pickups and the community as a whole. 

## Server List
The server list, as available to the bot, is available [here](/Servers) but I will hardcode them here on this website guide for redundancy in case the bot is not responding to that there. 

Dallas TFC pug server: connect 149.28.241.217:27015; password letsplay!  
[tinyurl](https://tinyurl.com/tfpcentralvultr2)
[philchute.com](http://philchute.com/servers/tfpcentral)
[tfpugs.com](http://tfpugs.com/servers/dallas)

Miami TFC pug server: connect 45.77.162.42:27015; password letsplay!   
[tinyurl](https://tinyurl.com/tfpsoutheastvultr)	
[philchute.com](http://philchute.com/servers/tfsoutheast)  
[tfpugs.com](http://tfpugs.com/servers/miami)

Current NYC pug server: connect 104.207.129.123:27015; password letsplay!  
[tinyurl](https://tinyurl.com/tfpeast2vultr)  
[philchute.com](http://philchute.com/servers/tfpeast2)  
[tfpugs.com](http://tfpugs.com/servers/nyc)

New NYC TFC conc pub server: connect 149.28.56.141:27014

New NYC TFC pug server: connect 149.28.56.141:27015; password letsplay!  
[philchute.com](http://philchute.com/servers/tfpugsnyc)
[tfpugs.com](http://tfpugs.com/servers/newnyc)

New NYC Ricochet 1v1 pug server: connect 149.28.56.141:27016; password letsplay!  
[philchute.com](http://philchute.com/servers/tfpugsricochet)
[tfpugs.com](http://tfpugs.com/servers/ricochet)

New Santiago, Chile TFC pug server: connect 64.176.14.39:27015; password letsplay!  
[tfpugs.com](http://tfpugs.com/servers/santiago)

## Bot commands

### Set Steam ID
The new bot awards achievements and tracks other information by `steamID`. Players are encouraged to register their steamID with the bot to receive these achievements, using the command `/registersteamid`. This is required for league matches.  
To assist players in registering their steamID, runners have access to the command `/setsteamid` which will allow them to set another player's steamID. Let me (phil) know if you have any suggestions for this. we might end up printing unassigned steamID's with the stats or something. There is no undo or redo or overwrite on this, all of that would be manual database action, so let an admin know if there are any issues regarding steamid registration. Only one steamID per player, only one player per steamID. For reference, the regex for acceptable values is r"STEAM_[0-5]:[01]:[0-9]+".  
Example usage:  
`/setsteamid @oldguy STEAM_0:0:12345`  
`/setsteamid @newkid STEAM_1:1:1122334455`  


### No ELO match
Games can optionally be played without counting the results for ELO using the command `/noelo`
This can be useful for a non-competitive game with new players or when playing on an experimental map.  
Example usage:  
`/noelo`


### Add Player
Players can be manually added by a runner to a pickup queue with the bot command:  
`/addplayer @username`  
This command must be performed in a game channel.


### Remove Player
Manually remove a player from a queue with the command `/removeplayer @username (reason)`  
This can be used to help players who are wanting or needing to be removed from a queue, or this can be used as an administrative tool if someone needs to be removed from a queue due to being afk, overly drunk, overly rude, etc. This is the legacy `!kick` command.  
Example usage:  
`!kick @someguy`  
`!removeplayer @afkguy afk`  
`/removeplayer @rudeguy yelling in voice chat`  


### Clear Queue
Runners can empty an active queue with the command `/clearqueue`

This is the legacy `!cancel` command (nothing is really cancelled in the backend here but this empties the queue like cancel did).  
Example usage:  
`!cancel`  
`!clearqueue`  
`/clearqueue`


### Notice
Players can notified of the status of the queue by using the command `/notice (message)`

This will ping the game players with a message in chat showing the status of the queue, and your optional message if you included one. This will also DM users who have opted into receiving DMs when a runner uses this command.

Example usage:  
`/notice`  
`/notice Friday night let's go!!`  
`/notice Need one!!!`  


### Teams command
The queue will fill until a runner starts a game with the `/teams` command. Each game as a default team size configured, so specifying a team size is not necessary but can be used to modify the default size.
The bot takes the first people in the queue and begins the process of turning the queue into a pug. This will typically involve a server vote followed by a map vote and then teams being generated.

Example usage:  
`/teams`  
`/teams 4`  
`!teams 5`  


### Force Vote
Runners can force a vote to end early with the command `/forcevote matchid`
Use this when you do not want to wait on a vote timer to complete. 
Example usage:  
`/forcevote 123456`


### Shuffle

### Swap Players

Runners have the ability to override the teams generated by the bot and manually set the teams.  
Use the command `/swap @player1 @player2` to swap two players between teams.  
Please use this at your discretion to create the most balanced and enjoyable matches possible. 
The order of the players specifed does not matter, the bot will swap them as needed.
The match will be ready to begin after the swap.
Example usage:  
`/swap @player1 @player2`  
`/swap @newkid @oldguy`  
`/swap @englishguy @spanishguy` 


### Sub

Runners have the ability to manually move a player out of a match and sub a different player in without requeuing the pug.
Use the command `/sub @player1 @player2` to sub players in and out of the game. 
Please use this at your discretion to keep matches moving when someone needs to leave and someone else wants to play.
The order of the players specifed does not matter, the bot will sub them as needed.
This command will re-generate new teams which include the new player and the match will be ready to begin. 
Example usage:  
`/sub @player1 @player2`  
`/sub @notready @readynow`  
`/sub @wantsin @wantsout` 


### Manually Report Game Results

The bot is designed to automatically report game results when a match is completed. However, there are some cases where the bot may not be able to report the results correctly, such as a network issue or the game is playing on a server which does not report results to our UDP listener, the results must be manually reported.

Use the command `/reportwin winningTeamNumber WinningTeamScore LosingTeamScore` to report a winner or the command `/reportdraw score` to report a draw. This automatically triggers the end of game stats process if available. 


### Cancel Match
Matches that are live and pending completion (viewed on `/viewmatches`) can be cancelled using the command `/cancelmatch matchid [isRequeue]`

Matches should be cancelled if they will not be completed to release the players to other queues and avoid cluttered displays or data.

Example usage:  
`/cancelmatch 123456`  
`/cancelmatch 224466 true`
`/cancelmatch 333666 false`

### Requeue

Requeue cancels a match and returns the players to the front of the queue.  
This is a conviencence command for runners to use when players should be requeued following a cancel match. 

Example usage:  
`/requeue 123456`  
`/requeue 224466`

### Cancel Tournament
Tournaments that are live and pending completion (viewed on `/viewmatches`) can be cancelled by runners with the command `/canceltournament tournamentID (reason)`

This command iterates through the matches in the tournament and calls a `/cancelmatch` on each.

Example usage:  
`/canceltournament 123456`  
`/canceltournament 223344 redoing bracket`  
`/canceltournament 222333 not going to finish`  

### Undo Match
Matches that are completed (viewed on `/recentmatches`) can be undone using the command `/undomatch matchID (reason)`

This reverses the elo change that took place for this match and places it in an undone status where it will not count for totals and statistics. This is the legacy `!undo` command.
Note this does not undo the stats sites that may have parsed the demo.
Note that the `/cancelmatch` and `/undomatch` commands obsolete the legacy `!removematch` command.

Example usage:  
`/undomatch 456789`  
`/undomatch 334455 just testing`  
`/undomatch 445533 player DCed`  

### Undo Tournament
Tournaments that are completed can be undone using the command `/undotournament tournamentid (reason)`

This iterates through the matches in the tournament and calls a `/undomatch` on each.

Example usage:  
`/undotournament 456789`  
`/undotournament 334455 just testing`  
`/undotournament 445533 wrong results`

### Timeout

Timeout is done using the bot command `/timeout @username duration (reason)`
This uses discord moderation features to temporarily remove someone's ability to speak in the discord server, join voice channels, or use bot commands. We generally do not ban people from the discord server, as it isn't really possible to practically enforce, but rather we apply a temporary timeout for a user to allow them to cool off and reflect on how they want to interact with the community. Try to give warnings and talk through the issue with people rather than blindside anyone with a punishment, keep things positive. Longer timeouts should be discussed and agreed upon with other admins, to avoid any splitting or drama. These should  come as a relief to regular players, rather than something hanging over everyone's head. Ultimately though, this is the behavior enforcement mechanism; we remove someone's ability to participate in chat and pickups. Remember, this is a global timeout that affects all channels and queues on the server. Feel empowered to use this, but feel free to discuss any application with other runners or the admins.
Duration can be formatted like 1m, 2h, 3d, 4w, etc. The maximum time allowed is 28 days.  
Example usage:  
`/timeout @hyperguy 10m chill out dude`  
`/timeout @drunkguy 12h go sleep`  
`/timeout @rudeguy 1w repeated ranting at new players`


### Map Test match
Games can optionally be played as map tests. We want to encourage active mapping and feedback for the mppers to iterate and improve and freshen the game. We'll work to find the best way to schedule and announce these but for now this is at the runner's discression. A queue can be turned into a map test with the command `/maptest mapname`  
The map vote will be skipped, elo changes will not count, but stats and achievements can still be earned.  
Example usage:  
`/maptest well2`  
`/maptest newmap_b7`

### Map information edit
The ability to upload a new map is currently an open feature available for everyone in the mapping channel. Please provide any assistance you're able to for people uploading maps there. In order to protect our database, only runners and admins can edit map information. The author is automatically entered but can be updated. You can add a display name and any applicable tags. The map pools for each region and game are determined by the map tags, for example `na_primary` and `na_seconary` are the tags for the TFC NA queue.  
Set a map to active/inactive:  
`/mapedit active map_name:<name> is_active:<True/False> [game_id:<id>]`   
Add or remove tags:  
`/mapedit tags map_name:<name> [add_tags:<tags>] [remove_tags:<tags>] [game_id:<id>]`  
Change the display name:  
`/mapedit displayname map_name:<name> new_display_name:<name> [game_id:<id>]`  
Change the author:  
`/mapedit author map_name:<name> new_author:<name> [game_id:<id>]`

### TFC player

This command `!tfc` is a legacy command that is no longer needed. Players can now select their own player roles in the role selection channels. 

### Stats

The `!stats` command is no longer used. Match results and stats are now automatically processed when matches end. If you need to reprocess a match's stats, you can use `/reportwin` or `/reportdraw` with the same or corrected score to trigger the stats generation process.
