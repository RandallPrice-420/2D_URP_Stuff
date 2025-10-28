using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Assets.Scenes.Game2048.Scripts
{
    public class UIManager : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   OnButtonBackToMenuClicked
        //   OnButtonPlayAgainClicked
        //   OnButtonQuitClicked
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public static event Action OnButtonBackToMenuClicked  = delegate { };
        public static event Action OnButtonPlayAgainClicked = delegate { };
        public static event Action OnButtonQuitClicked      = delegate { };

        #endregion



        // ---------------------------------------------------------------------
        // Serialized Fields:
        // ------------------
        //   _buttonSprites
        //   _textGameState
        //   _panelLoseScreen
        //   _panelWinScreen
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        [SerializeField] private List<Sprite> _buttonSprites;
        [SerializeField] private TMP_Text     _textGameState;
        [SerializeField] private GameObject   _panelLoseScreen;
        [SerializeField] private GameObject   _panelWinScreen;

        #endregion




        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   InvokeEvent
        // ---------------------------------------------------------------------

        #region .  InvokeEvent()  .
        // ---------------------------------------------------------------------
        //   Method.......:  InvokeEvent()
        //   Description..:  
        //   Parameters...:  string
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void InvokeEvent(string eventName)
        {
            switch (eventName)
            {
                case "BackToMenu":
                    OnButtonBackToMenuClicked?.Invoke();
                    break;

                case "PlayAgain":
                    OnButtonPlayAgainClicked?.Invoke();
                    break;

                case "Quit":
                    OnButtonQuitClicked?.Invoke();
                    break;
            }

        }   // InvokeEvent()
        #endregion

 

        // -------------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   ChangeButtonState()  -- COMMENTED OUT
        //   OnDisable()
        //   OnEnable()
        //   LoseGame()
        //   WinGame()
        //   UpdateGameStateText()
        // -------------------------------------------------------------------------

        #region .  ChangeButtonState()  -- COMMENTED OUT  .
        //// ---------------------------------------------------------------------
        ////   Method.......:  ChangeButtonState()
        ////   Description..:  
        ////   Parameters...:  None
        ////   Returns......:  Nothing
        //// ---------------------------------------------------------------------
        //private void ChangeButtonState(Button button, bool state)
        //{
        //    switch (state)
        //    {
        //        case true:
        //            button.interactable = state;
        //            button.GetComponent<Image>().sprite = _buttonSprites[int.Parse(button.tag)];
        //            break;

        //        case false:
        //            button.interactable = state;
        //            button.GetComponent<Image>().sprite = _buttonSprites[0];
        //            break;
        //    }

        //}   // ChangeButtonState()
        #endregion


        #region .  OnDisable()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnDisable()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void OnDisable()
        {
            GameManager.OnLoseGame         -= LoseGame;
            GameManager.OnWinGame          -= WinGame;
            GameManager.OnGameStateChanged -= UpdateGameStateText;

        }   // OnDisable()
        #endregion


        #region .  OnEnable()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnEnable()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void OnEnable()
        {
            GameManager.OnLoseGame         += LoseGame;
            GameManager.OnWinGame          += WinGame;
            GameManager.OnGameStateChanged += UpdateGameStateText;

        }   // OnEnable()
        #endregion


        #region .  LoseGame()  .
        // ---------------------------------------------------------------------
        //   Method.......:  LoseGame()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void LoseGame()
        {
            _panelLoseScreen.SetActive(true);
            UpdateGameStateText(GameState.LoseGame);

        }   // LoseGame()
        #endregion


        #region .  WinGame()  .
        // ---------------------------------------------------------------------
        //   Method.......:  WinGame()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void WinGame()
        {
            _panelWinScreen.SetActive(true);
            UpdateGameStateText(GameState.WinGame);

        }   // WinGame()
        #endregion


        #region .  UpdateGameState()  .
        // -------------------------------------------------------------------------
        //   Method.......:  UpdateGameState()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void UpdateGameStateText(GameState state)
        {
            string color = (state == GameState.WaitingInput) ? "blue"
                         : (state == GameState.LoseGame    ) ? "red"
                         : (state == GameState.WinGame     ) ? "green"
                         : "black";

            _textGameState.text = $"Game State:  <color={color}><b>{state.ToString()}</b></color>";

        }   // UpdateGameState()
        #endregion


    }   // class UIManager

}   // namespace Assets.Scenes.Game2048.Scripts
