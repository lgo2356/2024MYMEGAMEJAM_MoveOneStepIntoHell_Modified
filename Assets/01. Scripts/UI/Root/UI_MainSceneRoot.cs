using TeamJustFour.MoveOneStep.Controller;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_MainSceneRoot : MonoBehaviour
    {
        [SerializeField] private UI_MainSceneButtonGroup m_ButtonGroup;

        public void ReleaseReferences()
        {
            KeyboardInputManager.Instance.ReleaseKeyboardInputListener(OnKeyInput);
        }

        private void OnKeyInput(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.UpArrow:
                    {
                        m_ButtonGroup.PrevButton();
                    }
                    break;

                case KeyCode.DownArrow:
                    {
                        m_ButtonGroup.NextButton();
                    }
                    break;

                case KeyCode.Return:
                    {
                        m_ButtonGroup.ExcuteCurrentButton();
                    }
                    break;
            }
        }

        private void Awake()
        {
            KeyboardInputManager.Instance.SetOnKeyboardInputListener(OnKeyInput);
        }
    }
}
