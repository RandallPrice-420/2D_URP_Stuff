using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace Assets.Scenes.Game2048.Scripts.Managers
{
    public class HighScoresManager : Singleton<HighScoresManager>
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
        //   Description..:  An individual high score entry.
        //   Properties...:  int    score
        //                   int    moves
        //                   string name
        // ---------------------------------------------------------------------
        [System.Serializable]
        private class HighScoreEntry
        {
            public int    score;
            public int    moves;
            public string name;

        }   // class HighScoreEntry
        #endregion


        #region .  HighScores  .
        // ---------------------------------------------------------------------
        //   Class........:  HighScores
        //   Description..:  
        //   Properties...:  int BestScore
        //                   List<HighScoreEntry> HighScoresList
        // ---------------------------------------------------------------------
        private class HighScores
        {
            public int                  BestScore;
            public List<HighScoreEntry> HighScoresList;

        }   // class HighScores
        #endregion



        // ---------------------------------------------------------------------
        // Public Properties:
        // ------------------
        //   BestScore
        //   LowestScore
        //   HighScoreCount
        //   MaximumEntries
        // ---------------------------------------------------------------------

        #region .  Public Properties  .

        public int BestScore      =  0;
        public int LowestScore    =  0;
        public int HighScoreCount =  0;
        public int MaximumEntries = 10;

        #endregion



        // ---------------------------------------------------------------------
        // Serialized Fields:
        // ------------------
        //   _entryContainer
        //   _entryTemplate
        //   _loadDefaultValues
        //   _textEntryContainerValue
        //   _textEntryTemplateValue
        //   _textEntryTransformValue
        //   _textEntryRectTransformValue
        //   _textAnchoredPositionValue
        // ---------------------------------------------------------------------

        #region .  Serialized Fields  .

        [SerializeField] private Transform _entryContainer;
        [SerializeField] private Transform _entryTemplate;
        [SerializeField] private bool      _loadDefaultValues = false;
        [SerializeField] private TMP_Text  _textEntryContainerValue;
        [SerializeField] private TMP_Text  _textEntryTemplateValue;
        [SerializeField] private TMP_Text  _textEntryTransformValue;
        [SerializeField] private TMP_Text  _textEntryRectTransformValue;
        [SerializeField] private TMP_Text  _textAnchoredPositionValue;

        #endregion



        // ---------------------------------------------------------------------
        // Private Properties:
        // -------------------
        //   _colorGold
        //   _colorSilver
        //   _colorBronze
        //   _highScores
        //   _HIGH_SCORE_TABLE
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private Color        _colorGold;
        private Color        _colorSilver;
        private Color        _colorBronze;
        private HighScores   _highScores;

        private const string _HIGH_SCORE_TABLE = "HighScoreTable";

        #endregion



        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   AddHighScoreEntry()
        //   ClearHighScoresDisplay()
        //   CheckForHighScore()
        //   DeleteHighScoresFromPlayerPrefs()
        //   DisplayHighScores()
        //   GetHighScores()
        //   GetPlayerName()
        //   GetRandomString()
        //   Initialize()
        // ---------------------------------------------------------------------

        #region .  AddHighScoreEntry()  .
        // ---------------------------------------------------------------------
        //   Method.......:  AddHighScoreEntry()
        //   Description..:  Load the high scores data from PlayerPrefs into the
        //                   _highScores list, and add a new entry to the list.
        //                   The new _highScores list is saved to PlayerPrefs.
        //                   This method does NOT display the high scores, that
        //                   is done next in the DisplayHighScores() method.
        //   Parameters...:  int    : the player's score.
        //                   string : the player's name.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void AddHighScoreEntry(int newScore, int newMoves, string newName)
        {
            // Load the saved HighScores.
            string jsonString = PlayerPrefs.GetString(_HIGH_SCORE_TABLE);
            _highScores       = JsonUtility.FromJson<HighScores>(jsonString);

            // If there's no high scores in PlayerPrefs, initialize it.
            if (_highScores == null)
            {
                _highScores = new HighScores()
                {
                    HighScoresList = new List<HighScoreEntry>()
                };
            }

            // Create a new HighScoreEntry using the parameter values.
            HighScoreEntry highScoreEntry = new()
            {
                score = newScore,
                moves = newMoves,
                name  = newName
            };

            // Add the new highScoreEntry to the _highScores list and sort the list.
            _highScores.HighScoresList.Add(highScoreEntry);
            SortHighScores();

            // Save the updated _highScores back in PlayerPrefs.
            jsonString = JsonUtility.ToJson(_highScores);
            PlayerPrefs.SetString(_HIGH_SCORE_TABLE, jsonString);
            PlayerPrefs.Save();

        }   // AddHighScoreEntry()
        #endregion


        #region .  ClearHighScoresDisplay()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ClearHighScoresDisplay()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void ClearHighScoresDisplay()
        {
            // The objects to clear will match thism target name.
            string           targetName      = "HighScoreEntryTemplate(Clone)"; // Replace with the name you're looking for
            GameObject[]     allObjects      = FindObjectsOfType<GameObject>();
            List<GameObject> matchingObjects = new();

            // Find objects whose name matches the target name and add to the list.
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == targetName)
                {
                    matchingObjects.Add(obj);
                }
            }

            // Destroy them.
            foreach (GameObject gameObject in matchingObjects)
            {
                //pDebug.Log($"Destroying {gameObject.name} object.");
                Destroy(gameObject);
            }

            // Delete the high score keys from the Registry.
            DeleteHighScoresFromPlayerPrefs();
                
        }   // ClearHighScoresDisplay()
        #endregion


        #region .  CheckForHighScore()  .
        // ---------------------------------------------------------------------
        //   Method.......:  CheckForHighScore()
        //   Description..:  Check if the score earned a spot in the HighScores
        //                   list.  The list only holds the top 10 scores so to
        //                   add this score it must be greater than the lowest
        //                   score.  Also, check if the score is the new best!
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void CheckForHighScore(int score, int moves)
        {
            // New high score!
            AddHighScoreEntry(score, moves, GetPlayerName(5));
            GetHighScores();
            DisplayHighScores();

            // Did the player beat the best score?
            if (score > BestScore)
            {
                // Congratulations!
                BestScore = score;
                EventManager.RaiseOnBestScoreChanged(BestScore);
            }

        }   // CheckForHighScore()
        #endregion


        #region .  DeleteHighScoresFromPlayerPrefs()  .
        // ---------------------------------------------------------------------
        //   Method.......:  DeleteHighScoresFromPlayerPrefs()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void DeleteHighScoresFromPlayerPrefs()
        {
            // Delete the high scores from PlayerPrefs.
            PlayerPrefs.DeleteKey("BestScore");
            PlayerPrefs.DeleteKey(_HIGH_SCORE_TABLE);

            Debug.Log($"PlayerPrefs key [{_HIGH_SCORE_TABLE}] and [BestScore] values deleted.");

        }   // DeleteHighScoresFromPlayerPrefs()
        #endregion


        #region .  DisplayHighScores()  .
        // ---------------------------------------------------------------------
        //   Method.......:  DisplayHighScores()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void DisplayHighScores()
        {
            if (_highScores == null) return;

            ClearHighScoresDisplay();

            int    rank;
            string rankString;
            float  templateHeight = 20f;

            for (int i = 0; i < _highScores.HighScoresList.Count; i++)
            {
                if (i >= MaximumEntries) break;

                HighScoreEntry highScoreEntry = _highScores.HighScoresList[i];

                Transform     newEntry         = Instantiate(_entryTemplate, _entryContainer);
                RectTransform newEntryPosition = newEntry.GetComponent<RectTransform>();

                newEntryPosition.anchoredPosition = new Vector2(0, -templateHeight * i);

                _textEntryContainerValue  .text = _entryContainer .position        .ToString();
                _textEntryTemplateValue   .text = _entryTemplate  .position        .ToString();
                _textEntryTransformValue  .text = newEntry        .position        .ToString();
                _textAnchoredPositionValue.text = newEntryPosition.anchoredPosition.ToString();

                // Get references to the UI objects in the entry template.
                Transform background  = newEntry.Find("Image_BackgroundEntry");
                Image     imageTrophy = newEntry.Find("Image_Trophy"  ).GetComponent<Image>();
                TMP_Text  textMoves   = newEntry.Find("TMP_Text_Moves").GetComponent<TMP_Text>();
                TMP_Text  textRank    = newEntry.Find("TMP_Text_Rank" ).GetComponent<TMP_Text>();
                TMP_Text  textScore   = newEntry.Find("TMP_Text_Score").GetComponent<TMP_Text>();
                TMP_Text  textName    = newEntry.Find("TMP_Text_Name" ).GetComponent<TMP_Text>();

                rank = i + 1;

                switch (rank)
                {
                    case 1:
                        rankString        = "1<sup>st</sup>";
                        imageTrophy.color = _colorGold;
                        textRank   .color = _colorGold;
                        textMoves  .color = _colorGold;
                        textScore  .color = _colorGold;
                        textName   .color = _colorGold;
                        break;

                    case 2:
                        rankString        = "2<sup>nd</sup>";
                        imageTrophy.color = _colorSilver;
                        textRank   .color = _colorSilver;
                        textMoves  .color = _colorSilver;
                        textScore  .color = _colorSilver;
                        textName   .color = _colorSilver;
                        break;

                    case 3:
                        rankString        = "3<sup>rd</sup>";
                        imageTrophy.color = _colorBronze;
                        textRank   .color = _colorBronze;
                        textMoves  .color = _colorBronze;
                        textScore  .color = _colorBronze;
                        textName   .color = _colorBronze;
                        break;

                    default:
                        rankString        = $"{rank}<sup>th</sup>";
                        imageTrophy.gameObject.SetActive(false);
                        break;
                }

                // Set background visible odds and evens, easier to read.
                background.gameObject.SetActive(rank % 2 == 1);

                // Assign the entry values to their respective Text objects.
                textRank .text = rankString;
                textMoves.text = highScoreEntry.moves.ToString();
                textScore.text = highScoreEntry.score.ToString();
                textName .text = highScoreEntry.name;
                            
                // Show the high score enttry.
                newEntry.gameObject.SetActive(true);

            }   // foreach (highScoreEntry...)

        }   // DisplayHighScores()
        #endregion


        #region .  GetHighScores()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetHighScores()
        //   Description..:  Get a list of the high scores from PlayerPrefs.  If
        //                   _loadDefaultValues is true, than initialize the list
        //                   with random sample entries.  The list is sorted from
        //                   highest to lowest score.  This method only loads the
        //                   high scores, it does not add them to the display.
        //   Parameters...:  None
        //   Returns......:  _highScores = the high score entries.
        //                   BestScore   = the highest score.
        //                   LowestScore = the lowest score.
        // ---------------------------------------------------------------------
        public void GetHighScores()
        {
            // Load high score data from PlayerPrefs.
            string jsonString = PlayerPrefs.GetString(_HIGH_SCORE_TABLE);
            _highScores       = JsonUtility.FromJson<HighScores>(jsonString);


            #region .  Sample Data ??  ----------------------------------------.

            //  If there's no HighScoreTable in PlayerPrefs, check to add some.
            if ((_highScores == null) && (_loadDefaultValues))
            {
                Debug.Log("Initializing table with default values...");
                AddHighScoreEntry(872931,  Random.Range(20, 75), "DAV");
                AddHighScoreEntry(1000000, Random.Range(20, 75), "CMK");
                AddHighScoreEntry(785123,  Random.Range(20, 75), "CAT");
                AddHighScoreEntry(542024,  Random.Range(20, 75), "MAX");
                AddHighScoreEntry(68245,   Random.Range(20, 75), "AAA");
                AddHighScoreEntry(87639,   Random.Range(20, 75), "KFU");
                AddHighScoreEntry(897621,  Random.Range(20, 75), "JOE");
                AddHighScoreEntry(87406,   Random.Range(20, 75), "LDE");
                AddHighScoreEntry(98765,   Random.Range(20, 75), "KYX");
                AddHighScoreEntry(99887,   Random.Range(20, 75), "JNF");

                // Reload.
                jsonString  = PlayerPrefs.GetString(_HIGH_SCORE_TABLE);
                _highScores = JsonUtility.FromJson<HighScores>(jsonString);
            }
            #endregion


            // If there is no high score data yet, nothing else to do here.
            if (_highScores == null)
            {
                _textEntryContainerValue    .text = "";
                _textEntryTemplateValue     .text = "";
                _textEntryTransformValue    .text = "";
                _textEntryRectTransformValue.text = "";
                _textAnchoredPositionValue  .text = "";
                return;
            }

            SortHighScores();

            BestScore      = _highScores.HighScoresList[0].score;
            LowestScore    = _highScores.HighScoresList[_highScores.HighScoresList.Count - 1].score;
            HighScoreCount = _highScores.HighScoresList.Count;

            EventManager.RaiseOnBestScoreChanged(BestScore);

        }   // GetHighScores()
        #endregion


        #region .  GetPlayerName()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetPlayerName()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  string
        // ---------------------------------------------------------------------
        public string GetPlayerName(int length)
        {
            string name = GetRandomString(length);
            //string name = GetRandomString(_playerNameLength);

            return name;

        }   // GetPlayerName()
        #endregion


        #region .  GetRandomString()  .
        // ---------------------------------------------------------------------
        //   Method.......:  GetRandomString()
        //   Description..:  
        //   Parameters...:  string
        //                   int
        //   Returns......:  string
        // ---------------------------------------------------------------------
        private string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()<>?.";
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[Random.Range(0, chars.Length)];
            }

            return new string(result);

        }   // GetRandomString()
        #endregion


        #region .  Initialize()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Initialize()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void Initialize()
        {
            _entryTemplate.gameObject.SetActive(false);

            _colorGold   = Utils.Instance.GetColorFromString("FFD200");
            _colorSilver = Utils.Instance.GetColorFromString("C6C6C6");
            _colorBronze = Utils.Instance.GetColorFromString("B76F56");

            // Load the high scores from PlayerPrefs and get these variables:
            // (1) _highScores, (2) BestScore, (3) LowestScore
            GetHighScores();
            DisplayHighScores();

        }   // Initialize()
        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   CreateHighScoreEntry()
        //   SortHighScores()
        //   Start()
        // ---------------------------------------------------------------------

        #region .  CreateHighScoreEntry()  .
        // ---------------------------------------------------------------------
        //   Method.......:  CreateHighScoreEntry()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void CreateHighScoreEntry(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
        {
            float templateHeight     = 20f;
            int   transformListCount = transformList.Count;


            Transform     entryTransform     = Instantiate(_entryTemplate, container);
            RectTransform newEntryPosition = entryTransform.GetComponent<RectTransform>();

            Transform background  = entryTransform.Find("Image_BackgroundEntry");
            Image     imageTrophy = entryTransform.Find("Image_Trophy"  ).GetComponent<Image>();
            TMP_Text  textMoves   = entryTransform.Find("TMP_Text_Moves").GetComponent<TMP_Text>();
            TMP_Text  textRank    = entryTransform.Find("TMP_Text_Rank" ).GetComponent<TMP_Text>();
            TMP_Text  textScore   = entryTransform.Find("TMP_Text_Score").GetComponent<TMP_Text>();
            TMP_Text  textName    = entryTransform.Find("TMP_Text_Name" ).GetComponent<TMP_Text>();

            entryTransform.gameObject.SetActive(true);
            newEntryPosition.anchoredPosition = new Vector2(0, -templateHeight * transformListCount);

            int rank = transformListCount + 1;

            string rankString;

            switch (rank)
            {
                case 1:
                    rankString        = "1st";
                    textRank.color    = _colorGold;
                    textScore.color   = _colorGold;
                    textName.color    = _colorGold;
                    imageTrophy.color = _colorGold;
                    break;

                case 2:
                    rankString        = "2nd";
                    imageTrophy.color = _colorSilver;
                    break;

                case 3:
                    rankString        = "3rd";
                    imageTrophy.color = _colorBronze;
                    break;

                default:
                    rankString        = $"{rank}th";
                    imageTrophy.gameObject.SetActive(false);
                    break;
            }

            // Set background visible odds and evens, easier to read.
            background.gameObject.SetActive(rank % 2 == 1);

            // Display the entry.
            textRank .text = rankString;
            textScore.text = highScoreEntry.score.ToString();
            textMoves.text = highScoreEntry.moves.ToString();
            textName .text = highScoreEntry.name;

            // Add it to the entries list.
            transformList.Add(entryTransform);

        }   // CreateHighScoreEntry()
        #endregion


        #region .  SortHighScores()  .
        // ---------------------------------------------------------------------
        //   Method.......:  SortHighScores()
        //   Description..:  Sort high scores descending by Score.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void SortHighScores()
        {
            int count = _highScores.HighScoresList.Count;

            // Sort the high scores list (descending by Score).
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (_highScores.HighScoresList[j].score > _highScores.HighScoresList[i].score)
                    {
                        // Swap.
                        (_highScores.HighScoresList[j], _highScores.HighScoresList[i]) =
                        (_highScores.HighScoresList[i], _highScores.HighScoresList[j]);
                    }
                }
            }
        }   // SortHighScores
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
            Initialize();

        }   // Start()
        #endregion


    }   // class HighScoresManager

}   // namespace Assets.Scenes.Game2048.Scripts
