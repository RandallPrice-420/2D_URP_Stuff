using UnityEngine;


namespace Assets.Scenes.MainMenu.Scripts.LineDrawing
{
    public class Ball : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   Update()
        // ---------------------------------------------------------------------

        #region .  Update  .
        // ---------------------------------------------------------------------
        //   Method.......:  Update()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Update()
        {
            float distance = Vector3.Distance(transform.position, Camera.main.transform.position);

            if (distance > 500f)
            {
                Destroy(gameObject);
            }

        }   // Update()
        #endregion


    }   // class Ball

}   // namespace Assets.Scenes.MainMenu.Scripts.LineDrawing