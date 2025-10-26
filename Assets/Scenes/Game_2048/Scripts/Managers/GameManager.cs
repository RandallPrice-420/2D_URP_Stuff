using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace Assets.Scenes.Game2048.Scripts
{
    // -------------------------------------------------------------------------
    // Public Structs:
    // ---------------
    //   BlockType
    // -------------------------------------------------------------------------

    #region .  Public Structs  .

    [System.Serializable]
    public struct BlockType
    {
        public int    Value;
        public Sprite Sprite;
        public Block  BlockPrefab;

    }   // struct BlockType

    #endregion



    // -------------------------------------------------------------------------
    // Public Enums:
    // -------------
    //   GameState
    // -------------------------------------------------------------------------

    #region .  Public Enums  .

    [System.Serializable]
    public enum GameState
    {
        //Initialize,
        GenerateLevel,
        SpawnBlocks,
        WaitingInput,
        Moving,
        GameOver,
        Win,
        Lose,
        Quit
    }

    #endregion



    public class GameManager : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   OnGameOver
        //   OnGameStateChanged
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public static event Action            OnGameOver         = delegate { };
        public static event Action<GameState> OnGameStateChanged = delegate { };

        #endregion



        // ---------------------------------------------------------------------
        // Private Enums:
        // --------------
        //   SpriteType
        // ---------------------------------------------------------------------

        #region .  Private Enums  .

        private enum SpriteType
        {
            Gray,
            Blue,
            Green,
            Orange,
            Red
        }

        #endregion


        // ---------------------------------------------------------------------
        // Serialized Fields:
        // ------------------
        //   _camera
        //   _height
        //   _width
        //   _blockPrefab
        //   _boardPrefab
        //   _nodePrefab
        //   _blockTypes
        //   _buttonContinue
        //   _buttonDisabled
        //   _buttonQuit
        ////   _buttonStart
        ////   _buttonSprites
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        [SerializeField] private Camera           _camera;
        [SerializeField] private int              _height = 4;
        [SerializeField] private int              _width  = 4;
        [SerializeField] private Block            _blockPrefab;
        [SerializeField] private Block[]          _blockPrefabs;
        [SerializeField] private SpriteRenderer   _boardPrefab;
        [SerializeField] private Node             _nodePrefab;
        [SerializeField] private List<BlockType>  _blockTypes;
        [SerializeField] private Button           _buttonContinue;
        [SerializeField] private Button           _buttonDisabled;
        //[SerializeField] private Button           _buttonRestart;
        [SerializeField] private Button           _buttonQuit;
        //[SerializeField] private Button           _buttonStart;
        //[SerializeField] private List<Sprite>     _buttonSprites;

        #endregion



        // ---------------------------------------------------------------------
        // Private Properties:
        // -------------------
        //   _blocks
        //   _currentState
        //   _nodes
        //   _round
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private List<Block> _blocks;
        private GameState   _currentState;
        private List<Node>  _nodes;
        private int         _round;

        #endregion



        // ---------------------------------------------------------------------
        // Private Getters:
        // ----------------
        //   GetBlockTypeByValue
        // ---------------------------------------------------------------------

        #region .  Private Getters  .

        //private BlockType GetBlockTypeByValue(int value) => _blockTypes.First(t => t.Value == value);
        private BlockType GetBlockTypeByValue(int value)
        {
            int i = 0;

            for (i = 0; i < _blockTypes.Count(); i++)
            {
                if (_blockTypes[i].Value == value)
                {
                    break;
                }
            }

            return _blockTypes[i];

        }   // GetBlockTypeByValue()
        #endregion



        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   ButtonContinueClicked()
        //   ButtonQuitClicked()
        ////   ButtonRestartClicked()  --  COMMENTED OUT
        ////   ButtonStartClicked()    --  COMMENTED OUT
        //   SetBlockButtonStartClicked
        // ---------------------------------------------------------------------

        #region .  ButtonContinueClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonContinueClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ButtonContinueClicked()
        {
            //ChangeState(GameState.SpawnBlocks);
            SpawnBlocks(1);

        }   // ButtonContinueClicked()
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
            SceneManager.LoadScene("Scene_MainMenu");

            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();

        }   // ButtonQuitClicked()
        #endregion


        #region .  ButtonRestartClicked()  --  COMMENTED OUT  .
        //// ---------------------------------------------------------------------
        ////   Method.......:  ButtonRestartClicked()
        ////   Description..:  
        ////   Parameters...:  None
        ////   Returns......:  Nothing
        //// ---------------------------------------------------------------------
        //public void ButtonRestartClicked()
        //{
        //    //ClearBlocks();
        //    ChangeState(GameState.Initialize);

        //}   // ButtonRestartClicked()
        #endregion


        #region .  ButtonStartClicked()  --  COMMENTED OUT  .
        //// ---------------------------------------------------------------------
        ////   Method.......:  ButtonStartClicked()
        ////   Description..:  
        ////   Parameters...:  None
        ////   Returns......:  Nothing
        //// ---------------------------------------------------------------------
        //public void ButtonStartClicked()
        //{
        //    ChangeState(GameState.SpawnBlocks);

        //}   // ButtonStartClicked()
        #endregion



        // ---------------------------------------------------------------------
        // private Methods:
        // ----------------
        //   ChangeButtonState()  --  COMMENTED OUT
        //   ChangeState()
        //   ClearBlocks()
        //   GameOver
        //   GenerateLevel()
        //   Initialize()         --  COMMENTED OUT
        //   Start()
        //   SpawnBlocks()
        // ---------------------------------------------------------------------

        #region .  ChangeButtonState()  --  COMMENTED OUT  .
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


        #region .  ChangeState()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ChangeState()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void ChangeState(GameState newState)
        {
            _currentState = newState;

            switch (_currentState)
            {
                //case GameState.Initialize:
                //    Initialize();
                //    break;

                case GameState.GenerateLevel:
                    GenerateLevel();
                    ChangeState(GameState.SpawnBlocks);
                    break;

                case GameState.SpawnBlocks:
                    SpawnBlocks((_round++ == 0) ? 2 : 1);

                    //ChangeButtonState(_buttonStart,    false);
                    //ChangeButtonState(_buttonContinue, true );
                    //ChangeButtonState(_buttonRestart,  true );
                    //ChangeButtonState(_buttonQuit,     true );

                    ChangeState(GameState.WaitingInput);
                    OnGameStateChanged?.Invoke(newState);
                    break;

                case GameState.WaitingInput:
                    break;

                case GameState.Moving:
                    break;

                case GameState.GameOver:
                    GameOver();
                    break;

                case GameState.Win:
                    break;

                case GameState.Lose:
                    break;

                case GameState.Quit:
                    break;
            }


        }   // ChangeState()
        #endregion


        #region .  GameOver()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GameOver()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void GameOver()
        {
            //_buttonStart.interactable = true;
            _buttonContinue.interactable = false;
            _buttonQuit    .interactable = true;
            //_buttonRestart .interactable = true;

            // Need logic to determine the next game state.

        }   // GameOver()
        #endregion


        #region .  GenerateLevel()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GenerateLevel()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void GenerateLevel()
        {
            _blocks = new List<Block>();
            _nodes  = new List<Node>();

            var center = new Vector2((float)(_width / 2f) - 0.5f, (float)(_height / 2f) - 0.5f);
            var board  = Instantiate(_boardPrefab, center, Quaternion.identity);
            board.size = new Vector2(_width, _height);

            int index = 0;

            for (int x = 0; x < _height; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    var node   = Instantiate(_nodePrefab, new Vector2(x, y), Quaternion.identity);
                    node.Index = index++;
                    _nodes.Add(node);
                }
            }

            _camera.transform.position = new Vector3(center.x, center.y, -10);

        }   // GenerateLevel()
        #endregion


        #region .  GetNodeAtPosition()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetNodeAtPosition()
        //   Description..:  
        //   Parameters...:  Vector2
        //   Returns......:  Node
        // ---------------------------------------------------------------------
        private Node GetNodeAtPosition(Vector2 position)
        {
            Node node = _nodes.FirstOrDefault(n => n.Position == position);

            return node;

        }   // GetNodeAtPosition()
        #endregion


        #region .  Initialize()  --  COMMENTED OUT  .
        //// ---------------------------------------------------------------------
        ////   Method.......:  Initialize()
        ////   Description..:  
        ////   Parameters...:  None
        ////   Returns......:  Nothing
        //// ---------------------------------------------------------------------
        //private void Initialize()
        //{
        //    _blocks = new List<Block>();
        //    _nodes  = new List<Node>();

        //    ChangeState(GameState.GenerateLevel);

        //}   // Initialize()
        #endregion


        #region .  Shift()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Shift()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Shift(Vector2 direction)
        {
            var orderBlocks = _blocks.OrderBy(b => b.Position.x).ThenBy(b => b.Position.y).ToList();

            if (direction == Vector2.right || direction == Vector2.up)
            {
                orderBlocks.Reverse();
            }

            foreach (var block in orderBlocks)
            {
                var next = block.Node;

                do
                {
                    block.SetBlock(next);

                    var possibleNode = GetNodeAtPosition(next.Position + direction);

                    if (possibleNode != null)
                    {
                        if (possibleNode.OccupiedBlock == null)
                        {
                            next = possibleNode;
                        }
                    }


                } while (next != block.Node);

                block.transform.position = block.Node.Position;
            }

        }   // Shift()
        #endregion


        #region .  Start()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Start()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Start()
        {
            ChangeState(GameState.GenerateLevel);

        }   // Start()
        #endregion


        #region .  SpawnBlocks()  .
        // ---------------------------------------------------------------------
        //   Method.......:  SpawnBlocks()
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void SpawnBlocks(int amount)
        {
            var freeNodes = _nodes.Where(n => n.OccupiedBlock == null).OrderBy(b => Random.value).ToList();

            foreach (var node in freeNodes.Take(amount))
            {
                //Debug.Log($"Free node at {node.Position}");

                BlockType blockType = GetBlockTypeByValue(Random.value > 0.8 ? 4 : 2);
                Block newBlock = Instantiate(blockType.BlockPrefab, node.Position, Quaternion.identity);
                newBlock.Value = blockType.Value;
                newBlock.SetBlock(node);

                _blocks.Add(newBlock);

                node.OccupiedBlock = newBlock;
            }

            //if (freeNodes.Count() == 1)
            if (freeNodes.Count() < 2)
            {
                // Lost the game.
                OnGameOver?.Invoke();
                return;
            }

        }   // SpawnBlocks
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
            if (_currentState != GameState.WaitingInput)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Shift(Vector2.left);
            }

        }   // Update()
        #endregion


    }   // class GameManager

}   // namespace Assets.Scenes.Game2048.Scripts
