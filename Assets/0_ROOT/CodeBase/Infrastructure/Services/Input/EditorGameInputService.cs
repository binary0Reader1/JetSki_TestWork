using Infrastructure.InputHandlers;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public sealed class EditorGameInputService : IGameInputService
    {
        public EditorGameInputService(GameInputHandlersCollection
            gameInputHandlersCollection)
        {
            m_mobileSwipeGameInputHandler = gameInputHandlersCollection.GetMobile();
            m_standaloneGameInputHandler = gameInputHandlersCollection.GetStandalone();
        }

        private readonly MobileSwipeGameInputHandler m_mobileSwipeGameInputHandler;
        private readonly StandaloneGameInputHandler m_standaloneGameInputHandler;

        public float XInputDirection => Mathf.Clamp(m_mobileSwipeGameInputHandler.GetXInputDirection() +
                                                    m_standaloneGameInputHandler.GetXInputDirection(), -1, 1);
    }
}