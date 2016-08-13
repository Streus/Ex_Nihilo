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
