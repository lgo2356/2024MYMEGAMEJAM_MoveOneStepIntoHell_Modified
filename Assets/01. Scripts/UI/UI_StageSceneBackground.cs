using UnityEngine;
using UnityEngine.UI;

namespace TeamJustFour.MoveOneStep.UI
{
    public class UI_StageSceneBackground : MonoBehaviour
    {
        [SerializeField] private Image m_Image;

        private void Awake()
        {
            if (m_Image == null)
            {
                if (gameObject.TryGetComponent<Image>(out var image))
                {
                    m_Image = image;
                }
                else
                {
                    throw new System.Exception("Image component not found in GameObject.");
                }
            }
        }
    }
}
