using System;
using UnityEngine;


namespace Assets.Scenes.MainMenu.Scripts.LineDrawing
{
    public class Ball : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Public Events:
        // --------------
        //   OnBallDestroyed
        // ---------------------------------------------------------------------

        #region .  Public Events  .

        public static event Action OnBallDestroyed = delegate { };

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // ------------------
        //   _camera
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private Camera _camera;

        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   Start()
        //   Update()
        // ---------------------------------------------------------------------

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //print($"Ball collided with {collision.gameObject.name}");

        }   // OnCollisionEnter2D()


        #region .  Start()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Start()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Start()
        {
            _camera = Camera.main;

        }   // Start()
        #endregion


        #region .  Update  .
        // ---------------------------------------------------------------------
        //   Method.......:  Update()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Update()
        {
            if (transform.position.y < -6.0f)
            {
                Destroy(gameObject);
                OnBallDestroyed?.Invoke();
            }

        }   // Update()
        #endregion


    }   // class Ball

}   // namespace Assets.Scenes.MainMenu.Scripts.LineDrawing