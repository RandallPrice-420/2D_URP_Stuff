using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


namespace Assets.Scenes.Game2048.Scripts.Managers
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
    //   WinCondition
    // -------------------------------------------------------------------------

    #region .  Public Enums  .

    [System.Serializable]
    public enum GameState
    {
        Initialize,
        GenerateLevel,
        SpawnBlocks,
        WaitingInput,
        Moving,
        LoseGame,
        WinGame,
        Quit
    }

    [System.Serializable]
    public enum WinCondition
    {
        None    = 0,
        Win8    = 8,
        Win16   = 16,
        Win32   = 32,
        Win64   = 64,
        Win128  = 128,
        Win256  = 256,
        Win512  = 512,
        Win1024 = 1024,
        Win2048 = 2048
    }

    #endregion



    public class GameManager : Singleton<GameManager>
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   OnBestScoreChanged
        //   OnGameOver
        //   OnGameStateChanged
        //   OnMoveChanged
        //   OnScoreChanged
        //   OnWinConditionChanged
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        //public static event Action<int>       OnBestScoreChanged    = delegate { };
        //public static event Action<GameState> OnGameOver            = delegate { };
        //public static event Action<GameState> OnGameStateChanged    = delegate { };
        //public static event Action<int>       OnMoveChanged         = delegate { };
        //public static event Action<int>       OnScoreChanged        = delegate { };
        //public static event Action<int>       OnWinConditionChanged = delegate { };

        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   BestScore
        //   Moves
        //   Score
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        public int BestScore
        {
            get { return _bestScore; }
            set
            {
                _bestScore = value;
                EventManager.RaiseOnBestScoreChanged(_bestScore);
            }
        }

        public int Moves
        {
            get { return _moves; }
            set
            {
                _moves = value;
                EventManager.RaiseOnMovesChanged(_moves);
            }
        }

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                EventManager.RaiseOnScoreChanged(_score);
            }
        }

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
        //   _height
        //   _width
        //   _camera
        //   _blockPrefab
        //   _blockTypes
        //   _boardPrefab
        //   _nodePrefab
        //   _panelGameOver
        //   _panelHighScores
        //   _playerName
        //   _textHowToPlay
        //
        //   _playerNameLength
        //   _travelTime
        //   _winAmount
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        [SerializeField] private int             _height;
        [SerializeField] private int             _width;
        [SerializeField] private Camera          _camera;
        [SerializeField] private Block           _blockPrefab;
        [SerializeField] private List<BlockType> _blockTypes;
        [SerializeField] private SpriteRenderer  _boardPrefab;
        [SerializeField] private Node            _nodePrefab;
        [SerializeField] private GameObject      _panelGameOver;
        [SerializeField] private GameObject      _panelHighScores;
        [SerializeField] private string          _playerName;
        [SerializeField] private TMP_Text        _textHowToPlay;

        [SerializeField] private int             _playerNameLength = 5;
        [SerializeField] private float           _travelTime       = 0.5f;
        [SerializeField] private WinCondition    _winAmount        = WinCondition.Win64;

        #endregion



        // ---------------------------------------------------------------------
        // Private Properties:
        // -------------------
        //   _bestScore
        //   _lowestScore
        //   _blocks
        //   _currentState
        //   _nodes
        //   _moves
        //   _round
        //   _score
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private int         _bestScore;
        private int         _lowestScore;
        private List<Block> _blocks;
        private GameState   _currentState;
        private List<Node>  _nodes;
        private int         _moves;
        private int         _round;
        private int         _score;
        //private int         _winAmount;

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
        //   ButtonAddScoreClicked()
        //   ButtonBackToMenuClicked()
        //   ButtonHighScoresClicked()
        //   ButtonPlayAgainClicked()
        //   ButtonQuitClicked()
        // ---------------------------------------------------------------------

        #region .  ButtonAddScoreClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonAddScoreClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ButtonAddScoreClicked()
        {
            HighScoresManager.Instance.AddHighScoreEntry(Random.Range( 1, 10000),
                                                         Random.Range(30,   100),
                                                         HighScoresManager.Instance.GetPlayerName(_playerNameLength));

            HighScoresManager.Instance.GetHighScores();
            HighScoresManager.Instance.DisplayHighScores();

        }   // ButtonAddScoreClicked()
        #endregion


        #region .  ButtonBackToMenuClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonBackToMenuClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ButtonBackToMenuClicked()
        {
            SceneManager.LoadScene("Scene_MainMenu");

        }   // ButtonBackToMenuClicked()
        #endregion


        #region .  ButtonClearClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonClearClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ButtonClearClicked()
        {
            HighScoresManager.Instance.ClearHighScoresDisplay();

        }   // ButtonClearClicked()
        #endregion


        #region .  ButtonHighScoresClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonHighScoresClicked()
        //   Description..:  Toggle the HighScores panel visibility on and off.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ButtonHighScoresClicked()
        {
            _panelHighScores.gameObject.SetActive((_panelHighScores.activeSelf) ? false : true);

        }   // ButtonHighScoresClicked()
        #endregion


        #region .  ButtonPlayAgainClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonPlayAgainClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ButtonPlayAgainClicked()
        {
            _panelGameOver.gameObject.SetActive(false);

            List<Node> occupiedNodes = _nodes.Where(n => n.OccupiedBlock != null).ToList();

            foreach (Node node in occupiedNodes)
            {
                RemoveBlock(node.OccupiedBlock);
            }

            ChangeState(GameState.Initialize);

        }   // ButtonPlayAgainClicked()
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
            //SceneManager.LoadScene("Scene_MainMenu");

            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();

        }   // ButtonQuitClicked()
        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   ChangeState()
        //   GenerateLevel()
        //   GetNodeAtPosition()
        //   Initialize()
        //   GameOver()
        //   MergeBlocks
        //   RemoveBlock()
        //   ShiftBlocks()
        //   Start()
        //   SpawnBlock()
        //   SpawnBlocks()
        //   Update()
        // ---------------------------------------------------------------------

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

            switch (newState)
            {
                case GameState.Initialize:
                    Initialize();
                    ChangeState(GameState.GenerateLevel);
                    break;

                case GameState.GenerateLevel:
                    GenerateLevel();
                    ChangeState(GameState.SpawnBlocks);
                    break;

                case GameState.SpawnBlocks:
                    GameState state = SpawnBlocks((_round++ == 0) ? 2 : 1);
                    ChangeState(state);
                    break;

                case GameState.WaitingInput:
                    break;

                case GameState.Moving:
                    break;

                case GameState.LoseGame:
                    GameOver(newState);
                    break;

                case GameState.WinGame:
                    GameOver(newState);
                    break;

                case GameState.Quit:
                    break;
            }

            EventManager.RaiseOnGameStateChanged(newState);

        }   // ChangeState()
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
            int index = 0;

            for (int x = 0; x < _height; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    Node node  = Instantiate(_nodePrefab, new Vector2(x, y), Quaternion.identity);
                    node.Index = index++;
                    _nodes.Add(node);
                }
            }

            //ChangeState(GameState.SpawnBlocks);

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


        #region .  Initialize()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Initialize()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Initialize()
        {
            _panelGameOver.SetActive(false);

            _blocks = new List<Block>();
            _nodes  = new List<Node>();
            _round  = 0;

            this.Moves = 0;
            this.Score = 0;

            var center = new Vector2((float)(_width / 2f) - 0.5f, (float)(_height / 2f) - 0.5f);
            var board  = Instantiate(_boardPrefab, center, Quaternion.identity);
            board.size = new Vector2(_width, _height);

            _camera.transform.position = new Vector3(center.x, center.y + 0.5f, -10);

            //OnBestScoreChanged?.Invoke(HighScoresManager.Instance.BestScore);

            //ChangeState(GameState.GenerateLevel);

        }   // Initialize()
        #endregion


        #region .  GameOver()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GameOver()
        //   Description..:  
        //   Parameters...:  GameState
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void GameOver(GameState state)
        {
            EventManager     .RaiseOnGameOver(state);
            HighScoresManager.Instance.CheckForHighScore(this.Score, this.Moves);

        }   // GameOver()
        #endregion


        #region .  MergeBlocks()  .
        // ---------------------------------------------------------------------
        //   Method.......:  MergeBlocks()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void MergeBlocks(Block baseBlock, Block mergingBlock)
        {
            int value = baseBlock.Value * 2;

            SpawnBlock(baseBlock.Node, value);
            RemoveBlock(baseBlock);
            RemoveBlock(mergingBlock);

            this.Score += value;

        }   // MergeBlocks()
        #endregion


        #region.  RemoveBlock()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RemoveBlock()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void RemoveBlock(Block block)
        {
            _blocks.Remove(block);
            Destroy(block.gameObject);

        }   // RemoveBlock()
        #endregion


        #region .  ShiftBlocks()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ShiftBlocks()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void ShiftBlocks(Vector2 direction)
        {
            ChangeState(GameState.Moving);

            List<Block> orderBlocks = _blocks.OrderBy(b => b.Position.x).ThenBy(b => b.Position.y).ToList();

            if (direction == Vector2.right || direction == Vector2.up)
            {
                orderBlocks.Reverse();
            }

            foreach (Block block in orderBlocks)
            {
                Node next = block.Node;

                do
                {
                    block.SetBlock(next);

                    Node possibleNode = GetNodeAtPosition(next.Position + direction);
                    if (possibleNode != null)
                    {
                        // A node is present, so if it's possible to merge than set the merge.
                        if ((possibleNode.OccupiedBlock != null) && (possibleNode.OccupiedBlock.CanMerge(block.Value)))
                        {
                            block.MergeBlock(possibleNode.OccupiedBlock);
                        }
                        else if (possibleNode.OccupiedBlock == null)
                        {
                            next = possibleNode;
                        }
                    }

                } while (next != block.Node);
            }

            Sequence mySequence = DOTween.Sequence();

            foreach (Block block in orderBlocks)
            {
                Vector2 movePoint = (block.MergingBlock != null) ? block.MergingBlock.Node.Position : block.Node.Position;
                mySequence.InsertCallback(0f, () => SoundManager.Instance.PlaySound(SoundManager.Sounds.Swish_3))
                          .Insert(0, block.transform.DOMove(block.Node.Position, _travelTime));
            }

            mySequence.OnComplete(() =>
            {
                foreach (Block block in orderBlocks.Where(b => b.MergingBlock != null))
                {
                    MergeBlocks(block.MergingBlock, block);
                }

                ChangeState(GameState.SpawnBlocks);
            });

        }   // ShiftBlocks()
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
            int height = Screen.height;
            int width  = Screen.width;

            if (_winAmount == WinCondition.None) _winAmount = WinCondition.Win32;
            EventManager.RaiseOnWinConditionChanged(_winAmount);

            ChangeState(GameState.Initialize);

        }   // Start()
        #endregion


        #region .  SpawnBlock()  .
        // ---------------------------------------------------------------------
        //   Method.......:  SpawnBlock()
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void SpawnBlock(Node node, int value)
        {
            BlockType blockType = GetBlockTypeByValue(value);
            Block     newBlock  = Instantiate(blockType.BlockPrefab, node.Position, Quaternion.identity);

            newBlock.Value = blockType.Value;
            newBlock.SetBlock(node);

            _blocks.Add(newBlock);

        }   // SpawnBlock
        #endregion


        #region .  SpawnBlocks()  .
        // ---------------------------------------------------------------------
        //   Method.......:  SpawnBlocks()
        //   Description..:  Spawn either 2 blocks (first time) or 1 block (for
        //                   for all remaining blocks).  Check for WinGame or
        //                   GameOver state, if neither return WaitingInput.
        //   Parameters...:  int - 1 or 2
        //   Returns......:  GameState : GameOver | WinGame | WaitingInput
        // ---------------------------------------------------------------------
        private GameState SpawnBlocks(int amount)
        {
            GameState state = GameState.WaitingInput;

            var availableNodes = _nodes.Where(n => n.OccupiedBlock == null).OrderBy(b => Random.value).ToList();

            // Get 2 nodes on the first round and 1 node for all of the rest.
            foreach (var node in availableNodes.Take(amount))
            {
                // Spawn a random node:  80% chance for a 2, 20% chance for a 4.
                SpawnBlock(node, Random.value > 0.8 ? 4 : 2);
            }

            if (availableNodes.Count() == 1)
            {
                state = GameState.LoseGame;
            }
            else if (_blocks.Any(b => b.Value == (int)_winAmount))
            {
                state = GameState.WinGame;
            }

            //state = (t)
            //      ? GameState.GameOver
            //      : _blocks.Any(b => b.Value == (int)_winAmount) ? GameState.WinGame
            //                                                     : GameState.WaitingInput;

            return state;

            //// Check for the game over condition (when there is only one free node left).
            //if (availableNodes.Count() == 1)
            //{
            //    //ChangeState(GameState.GameOver);
            //    //return;
            //    state = GameState.GameOver;
            //}
            //else
            //{
            //    // Check for a wining condition, if not then continue waiting for the users' next move.
            //    state = _blocks.Any(b => b.Value == _winAmount) ? GameState.WinGame : GameState.WaitingInput;

            //    //foreach (var block in _blocks)
            //    //{
            //    //    if (block.Value == _winAmount)
            //    //    {
            //    //        //ChangeState(GameState.WinGame);
            //    //        //return;
            //    //        state = GameState.WinGame;
            //    //    }
            //    //}
            //}

            //// Check for a wining condition, if not then continue waiting for the users' next move.
            //GameState newState = _blocks.Any(b => b.Value == _winAmount) ? GameState.WinGame : GameState.WaitingInput;

            //Debug.Log($"newState = {newState.ToString()}, Max block value = {_blocks.Max(b => b.Value)}, Need {_winAmount} to win.");

            //ChangeState(newState);
            //ChangeState(GameState.WaitingInput);

        }   // SpawnBlocks
        #endregion


        #region .  Update()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Update()
        //   Description..:  If the current state isn't WaitingInput do nothing.
        //                   Otherwise, check for user input.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Update()
        {
            if (_currentState != GameState.WaitingInput) return;

            if (Input.GetKeyDown(KeyCode.LeftArrow )) { EventManager.RaiseOnMovesChanged(++this.Moves); ShiftBlocks(Vector2.left ); }
            if (Input.GetKeyDown(KeyCode.RightArrow)) { EventManager.RaiseOnMovesChanged(++this.Moves); ShiftBlocks(Vector2.right); }
            if (Input.GetKeyDown(KeyCode.UpArrow   )) { EventManager.RaiseOnMovesChanged(++this.Moves); ShiftBlocks(Vector2.up   ); }
            if (Input.GetKeyDown(KeyCode.DownArrow))  { EventManager.RaiseOnMovesChanged(++this.Moves); ShiftBlocks(Vector2.down ); }

        }   // Update()
        #endregion


    }   // class GameManager

}   // namespace Assets.Scenes.Game2048.Scripts
