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

        public AudioClip bgmClip; // 스테이지 선택 씬의 BGM으로 사용할 오디오 클립
        public AudioClip clickSound; // 마우스 클릭 시 재생할 오디오 클립
        public Button startButton; // Start 버튼

        private AudioSource bgmSource; // BGM을 재생할 AudioSource 컴포넌트
        private AudioSource soundSource; // 효과음을 재생할 AudioSource 컴포넌트

        private float developmentBgmVolume = 0.1f; // 개발 중에만 사용할 BGM 볼륨 (0.0f ~ 1.0f)
        private float clickSoundVolume = 0.1f; // 마우스 클릭 사운드 볼륨

        void Start()
        {
            // BGM AudioSource 설정 및 재생
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.clip = bgmClip;
            bgmSource.loop = true; // 반복 재생 여부 설정
            bgmSource.volume = developmentBgmVolume; // 개발 중에만 설정한 볼륨으로 설정
            bgmSource.Play();

            // Effect AudioSource 설정
            soundSource = gameObject.AddComponent<AudioSource>();

            // Start 버튼 클릭 이벤트 등록
            if (startButton != null)
            {
                startButton.onClick.AddListener(OnStartButtonClick);
            }
        }

        void Update()
        {
            // 마우스 클릭 입력 감지
            if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 버튼 클릭
            {
                // 마우스 클릭 소리 재생
                if (clickSound != null)
                {
                    soundSource.PlayOneShot(clickSound, clickSoundVolume);
                }
            }
        }

        private void OnStartButtonClick()
        {
            // BGM 중단
            if (bgmSource.isPlaying)
            {
                bgmSource.Stop();
            }

            // 추가로 Start 버튼 클릭 시 수행할 작업을 여기에 추가
        }
    }
}