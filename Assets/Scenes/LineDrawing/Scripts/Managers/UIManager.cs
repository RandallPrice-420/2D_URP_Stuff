using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Assets.Scenes.MainMenu.Scripts.LineDrawing.Managers
{
    public class UIManager : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   OnButtonResetClicked
        //   OnToggleAutomaticModeChanged
        //   OnToggleFloodModeChanged
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public static event Action OnButtonResetClicked         = delegate { };
        public static event Action OnToggleAutomaticModeChanged = delegate { };
        public static event Action OnToggleFloodModeChanged     = delegate { };

        #endregion



        // ---------------------------------------------------------------------
        // Serialized Fields:
        // ------------------
        //   _textBallPositionValue
        //   _textMousePositionValue
        //   _textBallCountValue
        //   _toggleAutomaticMode
        //   _toggleFloodMode
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        [SerializeField] private Text   _textBallPositionValue;
        [SerializeField] private Text   _textMousePositionValue;
        [SerializeField] private Text   _textBallCountValue;
        [SerializeField] private Toggle _toggleAutomaticMode;
        [SerializeField] private Toggle _toggleFloodMode;

        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   ButtonMainMenuClicked()
        //   ButtonQuitClicked()
        //   ButtonResetClicked()
        //   OnDisable()
        //   OnEnable()
        //   ToggleAutomaticModeChanged()
        //   ToggleFloodModeChanged()
        //   UpdateBallCount()
        //   UpdateBallPosition()
        //   UpdateMousePosition()
        // ---------------------------------------------------------------------

        #region .  ButtonMainMenuClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonMainMenuClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ButtonMainMenuClicked()
        {
            SceneManager.LoadScene("Scene_MainMenu");

        }   // ButtonMainMenuClicked()
        #endregion


        #region .  ButtonQuitClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonQuitClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ButtonQuitClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();

        }   // ButtonQuitClicked()
        #endregion


        #region .  ButtonResetClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonResetClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ButtonResetClicked()
        {
            UpdateBallCount(0);

            _textBallPositionValue .text = Vector2.zero.ToString();
            _textMousePositionValue.text = "";
            _toggleFloodMode       .isOn = false;
            _toggleAutomaticMode   .isOn = false;

            OnButtonResetClicked?.Invoke();

        }   // ButtonResetClicked()
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
            SpawnManager.OnBallDestroyed -= UpdateBallCount;
            SpawnManager.OnBallSpawned   -= UpdateBallCount;
            SpawnManager.OnBallStarted   -= UpdateBallPosition;

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
            SpawnManager.OnBallDestroyed += UpdateBallCount;
            SpawnManager.OnBallSpawned   += UpdateBallCount;
            SpawnManager.OnBallStarted   += UpdateBallPosition;

        }   // OnEnable()
        #endregion


        #region .  ToggleAutomaticModeChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ToggleAutomaticModeChanged()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ToggleAutomaticModeChanged()
        {
            _toggleFloodMode.interactable = !_toggleAutomaticMode.isOn;
            _toggleFloodMode.isOn         =  _toggleAutomaticMode.isOn
                                          ?  false
                                          :  _toggleFloodMode.isOn;

            // Fire events for any subscribers.
            OnToggleAutomaticModeChanged?.Invoke();

        }   // ToggleAutomaticModeChanged()
        #endregion


        #region .  ToggleFloodModeChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ToggleFloodModeChanged()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ToggleFloodModeChanged()
        {
            _toggleAutomaticMode.interactable = !_toggleFloodMode.isOn;
            _toggleAutomaticMode.isOn         = _toggleFloodMode.isOn
                                              ? false
                                              : _toggleAutomaticMode.isOn;

            // Fire events for any subscribers.
            OnToggleFloodModeChanged?.Invoke();

        }   // ToggleFloodModeChanged()
        #endregion


        #region .  UpdateBallCount()  .
        // ---------------------------------------------------------------------
        //   Method.......:  UpdateBallCount()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void UpdateBallCount(int ballCount)
        {
            _textBallCountValue.text = ballCount.ToString();

        }   // UpdateBallCount()
        #endregion


        #region .  UpdateBallPosition()  .
        // ---------------------------------------------------------------------
        //   Method.......:  UpdateBallPosition()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void UpdateBallPosition(Vector2 position)
        {
            _textBallPositionValue.text = position.ToString();

        }   // UpdateBallPosition()
        #endregion


        #region .  UpdateMousePosition()  .
        // ---------------------------------------------------------------------
        //   Method.......:  UpdateMousePosition()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        //private void UpdateMousePosition()
        private void Update()
        {
            _textMousePositionValue.text = Input.mousePosition.ToString();

        }   // UpdateMousePosition()
        #endregion


    }   // class UIManager

}   // namespace Assets.Scenes.MainMenu.Scripts.LineDrawing.Managers
