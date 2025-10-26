using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace Assets.Scenes.MainMenu.Scripts.LineDrawing.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   OnBallSpawned
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public static event Action<int> OnBallSpawned = delegate { };

        #endregion



        // ---------------------------------------------------------------------
        // Serialized Fields:
        // ------------------
        //   _ballPrefabs
        //   _minOffsetX
        //   _maxOffsetX
        //   _minOffsetY
        //   _maxOffsetY
        //   _textMousePosition
        //   _toggleAutomaticMode
        //   _toggleFloodMode
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        // Be sure to assign these in the Inspector.
        [SerializeField] private Ball[] _ballPrefabs;
        [SerializeField] private float  _minOffsetX;
        [SerializeField] private float  _maxOffsetX;
        [SerializeField] private float  _minOffsetY;
        [SerializeField] private float  _maxOffsetY;
        [SerializeField] private Text   _textMousePosition;
        [SerializeField] private Toggle _toggleAutomaticMode;
        [SerializeField] private Toggle _toggleFloodMode;

        #endregion



        // ---------------------------------------------------------------------
        // Private Properties:
        // -------------------
        //   _ballCount
        //   _camera
        //   _canSpawn
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private int    _ballCount;
        private Camera _camera;
        private bool   _canSpawn;

        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   ButtonResetClicked()
        //   GetRandomWaitTime()
        //   OnDisable()
        //   OnEnable()
        //   SpawnBall()
        //   SpawnBallRandom()
        //   Start()
        //   ToggleAutomaticModeClicked()
        //   ToggleFloodModeClicked()
        //   Update()
        //   WaitForSomeTime()
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
            // Find all of the Ball objects in the scene and destroy them.
            Ball[] balls = FindObjectsOfType<Ball>();

            foreach (Ball ball in balls)
            {
                Destroy(ball.gameObject);
            }

            _ballCount = 0;

        }   // ButtonResetClicked()
        #endregion


        #region .  GetRandomPosition  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetRandomPosition()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private Vector3 GetRandomPosition()
        {
            Vector3 position = new Vector3(Random.Range(0f, Screen.width), Random.Range(0f, Screen.width), 0f);

            return position;

        }   // GetRandomPosition()
        #endregion


        #region .  GetRandomWaitTime  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetRandomWaitTime()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private float GetRandomWaitTime()
        {
            float waitTime = Random.Range(0.1f, 2.0f);

            return waitTime;

        }   // GetWaitTime()
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
            UIManager.OnButtonResetClicked         -= ButtonResetClicked;
            UIManager.OnToggleAutomaticModeClicked -= ToggleAutomaticModeClicked;

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
            UIManager.OnButtonResetClicked         += ButtonResetClicked;
            UIManager.OnToggleAutomaticModeClicked += ToggleAutomaticModeClicked;

        }   // OnEnable()
        #endregion


        #region .  SpawnBall  .
        // ---------------------------------------------------------------------
        //   Method.......:  SpawnBall()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void SpawnBall(Vector2 position)
        {
            Vector2 spawnPosition = _camera.ScreenToWorldPoint(new Vector3(position.x,
                                                                       position.y,
                                                                       Mathf.Abs(_camera.transform.position.x)));

            //Debug.Log($"spawnPosition:  {spawnPosition}");

            // Instantiate the ball prefab at the mouse position.
            int  index = Random.Range(0, _ballPrefabs.Length - 1);
            Ball ball  = Instantiate(_ballPrefabs[index], spawnPosition, Quaternion.identity);

            _ballCount++;
            OnBallSpawned?.Invoke(_ballCount);

        }   // SpawnBall()
        #endregion


        #region .  SpawnBallRandom  .
        // ---------------------------------------------------------------------
        //   Method.......:  SpawnBallRandom()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void SpawnBallRandom()
        {
            if (_toggleAutomaticMode.isOn && !_toggleFloodMode.isOn)
            {
                Vector3 position = new(Random.Range(_minOffsetX, Screen.width  - _maxOffsetX),
                                   Random.Range(_minOffsetY, Screen.height - _maxOffsetY),
                                   0f);
                SpawnBall(position);
            }

        }   // SpawnballRandom()
        #endregion


        #region .  SpawnBallWithWaitTime  .
        // ---------------------------------------------------------------------
        //   Method.......:  SpawnBallWithWaitTime()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        IEnumerator SpawnBallWithWaitTime(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            Vector3 spawnPosition = GetRandomPosition();
            SpawnBall(spawnPosition);

            _canSpawn = true;

        }   // SpawnBallWithWaitTime()
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
            _canSpawn = true;

        }   // Start()
        #endregion


        #region .  ToggleAutomaticModeClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ToggleAutomaticModeClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void ToggleAutomaticModeClicked()
        {
            _canSpawn = _toggleAutomaticMode.isOn;

        }   // ButtonAutomaticModeClicked()
        #endregion


        #region .  ToggleFloodModeClicked()  --  COMMENTED OUT  .
        //// -------------------------------------------------------------------------
        ////   Method.......:  ToggleFloodModeClicked()
        ////   Description..:  
        ////   Parameters...:  None
        ////   Returns......:  Nothing
        //// -------------------------------------------------------------------------
        //private void ToggleFloodModeClicked()
        //{
        //    //_toggleAutomaticMode.interactable = !_toggleFloodMode.isOn;
        //    //_toggleAutomaticMode.isOn = _toggleFloodMode.isOn
        //    //                                  ? false
        //    //                                  : _toggleAutomaticMode.isOn;

        //}   // ToggleFloodModeClicked()
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
            //UpdateMousePosition();
            Vector3 spawnPosition;

            if (_toggleAutomaticMode.isOn)
            {
                if (_canSpawn)
                {
                    _canSpawn = false;
                    float waitTime = GetRandomWaitTime();
                    StartCoroutine(SpawnBallWithWaitTime(waitTime));
                }
            }
            else if (_toggleFloodMode.isOn)
            {
                spawnPosition = GetRandomPosition();
                SpawnBall(spawnPosition);
            }
            else if ((Input.GetMouseButtonDown(1) && !_toggleAutomaticMode.isOn))
            {
                spawnPosition = Input.mousePosition;
                SpawnBall(spawnPosition);
            }

        }   // Update()
        #endregion


        #region .  WaitForSomeTime()  .
        // ---------------------------------------------------------------------
        //   Method.......:  WaitForSomeTime()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private IEnumerator WaitForSomeTime(float waitTime)
        {
            while (true)
            {
                //Debug.Log($"Method waits for {waitTime} second!");

                yield return new WaitForSeconds(waitTime);
            }

        }   // WaitForSomeTime()
        #endregion


    }   // class SpawnManager

}   // namespace Assets.Scenes.MainMenu.Scripts.LineDrawing.Managers
