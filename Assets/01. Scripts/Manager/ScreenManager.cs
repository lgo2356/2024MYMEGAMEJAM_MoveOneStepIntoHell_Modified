using TeamJustFour.MoveOneStep.Module;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.Manager
{
    public class ScreenManager : Singleton<ScreenManager>
    {
        private static ScreenManager m_Instance;

        public int ScreenWidth
        { 
            get;
            private set;
        }

        public int ScreenHeight
        {
            get;
            private set;
        }

        public int PIXEL_PER_UNIT_X
        {
            get
            {
                return 96;
            }
        }

        public int PIXEL_PER_UNIT_Y
        {
            get
            {
                return 96;
            }
        }

        public void SetResolution(int width, int height)
        {
            Screen.SetResolution(width, height, true);
        }

        public void SetWindowed()
        {
            Screen.SetResolution(ScreenWidth, ScreenHeight, false);
        }

        public Resolution GetCurrentResolution()
        {
            Resolution resolution = Screen.currentResolution;
            //Resolution resolution = GetTestResolution();

            return resolution;
        }

        public Resolution GetTestResolution()
        {
            int width = Screen.width;
            int height = Screen.height;

            Resolution resolution = new()
            {
                width = width,
                height = height
            };

            return resolution;
        }

        private void InitScreen()
        {
            Resolution currentResolution = GetCurrentResolution();

            ScreenWidth = currentResolution.width;
            ScreenHeight = currentResolution.height;

            if (currentResolution.height != 1080)
            {
                SetResolution(currentResolution.width, currentResolution.height);
                SetWindowed();
            }
            else
            {
                SetResolution(currentResolution.width, currentResolution.height);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            InitScreen();
        }
    }
}
