# Enhanced Change AI

这是一个为基础mod[`EnhancedMission (RTS Camera)`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355)提供更多可选功能的mod。

## 功能

- 调整战斗AI：你可以将战斗AI在0-100间调整。

  默认情况下士兵的战斗AI取决于每个士兵的武器熟练度。350及以上的熟练度时士兵的战斗AI最好。

  在本mod中，你可以调整所有单位的战斗AI。若你将其调整为96，所有单位的战斗AI就会如同游戏内的战斗AI选项调至最高（Challenging）且熟练度为350。

- 可选择更真实的格挡：一种更慢的格挡方式，解决了格挡时武器瞬间移动的问题。

  在多人模式中该机制在b0.8.1中引入，在b0.8.4中移除；单人模式中除玩家外的所有角色默认使用该机制。

  在mod中开启该选项后，玩家也会使用该机制。

  更真实的格挡开启后，若按下格挡键的时候武器位置和目标格挡方向距离远（如武器在身体右侧而你要向左格挡），则你会格挡得更慢。
  下面是来自官方的b0.8.1补丁的描述：
  > "Actual defence starting moment after clicking the defend key now relies on a directional distance to the target direction instead of animation progression."

- 保存配置：保存战斗配置的文件夹为`(user directory)\Documents\Mount and Blade II Bannerlord\Configs\EnhancedMission\`

  主要配置保存在文件`MoreOptionsConfig.xml`中。

  你可以修改配置，但如果你编辑有误或配置文件被移除，配置会被初始化为默认内容。

## 如何安装
1. 复制`Modules`文件夹到砍二的安装目录下（例如`C:\Program Files\Steam\steamapps\common\Mount & Blade II Bannerlord - Beta`)，和砍二本体的Modules文件夹合并。

## 如何使用
- 启动游戏启动器，并选择单人模式(Singleplayer)。在Mods选项卡中勾选`EnhancedMission`并点击`Play`。

  之后正常进行游戏。

- 进入关卡（即进入场景）后：

  - 默认情况下按`O(字母)`键来打开本mod的菜单，你可以在其中访问本mod的所有功能。

    或者你可以用以下默认快捷键：

  - 按`F10`键来切换rts风格视角。

  - 按`F`键或`F10`键来在玩家死后控制其小兵。

  - 按`F11`键来切换不死模式。

  - 按`P`键来暂停游戏。

- 如何在rts风格的相机中进行游戏：

  - 在关卡（场景）中，按`F10`来切换到rts相机。

  - 你的玩家角色会被加入到mod菜单中选定的编队中。

  - 用`W`, `A`, `S`, `D`, `空格`, `Z`和鼠标中键来移动相机。

  - 用`shift`来加速移动相机。

  - 移动鼠标来旋转相机。或者当命令面板打开后，右键拖动鼠标来旋转相机。

  - 选中部队后左键在地面上拖动来改变部队的位置，方向和宽度。

## 解决问题
- 若启动器无法启动：

  - 卸载所有第三方mod，然后一个个重装它们来找出哪个mod导致了启动器不能启动。

- 若提示"Unable to initialize Steam API":

  - 请先启动Steam，并确保砍二在你登录的Steam账号的库中.

- 若mod启动后游戏崩溃：

  - 请取消载入该mod并等待mod更新。

    你可以选择告诉我重现崩溃的步骤。

- 若你忘了你设置的用哪个按键打开mod菜单：

  - 你可以移除配置文件，这样按键会恢复为默认。

## 联系我
* 请发邮件到：lizhenhuan1019@qq.com
