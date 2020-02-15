/// <summary>
/// 游戏开始的命令
/// </summary>
class StartUpCommand : Controller
{
    public override void Execute(object data)
    {
        //注册模型（Model）

        //RegisterModel(new MapModel());
        //注册命令
        RegisterController(ConstName.E_EnterScene, typeof(EnterSceneComand));
        RegisterController(ConstName.E_ExitScene, typeof(ExitSceneCommand));
        RegisterController(ConstName.E_PauseGame, typeof(PauseGame));
        RegisterController(ConstName.E_ContinueGame, typeof(ContinueGame));
        RegisterController(ConstName.E_CreatBlock, typeof(CreatBlock));
        RegisterController(ConstName.E_EliminataBlock, typeof(EliminataBlock));
        RegisterController(ConstName.E_RotateBlock, typeof(RotateBlock));
        RegisterController(ConstName.E_RotateAllBlock, typeof(RotateAllBlock));
        RegisterController(ConstName.E_ShootBlock, typeof(ShootBlock));
        RegisterController(ConstName.E_AlignedPostion, typeof(AlignedPostion));
        //进入开始界面
        Game.Instance.LoadScene(1);
    }
}


