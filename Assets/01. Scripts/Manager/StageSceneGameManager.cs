using TeamJustFour.MoveOneStep.Module;
using TeamJustFour.MoveOneStep.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TeamJustFour.MoveOneStep.Manager
{
    public class StageSceneGameManager : Singleton<StageSceneGameManager>
    {
        [SerializeField] private UI_StageSceneRoot m_UIRoot;

        public AudioClip bgmClip; // �������� ���� ���� BGM���� ����� ����� Ŭ��
        public AudioClip clickSound; // ���콺 Ŭ�� �� ����� ����� Ŭ��
        public Button startButton; // Start ��ư

        private AudioSource bgmSource; // BGM�� ����� AudioSource ������Ʈ
        private AudioSource soundSource; // ȿ������ ����� AudioSource ������Ʈ

        private float developmentBgmVolume = 0.1f; // ���� �߿��� ����� BGM ���� (0.0f ~ 1.0f)
        private float clickSoundVolume = 0.1f; // ���콺 Ŭ�� ���� ����

        public void ReleaseReferences()
        {
            m_UIRoot.ReleaseReferences();
        }

        private void OnStartButtonClick()
        {
            // BGM �ߴ�
            if (bgmSource.isPlaying)
            {
                bgmSource.Stop();
            }

            // �߰��� Start ��ư Ŭ�� �� ������ �۾��� ���⿡ �߰�
        }

        protected override void Awake()
        {
            base.Awake();

            RemoveDontDestroyOnLoad();
        }

        private void Start()
        {
            // BGM AudioSource ���� �� ���
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.clip = bgmClip;
            bgmSource.loop = true; // �ݺ� ��� ���� ����
            bgmSource.volume = developmentBgmVolume; // ���� �߿��� ������ �������� ����
            bgmSource.Play();

            // Effect AudioSource ����
            soundSource = gameObject.AddComponent<AudioSource>();

            // Start ��ư Ŭ�� �̺�Ʈ ���
            if (startButton != null)
            {
                startButton.onClick.AddListener(OnStartButtonClick);
            }
        }

        private void OnEnable()
        {
            Debug.Log("StageSceneGameManager is enabled.");

            m_UIRoot.ShowGuidePopup();
        }

        private void Update()
        {
            // ���콺 Ŭ�� �Է� ����
            if (Input.GetMouseButtonDown(0)) // ���� ���콺 ��ư Ŭ��
            {
                // ���콺 Ŭ�� �Ҹ� ���
                if (clickSound != null)
                {
                    soundSource.PlayOneShot(clickSound, clickSoundVolume);
                }
            }
        }
    }
}