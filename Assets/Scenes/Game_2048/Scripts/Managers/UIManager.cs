using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Assets.Scenes.Game2048.Scripts
{
    public class UIManager : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Serialized Fields:
        // ------------------
        //   _buttonSprites
        //   _panelGameOver
        //   _textBestValue
        //   _textGameOverLabel
        //   _textGameStateLabel
        //   _textMovesValue
        //   _textScoreValue
        //   _textWinConditionValue
        //   _textYouWinLabel
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        [SerializeField] private List<Sprite> _buttonSprites;
        [SerializeField] private GameObject   _panelGameOver;
        [SerializeField] private TMP_Text     _textBestValue;
        [SerializeField] private TMP_Text     _textGameOverLabel;
        [SerializeField] private TMP_Text     _textGameStateLabel;
        [SerializeField] private TMP_Text     _textMovesValue;
        [SerializeField] private TMP_Text     _textScoreValue;
        [SerializeField] private TMP_Text     _textWinConditionValue;
        [SerializeField] private TMP_Text     _textYouWinLabel;

        #endregion



        // -------------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   GameOver()
        //   OnDisable()
        //   OnEnable()
        //   UpdateGameStateText()
        //   UpdateMovesText()
        //   UpdateScoreText()
        //   UpdateWinText()
        //   UpdateWinConditionText()
        // -------------------------------------------------------------------------

        #region .  GameOver()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GameOver()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void GameOver(GameState state)
        {
            _panelGameOver.SetActive(true);
            _textGameOverLabel.gameObject.SetActive(state == GameState.LoseGame);
            _textYouWinLabel  .gameObject.SetActive(state == GameState.WinGame);

            UpdateGameStateText(state);

        }   // GameOver()
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
            GameManager.OnGameOver            -= GameOver;
            GameManager.OnBestScoreChanged    -= UpdateBestText;
            GameManager.OnGameStateChanged    -= UpdateGameStateText;
            GameManager.OnMoveChanged         -= UpdateMovesText;
            GameManager.OnScoreChanged        -= UpdateScoreText;
            GameManager.OnWinConditionChanged -= UpdateWinConditionText;

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
            GameManager.OnGameOver            += GameOver;
            GameManager.OnBestScoreChanged    += UpdateBestText;
            GameManager.OnGameStateChanged    += UpdateGameStateText;
            GameManager.OnMoveChanged         += UpdateMovesText;
            GameManager.OnScoreChanged        += UpdateScoreText;
            GameManager.OnWinConditionChanged += UpdateWinConditionText;

        }   // OnEnable()
        #endregion


        #region .  UpdateBestText()  .
        // -------------------------------------------------------------------------
        //   Method.......:  UpdateBestText()
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void UpdateBestText(int value)
        {
            _textBestValue.text = value.ToString();
            PlayerPrefs.SetInt("BestScore", value);

        }   // UpdateBestText()
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
                         : "white";

            _textGameStateLabel.text = $"Game State:  <color={color}><b>{state.ToString()}</b></color>";

        }   // UpdateGameState()
        #endregion


        #region .  UpdateMovesText()  .
        // -------------------------------------------------------------------------
        //   Method.......:  UpdateMovesText()
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void UpdateMovesText(int value)
        {
            _textMovesValue.text = value.ToString();

        }   // UpdateMovesText()
        #endregion


        #region .  UpdateScoreText()  .
        // -------------------------------------------------------------------------
        //   Method.......:  UpdateScoreText()
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void UpdateScoreText(int value)
        {
            _textScoreValue.text = value.ToString();

        }   // UpdateScoreText()
        #endregion


        #region .  UpdateWinConditionText()  .
        // -------------------------------------------------------------------------
        //   Method.......:  UpdateWinConditionText()
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void UpdateWinConditionText(int value)
        {
            _textWinConditionValue.text = value.ToString();

        }   // UpdateWinConditionText()
        #endregion


    }   // class UIManager

}   // namespace Assets.Scenes.Game2048.Scripts
