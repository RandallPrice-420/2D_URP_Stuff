using System.Collections.Generic;
using DG.Tweening;
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
        //   _rotation
        //   _textBestValue
        //   _textGameOverLabel
        //   _textGameStateLabel
        //   _textHowToPlay
        //   _textMovesValue
        //   _textResultLabel
        //   _textScoreValue
        //   _textWinConditionValue
        //   _textYouWinLabel
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        [SerializeField] private List<Sprite> _buttonSprites;
        [SerializeField] private GameObject   _panelGameOver;
        [SerializeField] private float        _rotation;
        [SerializeField] private TMP_Text     _textBestValue;
        [SerializeField] private TMP_Text     _textGameOverLabel;
        [SerializeField] private TMP_Text     _textGameStateLabel;
        [SerializeField] private TMP_Text     _textHowToPlay;
        [SerializeField] private TMP_Text     _textResultLabel;
        [SerializeField] private TMP_Text     _textMovesValue;
        [SerializeField] private TMP_Text     _textScoreValue;
        [SerializeField] private TMP_Text     _textWinConditionValue;
        [SerializeField] private TMP_Text     _textYouWinLabel;

        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   GameOver()
        //   OnDisable()
        //   OnEnable()
        //   UpdateBestcoreText()
        //   UpdateGameStateText()
        //   UpdateMovesText()
        //   UpdateResultsText()
        //   UpdateScoreText()
        //   UpdateWinConditionText()
        // ---------------------------------------------------------------------

        #region .  GameOver()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GameOver()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void GameOver(GameState state)
        {
            bool isWinGame = (state == GameState.WinGame);

            SoundManager.Sounds sound_1 = (isWinGame) ? SoundManager.Sounds.Win_Game_3 : SoundManager.Sounds.Game_Over_3;
            SoundManager.Sounds sound_2 = (isWinGame) ? SoundManager.Sounds.Win_Game_1 : SoundManager.Sounds.Lose_1;

            _textResultLabel.color = (isWinGame) ? _textYouWinLabel.color : _textGameOverLabel.color;
            _textResultLabel.text  = (isWinGame) ? _textYouWinLabel.text  : _textGameOverLabel.text;

            UpdateResultsText(isWinGame);

            _panelGameOver.SetActive(true);

            Transform transform  = _textResultLabel.transform;
            Sequence  mySequence = DOTween.Sequence();

            mySequence.InsertCallback(0f, () => SoundManager.Instance.PlaySound(sound_1))
                      .Append(transform.DORotate(new Vector3(360f, 360f, 360f), 2f, RotateMode.FastBeyond360).SetRelative())
                      //.SetDelay(0.5f)
                      .InsertCallback(2.5f, () => SoundManager.Instance.PlaySound(sound_2))
                      .Append(transform.DOScale(0.5f, 1f))
                      .Append(transform.DOScale(1.5f, 1f))
                      .Append(transform.DOScale(1.0f, 1f))
                      ;

            //transform.DORotate(new Vector3(360f, 360f, 360f), 2f, RotateMode.FastBeyond360).SetRelative();
            //transform.DOScale(1.5f, 2f).SetLoops(1, LoopType.Yoyo);

            //_textResult.DOColor(Color.green, 1).SetLoops(-1, LoopType.Yoyo);
            //purpleCube.GetComponent<Renderer>().material.DOColor(Color.yellow, 2).SetLoops(-1, LoopType.Yoyo);

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
        // ---------------------------------------------------------------------
        //   Method.......:  UpdateBestcoreText()
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void UpdateBestcoreText(int value)
        {
            _textBestValue.text = value.ToString();
            PlayerPrefs.SetInt("BestScore", value);

        }   // UpdateBestcoreText()
        #endregion


        #region .  UpdateGameStateText()  .
        // ---------------------------------------------------------------------
        //   Method.......:  UpdateGameStateText()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
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
        // ---------------------------------------------------------------------
        //   Method.......:  UpdateMovesText()
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void UpdateMovesText(int value)
        {
            _textMovesValue.text = value.ToString();

        }   // UpdateMovesText()
        #endregion


        #region .  UpdateResultsText()  .
        // ---------------------------------------------------------------------
        //   Method.......:  UpdateResultsText()
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void UpdateResultsText(bool isWinGame)
        {
            string color = (isWinGame) ? "green"               : "red";
            string text  = (isWinGame) ? _textYouWinLabel.text : _textGameOverLabel.text;

            _textResultLabel.text = $"<color={color}><b>{text}</b></color>";

        }   // UpdateMovesText()
        #endregion


        #region .  UpdateScoreText()  .
        // ---------------------------------------------------------------------
        //   Method.......:  UpdateScoreText()
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void UpdateScoreText(int value)
        {
            _textScoreValue.text = value.ToString();

        }   // UpdateScoreText()
        #endregion


        #region .  UpdateWinConditionText()  .
        // ---------------------------------------------------------------------
        //   Method.......:  UpdateWinConditionText()
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void UpdateWinConditionText(WinCondition value)
        {
            int amount                  = (int)value;
            _textWinConditionValue.text = amount.ToString();
            _textHowToPlay        .text = _textHowToPlay.text.Replace("%1", amount.ToString());

        }   // UpdateWinConditionText()
        #endregion


    }   // class UIManager
    
}   // namespace Assets.Scenes.Game2048.Scripts.Managers
