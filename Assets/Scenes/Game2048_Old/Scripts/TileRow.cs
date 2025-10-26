using UnityEngine;


namespace Assets.Scenes.Game2048_Old.Scripts
{
    public class TileRow : MonoBehaviour
    {
        public TileCell[] Cells { get; private set; }



        private void Awake()
        {
            Cells = GetComponentsInChildren<TileCell>();

        }   // Awake()


    }   // class TileRow

}   // namespace Assets.Scenes.Game2048_Old.Scripts
