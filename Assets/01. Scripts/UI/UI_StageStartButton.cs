using TeamJustFour.MoveOneStep.Module;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_StageStartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] private Image m_Image;
        [SerializeField] private Sprite m_PointerDownSprite;
        [SerializeField] private Sprite m_PointerUpSprite;

        public void OnPointerClick(PointerEventData eventData)
        {
            InGameSceneLoader.Instance.Load();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_Image.sprite = m_PointerDownSprite;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_Image.sprite = m_PointerUpSprite;
        }
    }
}
