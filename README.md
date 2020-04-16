# Enhanced Mission Change AI

A mod that provides features about changing AI to the base mode [`Enhanced Mission RTS Camera`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355).

## Features

- Enhance melee combat AI to fix the problem that AI won't defend in e1.1.0.

  - By default combat AI is determined by the weapon skill of soldiers. Soldier with weapon skill of 350 performs best when using corresponding weapon.

  - In this mod, this value is changed to 200 for melee AI: a soldier with 200 weapon skill now performs as if his weapon skill is 350 without this mod.

- Be able to adjust combat AI to any value between 0 and 100 .

  - In this mod, you can change all troops' melee and ranged AI using corresponding option.

- Realistic blocking.

  Realistic blocking is the blocking mechanism that introduced in Beta 0.8.1 and removed in Beta 0.8.4 in Multiplayer mode. Now in Single-player, It's only enabled for all the other characters except the player by default.

  If you enable the option in this mod, then player will use it too.

  With realistic blocking enabled, you block slower if for example your weapon is on the right side and you want to block on the left side.
  Here is the description from official patch note b0.8.1:
  > "Actual defence starting moment after clicking the defend key now relies on a directional distance to the target direction instead of animation progression."

- Configuration saving. The configuration is saved in directory `(user directory)\Documents\Mount and Blade II Bannerlord\Configs\EnhancedMission\`.
  
  The config file is saved in file `ChangeAIConfig.xml`.

  You can modify them manually, but if you edit it incorrectly or remove them, the configuration will be reset to default.

## Prerequisite
- [`Enhanced Mission RTS Camera`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355)

## How to install
1. Please download and install the prerequisite mod [`Enhanced Mission RTS Camera`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355) first.

2. Copy `Modules` folder into Bannerlord installation folder(For example `C:\Program Files\Steam\steamapps\common\Mount & Blade II Bannerlord - Beta`). It should be merged with `Modules` of the game.

## Loading sequence requirement
- `Enhanced Mission Change AI` should be loaded after `Enhanced Mission RTS Camera`.

## How to use
- Start the launcher and choose Single player mode. In `Mods` panel select `EnhancedMission RTS Camera` and `EnhancedMission Change AI`, then click `PLAY`.

  Then play the game as usual.

- After entering a mission (scene):

  - Press `O(letter)` (by default) and then click `Extension: Change AI` to open menu of this mod. You can access the options of this mod in it.

## Troubleshoot
- If the launcher can not start:

  - Uninstall all the third-party mods and reinstall them one by one to detect which one cause the launcher cannot start.

- If it shows "Unable to initialize Steam API":

  - Please start steam first, and make sure that Bannerlord is in your steam account.

- If the game crashed after starting:

  - Please make sure the loading sequence is correct.

  - Please uncheck the mod in launcher and wait for mod update.

    Optionally you can tell me the step to reproduce the crash.

## Contact with me
* Please mail to: lizhenhuan1019@qq.com
