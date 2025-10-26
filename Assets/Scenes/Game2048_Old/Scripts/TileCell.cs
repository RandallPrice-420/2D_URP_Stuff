using UnityEngine;


namespace Assets.Scenes.Game2048_Old.Scripts
{
    public class TileCell : MonoBehaviour
    {
        public Vector2Int Coordinates { get; set; }

        public Tile       Tile        { get; set; }

        public bool       Empty       => Tile == null;

        public bool       Occupied    => Tile != null;


    }   // class TileCell

} // namespace Assets.Scenes.Game2048_Old.Scripts
