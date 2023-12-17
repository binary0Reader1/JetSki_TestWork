using Canvas;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Infrastructure.InputHandlers
{
    public class MobileSwipeGameInputHandler : MonoBehaviour, IGameInputHandler
    {
        [Inject]
        public void Construct(TouchPanel touchPanel)
        {
            touchPanel.OnDragActions += OnDrag;
            
            touchPanel.OnEndDragActions +=
                _ => m_xInputDirection = 0.0f;
        }

        private const float SWIPE_SENS_DIVIDER = 200.0f;
        private float m_xInputDirection;
        private bool m_enabled = true;

        public void Enable() =>
            m_enabled = true;

        public void Disable() =>
            m_enabled = false;
        
        public float GetXInputDirection()
            => m_xInputDirection;

        private void OnDrag(PointerEventData eventData)
        {
            if (!m_enabled)
            {
                m_xInputDirection = 0.0f;
                return;
            }
            
            m_xInputDirection = Mathf.Clamp(m_xInputDirection + eventData.delta.x / SWIPE_SENS_DIVIDER, -1,1);
        }
    }
}