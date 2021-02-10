# Improved Combat AI
(Old name: Enhanced Mission Change AI)
A mod that provides features about changing AI to the base mode [`RTS Camera`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355).

## Features
- Improve combat(melee or ranged) AI level.

  - Combat AI level is a value in the game used to determine the soldier's combat ability. With higher melee ai level, the soldier can block better, attack more ferociously and dexterously. With higher ranged ai level, the soldier shoot more accurately.

  - By default combat AI level is determined by the weapon skill of soldiers: the combat level are equal to his weapon skill divided by 3.5.

  - So soldier with weapon skill of 350 has the highest 100 combat AI level when using corresponding weapon.

  - Given the melee AI difficulty `x` set in this mod, the melee AI level will be divided by `1 - x/100`.

  - For example, if a soldier has 100 one-handed weapon skill, and when he uses one-handed weapon, by default his melee AI level is 100 / 350 * 100 = 28.57. If the "Melee AI Difficulty" is set to 50, then his melee AI level will be 28.57 / (1 - 50/100) = 57.14, that is, doubled.

- Be able to adjust combat AI level to any value between 0 and 100 directly.

  - Then the weapon skill will be ignored when determining combat AI level, and all units will have the same combat AI level you have set.

- Adjust ranged lead error. The value is used when aiming moving targets. 0.2-0.3 is the best value as I tested. Set this between 0.2-0.3 so your archers can hit the enemy horse archers.

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
- [`RTS Camera`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355) is not required any more but if you use it, please make sure it's version is e3.9.15 or higher.

## How to install
1. Copy `ImprovedCombatAI` folder into Bannerlord modules folder(For example `C:\Program Files\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules`). Or use Vortex to install it automatically.

## How to use
- In a battle, press `L(letter)` (by default) then click to open menu of this mod. You can adjust settings of this mod in it.

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
* Please mail to: lizhenhuan1019@qq.com or lizhenhuan1019@outlook.com
