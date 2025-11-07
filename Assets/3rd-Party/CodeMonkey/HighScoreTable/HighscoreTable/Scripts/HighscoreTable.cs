using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;


public class HighscoreTable : MonoBehaviour
{
    [SerializeField] private Transform _entryContainer;
    [SerializeField] private Transform _entryTemplate;
    [SerializeField] private Image     _imageTrophy;
    [SerializeField] private Text      _textRank;
    [SerializeField] private Text      _textScore;
    [SerializeField] private Text      _textName;


    private List<Transform> _highScoreEntryList;



    private void Awake()
    {
        //_entryContainer = transform     .Find("highscoreEntryContainer");
        //_entryTemplate  = _entryContainer.Find("highscoreEntryTemplate");

        _entryTemplate.gameObject.SetActive(false);

        string     jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize.
            Debug.Log("Initializing table with default values...");
            AddHighScoreEntry(1000000, "CMK");
            AddHighScoreEntry(897621,  "JOE");
            AddHighScoreEntry(872931,  "DAV");
            AddHighScoreEntry(785123,  "CAT");
            AddHighScoreEntry(542024,  "MAX");
            AddHighScoreEntry(68245,   "AAA");

            // Reload.
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }

        // Sort entry list by Score.
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap.
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        _highScoreEntryList = new List<Transform>();

        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighScoreEntry(highscoreEntry, _entryContainer, _highScoreEntryList);
        }

    }   // Awake()


    private void CreateHighScoreEntry(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 31f;

        Transform     entryTransform     = Instantiate(_entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

        entryTransform.gameObject.SetActive(true);
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);

        int    rank  = transformList.Count + 1;
        int    score = highscoreEntry.score;
        string name  = highscoreEntry.name;
        string rankString;

        switch (rank)
        {
            case 1:
                rankString         = "1st";
                _textRank .color   = Color.green;
                _textScore.color   = Color.green;
                _textName .color   = Color.green;
                _imageTrophy.color = UtilsClass.GetColorFromString("FFD200");
                break;

            case 2:
                rankString         = "2nd";
                _imageTrophy.color = UtilsClass.GetColorFromString("C6C6C6");
                break;

            case 3:
                rankString         = "3rd";
                _imageTrophy.color = UtilsClass.GetColorFromString("B76F56");
                break;

            default:
                rankString         = $"{rank}th";
                _imageTrophy.gameObject.SetActive(false);
                break;
        }

        _textRank .text = rankString;
        _textScore.text = score.ToString();
        _textName .text = name;

        // Set background visible odds and evens, easier to read
        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        //// Set tropy
        //switch (rank)
        //{
        //    case 1:  _imageTrophy.color = UtilsClass.GetColorFromString("FFD200"); break;
        //    case 2:  _imageTrophy.color = UtilsClass.GetColorFromString("C6C6C6"); break;
        //    case 3:  _imageTrophy.color = UtilsClass.GetColorFromString("B76F56"); break;
        //    default: _imageTrophy.gameObject.SetActive(false);                     break;
        //}

        transformList.Add(entryTransform);

    }   // CreateHighScoreEntry()


    private void AddHighScoreEntry(int score, string name)
    {
        // Create HighscoreEntry.
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        // Load saved Highscores.
        string     jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize.
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores.
        string json = JsonUtility.ToJson(highscores);

        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

    }   // AddHighScoreEntry()



    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }



    /*
     * Represents a single High score entry
     * */
    [System.Serializable]
    private class HighscoreEntry
    {
        public int   score;
        public string name;
    }

}
