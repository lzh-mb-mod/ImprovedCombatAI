# Improved Combat AI
(Old name: Enhanced Mission Change AI)
A mod that provides features about changing AI to the base mode [`RTS Camera`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355).

## Features
- Adjust combat(melee or ranged) AI difficulty between 0 and 100.

  - Combat AI level is a value in the game used to determine the soldier's combat ability.

  - By default combat AI level is determined by the weapon skill of soldiers: the combat level are equal to his weapon skill divided by 3.5.

  - So soldier with weapon skill of 350 has the highest 100 combat AI level when using corresponding weapon.

  - In this mod, combat AI level is increased by the value of melee or ranged AI difficulty, which you can adjust.

  - For example, if a soldier has 100 one-handed weapon skill, and when he uses one-handed weapon, by default his melee AI level is 100 / 350 * 100 = 28.57. If you adjust melee AI difficulty to 50, then his melee AI level will be 50 + 28.57 = 78.57.

- Be able to adjust combat AI level to any value between 0 and 100 directly.

  - Then the weapon skill will be ignored when determining combat AI level, and all units will have the same combat AI level you have set.

- Realistic blocking.

  Realistic blocking is the blocking mechanism that introduced in Beta 0.8.1 and removed in Beta 0.8.4 in Multiplayer mode. Now in Single-player, It's only enabled for all the other characters except the player by default.

  If you enable the option in this mod, then player will use it too.

  With realistic blocking enabled, you block slower if for example your weapon is on the right side and you want to block on the left side.
  Here is the description from official patch note b0.8.1:
  > "Actual defence starting moment after clicking the defend key now relies on a directional distance to the target direction instead of animation progression."

- Configuration saving. The configuration is saved in directory `(user directory)\Documents\Mount and Blade II Bannerlord\Configs\RTSCamera\`.
  
  The config file is saved in file `ChangeAIConfig.xml`.

  You can modify them manually, but if you edit it incorrectly or remove them, the configuration will be reset to default.

## Prerequisite
- [`RTS Camera`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355)

## How to install
1. Please download and install the prerequisite mod [`RTS Camera`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355) first.

2. Copy `Modules` folder into Bannerlord installation folder(For example `C:\Program Files\Steam\steamapps\common\Mount & Blade II Bannerlord - Beta`). It should be merged with `Modules` of the game. Or use Vortex to install it automatically.

## Loading sequence requirement
- `Improved Combat AI` should be loaded after `RTS Camera`.

## How to use
- Start the launcher and choose Single player mode. In `Mods` panel select `RTS Camera` and `Improved Combat AI`, then click `PLAY`.

  Then play the game as usual.

- After entering a mission (scene):

  - Press `O(letter)` (by default) and then click `Extension: Improved Combat AI` to open menu of this mod. You can access the options of this mod in it.

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
