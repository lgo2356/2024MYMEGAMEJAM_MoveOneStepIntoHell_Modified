using UnityEngine;
using UnityEngine.UI;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_StageSceneBackground : MonoBehaviour
    {
        [SerializeField] private int m_Page;

        public int Page => m_Page;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
