using UnityEngine;

namespace Infrastructure.InputHandlers
{
    public class StandaloneGameInputHandler : MonoBehaviour, IGameInputHandler
    {
        private bool m_isLeftPressed;
        private bool m_isRightPressed;

        private bool m_enabled = true;

        private float m_xInputDirection;
        
        public void Enable() => 
            m_enabled = true;

        public void Disable() => 
            m_enabled = false;

        public float GetXInputDirection() 
            => m_xInputDirection;

        private void Update()
        {
            float tempInputDirection = 0;
            if(!m_enabled)
                return;
            
            //Press check
            if (Input.GetKey(KeyCode.A)) 
                tempInputDirection -= 1.0f;
            if (Input.GetKey(KeyCode.D)) 
                tempInputDirection += 1.0f;

            m_xInputDirection = tempInputDirection;
            //Debug.Log(m_xInputDirection + " xInp");
        }
    }
}