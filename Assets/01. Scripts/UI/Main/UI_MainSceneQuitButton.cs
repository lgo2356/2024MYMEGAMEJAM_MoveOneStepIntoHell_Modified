using UnityEngine;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_MainSceneQuitButton : UI_MainSceneButton
    {
        public override void Excute()
        {
            Application.Quit();
        }
    }
}
