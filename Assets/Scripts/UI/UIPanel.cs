using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TokioSchool.FinalProject.UI
{
    public class UIPanel : MonoBehaviour
    {
        public Selectable s_StartSelectable;

        public UnityEvent onScreenStart = new UnityEvent();
        public UnityEvent onScreenClose = new UnityEvent();

        void Start()
        {
            if (s_StartSelectable)
            {
                EventSystem.current.SetSelectedGameObject(s_StartSelectable.gameObject);
            }
        }

        public virtual void StartScreen()
        {
            if (onScreenStart != null)
            {
                onScreenStart.Invoke();
            }
            gameObject.SetActive(true);
        }

        public virtual void CloseScreen()
        {
            if (onScreenClose != null)
            {
                onScreenClose.Invoke();
            }
            gameObject.SetActive(false);
        }
    }
}
