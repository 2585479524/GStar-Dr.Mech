# Dr.Mech

![image](https://github.com/lexsaints/powershell/blob/master/IMG/ps2.png)

游戏原型为俄罗斯方块，在此基础上进行创新

以消除为导向，灵感来源于口红机，中间的大方块会进行90度旋转，玩家通过点击发射方块

同一侧方块成排后可以消除，而不同侧的方块位置的不同也会影响其他侧

（当然，游戏玩法是策划的功劳）

# MVC架构

由Controller层处理下落确认、消除判定、旋转处理，通知view改变

由Model层定义消除区域大小，基本方块组合等信息

由view层更新数据，控制显示内容
