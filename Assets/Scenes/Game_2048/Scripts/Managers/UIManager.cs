using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Assets.Scenes.Game2048.Scripts.Managers
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
        //   _textHowToPlay
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
        [SerializeField] private TMP_Text     _textHowToPlay;
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
        //   UpdateBestcoreText()
        //   UpdateGameStateText()
        //   UpdateMovesText()
        //   UpdateScoreText()
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
            EventManager.OnGameOver            -= GameOver;
            EventManager.OnBestScoreChanged    -= UpdateBestcoreText;
            EventManager.OnGameStateChanged    -= UpdateGameStateText;
            EventManager.OnMovesChanged        -= UpdateMovesText;
            EventManager.OnScoreChanged        -= UpdateScoreText;
            EventManager.OnWinConditionChanged -= UpdateWinConditionText;

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
            EventManager.OnGameOver            += GameOver;
            EventManager.OnBestScoreChanged    += UpdateBestcoreText;
            EventManager.OnGameStateChanged    += UpdateGameStateText;
            EventManager.OnMovesChanged        += UpdateMovesText;
            EventManager.OnScoreChanged        += UpdateScoreText;
            EventManager.OnWinConditionChanged += UpdateWinConditionText;

        }   // OnEnable()
        #endregion


        #region .  UpdateBestcoreText()  .
        // -------------------------------------------------------------------------
        //   Method.......:  UpdateBestcoreText()
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // -------------------------------------------------------------------------
        private void UpdateBestcoreText(int value)
        {
            _textBestValue.text = value.ToString();
            PlayerPrefs.SetInt("BestScore", value);

        }   // UpdateBestcoreText()
        #endregion


        #region .  UpdateGameStateText()  .
        // -------------------------------------------------------------------------
        //   Method.......:  UpdateGameStateText()
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

        }   // UpdateGameStateText()
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
        private void UpdateWinConditionText(WinCondition value)
        {
            int amount                  = (int)value;
            _textWinConditionValue.text = amount.ToString();
            _textHowToPlay        .text = _textHowToPlay.text.Replace("%1", amount.ToString());

        }   // UpdateWinConditionText()
        #endregion


    }   // class UIManager
    
}   // namespace Assets.Scenes.Game2048.Scripts.Managers
