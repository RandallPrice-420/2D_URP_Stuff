using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
        Blank,
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
        //   _height
        //   _width
        //   _camera
        //   _blockPrefab
        //   _blockTypes
        //   _boardPrefab
        //   _buttonBackToMenu
        //   _buttonContinue
        //   _buttonDisabled
        //   _buttonQuit
        //   _nodePrefab
        //   _travelTime
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        [SerializeField] private int              _height;
        [SerializeField] private int              _width;
        [SerializeField] private Camera           _camera;
        [SerializeField] private Block            _blockPrefab;
        [SerializeField] private List<BlockType>  _blockTypes;
        [SerializeField] private SpriteRenderer   _boardPrefab;
        [SerializeField] private Button           _buttonBackToMenu;
        [SerializeField] private Button           _buttonContinue;
        [SerializeField] private Button           _buttonDisabled;
        [SerializeField] private Button           _buttonQuit;
        [SerializeField] private Node             _nodePrefab;
        [SerializeField] private float            _travelTime = 0.5f;

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
        //   ButtonBackToMenuClicked()
        //   ButtonContinueClicked()
        //   ButtonQuitClicked()
        // ---------------------------------------------------------------------

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


        #region .  ButtonContinueClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonContinueClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ButtonContinueClicked()
        {
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



        // ---------------------------------------------------------------------
        // private Methods:
        // ----------------
        //   ChangeState()
        //   GameOver
        //   GenerateLevel()
        //   GetNodeAtPosition()
        //   Start()
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

            OnGameStateChanged?.Invoke(newState);

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


        #region .  MergeBlocks()  .
        // ---------------------------------------------------------------------
        //   Method.......:  MergeBlocks()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void MergeBlocks(Block baseBlock, Block mergingBlock)
        {
            SpawnBlock(baseBlock.Node, baseBlock.Value * 2);

            RemoveBlock(baseBlock);
            RemoveBlock(mergingBlock);

        }   // MergeBlocks()
        #endregion


        #region .  RemoveBlock()  .
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


        #region .  Shift()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Shift()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Shift(Vector2 direction)
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
            _currentState = GameState.Blank;
            ChangeState(GameState.GenerateLevel);

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
            var freeNodes = _nodes.Where(n => n.OccupiedBlock == null).OrderBy(b => Random.value).ToList();

            foreach (var node in freeNodes.Take(amount))
            {
                SpawnBlock(node, Random.value > 0.8 ? 4 : 2);
            }

            //if (freeNodes.Count() == 1)
            if (freeNodes.Count() < 2)
            {
                // Lost the game.
                OnGameOver?.Invoke();
                return;
            }

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

            if (Input.GetKeyDown(KeyCode.LeftArrow )) Shift(Vector2.left );
            if (Input.GetKeyDown(KeyCode.RightArrow)) Shift(Vector2.right);
            if (Input.GetKeyDown(KeyCode.UpArrow   )) Shift(Vector2.up   );
            if (Input.GetKeyDown(KeyCode.DownArrow )) Shift(Vector2.down );

        }   // Update()
        #endregion


    }   // class GameManager

}   // namespace Assets.Scenes.Game2048.Scripts
