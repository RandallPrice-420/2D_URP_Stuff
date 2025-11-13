using System;
using UnityEngine;


namespace Assets.Scenes.Game2048.Scripts.Managers
{
    public class EventManager : MonoBehaviour
    {
        // -------------------------------------------------------------------------
        // Public Static Events:
        // ---------------------
        //   OnBestScoreChanged
        //   OnGameOver
        //   OnGameStateChanged
        //   OnMovesChanged
        //   OnScoreChanged
        //   OnWinConditionChanged
        // -------------------------------------------------------------------------

        #region .  Public Static Events  .

        // GameManager
        public static event Action<int>          OnBestScoreChanged    = delegate { };
        public static event Action<GameState>    OnGameOver            = delegate { };
        public static event Action<GameState>    OnGameStateChanged    = delegate { };
        public static event Action<int>          OnMovesChanged        = delegate { };
        public static event Action<int>          OnScoreChanged        = delegate { };
        public static event Action<WinCondition> OnWinConditionChanged = delegate { };

        #endregion



        // -------------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   RaiseOnBestScoreChanged()
        //   RaiseOnGameOver()
        //   RaiseOnGameStateChanged()
        //   RaiseOnMovesChanged()
        //   RaiseOnScoreChanged()
        //   RaiseOnWinConditionChanged()
        // -------------------------------------------------------------------------


        #region .  RaiseOnBestScoreChanged()  .
        // -------------------------------------------------------------------------
        //   Method.......:  RaiseOnBestScoreChanged()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        public static void RaiseOnBestScoreChanged(int score)
        {
            OnBestScoreChanged?.Invoke(score);

        }   // RaiseOnBestScoreChanged()
        #endregion


        #region .  RaiseOnGameOver()  .
        // -------------------------------------------------------------------------
        //   Method.......:  RaiseOnGameOver()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        public static void RaiseOnGameOver(GameState state)
        {
            OnGameOver?.Invoke(state);

        }   // RaiseOnGameOver()
        #endregion


        #region .  RaiseOnGameStateChanged()  .
        // -------------------------------------------------------------------------
        //   Method.......:  RaiseOnGameStateChanged()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        public static void RaiseOnGameStateChanged(GameState state)
        {
            OnGameStateChanged?.Invoke(state);

        }   // RaiseOnGameORaiseOnGameStateChangedver()
        #endregion


        #region .  RaiseOnMovesChanged()  .
        // -------------------------------------------------------------------------
        //   Method.......:  RaiseOnMovesChanged()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        public static void RaiseOnMovesChanged(int moves)
        {
            OnMovesChanged?.Invoke(moves);

        }   // RaiseOnMovesChanged()
        #endregion


        #region .  RaiseOnScoreChanged()  .
        // -------------------------------------------------------------------------
        //   Method.......:  RaiseOnScoreChanged()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        public static void RaiseOnScoreChanged(int score)
        {
            OnScoreChanged?.Invoke(score);

        }   // RaiseOnScoreChanged()
        #endregion


        #region .  RaiseOnWinConditionChanged()  .
        // -------------------------------------------------------------------------
        //   Method.......:  RaiseOnWinConditionChanged()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        public static void RaiseOnWinConditionChanged(WinCondition value)
        {
            OnWinConditionChanged?.Invoke(value);

        }   // RaiseOnWinConditionChanged()
        #endregion


    }   // class EventManager

}   // namespace Assets.Scenes.Game2048.Scripts
