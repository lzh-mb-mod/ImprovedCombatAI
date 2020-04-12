# Enhanced Mission Change AI

A mod that provides more options avaiable to the base mode [`EnhancedMission (RTS Camera)`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355).

## Features

- Adjust combat AI to any value between 0 and 100.

  By default combat AI is determined by the weapon skill of soldiers. Soldiers with weapon skill of 350 performs best when using corresponding weapon.

  In this mod, you can change all troops' combat AI using "combat AI" option. If you set the combat AI in this mod to "96", their combat AI will be as if combat AI in game option set to "Challenging" and weapon skill set to 350.

- Realistic blocking.

  Realistic blocking is the blocking mechanism that introduced in Beta 0.8.1 and removed in Beta 0.8.4 in Multiplayer mode. Now in Single-player, It's only enabled for all the other characters except the player by default.

  If you enable the option in this mod, then player will use it too.

  With realistic blocking enabled, you block slower if for example your weapon is on the right side and you want to block on the left side.
  Here is the description from official patch note b0.8.1:
  > "Actual defence starting moment after clicking the defend key now relies on a directional distance to the target direction instead of animation progression."

- Configuration saving. The battle configuration is saved in directory `(user directory)\Documents\Mount and Blade II Bannerlord\Configs\EnhancedMission\`.
  
  The main config is saved in file `MoreOptionsConfig.xml`.

  You can modify them manually, but if you edit it incorrectly or remove them, the configuration will be reset to default.

## How to install
1. Copy `Modules` folder into Bannerlord installation folder(For example `C:\Program Files\Steam\steamapps\common\Mount & Blade II Bannerlord - Beta`). It should be merged with `Modules` of the game.

## How to use
- Start the launcher and choose Single player mode. In `Mods` panel select `EnhancedMission (RTS Camera)` and `EnhancedMission More Options Plugin`, then click `PLAY`.

  Then play the game as usual.

- After entering a mission (scene):

  - Press `O(letter)` (by default) to open menu of this mod. You can access the features of this mod in it.

  - You should see `Change Combat AI` and `Use Realistic Blocking` option appears in the menu.

## Troubleshoot
- If the launcher can not start:

  - Uninstall all the third-party mods and reinstall them one by one to detect which one cause the launcher cannot start.

- If it shows "Unable to initialize Steam API":

  - Please start steam first, and make sure that Bannerlord is in your steam account.

- If the game crashed after starting:

  - Please uncheck the mod in launcher and wait for mod update.

    Optionally you can tell me the step to reproduce the crash.

- If you forget the hotkey you set for opening menu:

  - you can remove the config file so that it will be reset to default.

## Contact with me
* Please mail to: lizhenhuan1019@qq.com
