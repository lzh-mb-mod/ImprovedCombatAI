# Improved Combat AI
(旧名：Change AI)
这是一个为基础mod[`RTS Camera`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355)提供改变AI的功能的mod。

## 功能
- 提高战斗AI等级（包括近战AI或远程AI）。

  -  战斗AI等级决定了士兵AI的战斗表现。更高的近战AI等级的士兵防御得更好，攻击也更凶猛和灵活。更高远程AI等级的士兵射击更加精准。

  - 默认情况下士兵的战斗AI等级取决于每个士兵的武器熟练度：战斗AI等级等于武器熟练度除以3.5。

  - 所以350熟练度的士兵在使用对应武器时的战斗AI等级为最高的100。

  - 假如在本mod中设置近战AI难度`x`，则近战AI等级会乘以`1 - x/100`。

  - 比如，加入一个士兵有100的单手熟练度，当他使用单手武器时，默认情况下，他的近战AI等级为100 / 350 * 100 = 28.57。如果你把“近战AI难度”调整为50， 那么他的近战AI等级就会是28.57 / (1 - 50/100) = 57.14，也就是翻倍。

- 调整战斗AI等级：你可以直接将战斗AI等级在0-100间调整。

  - 之后在决定战斗AI等级时，武器熟练度会被忽略。所有单位都会有你设置好的相同的战斗AI。

- 调整AI预判。根据我的测试结果，0.2-0.3效果最好。设置该值以让你的步弓可以命中敌方骑射

- 可选择更真实的格挡：一种更慢的格挡方式，解决了格挡时武器瞬间移动的问题。

  在多人模式中该机制在b0.8.1中引入，在b0.8.4中移除；单人模式中除玩家外的所有角色默认使用该机制。

  在mod中开启该选项后，玩家也会使用该机制。

  更真实的格挡开启后，若按下格挡键的时候武器位置和目标格挡方向距离远（如武器在身体右侧而你要向左格挡），则你会格挡得更慢。
  下面是来自官方的b0.8.1补丁的描述：
  > "Actual defence starting moment after clicking the defend key now relies on a directional distance to the target direction instead of animation progression."

- 保存配置：保存配置的文件夹为`(user directory)\Documents\Mount and Blade II Bannerlord\Configs\RTSCamera\`

  配置保存在文件`ChangeAIConfig.xml`中。

  你可以修改配置，但如果你编辑有误或配置文件被移除，配置会被初始化为默认内容。

## 前置要求
- [`RTS Camera`](https://www.nexusmods.com/mountandblade2bannerlord/mods/355) 已经不再是前置要求，但如果你使用了这个mod，请确保它是e3.9.15或更高的版本。

## 如何安装
1. 复制`ImprovedCombatAI`文件夹到砍二的Modules目录下（例如`C:\Program Files\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules`)。
   
   或者你可以使用Vortex来自动安装。

## 如何使用
- 在战斗中按L可打开mod菜单。

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
* 请发邮件到：lizhenhuan1019@qq.com或lizhenhuan1019@outlook.com
