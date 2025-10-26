using UnityEngine;


namespace Assets.Scenes.Game2048_Old.Scripts
{
    [CreateAssetMenu(menuName = "Tile State")]
    public class TileState : ScriptableObject
    {
        public int   Number;
        public Color BackgroundColor;
        public Color TextColor;

    }   // class TileState

}   // namespace Assets.Scenes.Game2048_Old.Scripts
