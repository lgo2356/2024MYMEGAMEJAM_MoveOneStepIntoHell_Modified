using TeamJustFour.MoveOneStep.Manager;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_MainSceneSettingButton : UI_MainSceneButton
    {
        public override void Excute()
        {
            Debug.Log("Excute Setting Button!");

            //ScreenManager.Instance.SetResolution(1280, 720);
            //ScreenManager.Instance.SetWindowed();
        }
    }
}
