using UnityEngine;


namespace Assets.Scenes.MainMenu.Scripts.LineDrawing.Managers
{
    public class DrawManager : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   RESOLUTION
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        public const float RESOLUTION = 0.1f;

        #endregion



        // ---------------------------------------------------------------------
        // Serialized Fields:
        // ------------------
        //   _linePrefab
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        // Be sure to assign these in the Inspector.
        [SerializeField] private Line _linePrefab;

        #endregion



        // ---------------------------------------------------------------------
        // Private Properties:
        // -------------------
        //   _camera
        //   _currentLine
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private Camera _camera;
        private Line   _currentLine;

        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   ButtonResetClicked()
        //   OnDisable()
        //   OnEnable()
        //   Start()
        //   Update()
        // ---------------------------------------------------------------------

        #region .  ButtonResetClicked  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonResetClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void ButtonResetClicked()
        {
            // Find all Line objects in the scene and clear their lines.
            Line[] lines = FindObjectsOfType<Line>();
            foreach (Line line in lines)
            {
                line.ClearLine();
                Destroy(line.gameObject);
            }

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
            UIManager.OnButtonResetClicked -= ButtonResetClicked;

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
            UIManager.OnButtonResetClicked += ButtonResetClicked;

        }   // OnEnable()
        #endregion


        #region .  Start  .
        // ---------------------------------------------------------------------
        //   Method.......:  Start()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Start()
        {
            _camera = Camera.main;

        }   // Start()
        #endregion


        #region .  Update()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Update()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Update()
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                // This is the start of a new line.
                _currentLine = Instantiate(_linePrefab, mousePosition, Quaternion.identity);
            }

            if (Input.GetMouseButton(0))
            {
                // This is the continuation of the current line being drawn.  The
                // end point of the line follows the mouse position.  Releasing the
                // mouse button will end the line.
                _currentLine.SetPosition(mousePosition);
            }

        }   // Update()
        #endregion


    }   // class DrawManager

}   // namespace Assets.Scenes.MainMenu.Scripts.LineDrawing.Managers
