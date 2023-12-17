using Infrastructure.InputHandlers;

namespace Infrastructure.Services.Input
{
    public sealed class MobileGameInputService : IGameInputService
    {
        public MobileGameInputService(GameInputHandlersCollection gameInputHandlersCollection)
        {
            m_mobileSwipeGameInputHandler = gameInputHandlersCollection.GetMobile();
        }

        private readonly MobileSwipeGameInputHandler m_mobileSwipeGameInputHandler;

        public float XInputDirection => m_mobileSwipeGameInputHandler.GetXInputDirection();
    }
}