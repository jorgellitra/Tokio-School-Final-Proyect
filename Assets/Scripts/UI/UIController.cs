using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TokioSchool.FinalProject.UI
{
    public class UIController : MonoBehaviour
    {
        [Header("Main Properties")]
        public UIPanel startScreen;

        [Header("Screen Options")]
        public Component[] screens = new Component[0];
        private UIPanel currentScreen;
        public UIPanel CurrentScreen { get { return currentScreen; } }
        private Stack<UIPanel> previousScreens;
        public Stack<UIPanel> PreviousScreens { get { return previousScreens; } }

        private UIController instance;
        public UIController Instance { get => instance; set => instance = value; }

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            previousScreens = new Stack<UIPanel>();
            screens = GetComponentsInChildren<UIPanel>(true);

            InitializeScreens();

            if (startScreen)
            {
                SwitchScreens(startScreen);
            }
        }

        public void SwitchScreens(UIPanel screen)
        {
            if (screen)
            {
                if (currentScreen)
                {
                    currentScreen.CloseScreen();
                    previousScreens.Push(currentScreen);
                }

                currentScreen = screen;
                currentScreen.StartScreen();
            }
        }

        public void GoToPreviousScreen()
        {
            if (previousScreens.Count > 0)
            {
                UIPanel previousScreen = previousScreens.Pop();
                currentScreen.CloseScreen();
                currentScreen = previousScreen;
                currentScreen.StartScreen();
            }
        }

        void InitializeScreens()
        {
            foreach (var screen in screens)
            {
                screen.gameObject.SetActive(false);
            }
        }
    }
}
