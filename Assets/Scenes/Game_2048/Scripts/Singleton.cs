using UnityEngine;


namespace Assets.Scenes.Game2048.Scripts
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // -------------------------------------------------------------------------
        // Private Properties:
        // -------------------
        //   _instance
        // -------------------------------------------------------------------------

        #region .  Private Properties  .

        private static T _instance;

        #endregion



        // -------------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   Instance
        // -------------------------------------------------------------------------

        #region .  Public Properties  .

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();

                    if (_instance == null)
                    {
                        _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                    }
                }

                return _instance;
            }

        }   // Instance

        #endregion


    }   // class Singleton<T>

}   // namespace Assets.Scenes.Game2048.Scripts