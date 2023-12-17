using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.InputHandlers
{
    public sealed class GameInputHandlersCollection : MonoBehaviour
    {
        [SerializeField] private MobileSwipeGameInputHandler m_mobileSwipeGameInputHandler;
        [SerializeField] private StandaloneGameInputHandler m_standaloneInputHandler;
        
        private readonly List<IGameInputHandler> m_handlers = new();

        public MobileSwipeGameInputHandler GetMobile()
        {
            AddHandlerInUse(m_mobileSwipeGameInputHandler);
            return m_mobileSwipeGameInputHandler;
        }

        public StandaloneGameInputHandler GetStandalone()
        {
            AddHandlerInUse(m_standaloneInputHandler);
            return m_standaloneInputHandler;
        }
        
        public void EnableInput()
        {
            foreach (IGameInputHandler handler in m_handlers) 
                handler.Enable();
        }
        
        ////We can use this, for example, if player has been died 
        public void DisableInput()
        {
            foreach (IGameInputHandler handler in m_handlers) 
                handler.Disable();
        }

        private void AddHandlerInUse(IGameInputHandler usedHandler)
        {
            if(m_handlers.Contains(usedHandler))
                return;
            
            m_handlers.Add(usedHandler);
        }
    }
}
