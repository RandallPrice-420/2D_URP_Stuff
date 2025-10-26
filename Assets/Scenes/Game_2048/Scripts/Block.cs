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
        // ---------------------------------------------------------------------

        #region .  Public Methods  .

        public int  Value;
        public Node Node;

        #endregion



        // ---------------------------------------------------------------------
        // Private Getters:
        // ----------------
        //   Position
        // ---------------------------------------------------------------------

        #region .  Public Getters  .

        public Vector2 Position => transform.position;

        #endregion



       // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   SetBlock()
        // ---------------------------------------------------------------------

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
