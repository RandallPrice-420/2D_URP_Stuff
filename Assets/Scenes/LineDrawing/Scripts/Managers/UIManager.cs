using System;
using System.Collections.Generic;

using Assets.Scenes.Game2048.Scripts;

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
        //   OnToggleAutomaticModeClicked
        //   OnToggleFloodModeClicked
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public static event Action OnButtonResetClicked         = delegate { };
        public static event Action OnToggleAutomaticModeClicked = delegate { };
        public static event Action OnToggleFloodModeClicked     = delegate { };

        #endregion



        // ---------------------------------------------------------------------
        // Serialized Fields:
        // ------------------
        //   _textBallCount
        //   _textGameState
        //   _textMousePosition
        //   _toggleAutomaticMode
        //   _toggleFloodMode
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        [SerializeField] private Text   _textBallCount;
        [SerializeField] private Text   _textGameState;
        [SerializeField] private Text   _textMousePosition;
        [SerializeField] private Toggle _toggleAutomaticMode;
        [SerializeField] private Toggle _toggleFloodMode;

        #endregion



        // ---------------------------------------------------------------------
        // Private Properties:
        // -------------------
        //   _blocks
        //   _nodes
        //   _round
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private List<Block> _blocks;
        private List<Node>  _nodes;
        private int         _round;

        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   ButtonQuitClicked()
        //   ButtonResetClicked()
        //   OnDisable()
        //   OnEnable()
        //   ToggleAutomaticModeClicked()
        //   ToggleFloodModeClicked()
        //   UpdateBallCount()
        //   UpdateMousePosition()
        // ---------------------------------------------------------------------

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
            SceneManager.LoadScene("Scene_MainMenu");

            //UnityEditor.EditorApplication.isPlaying = false;
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
            OnButtonResetClicked?.Invoke();
            UpdateBallCount(0);

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
            SpawnManager.OnBallSpawned -= UpdateBallCount;

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
            SpawnManager.OnBallSpawned += UpdateBallCount;
            ;

        }   // OnEnable()
        #endregion


        #region .  ToggleAutomaticModeClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ToggleAutomaticModeClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ToggleAutomaticModeClicked()
        {
            _toggleFloodMode.interactable = !_toggleAutomaticMode.isOn;
            _toggleFloodMode.isOn = _toggleAutomaticMode.isOn
                                          ? false
                                          : _toggleFloodMode.isOn;

            // Fire this event for anyone who is subscribed.
            OnToggleAutomaticModeClicked?.Invoke();

        }   // ToggleAutomaticModeClicked()
        #endregion


        #region .  ToggleFloodModeClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ToggleFloodModeClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ToggleFloodModeClicked()
        {
            _toggleAutomaticMode.interactable = !_toggleFloodMode.isOn;
            _toggleAutomaticMode.isOn = _toggleFloodMode.isOn
                                              ? false
                                              : _toggleAutomaticMode.isOn;

            OnToggleFloodModeClicked?.Invoke();

        }   // ToggleFloodModeClicked()
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
            _textBallCount.text = $"Ball Count:             {ballCount.ToString()}";

        }   // UpdateBallCount()
        #endregion


        #region .  UpdateMousePosition()  .
        // ---------------------------------------------------------------------
        //   Method.......:  UpdateMousePosition()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void UpdateMousePosition()
        {
            _textMousePosition.text = $"Mouse Position:    {Input.mousePosition.ToString()}";

        }   // UpdateMousePosition()
        #endregion


    }   // class UIManager

}   // namespace Assets.Scenes.MainMenu.Scripts.LineDrawing.Managers
