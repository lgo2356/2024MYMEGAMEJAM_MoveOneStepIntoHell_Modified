using TeamJustFour.MoveOneStep.Controller;
using TeamJustFour.MoveOneStep.Manager;
using TMPro;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_InGameSceneRoot : MonoBehaviour
    {
        [SerializeField] private RectTransform m_Canvas;
        [SerializeField] private UI_ClearPopup m_ClearPopup;
        [SerializeField] private UI_FailedPopup m_FailedPopup;
        [SerializeField] private TextMeshProUGUI m_StageText;
        [SerializeField] private UI_ReturnMainButton m_ReturnMainButton;
        [SerializeField] private UI_WeaponButton m_HammerButton;
        [SerializeField] private UI_WeaponButton m_ShovelButton;
        [SerializeField] private UI_WeaponButton m_BucketButton;
        [SerializeField] private UI_SystemMessage m_SystemMessage;

        public void ShowSystemMessage()
        {
            m_SystemMessage.Show();
        }

        public void ShowClearPopup()
        {
            m_ClearPopup.transform.SetAsLastSibling();
            m_ClearPopup.gameObject.SetActive(true);

            m_StageText.gameObject.SetActive(false);

            m_ReturnMainButton.gameObject.SetActive(true);
            m_ReturnMainButton.transform.SetAsLastSibling();
        }

        public void HideClearPopup()
        {
            m_ClearPopup.gameObject.SetActive(false);
        }

        public void ShowFailedPopup()
        {
            m_FailedPopup.transform.SetAsLastSibling();
            m_FailedPopup.gameObject.SetActive(true);

            m_StageText.gameObject.SetActive(false);

            m_ReturnMainButton.gameObject.SetActive(true);
            m_ReturnMainButton.transform.SetAsLastSibling();
        }

        public void HideFailedPopup()
        {
            m_FailedPopup.gameObject.SetActive(false);
        }

        private void SetCanvas()
        {
            float width = m_Canvas.rect.width;
            float height = m_Canvas.rect.height;

            if (width * 9 > height * 16)
            {
                width = height * 16 / 9;
            }

            m_Canvas.anchorMin = new Vector2(0.5f, 0.5f);
            m_Canvas.anchorMax = new Vector2(0.5f, 0.5f);
            m_Canvas.sizeDelta = new Vector2(width, height);
        }

        private void OnKeyboardInput(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.Alpha1:
                    {
                        m_HammerButton.Execute();
                        m_ShovelButton.Release();
                        m_BucketButton.Release();
                    }
                    break;

                case KeyCode.Alpha2:
                    {
                        m_ShovelButton.Execute();
                        m_HammerButton.Release();
                        m_BucketButton.Release();
                    }
                    break;

                case KeyCode.Alpha3:
                    {
                        m_BucketButton.Execute();
                        m_HammerButton.Release();
                        m_ShovelButton.Release();
                    }
                    break;
            }
        }

        private void Awake()
        {
            KeyboardInputManager.Instance.SetOnKeyboardInputListener(OnKeyboardInput);

            SetCanvas();
        }

        private void Start()
        {
            int stage = PlayerPrefsManager.Instance.GetStage() + 1;
            string stageText = "Stage " + stage;

            m_StageText.text = stageText;
        }
    }
}
