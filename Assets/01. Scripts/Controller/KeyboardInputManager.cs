using System;
using TeamJustFour.MoveOneStep.Module;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.Controller
{
    public class KeyboardInputManager : Singleton<KeyboardInputManager>
    {
        private Action<KeyCode> m_OnKeyInput;

        public void SetOnKeyboardInputListener(Action<KeyCode> callback)
        {
            m_OnKeyInput -= callback;
            m_OnKeyInput += callback;
        }

        public void ReleaseKeyboardInputListener(Action<KeyCode> callback)
        {
            m_OnKeyInput -= callback;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_OnKeyInput?.Invoke(KeyCode.UpArrow);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                m_OnKeyInput?.Invoke(KeyCode.DownArrow);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                m_OnKeyInput?.Invoke(KeyCode.LeftArrow);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                m_OnKeyInput?.Invoke(KeyCode.RightArrow);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                m_OnKeyInput?.Invoke(KeyCode.Return);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                m_OnKeyInput?.Invoke(KeyCode.Escape);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                m_OnKeyInput?.Invoke(KeyCode.Alpha1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                m_OnKeyInput?.Invoke(KeyCode.Alpha2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                m_OnKeyInput?.Invoke(KeyCode.Alpha3);
            }
        }
    }
}
