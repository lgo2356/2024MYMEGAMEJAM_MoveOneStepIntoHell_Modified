using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.Module
{
    public class MainSceneGameManager : Singleton<MainSceneGameManager>
    {
        public AudioClip bgmClip; // BGM으로 사용할 오디오 클립
        public AudioClip upSound; // 위쪽 화살표 키를 눌렀을 때 재생할 오디오 클립
        public AudioClip downSound; // 아래쪽 화살표 키를 눌렀을 때 재생할 오디오 클립
        public AudioClip enterSound; // 엔터 키를 눌렀을 때 재생할 오디오 클립
        public AudioClip clickSound; // 마우스 클릭 시 재생할 오디오 클립

        private AudioSource bgmSource; // BGM을 재생할 AudioSource 컴포넌트
        private AudioSource soundSource; // 효과음을 재생할 AudioSource 컴포넌트

        private float developmentBgmVolume = 0.3f; // 개발 중에만 사용할 BGM 볼륨 (0.0f ~ 1.0f)
        private float developmentEffectVolume = 0.3f; // 개발 중에만 사용할 효과음 볼륨 (0.0f ~ 1.0f)
        private float upSoundVolume = 0.6f; // 위쪽 화살표 키 입력 사운드 볼륨
        private float downSoundVolume = 0.6f; // 아래쪽 화살표 키 입력 사운드 볼륨
        private float enterSoundVolume = 0.6f; // 엔터 키 입력 사운드 볼륨
        private float clickSoundVolume = 0.6f; // 마우스 클릭 사운드 볼륨

        protected override void Awake()
        {
            base.Awake();

            RemoveDontDestroyOnLoad();
        }

        void Start()
        {
            // MainCamera에서 AudioSource 컴포넌트를 찾아 가져옴
            bgmSource = Camera.main.GetComponent<AudioSource>();

            if (bgmSource == null)
            {
                // AudioSource가 없으면 추가
                bgmSource = Camera.main.gameObject.AddComponent<AudioSource>();
            }

            // BGM 설정 및 재생
            bgmSource.clip = bgmClip;
            bgmSource.loop = true; // 반복 재생 여부 설정
            bgmSource.volume = developmentBgmVolume; // 개발 중에만 설정한 볼륨으로 설정
            bgmSource.Play();

            // Effect AudioSource 설정
            soundSource = gameObject.AddComponent<AudioSource>();
        }

        void Update()
        {
            // 위쪽 화살표 키 입력 감지
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // 위쪽 화살표 키 소리 재생
                if (upSound != null)
                {
                    soundSource.PlayOneShot(upSound, upSoundVolume);
                }
            }

            // 아래쪽 화살표 키 입력 감지
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // 아래쪽 화살표 키 소리 재생
                if (downSound != null)
                {
                    soundSource.PlayOneShot(downSound, downSoundVolume);
                }
            }

            // 엔터 키 입력 감지
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                // 엔터 키 소리 재생
                if (enterSound != null)
                {
                    soundSource.PlayOneShot(enterSound, enterSoundVolume);
                }
            }

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
    }
}