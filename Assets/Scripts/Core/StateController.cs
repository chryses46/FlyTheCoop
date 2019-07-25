using UnityEngine;

namespace FlyTheCoop.Core
{
    public class StateController : MonoBehaviour
    {
#region GameMode
            //Enum for game mode
            public enum GameMode
            {
                Normal,
                Hard
            }
            private GameMode _gameMode;
            public GameMode CurrentGameMode
            {
                get{ return _gameMode; }
                set{ _gameMode = value; } 
            }
#endregion
#region GameState
            //Enum for gameState
            public enum GameState
            {
                Menu,
                Play,
                Pause
            }
            private GameState _gameState;
            public GameState CurrentGameState
            {
                get{ return _gameState;}
                set{ _gameState = value;}
            }
#endregion
    }
}
