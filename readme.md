# Welcome to the Ex Nihilo project home.

Quick rundown of formatting tips:
* You can make lists with an asterisk "*"
* Headers are denoted by amounts of "#" - H1 through H6 based on the number
* Italics are *words in asterisks* "*"
* Bold with **double asterisks** "**"
* Strikethrough with ~~two tildes~~ "~~"

1. Numbered lists are just *a* number, doesn't matter which.
  1. Two spaces for indenting, sub-lists are automatic.

Links are like so: enclose the hypertext in square brackets "[]", and the link in parenthesis after "()".
Example: [Google](https://www.google.com)

## Patch notes (in chronological order)

### Build #68
* Added a pause variable and methods in Game
* Gave cells behavior conditional on the pause state

------
### Build #66
* Hotbar added.
* Energy bar added.
* Pause menu and most sub-menus added.
* Most back-end functionality still pending.

------
### Build #63
* *Note: builds 61-63 were very minor, and are lumped into the stable 63 build.*
* Fixed pan border to use percents instead of pixels
* In-game GUI started
* Added unlocked camera controls - where you move the mouse to the edges of the screen to pan instead of the camera being fixed on the cells' center of position

------
### Build #59
* Added unlocked camera controls - where you move the mouse to the edges of the screen to pan instead of the camera being fixed on the cells' center of position

------
### Build #56
* Pan Speed, Pan Border Size, and Camera Lock options added.

------

### Build #55
* If Game.playerControllingCell = false, then none of the movement scripts work. This fix makes selection always work, and movement work only if Game.playerControllingCell, as well as sets that variable properly.
* Added configuration methods for Movement class

------
### Build #54
* Fixed and re-added Flag asset (see issue [#52](https://github.com/Streus/Ex_Nihilo/issues/52))

------
### Build #51
* CellBase movement code now migrated to "Movement" class
* Generalized movement code to allow for many cells, all grouped around their mutual center of position
* Removed camera panning control (until multiple cell selection design is finalized)
* Removed flag spawning temporarily due to a bug in Game.destroy() method not properly removing references to deleted objects

------
### Build #49
* Made left-click selection GUI work
* Added "selection" arraylist that holds every CellBase-attached GameObject within the current selection
* Began work on support for controlling multiple cells
* Added more static Game methods (filterBy, getBetween)

------
### Build #46
* Removed 'Tank' control scheme support
* Fleshed out 'Click to move' control scheme support

------
### Build #45
* Added in cleaner way to add GameObjects to the world (currently in beta) - Game.create()
* Removed WASD controls
* Fixed RMB camera panning when controlling a cell

------
### Build #44
* Made a small fix to GameManager

------
### Build #43
* Refactored folders
* Added a lot of infrastucture for CellBase
* Minor code cleanup (removed some commented code)

------
### Build #42
* Beginnings of save game support added. Back-end still needed.
* Rebind buttons now work.
* Can now toggle movement sets. Has yet to be hooked up to player.
* Changed movement marker sprite.
* Options now properly save and load.

------
### Build #38
* Updated readme.md file - would be nice to keep this guy up to date

------
### Build #37
* Modified cell mouse movement algorithm slightly to increase its effectiveness
* Removed exponential movement speed from that algorithm (-> now fastest possible)
* Removed slower turn speed from that algorithm (-> now fastest possible)
* Added flag

------
### Build #36
* Added in the ability to click to move a cell to a position, using tracking of the destination (and exponentials!)
* Cleaned up code (some debug code remains commented out)

------
### Build #35
* Added a background that re-generates itself around the camera, with configuration options for its size
* Cleaned up code

------
### Build #34
* fuckity fuck fuck fucker

------
### Build #33
* Edits made to key rebinding code, the controls menu, and cell movement.
* Additions made in audio level control and fullscreen toggling.

------
### Build #32
* Made Dev Console larger by default
* Made more Dev Console variables public (command API support)
* Made "console size" command resiliant to bad input
* Made Dev Console text color auto-select based on current BG color(black/white based on contrast)
* Minor code cleanup

------
### Build #28
* Small changes to keybindings

------
### Build #26
* Updated Dev Console

------
### Build #25
* Progress on Key Rebinding

------
### Build #24
* Finished up command infrastructure

------
### Build #23
* Unborked the dev console removal

------
### Build #21
* Minor changes to dev console

------
### Build #20
* Minor hotfixes

------
### Build #19
* Command/dev console hotfix (allows successful compilation)

------
### Build #18
* Added persistent dev console

------
### Build #17
* Pushed the basic infrastructure for commands - waiting for the dev console to be finished for more

------
### Build #16
* Hotfix and work on persistence support

------
### Build #15
* Fixes to dev consle and started work on commands

------
### Build #14
* Minor changes

------
### Build #13
* Added basic background logic (unfinished)

------
### Build #12
* Key rebinding started

------
### Build #11
* Added dev console

------
### Build #10
* Added basic mesh deformation prototype work - see the "JellyPhysics" scene

------
### Build #9
* GUI edited and added to

------
### Build #8
* "The Great Re-Wording" - removed plans for both a Simulation and Sandbox game modes. Now we are not creating a simulation.

------
### Build #7
* Removed extra header files

------
### Build #6
* "Fixed the brock" - readded a deleted main menu controller

------
### Build #5
* Adding backend support
* Added AI support
* Gameplay changes
* Changes to cell backend

------
### Build #4
* Frontend mostly completed

------
### Build #3
* Basic main menu added

------
### Build #2
* Work started on game proper
* Added base cell prefab
* Added base AI
* Added camera controls

------
### Build #1
* Made the skybox color grey
* (Github commit/merge workflow test)
