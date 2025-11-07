using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace Assets.Scenes.MainMenu.Scripts.LineDrawing.Managers
{
    public class SpawnManager : Singleton<SpawnManager>
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   OnBallDestroyed
        //   OnBallSpawned
        //   OnBallStarted
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public static event Action<int>     OnBallDestroyed = delegate { };
        public static event Action<int>     OnBallSpawned   = delegate { };
        public static event Action<Vector2> OnBallStarted   = delegate { };

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
        // Private Variables:
        // ------------------
        //   _ballCount
        //   _camera
        //   _canSpawn
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

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
        //   ToggleAutomaticModeChanged()
        //   ToggleFloodModeChanged()
        //   Update()
        //   WaitForSomeTime()
        // ---------------------------------------------------------------------

        #region .  BallDestroyed  .
        // ---------------------------------------------------------------------
        //   Method.......:  BallDestroyed()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void BallDestroyed()
        {
            _ballCount--;
            OnBallDestroyed?.Invoke(_ballCount);

        }   // BallDestroyed()
        #endregion


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
            //Vector3 position = new(Random.Range(0f, Screen.width), Random.Range(0f, Screen.height), 0f);
            Vector3 position = new(Random.Range(_minOffsetX, _maxOffsetX), Random.Range(_minOffsetY, _maxOffsetY), 0f);

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
            Ball     .OnBallDestroyed              -= BallDestroyed;
            UIManager.OnButtonResetClicked         -= ButtonResetClicked;
            UIManager.OnToggleAutomaticModeChanged -= ToggleAutomaticModeChanged;

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
            Ball     .OnBallDestroyed              += BallDestroyed;
            UIManager.OnButtonResetClicked         += ButtonResetClicked;
            UIManager.OnToggleAutomaticModeChanged += ToggleAutomaticModeChanged;

        }   // OnEnable()
        #endregion


        #region .  SpawnBall  .
        // ---------------------------------------------------------------------
        //   Method.......:  SpawnBall()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        //private IEnumerator SpawnBall(Vector2 position)
        private void SpawnBall(Vector2 position)
        {
            //Vector2 spawnPosition = _camera.ScreenToWorldPoint(new Vector3(position.x,
            //                                                               position.y,
            //                                                               Mathf.Abs(_camera.transform.position.x)));
            Vector2 spawnPosition = _camera.ScreenToWorldPoint(new Vector2(position.x, position.y));

            // Instantiate the ball prefab at the mouse position.
            int  index = Random.Range(0, _ballPrefabs.Length - 1);
            Ball ball  = Instantiate(_ballPrefabs[index], spawnPosition, Quaternion.identity);

            _ballCount++;

            OnBallSpawned?.Invoke(_ballCount);
            OnBallStarted?.Invoke(spawnPosition);

            //yield return null;

        }   // SpawnBall()
        #endregion


        #region .  SpawnBallRandom  .
        // ---------------------------------------------------------------------
        //   Method.......:  SpawnBallRandom()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        //private IEnumerator SpawnBallRandom()
        private void SpawnBallRandom()
        {
            if (_toggleAutomaticMode.isOn && !_toggleFloodMode.isOn)
            {
                Vector3 spawnPosition = new(Random.Range(0f, (float)Screen.width), 0f, 0f);
                //StartCoroutine(SpawnBall(spawnPosition));
                SpawnBall(spawnPosition);
            }

            //yield return null;

        }   // SpawnballRandom()
        #endregion


        #region .  SpawnBallWithWaitTime  .
        // ---------------------------------------------------------------------
        //   Method.......:  SpawnBallWithWaitTime()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private IEnumerator SpawnBallWithWaitTime(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            Vector3 spawnPosition = GetRandomPosition();
            //StartCoroutine(SpawnBall(spawnPosition));
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
            _camera   = Camera.main;
            _canSpawn = true;

        }   // Start()
        #endregion


        #region .  ToggleAutomaticModeChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ToggleAutomaticModeChanged()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void ToggleAutomaticModeChanged()
        {
            _canSpawn = _toggleAutomaticMode.isOn;

        }   // ButtonAutomaticModeClicked()
        #endregion


        #region .  ToggleFloodModeChanged()  --  COMMENTED OUT  .
        //// -------------------------------------------------------------------------
        ////   Method.......:  ToggleFloodModeChanged()
        ////   Description..:  
        ////   Parameters...:  None
        ////   Returns......:  Nothing
        //// -------------------------------------------------------------------------
        //private void ToggleFloodModeChanged()
        //{
        //    //_toggleAutomaticMode.interactable = !_toggleFloodMode.isOn;
        //    //_toggleAutomaticMode.isOn = _toggleFloodMode.isOn
        //    //                                  ? false
        //    //                                  : _toggleAutomaticMode.isOn;

        //}   // ToggleFloodModeChanged()
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
            Vector3 spawnPosition;
            //Vector3 spawnPosition = Utils.RestrictValue(Input.mousePosition, _minOffsetX, _maxOffsetX, _minOffsetY, _maxOffsetY);

            //if (spawnPosition.x < _minOffsetX) spawnPosition.x = _minOffsetX;
            //if (spawnPosition.x > _maxOffsetX) spawnPosition.x = _maxOffsetX;

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
                spawnPosition = Utils.RestrictValue(Input.mousePosition, _minOffsetX, _maxOffsetX, _minOffsetY, _maxOffsetY);
                SpawnBall(spawnPosition);

                //if ((spawnPosition.x >= _minOffsetX) && (spawnPosition.x <= _maxOffsetX))
                //{
                //    spawnPosition.x = Mathf.Clamp(spawnPosition.x, _minOffsetX, _maxOffsetX);
                //    SpawnBall(spawnPosition);
                //}
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
                //print($"Method waits for {waitTime} second!");

                yield return new WaitForSeconds(waitTime);
            }

        }   // WaitForSomeTime()
        #endregion


    }   // class SpawnManager

}   // namespace Assets.Scenes.MainMenu.Scripts.LineDrawing.Managers
