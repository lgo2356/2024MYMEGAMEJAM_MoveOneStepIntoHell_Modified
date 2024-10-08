using TeamJustFour.MoveOneStep.Controller;
using TeamJustFour.MoveOneStep.Manager;
using TeamJustFour.MoveOneStep.Module;
using UnityEngine;
using UnityEngine.UI;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_StageSceneRoot : MonoBehaviour
    {
        [SerializeField] private RectTransform m_Canvas;
        [SerializeField] private UI_StageSceneBackgroundGroup m_BackgroundGroup;
        [SerializeField] private Button m_LeftButton;
        [SerializeField] private Button m_RightButton;
        [SerializeField] private Image m_GuidePopup;

        public void ShowGuidePopup()
        {
            m_GuidePopup.gameObject.SetActive(true);
        }

        public void HideGuidePopup()
        {
            m_GuidePopup.gameObject.SetActive(false);
        }

        public void OnLeftButtonClick()
        {
            if (!m_BackgroundGroup.CanSlideLeft())
            {
                return;
            }

            m_BackgroundGroup.SlideLeft();

            m_LeftButton.gameObject.SetActive(false);
            m_RightButton.gameObject.SetActive(false);
        }

        public void OnRightButtonClick()
        {
            if (!m_BackgroundGroup.CanSlideRight())
            {
                return;
            }

            m_BackgroundGroup.SlideRight();

            m_LeftButton.gameObject.SetActive(false);
            m_RightButton.gameObject.SetActive(false);
        }

        public void OnStartButtonClick()
        {
            StageSceneGameManager.Instance.ReleaseReferences();

            PlayerPrefsManager.Instance.SetStage(m_BackgroundGroup.CurrentPage);

            InGameSceneLoader.Instance.Load();
        }

        public void ReleaseReferences()
        {
            KeyboardInputManager.Instance.ReleaseKeyboardInputListener(OnKeyboardInput);
        }

        private void InitUI()
        {
            m_LeftButton.gameObject.SetActive(m_BackgroundGroup.CanSlideLeft());
            m_RightButton.gameObject.SetActive(m_BackgroundGroup.CanSlideRight());
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

        private void OnCompleteBackgroundSlide()
        {
            m_LeftButton.gameObject.SetActive(m_BackgroundGroup.CanSlideLeft());
            m_RightButton.gameObject.SetActive(m_BackgroundGroup.CanSlideRight());
        }

        private void OnKeyboardInput(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.LeftArrow:
                    {
                        OnLeftButtonClick();
                    }
                    break;

                case KeyCode.RightArrow:
                    {
                        OnRightButtonClick();
                    }
                    break;

                case KeyCode.Escape:
                    {
                        m_GuidePopup.gameObject.SetActive(false);
                    }
                    break;

                case KeyCode.Return:
                    {
                        OnStartButtonClick();
                    }
                    break;
            }
        }

        private void Awake()
        {
            InitUI();
            
            m_BackgroundGroup.SetOnCompleteSlideListener(OnCompleteBackgroundSlide);

            KeyboardInputManager.Instance.SetOnKeyboardInputListener(OnKeyboardInput);
        }

        private void Start()
        {
            SetCanvas();
        }
    }
}
