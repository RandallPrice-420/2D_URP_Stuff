using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scenes.MainMenu.Scripts.MainMenu
{
    public class GameManager : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   ButtonClicked()
        //   ButtonQuitClicked()
        //   LoadScene()
        // ---------------------------------------------------------------------

        #region .  ButtonClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ButtonClicked(string sceneName)
        {
            this.LoadScene(sceneName);

        }   // ButtonClicked()
        #endregion


        #region .  ButtonQuitClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonQuitClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ButtonQuitClicked()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();

        }   // ButtonQuitClicked()
        #endregion


        #region .  LoadScene()  .
        // ---------------------------------------------------------------------
        //   Method.......:  LoadScene()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void LoadScene(string sceneName)
        {
            try
            {
                SceneManager.LoadScene(sceneName);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to load scene '{sceneName}': {ex.Message}");
            }

        }   // LoadScene()
        #endregion


    }   // class GameManager

}   // namespace Assets.Scenes.MainMenu.Scripts.MainMenu
