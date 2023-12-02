// Handles the current state of the game

namespace Engine
{
    public static class StateManager
    {
        private static GameState gameState;

        public static void SetState(GameState state)
        {
            gameState = state;
        }

        public static GameState GetState()
        {
            return gameState;
        }
    }
}