using UnityEngine;


namespace Assets.Scenes.Game2048_Old.Scripts
{
    public class TileGrid : MonoBehaviour
    {
        public TileRow[]  Rows  { get; private set; }
        public TileCell[] Cells { get; private set; }

        public int Size   => Cells.Length;
        public int Height => Rows.Length;
        public int Width  => Size / Height;



        private void Awake()
        {
            Rows  = GetComponentsInChildren<TileRow>();
            Cells = GetComponentsInChildren<TileCell>();

            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i].Coordinates = new Vector2Int(i % Width, i / Width);
            }

        }   // Awake()


        public TileCell GetCell(Vector2Int coordinates)
        {
            return GetCell(coordinates.x, coordinates.y);

        }   // GetCell()


        public TileCell GetCell(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                return Rows[y].Cells[x];
            }
            else
            {
                return null;
            }

        }   // GetCell()


        public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
        {
            Vector2Int coordinates = cell.Coordinates;
            coordinates.x += direction.x;
            coordinates.y -= direction.y;

            return GetCell(coordinates);

        }   // GetAdjacentCell()


        public TileCell GetRandomEmptyCell()
        {
            int index         = Random.Range(0, Cells.Length);
            int startingIndex = index;

            while (Cells[index].Occupied)
            {
                index++;

                if (index >= Cells.Length)
                {
                    index = 0;
                }

                // All cells are occupied.
                if (index == startingIndex)
                {
                    return null;
                }
            }

            return Cells[index];

        }   // GetRandomEmptyCell()


    }   // class TileGrid

}   // namespace Assets.Scenes.Game2048_Old.Scripts
