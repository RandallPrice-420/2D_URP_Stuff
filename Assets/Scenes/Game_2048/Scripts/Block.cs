using UnityEngine;


namespace Assets.Scenes.Game2048.Scripts
{
    public class Block : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   Value
        //   Node
        //   Merging
        //   MergingBlock
        // ---------------------------------------------------------------------

        #region .  Public Methods  .

        public int   Value;
        public Node  Node;
        public bool  Merging;
        public Block MergingBlock;

        #endregion



        // ---------------------------------------------------------------------
        // Private Getters:
        // ----------------
        //   CanMerge
        //   Position
        // ---------------------------------------------------------------------

        #region .  Public Getters  .

        public bool CanMerge(int value) => (value        == Value) && 
                                           (Merging      == false) &&
                                           (MergingBlock == null );

        public Vector2 Position => transform.position;

        #endregion



        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   MergeBlock()
        //   SetBlock()
        // ---------------------------------------------------------------------

        #region .  MergeBlock()  .
        // ---------------------------------------------------------------------
        //   Method.......:  MergeBlock()
        //   Description..:  
        //   Parameters...:  Node
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void MergeBlock(Block blockToMergeWith)
        {
            // Set the block to merge with.
            MergingBlock = blockToMergeWith;

            // Set current as unoccupied to allow other blocks to use it.
            Node.OccupiedBlock = null;

            // Set the base block as merging so it does not get used more than once.
            blockToMergeWith.Merging = true;

        }   // SetMergeBlockBlock()
        #endregion


        #region .  SetBlock()  .
        // ---------------------------------------------------------------------
        //   Method.......:  SetBlock()
        //   Description..:  
        //   Parameters...:  Node
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void SetBlock(Node newNode)
        {
            if (Node != null) Node.OccupiedBlock = null;
            Node = newNode;
            Node.OccupiedBlock = this;

        }   // SetBlock()
        #endregion


    }   // class Block

}   // namespace Assets.Scenes.Game2048.Scripts
