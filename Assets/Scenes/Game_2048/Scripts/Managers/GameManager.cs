using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        Initialize,
        GenerateLevel,
        SpawnBlocks,
        WaitingInput,
        Moving,
        LoseGame,
        WinGame,
        Quit
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

        public static event Action<int>       OnBestScoreChanged    = delegate { };
        public static event Action<GameState> OnGameOver            = delegate { };
        public static event Action<GameState> OnGameStateChanged    = delegate { };
        public static event Action<int>       OnMoveChanged         = delegate { };
        public static event Action<int>       OnScoreChanged        = delegate { };
        public static event Action<int>       OnWinConditionChanged = delegate { };

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
        //   _playerNameLength
        //   _travelTime
        //   _winCondition
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
        [SerializeField] private int             _playerNameLength = 5;
        [SerializeField] private float           _travelTime       = 0.5f;
        [SerializeField] private int             _winCondition     = 0;
        [SerializeField] private TMP_Text        _textHowToPlay;

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
            HighScoresManager.Instance.AddHighScoreEntry(Random.Range(1, 10000), Random.Range(30, 100), GetPlayerName());
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
            HighScoresManager.Instance.ClearHighScores();

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
        //   CheckForHighScore()
        //   GenerateLevel()
        //   GetNodeAtPosition()
        //   GetPlayerName()
        //   GetRandomString()
        //   Initialize()
        //   LoseGame
        //   MergeBlocks
        //   RemoveBlock()
        //   ShiftBlocks()
        //   Start()
        //   SpawnBlock()
        //   SpawnBlocks()
        //   Update()
        //   WinGame
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
                    break;

                case GameState.GenerateLevel:
                    GenerateLevel();
                    break;

                case GameState.SpawnBlocks:
                    SpawnBlocks((_round++ == 0) ? 2 : 1);
                    break;

                case GameState.WaitingInput:
                    break;

                case GameState.Moving:
                    break;

                case GameState.LoseGame:
                    LoseGame();
                    break;

                case GameState.WinGame:
                    WinGame();
                    break;

                case GameState.Quit:
                    break;
            }

            OnGameStateChanged?.Invoke(newState);

        }   // ChangeState()
        #endregion


        #region .  CheckForHighScore()  .
        // ---------------------------------------------------------------------
        //   Method.......:  CheckForHighScore()
        //   Description..:  Check if the score earned a spot in the HighScores
        //                   list.  The list only holds the top 10 scores so to
        //                   add this score it must be greater than the lowest
        //                   score.  Also, check if the score is the new best!
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void CheckForHighScore()
        {
            if (HighScoresManager.Instance.HighScoreCount < HighScoresManager.Instance.MaximumEntries)
            {
                HighScoresManager.Instance.AddHighScoreEntry(_score, _moves, GetPlayerName());
                HighScoresManager.Instance.GetHighScores();
                HighScoresManager.Instance.DisplayHighScores();
            }
            else
            {
                print($"Cannot add a new entry, already reached the maximum of {HighScoresManager.Instance.MaximumEntries}");
            }

            // Did you beat the best score?
            if (_score > HighScoresManager.Instance.BestScore)
            {
                // Congratulations!
                HighScoresManager.Instance.BestScore = _score;
                OnBestScoreChanged?.Invoke(HighScoresManager.Instance.BestScore);
            }

        }   // CheckForHighScore()
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

            ChangeState(GameState.SpawnBlocks);

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


        #region .  GetPlayerName()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetPlayerName()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  string
        // ---------------------------------------------------------------------
        private string GetPlayerName()
        {
            string name = GetRandomString(_playerNameLength);

            return name;

        }   // GetPlayerName()
        #endregion


        #region .  GetRandomString()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetRandomString()
        //   Description..:  
        //   Parameters...:  string
        //                   int
        //   Returns......:  string
        // ---------------------------------------------------------------------
        private string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()<>?.";
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[Random.Range(0, chars.Length)];
            }

            return new string(result);

        }   // GetRandomString()
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
            _moves  = 0;
            _round  = 0;
            _score  = 0;

            var center = new Vector2((float)(_width / 2f) - 0.5f, (float)(_height / 2f) - 0.5f);
            var board  = Instantiate(_boardPrefab, center, Quaternion.identity);
            board.size = new Vector2(_width, _height);

            _camera.transform.position = new Vector3(center.x, center.y + 0.5f, -10);

            _bestScore   = HighScoresManager.Instance.BestScore;
            _lowestScore = HighScoresManager.Instance.BestScore;

            OnMoveChanged     ?.Invoke(_moves);
            OnScoreChanged    ?.Invoke(_score);
            OnBestScoreChanged?.Invoke(HighScoresManager.Instance.BestScore);

            ChangeState(GameState.GenerateLevel);

        }   // Initialize()
        #endregion


        #region .  LoseGame()  .
        // ---------------------------------------------------------------------
        //   Method.......:  LoseGame()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void LoseGame()
        {
            CheckForHighScore();

            OnGameOver?.Invoke(GameState.LoseGame);

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

            _score += value;
            OnScoreChanged?.Invoke(_score);

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

            var sequence = DOTween.Sequence();

            foreach (var block in orderBlocks)
            {
                var movePoint = (block.MergingBlock != null) ? block.MergingBlock.Node.Position : block.Node.Position;
                sequence.Insert(0, block.transform.DOMove(block.Node.Position, _travelTime));
            }

            sequence.OnComplete(() =>
            {
                foreach (var block in orderBlocks.Where(b => b.MergingBlock != null))
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

            if (_winCondition == 0) _winCondition = 32;
            _textHowToPlay.text = _textHowToPlay.text.Replace("%1", _winCondition.ToString());

            OnWinConditionChanged?.Invoke(_winCondition);

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
        //   Description..:  
        //   Parameters...:  int
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void SpawnBlocks(int amount)
        {
            var availableNodes = _nodes.Where(n => n.OccupiedBlock == null).OrderBy(b => Random.value).ToList();

            // Get 2 nodes on the first round and 1 node for all of the rest.
            foreach (var node in availableNodes.Take(amount))
            {
                // Spawn a random node:  80% chance for a 2, 20% chance for a 4.
                SpawnBlock(node, Random.value > 0.8 ? 4 : 2);
            }

            // Check for the game over condition (when there is only one free node left).
            if (availableNodes.Count() == 1)
            {
                ChangeState(GameState.LoseGame);
                return;
            }

            foreach (var block in _blocks)
            {
                if (block.Value == _winCondition)
                {
                    ChangeState(GameState.WinGame);
                    return;
                }
            }

            // Check for a wining condition, if not then continue waiting for the users' next move.
            GameState newState = _blocks.Any(b => b.Value == _winCondition) ? GameState.WinGame : GameState.WaitingInput;

            //Debug.Log($"newState = {newState.ToString()}, Max block value = {_blocks.Max(b => b.Value)}, Need {_winCondition} to win.");

            //ChangeState(newState);
            ChangeState(GameState.WaitingInput);

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

            if (Input.GetKeyDown(KeyCode.LeftArrow )) { OnMoveChanged?.Invoke(_moves++); ShiftBlocks(Vector2.left ); }
            if (Input.GetKeyDown(KeyCode.RightArrow)) { OnMoveChanged?.Invoke(_moves++); ShiftBlocks(Vector2.right); }
            if (Input.GetKeyDown(KeyCode.UpArrow   )) { OnMoveChanged?.Invoke(_moves++); ShiftBlocks(Vector2.up   ); }
            if (Input.GetKeyDown(KeyCode.DownArrow))  { OnMoveChanged?.Invoke(_moves++); ShiftBlocks(Vector2.down ); }

        }   // Update()
        #endregion


        #region .  WinGame()  .
        // ---------------------------------------------------------------------
        //   Method.......:  WinGame()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void WinGame()
        {
            CheckForHighScore();

            OnGameOver?.Invoke(GameState.WinGame);

        }   // WinGame()
        #endregion


    }   // class GameManager

}   // namespace Assets.Scenes.Game2048.Scripts
