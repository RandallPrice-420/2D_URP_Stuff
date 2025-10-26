using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scenes.MainMenu.Scripts.LineDrawing
{
    public class Line : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Serialized Fields:
        // ------------------
        //   _edgeCollider
        //   _lineRenderer
        //   _points
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        [SerializeField] private EdgeCollider2D _edgeCollider;
        [SerializeField] private LineRenderer   _lineRenderer;

        #endregion



        // ---------------------------------------------------------------------
        // Private Properties:
        // -------------------
        //   _linePoints
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private readonly List<Vector2> _linePoints = new();

        #endregion



        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   ClearLine()
        //   SetPosition()
        // ---------------------------------------------------------------------

        #region .  ClearLine()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ClearLine()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ClearLine()
        {
            _lineRenderer.positionCount = 0;
            _linePoints.Clear();

            Destroy(_lineRenderer.GetComponentInChildren<EdgeCollider2D>().gameObject);

        }   // ClearLine()
        #endregion


        #region .  SetPosition()  .
        // ---------------------------------------------------------------------
        //   Method.......:  SetPosition()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void SetPosition(Vector2 position)
        {
            if (CanAppend(position))
            {
                _linePoints.Add(position);

                int pointCount = _lineRenderer.positionCount;
                _lineRenderer.positionCount = pointCount + 1;
                _lineRenderer.SetPosition(pointCount, position);

                _edgeCollider.points = _linePoints.ToArray();
            }

        }   // SetPosition()
        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   CanAppend()
        //   Start()
        // ---------------------------------------------------------------------

        #region .  CanAppend()  .
        // ---------------------------------------------------------------------
        //   Method.......:  CanAppend()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private bool CanAppend(Vector2 currrentPosition)
        {
            if (_lineRenderer.positionCount == 0)
                return true;

            Vector2 lastPosition = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);
            float   distance     = Vector2.Distance(lastPosition, currrentPosition);

            return (distance >= Managers.DrawManager.RESOLUTION);

        }   // CanAppend()
        #endregion


        #region .  Start()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Start()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Start()
        {
            _edgeCollider.transform.position -= this.transform.position;

        }   // Start()
        #endregion


    }   // class Line

}   // namespace Assets.Scenes.MainMenu.Scripts.LineDrawing
