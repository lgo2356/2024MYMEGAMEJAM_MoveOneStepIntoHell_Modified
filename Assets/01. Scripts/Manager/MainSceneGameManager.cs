using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.Module
{
    public class MainSceneGameManager : Singleton<MainSceneGameManager>
    {
        public AudioClip bgmClip; // BGM���� ����� ����� Ŭ��
        public AudioClip upSound; // ���� ȭ��ǥ Ű�� ������ �� ����� ����� Ŭ��
        public AudioClip downSound; // �Ʒ��� ȭ��ǥ Ű�� ������ �� ����� ����� Ŭ��
        public AudioClip enterSound; // ���� Ű�� ������ �� ����� ����� Ŭ��
        public AudioClip clickSound; // ���콺 Ŭ�� �� ����� ����� Ŭ��

        private AudioSource bgmSource; // BGM�� ����� AudioSource ������Ʈ
        private AudioSource soundSource; // ȿ������ ����� AudioSource ������Ʈ

        private float developmentBgmVolume = 0.3f; // ���� �߿��� ����� BGM ���� (0.0f ~ 1.0f)
        private float developmentEffectVolume = 0.3f; // ���� �߿��� ����� ȿ���� ���� (0.0f ~ 1.0f)
        private float upSoundVolume = 0.6f; // ���� ȭ��ǥ Ű �Է� ���� ����
        private float downSoundVolume = 0.6f; // �Ʒ��� ȭ��ǥ Ű �Է� ���� ����
        private float enterSoundVolume = 0.6f; // ���� Ű �Է� ���� ����
        private float clickSoundVolume = 0.6f; // ���콺 Ŭ�� ���� ����

        protected override void Awake()
        {
            base.Awake();

            RemoveDontDestroyOnLoad();
        }

        void Start()
        {
            // MainCamera���� AudioSource ������Ʈ�� ã�� ������
            bgmSource = Camera.main.GetComponent<AudioSource>();

            if (bgmSource == null)
            {
                // AudioSource�� ������ �߰�
                bgmSource = Camera.main.gameObject.AddComponent<AudioSource>();
            }

            // BGM ���� �� ���
            bgmSource.clip = bgmClip;
            bgmSource.loop = true; // �ݺ� ��� ���� ����
            bgmSource.volume = developmentBgmVolume; // ���� �߿��� ������ �������� ����
            bgmSource.Play();

            // Effect AudioSource ����
            soundSource = gameObject.AddComponent<AudioSource>();
        }

        void Update()
        {
            // ���� ȭ��ǥ Ű �Է� ����
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // ���� ȭ��ǥ Ű �Ҹ� ���
                if (upSound != null)
                {
                    soundSource.PlayOneShot(upSound, upSoundVolume);
                }
            }

            // �Ʒ��� ȭ��ǥ Ű �Է� ����
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // �Ʒ��� ȭ��ǥ Ű �Ҹ� ���
                if (downSound != null)
                {
                    soundSource.PlayOneShot(downSound, downSoundVolume);
                }
            }

            // ���� Ű �Է� ����
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                // ���� Ű �Ҹ� ���
                if (enterSound != null)
                {
                    soundSource.PlayOneShot(enterSound, enterSoundVolume);
                }
            }

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