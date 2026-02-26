using UnityEngine;


namespace Assets.Scenes.Game2048.Scripts
{
    public class Node : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        //
        //      |-----|-----|-----|-----|
        //      |  3  |  7  | 11  | 15  |
        //      |-----|-----|-----|-----|
        //      |  2  |  6  | 10  | 14  |
        //      |-----|-----|-----|-----|
        //      |  1  |  5  |  9  | 13  |
        //      |-----|-----|-----|-----|
        //      |  0  |  4  |  8  | 12  |
        //      |-----|-----|-----|-----|
        //
        // ---------------------------------------------------------------------

        // ---------------------------------------------------------------------
        // Public Variables:
        // -----------------
        //   Index
        //   OccupiedBlock
        // ---------------------------------------------------------------------

        #region .  Public Variables  .

        public int   Index;
        public Block OccupiedBlock;

        #endregion



        // ---------------------------------------------------------------------
        // Public Getters:
        // ---------------
        //   Position
        // ---------------------------------------------------------------------

        #region .  Public Getters  .

        public Vector2 Position => this.transform.position;

        #endregion


    }   // class Node

}   // namespace Assets.Scenes.Game2048.Scripts
