using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Assets.Scenes.Game2048.Scripts
{
    public class Utils : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Public Static Methods:
        // ----------------------
        //   DestroyObjects()
        //   LoadAssets()
        // ---------------------------------------------------------------------

        #region .  DestroyObjects()  .
        // ---------------------------------------------------------------------
        //   Method.......:  DestroyObjects()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void DestroyObjects (Object[] allObjects, string objectName)
        {
            foreach (var obj in allObjects)
            {
                //Debug.Log($"Found {nameof(type)}:  " + obj.name);
                Debug.Log($"Found {objectName}:  " + obj.name);
            }

        }   // DestroyObjects()
        #endregion


        #region .  LoadAssets()  .
        // ---------------------------------------------------------------------
        //  Method.......:  LoadAssets()
        //  Description..:  
        //  Parameters...:  label
        //  Returns......:  Nothing
        // ----------------------------------------------------------------------
        public static List<GameObject> LoadAssets(string assetsPath)
        {
            List<GameObject> assetsList = new();

            GameObject[] prefabs = Resources.LoadAll<GameObject>(assetsPath);
            foreach (GameObject prefab in prefabs)
            {
                assetsList.Add(prefab);
                Debug.Log($"Loaded Prefab: {prefab.name}");
            }

            //string[] guids = AssetDatabase.FindAssets("t:prefab", new string[] { assetsPath });

            //foreach (string guid in guids)
            //{
            //    string     path  = AssetDatabase.GUIDToAssetPath(guid);
            //    GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            //    assetsList.Add(asset);
            //}

            return assetsList;

        }   // LoadAssets()
        #endregion


    }   // class Utils

}   // namespace Assets.Scenes.Game2048.Scripts
