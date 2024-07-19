using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_SystemMessage : MonoBehaviour
    {
        public void Show()
        {
            transform.DOMoveY(transform.position.y - 160, 0.5f)
                .SetEase(Ease.OutBounce)
                .Play();

            StartCoroutine(ShowCoroutine());
        }

        public void Hide()
        {
            transform.DOMoveY(transform.position.y + 160, 0.5f)
                .SetEase(Ease.OutBounce)
                .Play();
        }

        private IEnumerator ShowCoroutine()
        {
            yield return new WaitForSeconds(2.0f);

            Hide();
        }
    }
}
