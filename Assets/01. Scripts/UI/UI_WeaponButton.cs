using System.Collections;
using System.Collections.Generic;
using TeamJustFour.MoveOneStep.Game;
using TeamJustFour.MoveOneStep.Module;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_WeaponButton : MonoBehaviour
    {
        [SerializeField] private Image m_NormalBackground;
        [SerializeField] private Image m_NormalIcon;
        [SerializeField] private Image m_HighlightImage;
        
        public WeaponType WeaponType;

        public void Execute()
        {
            SetHighlight(true);

            InGameSceneGameManager.Instance.Player.ChangeWeapon(WeaponType);
        }

        public void Release()
        {
            SetHighlight(false);
        }

        public void SetHighlight(bool highlighted)
        {
            m_NormalBackground.gameObject.SetActive(!highlighted);
            m_NormalIcon.gameObject.SetActive(!highlighted);

            m_HighlightImage.gameObject.SetActive(highlighted);
        }
    }
}
