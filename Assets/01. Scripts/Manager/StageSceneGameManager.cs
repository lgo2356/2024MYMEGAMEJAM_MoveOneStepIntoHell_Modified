using System.Collections;
using System.Collections.Generic;
using TeamJustFour.MoveOneStep.Module;
using UnityEngine;
using UnityEngine.UI;

namespace TeamJustFour.MoveOneStep.Manager
{
    public class StageSceneGameManager : Singleton<StageSceneGameManager>
    {
        public int CurrentStage { get; set; } = 0;

        public AudioClip bgmClip; // �������� ���� ���� BGM���� ����� ����� Ŭ��
        public AudioClip clickSound; // ���콺 Ŭ�� �� ����� ����� Ŭ��
        public Button startButton; // Start ��ư

        private AudioSource bgmSource; // BGM�� ����� AudioSource ������Ʈ
        private AudioSource soundSource; // ȿ������ ����� AudioSource ������Ʈ

        private float developmentBgmVolume = 0.1f; // ���� �߿��� ����� BGM ���� (0.0f ~ 1.0f)
        private float clickSoundVolume = 0.1f; // ���콺 Ŭ�� ���� ����

        void Start()
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

        void Update()
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

        private void OnStartButtonClick()
        {
            // BGM �ߴ�
            if (bgmSource.isPlaying)
            {
                bgmSource.Stop();
            }

            // �߰��� Start ��ư Ŭ�� �� ������ �۾��� ���⿡ �߰�
        }
    }
}