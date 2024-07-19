using TeamJustFour.MoveOneStep.Algorithm;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_MainSceneButtonGroup : MonoBehaviour
    {
        private CircularList<UI_MainSceneButton> m_Buttons;

        public void NextButton()
        {
            UI_MainSceneButton curButton = m_Buttons.Current();
            curButton.SetHighlight(false);

            UI_MainSceneButton nextButton = m_Buttons.Next();
            nextButton.SetHighlight(true);
        }

        public void PrevButton()
        {
            UI_MainSceneButton curButton = m_Buttons.Current();
            curButton.SetHighlight(false);

            UI_MainSceneButton prevButton = m_Buttons.Previous();
            prevButton.SetHighlight(true);
        }

        public void ExcuteCurrentButton()
        {
            UI_MainSceneButton curButton = m_Buttons.Current();
            curButton.Excute();
        }

        private void Init()
        {
            m_Buttons = new();

            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out UI_MainSceneButton button))
                {
                    button.SetHighlight(false);
                    button.SetOnPointerEnterListener(OnPointerEnter);
                    button.SetOnPointerExitListener(OnPointerExit);

                    m_Buttons.Add(button);
                }
            }

            UI_MainSceneButton curButton = m_Buttons.Current();
            curButton.SetHighlight(true);
        }

        private void OnPointerEnter(UI_MainSceneButton highlightButton)
        {
            foreach (var button in m_Buttons)
            {
                if (button != highlightButton)
                {
                    button.SetHighlight(false);
                }
            }
        }

        private void OnPointerExit(UI_MainSceneButton highlightButton) 
        {

        }

        private void Awake()
        {
            Init();
        }
    }
}
