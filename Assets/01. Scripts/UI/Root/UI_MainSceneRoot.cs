using TeamJustFour.MoveOneStep.Controller;
using TeamJustFour.MoveOneStep.Manager;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_MainSceneRoot : MonoBehaviour
    {
        [SerializeField] private RectTransform m_Canvas;
        [SerializeField] private UI_MainSceneButtonGroup m_ButtonGroup;

        public void ReleaseReferences()
        {
            KeyboardInputManager.Instance.ReleaseKeyboardInputListener(OnKeyInput);
        }

        private void SetCanvas()
        {
            float width = ScreenManager.Instance.ScreenWidth;
            float height = ScreenManager.Instance.ScreenHeight;
            
            if (width * 9 > height * 16)
            {
                width = height * 16 / 9;
            }

            m_Canvas.sizeDelta = new Vector2(width, height);
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

            SetCanvas();
        }
    }
}
