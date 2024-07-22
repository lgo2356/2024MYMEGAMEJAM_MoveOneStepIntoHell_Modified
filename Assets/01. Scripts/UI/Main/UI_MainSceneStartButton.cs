using TeamJustFour.MoveOneStep.Module;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_MainSceneStartButton : UI_MainSceneButton
    {
        public override void Excute()
        {
            MainSceneGameManager.Instance.ReleaseReferences();

            StageSceneLoader.Instance.Load();
        }
    }
}
