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
        private int m_CurrentPage = 0;

        public void SlideLeft()
        {
            if (m_CurrentPage == 0)
            {
                return;
            }

            m_CurrentPage--;

            float positionX = transform.position.x + 1920f;

            if (m_CurrentTween != null && m_CurrentTween.IsActive())
            {
                return;
            }

            m_CurrentTween = DOTween.Sequence()
                .OnStart(() =>
                {
                    m_Backgrounds[m_CurrentPage].gameObject.SetActive(true);
                })
                .Append(transform.DOMoveX(positionX, 0.5f))
                .OnComplete(() =>
                {
                    transform.position = new(positionX, transform.position.y);

                    for (int i = 0; i < m_Backgrounds.Length; i++)
                    {
                        if (i != m_CurrentPage)
                        {
                            m_Backgrounds[i].gameObject.SetActive(false);
                        }
                    }

                    m_OnCompleteSlide?.Invoke();
                })
                .Play();
        }

        public void SlideRight()
        {
            if (m_CurrentPage == 2)
            {
                return;
            }

            m_CurrentPage++;

            float positionX = transform.position.x - 1920f;

            if (m_CurrentTween != null && m_CurrentTween.IsActive())
            {
                return;
            }

            m_CurrentTween = DOTween.Sequence()
                .OnStart(() =>
                {
                    m_Backgrounds[m_CurrentPage].gameObject.SetActive(true);
                })
                .Append(transform.DOMoveX(positionX, 0.5f))
                .OnComplete(() =>
                {
                    transform.position = new(positionX, transform.position.y);

                    for (int i = 0; i < m_Backgrounds.Length; i++)
                    {
                        if (i != m_CurrentPage)
                        {
                            m_Backgrounds[i].gameObject.SetActive(false);
                        }
                    }

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
            return m_CurrentPage > 0;
        }

        public bool CanSlideRight()
        {
            return m_CurrentPage < 2;
        }

        private void Awake()
        {
            if (m_Backgrounds == null || m_Backgrounds.Length == 0)
            {
                m_Backgrounds = GetComponentsInChildren<UI_StageSceneBackground>();
            }
        }
    }
}
