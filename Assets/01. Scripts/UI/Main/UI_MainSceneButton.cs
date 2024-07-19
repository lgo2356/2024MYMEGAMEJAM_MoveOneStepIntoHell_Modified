using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TeamJustFour.MoveOneStep.UI
{
    public abstract class UI_MainSceneButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private int Id;
        [SerializeField] private Image m_HighlightImage;

        private Action<UI_MainSceneButton> m_OnPointerEnterAction;
        private Action<UI_MainSceneButton> m_OnPointerExitAction;

        public abstract void Excute();

        public void SetHighlight(bool highlighted)
        {
            if (highlighted)
            {
                m_HighlightImage.gameObject.SetActive(true);
            }
            else
            {
                m_HighlightImage.gameObject.SetActive(false);
            }
        }

        public void SetOnPointerEnterListener(Action<UI_MainSceneButton> callback)
        {
            m_OnPointerEnterAction -= callback;
            m_OnPointerEnterAction += callback;
        }

        public void SetOnPointerExitListener(Action<UI_MainSceneButton> callback)
        {
            m_OnPointerExitAction -= callback;
            m_OnPointerExitAction += callback;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Excute();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetHighlight(true);

            m_OnPointerEnterAction?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetHighlight(false);

            m_OnPointerExitAction?.Invoke(this);
        }
    }
}
