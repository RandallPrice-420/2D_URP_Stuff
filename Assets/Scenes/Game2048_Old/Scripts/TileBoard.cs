using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scenes.Game2048_Old.Scripts
{
    public class TileBoard : MonoBehaviour
    {
        [SerializeField] private Tile        tilePrefab;
        [SerializeField] private TileState[] tileStates;



        private TileGrid   grid;
        private List<Tile> tiles;
        private bool       waiting;



        public bool CheckForGameOver()
        {
            if (tiles.Count != grid.Size)
            {
                return false;
            }

            foreach (var tile in tiles)
            {
                TileCell up    = grid.GetAdjacentCell(tile.Cell, Vector2Int.up);
                TileCell down  = grid.GetAdjacentCell(tile.Cell, Vector2Int.down);
                TileCell left  = grid.GetAdjacentCell(tile.Cell, Vector2Int.left);
                TileCell right = grid.GetAdjacentCell(tile.Cell, Vector2Int.right);

                if (up != null    && CanMerge(tile, up.Tile))
                {
                    return false;
                }

                if (down != null  && CanMerge(tile, down.Tile))
                {
                    return false;
                }

                if (left != null  && CanMerge(tile, left.Tile))
                {
                    return false;
                }

                if (right != null && CanMerge(tile, right.Tile))
                {
                    return false;
                }
            }

            return true;

        }   // CheckForGameOver()


        public void ClearBoard()
        {
            foreach (var cell in grid.Cells)
            {
                cell.Tile = null;
            }

            foreach (var tile in tiles)
            {
                Destroy(tile.gameObject);
            }

            tiles.Clear();

        }   //  ClearBoard()


        public void CreateTile()
        {
            Tile tile = Instantiate(tilePrefab, grid.transform);

            tile.SetState(tileStates[0]);
            tile.Spawn(grid.GetRandomEmptyCell());
            tiles.Add(tile);

        }   // CreateTile()



        private void Awake()
        {
            grid  = GetComponentInChildren<TileGrid>();
            tiles = new List<Tile>(16);

        }   // Awake()


        private bool CanMerge(Tile a, Tile b)
        {
            return a.State == b.State && !b.Locked;

        }   // CanMerge()


        private int IndexOf(TileState state)
        {
            for (int i = 0; i < tileStates.Length; i++)
            {
                if (state == tileStates[i])
                {
                    return i;
                }
            }

            return -1;

        }   // IndexOf()


        private void MergeTiles(Tile a, Tile b)
        {
            tiles.Remove(a);
            a.Merge(b.Cell);

            int index = Mathf.Clamp(IndexOf(b.State) + 1, 0, tileStates.Length - 1);
            TileState newState = tileStates[index];

            b.SetState(newState);
            GameManager.Instance.IncreaseScore(newState.Number);

        }   // MergeTiles()


        private void Move(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
        {
            bool changed = false;

            for (int x = startX; x >= 0 && x < grid.Width; x += incrementX)
            {
                for (int y = startY; y >= 0 && y < grid.Height; y += incrementY)
                {
                    TileCell cell = grid.GetCell(x, y);

                    if (cell.Occupied)
                    {
                        changed |= MoveTile(cell.Tile, direction);
                    }
                }
            }

            if (changed)
            {
                StartCoroutine(WaitForChanges());
            }

        }   // Move()


        private bool MoveTile(Tile tile, Vector2Int direction)
        {
            TileCell newCell  = null;
            TileCell adjacent = grid.GetAdjacentCell(tile.Cell, direction);

            while (adjacent != null)
            {
                if (adjacent.Occupied)
                {
                    if (CanMerge(tile, adjacent.Tile))
                    {
                        MergeTiles(tile, adjacent.Tile);
                        return true;
                    }

                    break;
                }

                newCell  = adjacent;
                adjacent = grid.GetAdjacentCell(adjacent, direction);
            }

            if (newCell != null)
            {
                tile.MoveTo(newCell);
                return true;
            }

            return false;

        }   // MoveTile()


        private void Update()
        {
            if (waiting)
                return;

            if      (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(Vector2Int.up, 0, 1, 1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector2Int.left, 1, 1, 0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(Vector2Int.down, 0, 1, grid.Height - 2, -1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector2Int.right, grid.Width - 2, -1, 0, 1);
            }

        }   // Update()


        private IEnumerator WaitForChanges()
        {
            waiting = true;

            yield return new WaitForSeconds(0.1f);

            waiting = false;

            foreach (var tile in tiles)
            {
                tile.Locked = false;
            }

            if (tiles.Count != grid.Size)
            {
                CreateTile();
            }

            if (CheckForGameOver())
            {
                GameManager.Instance.GameOver();
            }

        }   // WaitForChanges()


    }   // class TileBoard

}   // namespace Assets.Scenes.Game2048_Old.Scripts