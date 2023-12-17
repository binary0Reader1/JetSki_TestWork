namespace Infrastructure.InputHandlers
{
    public interface IGameInputHandler
    {
        public void Enable();
        public void Disable();

        public float GetXInputDirection();
    }
}