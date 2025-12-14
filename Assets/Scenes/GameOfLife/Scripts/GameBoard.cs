using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[DefaultExecutionOrder(-1)]
public class GameBoard : Singleton<GameBoard>
{
    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   Iterations
    //   PatternTime
    //   Population
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public int   Iterations  { get; private set; }
    public float PatternTime { get; private set; }
    public int   Population  { get; private set; }

    #endregion



    // -------------------------------------------------------------------------
    // Private Serialize Fields:
    // -------------------------
    //   _currentStat
    //   _nextStat
    //   _aliveTil
    //   _deadTil
    //   _pattern
    //   _updateInterval
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    [SerializeField] private Tilemap _currentState;
    [SerializeField] private Tilemap _nextState;
    [SerializeField] private Tile    _aliveTile;
    [SerializeField] private Tile    _deadTile;
    [SerializeField] private Pattern _pattern;
    [SerializeField] private float   _updateInterval = 0.05f;

    #endregion



    // -------------------------------------------------------------------------
    // Private Properties:
    // -------------------
    //   _aliveCells
    //   _cellsToCheck
    //   _UIManager
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    private readonly HashSet<Vector3Int> _aliveCells   = new HashSet<Vector3Int>();
    private readonly HashSet<Vector3Int> _cellsToCheck = new HashSet<Vector3Int>();

    private UIManager _UIManager;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   Play()
    // -------------------------------------------------------------------------

    #region .  Play()  .
    public void Play(Pattern pattern)
    {
        this._pattern = pattern;

        this.SetPattern(_pattern);

        StartCoroutine(this.Simulate());

    }   // StartGame()
    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Clear()
    //   CountNeighbors()
    //   IsAlive()
    //   SetPattern()
    //   Simulate()
    //   Start()
    //   UpdateState()
    // -------------------------------------------------------------------------

    #region .  Clear()  .
    private void Clear()
    {
        this._aliveCells  .Clear();
        this._cellsToCheck.Clear();
        this._currentState.ClearAllTiles();
        this._nextState   .ClearAllTiles();

        this.Iterations  = 0;
        this.Population  = 0;
        this.PatternTime = 0f;

    }   // Clear()
    #endregion


    #region .  CountNeighbors()  .
    private int CountNeighbors(Vector3Int cell)
    {
        int count = 0;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                Vector3Int neighbor = cell + new Vector3Int(x, y, 0);

                if (this.IsAlive(neighbor)) count++;
            }
        }

        return count;

    }   // CountNeighbors()
    #endregion


    #region .  IsAlive()  .
    private bool IsAlive(Vector3Int cell)
    {
        return this._currentState.GetTile(cell) == _aliveTile;

    }   // IsAlive()
    #endregion


    #region .  SetPattern()  .
    private void SetPattern(Pattern pattern)
    {
        this.Clear();

        for (int i = 0; i < pattern.cells.Length; i++)
        {
            Vector3Int cell = (Vector3Int)(pattern.cells[i] - pattern.GetCenter());
            this._currentState.SetTile(cell, this._aliveTile);
            this._aliveCells.Add(cell);
        }

        this.Population = this._aliveCells.Count;

    }   // SetPattern()
    #endregion


    #region .  Simulate()  .
    private IEnumerator Simulate()
    {
        var interval = new WaitForSeconds(this._updateInterval);
        yield return interval;

        while (this.enabled)
        {
            this.UpdateState();

            this.Population = this._aliveCells.Count;
            this.Iterations++;

            this.PatternTime += this._updateInterval;

            yield return interval;
        }

    }   // Simulate()
    #endregion


    #region .  Start()  .
    private void Start()
    {
        this._UIManager = FindObjectOfType<UIManager>();
        this._pattern   = this._UIManager.GetCurrentPattern();

        //this.Play();

    }   // Start()
    #endregion


    #region .  UpdateState()  .
    private void UpdateState()
    {
        this._cellsToCheck.Clear();

        // Gather cells to check.
        foreach (Vector3Int cell in this._aliveCells)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    this._cellsToCheck.Add(cell + new Vector3Int(x, y, 0));
                }
            }
        }

        // Transition cells to the next state.
        foreach (Vector3Int cell in this._cellsToCheck)
        {
            int  neighbors = this.CountNeighbors(cell);
            bool alive     = this.IsAlive(cell);

            if (!alive && neighbors == 3)
            {
                this._currentState.SetTile(cell, this._aliveTile);
                this._aliveCells.Add(cell);
            }
            else if (alive && (neighbors < 2 || neighbors > 3))
            {
                this._nextState.SetTile(cell, this._deadTile);
                this._aliveCells.Remove(cell);
            }
            else // no change
            {
                this._nextState.SetTile(cell, this._currentState.GetTile(cell));
            }
        }

        // Swap current state with next state.
        (this._nextState, this._currentState) = (this._currentState, this._nextState);
        this._nextState.ClearAllTiles();

    }   // UpdateState()
    #endregion


}   // class GameBoard
