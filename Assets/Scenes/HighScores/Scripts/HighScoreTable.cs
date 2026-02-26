using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Assets.Scenes.HighScoreTable.Scripts
{
    public class HighScoreTable : MonoBehaviour
    {
        // ---------------------------------------------------------------------
        // Private Classes:
        // ----------------
        //   HighScoreEntry
        //   HighScores
        // ---------------------------------------------------------------------

        #region .  HighScoreEntry  .
        // ---------------------------------------------------------------------
        //   Class........:  HighScoreEntry
        //   Description..:  
        // ---------------------------------------------------------------------
        [System.Serializable]
        private class HighScoreEntry
        {
            public int    score;
            public string name;

        }   // class HighScoreEntry
        #endregion


        #region .  HighScores  .
        // ---------------------------------------------------------------------
        //   Class........:  HighScores
        //   Description..:  
        // ---------------------------------------------------------------------
        private class HighScores
        {
            public List<HighScoreEntry> HighScoreEntryList;

        }   // class HighScores
        #endregion



        // ---------------------------------------------------------------------
        // Serialized Fields:
        // ------------------
        //   _entryContainer
        //   _entryTemplate
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        [SerializeField] private Transform _entryContainer;
        [SerializeField] private Transform _entryTemplate;

        #endregion



        // ---------------------------------------------------------------------
        // Private Variables:
        // -------------------
        //   _highScoreEntryList
        //   _HIGH_SCORE_TABLE
        // ---------------------------------------------------------------------

        #region .  Private Variables  .

        private List<Transform> _highScoreEntryList;
        private const string    _HIGH_SCORE_TABLE = "HighScoreTable";

        #endregion



        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   ButtonDeleteClicked
        //   ButtonQuitClicked()
        // ---------------------------------------------------------------------

        #region .  ButtonDeleteClicked()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ButtonDeleteClicked()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ButtonDeleteClicked()
        {
            PlayerPrefs.DeleteKey(_HIGH_SCORE_TABLE);
            Debug.Log($"PlayerPrefs key {_HIGH_SCORE_TABLE} all values deleted.");

        }   // ButtonDeleteClicked()
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
            SceneManager.LoadScene("Scene_MainMenu");

            //UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();

        }   // ButtonQuitClicked()
        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   Awake()
        //   AddHighScoreEntry()
        //   CreateHighScoreEntry()
        // ---------------------------------------------------------------------

        #region .  Awake()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Awake()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Awake()
        {
            _entryTemplate.gameObject.SetActive(false);

            string     jsonString = PlayerPrefs.GetString(_HIGH_SCORE_TABLE);
            HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

            if (highScores == null)
            {
                // There's no stored table, initialize.
                Debug.Log("Initializing table with default values...");
                AddHighScoreEntry(872931,  "DAV");
                AddHighScoreEntry(1000000, "CMK");
                AddHighScoreEntry(785123,  "CAT");
                AddHighScoreEntry(542024,  "MAX");
                AddHighScoreEntry(68245,   "AAA");
                AddHighScoreEntry(87639,   "KFU");
                AddHighScoreEntry(897621,  "JOE");
                AddHighScoreEntry(87406,   "LDE");
                AddHighScoreEntry(98765,   "KYX");
                AddHighScoreEntry(99887,   "ENF");

                // Reload.
                jsonString = PlayerPrefs.GetString(_HIGH_SCORE_TABLE);
                highScores = JsonUtility.FromJson<HighScores>(jsonString);
            }

            // Sort entry list by Score.
            for (int i = 0; i < highScores.HighScoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highScores.HighScoreEntryList.Count; j++)
                {
                    if (highScores.HighScoreEntryList[j].score > highScores.HighScoreEntryList[i].score)
                    {
                        // Swap.
                        (highScores.HighScoreEntryList[j], highScores.HighScoreEntryList[i]) =
                        (highScores.HighScoreEntryList[i], highScores.HighScoreEntryList[j]);
                    }
                }
            }

            _highScoreEntryList = new List<Transform>();

            foreach (HighScoreEntry highscoreEntry in highScores.HighScoreEntryList)
            {
                CreateHighScoreEntry(highscoreEntry, _entryContainer, _highScoreEntryList);
            }

        }   // Awake()
        #endregion


        #region .  AddHighScoreEntry()  .
        // ---------------------------------------------------------------------
        //   Method.......:  AddHighScoreEntry()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void AddHighScoreEntry(int score, string name)
        {
            // Create HighScoreEntry.
            HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };

            // Load saved HighScores.
            string     jsonString = PlayerPrefs.GetString(_HIGH_SCORE_TABLE);
            HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

            if (highScores == null)
            {
                // There's no stored table, initialize.
                highScores = new HighScores()
                {
                    HighScoreEntryList = new List<HighScoreEntry>()
                };
            }

            // Add new entry to HighScores.
            highScores.HighScoreEntryList.Add(highScoreEntry);

            // Save updated HighScores.
            jsonString = JsonUtility.ToJson(highScores);
            PlayerPrefs.SetString(_HIGH_SCORE_TABLE, jsonString);
            PlayerPrefs.Save();

        }   // AddHighScoreEntry()
        #endregion


        #region .  CreateHighScoreEntry()  .
        // ---------------------------------------------------------------------
        //   Method.......:  CreateHighScoreEntry()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void CreateHighScoreEntry(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
        {
            float templateHeight = 40f;

            Transform     entryTransform     = Instantiate(_entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

            Transform background  = entryTransform.Find("Image_Background_Entry");
            Image     imageTrophy = entryTransform.Find("Image_Trophy"  ).GetComponent<Image>();
            TMP_Text  textRank    = entryTransform.Find("TMP_Text_Rank" ).GetComponent<TMP_Text>();
            TMP_Text  textScore   = entryTransform.Find("TMP_Text_Score").GetComponent<TMP_Text>();
            TMP_Text  textName    = entryTransform.Find("TMP_Text_Name" ).GetComponent<TMP_Text>();

            entryTransform.gameObject.SetActive(true);
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);

            int rank = transformList.Count + 1;

            string rankString;
            Color  gold = GetColorFromString("FFD200");

            switch (rank)
            {
                case 1:
                    rankString        = "1st";
                    textRank.color    = gold;
                    textScore.color   = gold;
                    textName.color    = gold;
                    imageTrophy.color = gold;
                    break;

                case 2:
                    rankString        = "2nd";
                    imageTrophy.color = GetColorFromString("C6C6C6");
                    break;

                case 3:
                    rankString        = "3rd";
                    imageTrophy.color = GetColorFromString("B76F56");
                    break;

                default:
                    rankString        = $"{rank}th";
                    imageTrophy.gameObject.SetActive(false);
                    break;
            }

            // Set background visible odds and evens, easier to read.
            background.gameObject.SetActive(rank % 2 == 1);

            textRank .text = rankString;
            textScore.text = highScoreEntry.score.ToString();
            textName .text = highScoreEntry.name;

            transformList.Add(entryTransform);

        }   // CreateHighScoreEntry()
        #endregion


        #region .  GetColorFromString()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetColorFromString()
        //   Description..:  Get Color from Hex string FF00FFAA.
        //   Parameters...:  string
        //   Returns......:  Color
        // ---------------------------------------------------------------------
        // Get Color from Hex string FF00FFAA
        private Color GetColorFromString(string color)
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
        private float Hex_To_Dec(string hex)
        {
            return Convert.ToInt32(hex, 16) / 255f;

        }   // Hex_To_Dec()
        #endregion


    }   // class HighScoreTable

}   // namespace Assets.Scenes.HighScoreTable.Scripts
