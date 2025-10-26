using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scenes.Game2048_Old.Scripts
{
    public class Tile : MonoBehaviour
    {
        public TileState State  { get; private set; }
        public TileCell  Cell   { get; private set; }
        public bool      Locked { get; set; }



        private Image           _background;
        private TextMeshProUGUI _text;



        public void Merge(TileCell cell)
        {
            if (this.Cell != null)
            {
                this.Cell.Tile = null;
            }

            this.Cell        = null;
            cell.Tile.Locked = true;

            StartCoroutine(Animate(cell.transform.position, true));

        }   // Merge()


        public void MoveTo(TileCell cell)
        {
            if (this.Cell != null)
            {
                this.Cell.Tile = null;
            }

            this.Cell      = cell;
            this.Cell.Tile = this;

            StartCoroutine(Animate(cell.transform.position, false));

        }   // MoveTo()


        public void SetState(TileState state)
        {
            this.State = state;

            _background.color = state.BackgroundColor;
            _text.color       = state.TextColor;
            _text.text        = state.Number.ToString();

        }   // SetState()


        public void Spawn(TileCell cell)
        {
            if (this.Cell != null)
            {
                this.Cell.Tile = null;
            }

            this.Cell      = cell;
            this.Cell.Tile = this;

            transform.position = cell.transform.position;

        }   // Spawn()



        private void Awake()
        {
            _background = GetComponent<Image>();
            _text       = GetComponentInChildren<TextMeshProUGUI>();

        }   // Awake()


        private IEnumerator Animate(Vector3 to, bool merging)
        {
            float elapsed  = 0f;
            float duration = 0.1f;

            Vector3 from = transform.position;

            while (elapsed < duration)
            {
                transform.position = Vector3.Lerp(from, to, elapsed / duration);
                elapsed           += Time.deltaTime;

                yield return null;
            }

            transform.position = to;

            if (merging)
            {
                Destroy(gameObject);
            }

        }   // Animate()


    }   // class Tile

}   // namespace Assets.Scenes.Game2048_Old.Scripts