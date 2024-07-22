using System;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.Game
{
    public class PlayerAnimEvent : MonoBehaviour
    {
        private Action m_StartSmashAnimAction;
        private Action m_OnSmashAnimAction;
        private Action m_EndSmashAnimAction;
        private Action m_StartMoveAnimAction;
        private Action m_EndMoveAnimAction;

        public void SetStartSmashAnimEventListener(Action callback)
        {
            m_StartSmashAnimAction -= callback;
            m_StartSmashAnimAction += callback;
        }

        public void SetOnSmashAnimEventListener(Action callback)
        {
            m_OnSmashAnimAction -= callback;
            m_OnSmashAnimAction += callback;
        }

        public void SetEndSmashAnimEventListener(Action callback)
        {
            m_EndSmashAnimAction -= callback;
            m_EndSmashAnimAction += callback;
        }

        public void SetStartMoveAnimEventListener(Action callback)
        {
            m_StartMoveAnimAction -= callback;
            m_StartMoveAnimAction += callback;
        }

        public void SetEndMoveAnimEventListener(Action callback)
        {
            m_EndMoveAnimAction -= callback;
            m_EndMoveAnimAction += callback;
        }

        public void StartMove()
        {
            m_StartMoveAnimAction?.Invoke();
        }

        public void EndMove()
        {
            m_EndMoveAnimAction?.Invoke();
        }

        public void StartSmash()
        {
            m_StartSmashAnimAction?.Invoke();
        }

        public void OnSmash()
        {
            m_OnSmashAnimAction?.Invoke();
        }

        public void EndSmash()
        {
            m_EndSmashAnimAction?.Invoke();
        }
    }
}
