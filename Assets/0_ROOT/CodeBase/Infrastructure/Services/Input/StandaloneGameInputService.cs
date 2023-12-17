using Infrastructure.InputHandlers;

namespace Infrastructure.Services.Input
{
    public class StandaloneGameInputService : IGameInputService
    {
        public StandaloneGameInputService(GameInputHandlersCollection gameInputHandlersCollection)
        {
            m_standaloneGameInputHandler = gameInputHandlersCollection.GetStandalone();
        }

        private readonly StandaloneGameInputHandler m_standaloneGameInputHandler;
        
        public float XInputDirection => m_standaloneGameInputHandler.GetXInputDirection();
    }
}