using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scenes.Game2048.Scripts
{
    public class UIManager : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Serialized Fields:
        // ------------------
        //   _buttonContinue
        //   _buttonDisabled
        //   _buttonQuit
        //   _buttonSprites
        //   _textGameState
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        [SerializeField] private Button       _buttonContinue;
        [SerializeField] private Button       _buttonDisabled;
        [SerializeField] private Button       _buttonQuit;
        [SerializeField] private List<Sprite> _buttonSprites;
        [SerializeField] private TMP_Text     _textGameState;

        #endregion



        // -------------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   OnDisable()
        //   OnEnable()
        //   OnGameOver()
        //   OnUpdateGameState()
        // -------------------------------------------------------------------------

        #region .  ChangeButtonState()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ChangeButtonState()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void ChangeButtonState(Button button, bool state)
        {
            switch (state)
            {
                case true:
                    button.interactable = state;
                    button.GetComponent<Image>().sprite = _buttonSprites[int.Parse(button.tag)];
                    break;

                case false:
                    button.interactable = state;
                    button.GetComponent<Image>().sprite = _buttonSprites[0];
                    break;
            }

        }   // ChangeButtonState()
        #endregion


        #region .  OnGameOver()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnGameOver()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void OnGameOver()
        {
            ChangeButtonState(_buttonContinue, false);
            OnUpdateGameState(GameState.GameOver);

        }   // OnGameOver()
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
            GameManager.OnGameOver         -= OnGameOver;
            GameManager.OnGameStateChanged -= OnUpdateGameState;

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
            GameManager.OnGameOver         += OnGameOver;
            GameManager.OnGameStateChanged += OnUpdateGameState;

        }   // OnEnable()
        #endregion


        #region .  OnUpdateGameState()  .
        // -------------------------------------------------------------------------
        //   Method.......:  OnUpdateGameState()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void OnUpdateGameState(GameState state)
        {
            _textGameState.text = $"Game State:  {state.ToString()}";

            switch (state)
            {
                case GameState.SpawnBlocks:
                    ChangeButtonState(_buttonContinue, true);
                    ChangeButtonState(_buttonQuit, true);
                    break;
            }

        }   // OnUpdateGameState()
        #endregion


    }   // class UIManager

}   // namespace Assets.Scenes.Game2048.Scripts
