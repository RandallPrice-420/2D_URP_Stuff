using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scenes.Game2048.Scripts
{
    public class Utils : Singleton<Utils>
    {
        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _countOfChildren
        //   _listOfChildren
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private int              _countOfChildren;
        private List<GameObject> _listOfChildren = new List<GameObject>();

        #endregion


        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   CountAllChildComponents()
        //   GetAllChildComponents()
        //   GetAllKidComponents()
        //   GetColorFromString()
        //   Hex_To_Dec()
        //   ScaleObjectX()
        //   ScaleObjectY()
        // ---------------------------------------------------------------------

        #region .  CountAllChildComponents()  .
        // ---------------------------------------------------------------------
        //   Method.......:  CountAllChildComponents()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public int CountAllChildComponents(GameObject parentObject, string startsWith = "")
        {
            CountAllChildRecursive(parentObject, startsWith);

            return _countOfChildren;

        }// CountAllChildComponents()
        #endregion


        #region .  GetAllChildComponents()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetAllChildComponents()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public List<GameObject> GetAllChildComponents(GameObject parentObject, string startsWith = "")
        {
            GetAllChildRecursive(parentObject, startsWith);

            return _listOfChildren;

        }// GetAllChildComponents()
        #endregion


        #region .  GetAllKidComponents()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetAllKidComponents()
        //   Description..:
        //   Parameters...:  None
        //   Returns......:  Nothing
        //   Exaple.......:  List<GameObject> kids = GetAllKidComponents(theObject.transform);
        // ---------------------------------------------------------------------
        public List<GameObject> GetAllKidComponents(Transform parentTransform)
        {
            List<GameObject> kids  = new List<GameObject>();
            Queue<Transform> queue = new Queue<Transform>();

            queue.Enqueue(parentTransform);

            while (queue.Count > 0)
            {
                var kidTransform = queue.Dequeue();

                kids.Add(kidTransform.gameObject);

                foreach (Transform transform in kidTransform)
                {
                    queue.Enqueue(transform);
                }
            }

            return kids;

        }   // GetAllKidComponents()
        #endregion


        #region .  GetColorFromString()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetColorFromString()
        //   Description..:  Get Color from Hex string, like FF00FFAA.
        //   Parameters...:  string
        //   Returns......:  Color
        // ---------------------------------------------------------------------
        public Color GetColorFromString(string color)
        {
            float red   = Hex_To_Dec(color.Substring(0, 2));
            float green = Hex_To_Dec(color.Substring(2, 2));
            float blue  = Hex_To_Dec(color.Substring(4, 2));
            float alpha = 1f;

            if (color.Length >= 8)
            {
                // Color string contains alpha.
                alpha = Hex_To_Dec(color.Substring(6, 2));
            }

            return new Color(red, green, blue, alpha);

        }   // GetColorFromString()
        #endregion


        #region .  Hex_To_Dec()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Hex_To_Dec()
        //   Description..:  Convert hex string yo a float.
        //   Parameters...:  string
        //   Returns......:  float
        // ---------------------------------------------------------------------
        public float Hex_To_Dec(string hex)
        {
            return Convert.ToInt32(hex, 16) / 255f;

        }   // Hex_To_Dec()
        #endregion


        #region .  ScaleObjectX()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ScaleObjectX()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ScaleObjectX(GameObject gameObject, float newSizeX)
        {
            //float   size  = gameObject.GetComponent<Renderer>().bounds.size.x;

            //float   sizeX      = gameObject.transform.localScale.x;
            //Vector3 localScale = gameObject.transform.localScale;
            //localScale.x       = newSizeX * localScale.x / sizeX;


            Vector3 localScale = gameObject.transform.localScale;               // Get the current scale, e.g:  <1.0, 2.0, 3.0>
            localScale.x = (newSizeX * localScale.x) / localScale.x;      // Calculate new X scale, e.g:  <5.0, 2.0, 3.0>     newSizeX = 5.0

            gameObject.transform.localScale = localScale;                       // Change the gameObject scale

        }   // ScaleObjectX()
        #endregion


        #region .  ScaleObjectY()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ScaleObjectY()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ScaleObjectY(GameObject gameObject, float ySize)
        {
            float size = gameObject.GetComponent<Renderer>().bounds.size.y;

            Vector3 scale = gameObject.transform.localScale;

            scale.y = ySize * scale.y / size;

            gameObject.transform.localScale = scale;

        }   // ScaleObjectY()
        #endregion


        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   CountAllChildRecursive()
        //   GetAllChildRecursive()
        // ---------------------------------------------------------------------

        #region .  CountAllChildRecursive()  .
        // ---------------------------------------------------------------------
        //   Method.......:  CountAllChildRecursive()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void CountAllChildRecursive(GameObject parentObject, string startsWith = "")
        {
            if (parentObject == null)
                return;

            foreach (Transform child in parentObject.transform)
            {
                if (child == null)
                    continue;

                if (child.name.StartsWith(startsWith))
                {
                    this._countOfChildren++;
                }

                CountAllChildRecursive(child.gameObject, "Brick");
            }

        }   // CountAllChildRecursive()
        #endregion


        #region .  GetAllChildRecursive()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetAllChildRecursive()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void GetAllChildRecursive(GameObject parentObject, string startsWith = "")
        {
            if (parentObject == null)
                return;

            foreach (Transform child in parentObject.transform)
            {
                if (child == null)
                    continue;

                if (child.name.StartsWith(startsWith))
                {
                    this._listOfChildren.Add(child.gameObject);
                }

                GetAllChildRecursive(child.gameObject, startsWith);
            }

        }   // GetAllChildRecursive()
        #endregion


    }   // class Utils

}   // namespace Assets.Scenes.Game2048.Scripts
