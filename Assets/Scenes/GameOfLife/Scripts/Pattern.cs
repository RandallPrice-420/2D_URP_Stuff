using UnityEngine;


[CreateAssetMenu(menuName = "Game of Life/Pattern")]
//public class Pattern : Singleton<Pattern>
public class Pattern : ScriptableObject
{

    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   AllPatterns
    //   Cells
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public Pattern[]    AllPatterns;
    public Vector2Int[] cells;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   GetCenter
    // -------------------------------------------------------------------------

    #region .  GetCenter()  .
    public Vector2Int GetCenter()
    {
        if (this.cells == null || this.cells.Length == 0) {
            return Vector2Int.zero;
        }

        Vector2Int min = Vector2Int.zero;
        Vector2Int max = Vector2Int.zero;

        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector2Int cell = this.cells[i];
            min.x = Mathf.Min(min.x, cell.x);
            min.y = Mathf.Min(min.y, cell.y);
            max.x = Mathf.Max(max.x, cell.x);
            max.y = Mathf.Max(max.y, cell.y);
        }

        return (min + max) / 2;

    }   // GetCenter()
    #endregion



    // -------------------------------------------------------------------------    
    // Private Methods:
    // ----------------
    //   Awake()
    // -------------------------------------------------------------------------

    #region .  Awake()  .
    private void Awake()
    {
        this.AllPatterns = Resources.LoadAll<Pattern>("Patterns");

    }   // Awake()
    #endregion


}   // class Pattern
