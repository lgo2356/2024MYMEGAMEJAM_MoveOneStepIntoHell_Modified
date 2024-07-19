using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TeamJustFour.MoveOneStep.Game
{
    public enum BlockType
    {
        Block1,
        Block2,
        Block3,
    }

    public class Block : MonoBehaviour
    {
        [SerializeField] private GameObject m_DestroyFX;
        [SerializeField] private Transform m_Parent;

        public bool CanDestroy = true;
        public BlockType BlockType;

        public void Destory()
        {
            Debug.Log("Block has been destroyed!");

            //StartCoroutine(DestroyFXCoroutine(1.0f));

            GetComponent<Image>().enabled = false;

            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }

        private IEnumerator DestroyFXCoroutine(float duration)
        {
            GameObject instance = Instantiate(m_DestroyFX, m_Parent);
            instance.transform.position = transform.position;

            yield return new WaitForSeconds(duration);

            Destroy(instance);
            Destroy(gameObject);
        }

        private void Awake()
        {
            m_Parent = GameObject.Find("[ World ]").transform;
        }
    }
}
