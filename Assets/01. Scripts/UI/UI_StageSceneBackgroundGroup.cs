using DG.Tweening;
using System;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_StageSceneBackgroundGroup : MonoBehaviour
    {
        [SerializeField] private UI_StageSceneBackground[] m_Backgrounds;

        private Action m_OnCompleteSlide;
        private Tween m_CurrentTween;
        private float m_CurrentPosition;

        public void SlideLeft()
        {
            float positionX = transform.position.x + 1920f;

            if (m_CurrentTween != null && m_CurrentTween.IsActive())
            {
                return;
            }
            
            m_CurrentTween = DOTween.Sequence()
                .Append(transform.DOMoveX(positionX, 0.5f))
                .OnComplete(() =>
                {
                    transform.position = new(positionX, transform.position.y);

                    m_CurrentPosition = transform.position.x;

                    m_OnCompleteSlide?.Invoke();
                })
                .Play();
        }

        public void SlideRight()
        {
            float positionX = transform.position.x - 1920f;

            if (m_CurrentTween != null && m_CurrentTween.IsActive())
            {
                return;
            }

            m_CurrentTween = DOTween.Sequence()
                .Append(transform.DOMoveX(positionX, 0.5f))
                .OnComplete(() =>
                {
                    transform.position = new(positionX, transform.position.y);

                    m_CurrentPosition = transform.position.x;

                    m_OnCompleteSlide?.Invoke();
                })
                .Play();
        }

        public void SetOnCompleteSlideListener(Action callback)
        {
            m_OnCompleteSlide -= callback;
            m_OnCompleteSlide += callback;
        }

        public bool CanSlideLeft()
        {
            return m_CurrentPosition < 0f;
        }

        public bool CanSlideRight()
        {
            int value = -(1920 * (transform.childCount - 1));

            return m_CurrentPosition > value;
        }

        private void Awake()
        {
            m_Backgrounds = new UI_StageSceneBackground[transform.childCount];
        }
    }
}
