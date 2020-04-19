# Enhanced Mission Change AI

这是一个为基础mod[`Enhanced Mission RTS Camera`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355)提供改变AI的功能的mod。

## 功能
- 在0到100之间调整战斗AI难度（包括近战AI或远程AI）。

  - 默认情况下士兵的战斗AI等级取决于每个士兵的武器熟练度。350及以上的熟练度时士兵的战斗AI等级为100，此时AI表现最好。

  - 在本mod中，战斗AI难度会额外加入到战斗AI等级中。

  - 比如，加入一个士兵有100的单手熟练度，当他使用单手武器时，默认情况下，他的近战AI等级为100 / 350 * 100 = 28.57。如果你把近战AI难度调整为50， 那么他的近战AI等级就会是50 + 28.57 = 78.57。

- 调整战斗AI等级：你可以直接将战斗AI等级在0-100间调整。

  - 之后在决定战斗等级时，武器熟练度会被忽略。所有单位都会有你设置好的相同的战斗AI。

- 可选择更真实的格挡：一种更慢的格挡方式，解决了格挡时武器瞬间移动的问题。

  在多人模式中该机制在b0.8.1中引入，在b0.8.4中移除；单人模式中除玩家外的所有角色默认使用该机制。

  在mod中开启该选项后，玩家也会使用该机制。

  更真实的格挡开启后，若按下格挡键的时候武器位置和目标格挡方向距离远（如武器在身体右侧而你要向左格挡），则你会格挡得更慢。
  下面是来自官方的b0.8.1补丁的描述：
  > "Actual defence starting moment after clicking the defend key now relies on a directional distance to the target direction instead of animation progression."

- 保存配置：保存配置的文件夹为`(user directory)\Documents\Mount and Blade II Bannerlord\Configs\EnhancedMission\`

  配置保存在文件`ChangeAIConfig.xml`中。

  你可以修改配置，但如果你编辑有误或配置文件被移除，配置会被初始化为默认内容。

## 前置要求
- [`Enhanced Mission RTS Camera`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355)

## 如何安装
1. 请先下载安装前置mod[`Enhanced Mission RTS Camera`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355)。

2. 复制`Modules`文件夹到砍二的安装目录下（例如`C:\Program Files\Steam\steamapps\common\Mount & Blade II Bannerlord - Beta`)，和砍二本体的Modules文件夹合并。

## 加载顺序
- `Enhanced Mission Change AI`的加载顺序应当在`Enhanced Mission RTS Camera`之后。

## 如何使用
- 启动游戏启动器，并选择单人模式(Singleplayer)。在Mods选项卡中勾选`Enhanced Mission RTS Camera`和`Enhanced Mission Change AI`并点击`Play`。

  之后正常进行游戏。

- 进入关卡（即进入场景）后：

  - 默认情况下按`O(字母)`键，然后点击`扩展：改变AI`来打开本mod的菜单，你可以在其中访问本mod的所有选项。

## 解决问题
- 若启动器无法启动：

  - 卸载所有第三方mod，然后一个个重装它们来找出哪个mod导致了启动器不能启动。

- 若提示"Unable to initialize Steam API":

  - 请先启动Steam，并确保砍二在你登录的Steam账号的库中.

- 若mod启动后游戏崩溃：

  - 请确保加载顺序正确。

  - 请取消载入该mod并等待mod更新。

    你可以选择告诉我重现崩溃的步骤。

## 联系我
* 请发邮件到：lizhenhuan1019@qq.com
