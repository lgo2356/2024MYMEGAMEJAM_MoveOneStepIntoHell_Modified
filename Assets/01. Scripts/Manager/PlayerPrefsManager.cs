using UnityEngine;

namespace TeamJustFour.MoveOneStep.Manager
{
    public class PlayerPrefsManager
    {
        private static PlayerPrefsManager m_Instance;

        public static PlayerPrefsManager Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new PlayerPrefsManager();
                }

                return m_Instance;
            }
        }

        public void SetStage(int stage)
        {
            PlayerPrefs.SetInt("Stage", stage);
        }

        public int GetStage()
        {
            int stage = PlayerPrefs.GetInt("Stage", -1);

            if (stage == -1)
                throw new System.Exception("Stage is not set yet.");

            return stage;
        }
    }
}
