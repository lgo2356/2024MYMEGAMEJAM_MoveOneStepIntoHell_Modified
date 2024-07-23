using DG.Tweening;
using System.Collections;
using TeamJustFour.MoveOneStep.Controller;
using TeamJustFour.MoveOneStep.Manager;
using TeamJustFour.MoveOneStep.Module;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.Game
{
    public enum WeaponType
    {
        Hammer,
        Shovel,
        Bucket,
    }

    public enum PlayerState
    {
        Idle,
        Move,
        Stun,
        Smash,
    }

    public class Player : MonoBehaviour
    {
        [SerializeField] private AudioClip m_MoveSound;
        [SerializeField] private AudioClip m_FailedSound;
        [SerializeField] private AudioClip m_TrySound;
        [SerializeField] private PlayerState m_CurrentState = PlayerState.Idle;

        private const int WIDTH = 14;
        private const int HEIGHT = 8;

        private Vector2 m_CurrentPosition;
        private Animator m_Anim;
        private Block m_NextBlock;

        public WeaponType CurrentWeaponType { get; private set; }

        public PlayerState CurrentState 
        { 
            get
            {
                return m_CurrentState;
            }

            private set
            {
                m_CurrentState = value;
            }
        }

        public void SetPosition(int x, int y, bool immediately = false)
        {
            int screenWidth = ScreenManager.Instance.GetCurrentResolution().width / 2;
            int screenHeight = ScreenManager.Instance.GetCurrentResolution().height / 2;

            int pixelPerUnitX = ScreenManager.Instance.PIXEL_PER_UNIT_X;
            int pixelPerUnitY = ScreenManager.Instance.PIXEL_PER_UNIT_Y;

            int xPos = screenWidth - WIDTH * pixelPerUnitX / 2;
            int yPos = screenHeight - HEIGHT * pixelPerUnitY / 2;
            Vector3 vector = new(x * pixelPerUnitX + xPos, (HEIGHT - y - 1) * pixelPerUnitY + yPos);

            if (immediately)
            {
                transform.position = vector;
                m_CurrentPosition = new Vector2(x, y);
            }
            else
            {
                switch (CurrentWeaponType)
                {
                    case WeaponType.Hammer:
                        {
                            m_Anim.SetTrigger("doMoveHammer");
                        }
                        break;

                    case WeaponType.Shovel:
                        {
                            m_Anim.SetTrigger("doMoveShovel");
                        }
                        break;

                    case WeaponType.Bucket:
                        {
                            m_Anim.SetTrigger("doMoveBucket");
                        }
                        break;
                }

                transform.DOMove(vector, 0.2f)
                    .SetEase(Ease.InSine)
                    .OnComplete(() =>
                    {
                        transform.position = vector;
                        m_CurrentPosition = new Vector2(x, y);
                    })
                    .Play();
            }
        }

        public void ChangeWeapon(WeaponType weaponType)
        {
            CurrentWeaponType = weaponType;

            switch (weaponType)
            {
                case WeaponType.Hammer:
                    {
                        m_Anim.SetTrigger("doIdleHammer");
                    }
                    break;

                case WeaponType.Shovel:
                    {
                        m_Anim.SetTrigger("doIdleShovel");
                    }
                    break;

                case WeaponType.Bucket:
                    {
                        m_Anim.SetTrigger("doIdleBucket");
                    }
                    break;
            }
        }

        public void ChangeState(PlayerState state)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            CurrentState = state;

            if (state == PlayerState.Stun)
            {
                audioSource.clip = m_FailedSound;
                audioSource.Play();

                switch (CurrentWeaponType)
                {
                    case WeaponType.Hammer:
                        {
                            m_Anim.SetBool("isStunHammer", true);
                        }
                        break;

                    case WeaponType.Shovel:
                        {
                            m_Anim.SetBool("isStunShovel", true);
                        }
                        break;

                    case WeaponType.Bucket:
                        {
                            m_Anim.SetBool("isStunBucket", true);
                        }
                        break;
                }

                StartCoroutine(StunCoroutine(1.0f));
            }
            else if (state == PlayerState.Move)
            {
                audioSource.clip = m_MoveSound;
                audioSource.Play();
            }
        }

        public void ReleaseReferences()
        {
            KeyboardInputManager.Instance.ReleaseKeyboardInputListener(OnKeyboardInput);
        }

        private IEnumerator StunCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);

            switch (CurrentWeaponType)
            {
                case WeaponType.Hammer:
                    {
                        m_Anim.SetBool("isStunHammer", false);
                    }
                    break;

                case WeaponType.Shovel:
                    {
                        m_Anim.SetBool("isStunShovel", false);
                    }
                    break;

                case WeaponType.Bucket:
                    {
                        m_Anim.SetBool("isStunBucket", false);
                    }
                    break;
            }
            
            ChangeState(PlayerState.Idle);
        }

        private IEnumerator TryMoveOrSmashBlock(int nextX, int nextY)
        {
            Block nextBlock = InGameSceneGameManager.Instance.GetBlock(nextX, nextY);

            if (nextBlock != null)
            {
                if (nextBlock.CanDestroy)
                {
                    if ((int)nextBlock.BlockType == (int)CurrentWeaponType)
                    {
                        switch (CurrentWeaponType)
                        {
                            case WeaponType.Hammer:
                                {
                                    m_Anim.SetTrigger("doClearHammer");
                                }
                                break;

                            case WeaponType.Shovel:
                                {
                                    m_Anim.SetTrigger("doClearShovel");
                                }
                                break;

                            case WeaponType.Bucket:
                                {
                                    m_Anim.SetTrigger("doClearBucket");
                                }
                                break;
                        }

                        InGameSceneGameManager.Instance.RemoveBlockCoordinate(nextX, nextY);
                        m_NextBlock = nextBlock;
                    }
                    else
                    {
                        ChangeState(PlayerState.Stun);

                        InGameSceneGameManager.Instance.UIRoot.ShowSystemMessage();

                        Debug.Log("³Ñ¾îÁü!");
                    }
                }
                else
                {
                    Debug.Log("Can't destroy");
                }
            }
            else
            {
                if (InGameSceneGameManager.Instance.CanMove(nextX, nextY))
                {
                    SetPosition(nextX, nextY);
                }
            }

            yield return null;
        }

        private void OnStartSmash()
        {
            ChangeState(PlayerState.Smash);
        }

        private void OnSmash()
        {
            m_NextBlock.Destory();
        }

        private void OnEndSmash()
        {
            ChangeState(PlayerState.Idle);
        }

        private void OnStartMove()
        {
            ChangeState(PlayerState.Move);
        }

        private void OnEndMove()
        {
            ChangeState(PlayerState.Idle);
        }

        private float m_KeyboardInputTimer = 0f;

        private IEnumerator KeyboardInputTimerCoroutine(float timer)
        {
            while (true)
            {
                m_KeyboardInputTimer += Time.deltaTime;

                if (m_KeyboardInputTimer >= timer)
                {
                    m_KeyboardInputTimer = 0f;
                    break;
                }

                yield return null;
            }
        }

        private void OnKeyboardInput(KeyCode keyCode)
        {
            if (CurrentState != PlayerState.Idle)
            {
                return;
            }

            if (m_KeyboardInputTimer != 0f)
            {
                return;
            }

            StartCoroutine(KeyboardInputTimerCoroutine(0.2f));

            switch (keyCode)
            {
                case KeyCode.UpArrow:
                    {
                        int nextX = (int)m_CurrentPosition.x;
                        int nextY = (int)m_CurrentPosition.y - 1;

                        StartCoroutine(TryMoveOrSmashBlock(nextX, nextY));
                    }
                    break;

                case KeyCode.DownArrow:
                    {
                        int nextX = (int)m_CurrentPosition.x;
                        int nextY = (int)m_CurrentPosition.y + 1;

                        StartCoroutine(TryMoveOrSmashBlock(nextX, nextY));
                    }
                    break;

                case KeyCode.LeftArrow:
                    {
                        int nextX = (int)m_CurrentPosition.x - 1;
                        int nextY = (int)m_CurrentPosition.y;

                        StartCoroutine(TryMoveOrSmashBlock(nextX, nextY));
                    }
                    break;

                case KeyCode.RightArrow:
                    {
                        int nextX = (int)m_CurrentPosition.x + 1;
                        int nextY = (int)m_CurrentPosition.y;

                        StartCoroutine(TryMoveOrSmashBlock(nextX, nextY));
                    }
                    break;
            }
        }

        private void Awake()
        {
            KeyboardInputManager.Instance.SetOnKeyboardInputListener(OnKeyboardInput);

            m_Anim = gameObject.GetComponent<Animator>();

            PlayerAnimEvent animEvt = gameObject.GetComponent<PlayerAnimEvent>();
            animEvt.SetStartSmashAnimEventListener(OnStartSmash);
            animEvt.SetOnSmashAnimEventListener(OnSmash);
            animEvt.SetEndSmashAnimEventListener(OnEndSmash);
            animEvt.SetStartMoveAnimEventListener(OnStartMove);
            animEvt.SetEndMoveAnimEventListener(OnEndMove);
        }

        private void Start()
        {
            ChangeWeapon(WeaponType.Hammer);
            ChangeState(PlayerState.Idle);
        }

        private void OnDisable()
        {
            ReleaseReferences();
        }
    }
}
