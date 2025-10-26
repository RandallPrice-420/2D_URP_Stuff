using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scenes.Game2048_Old.Scripts
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }



        [SerializeField] private TileBoard       _board;
        [SerializeField] private CanvasGroup     _gameOver;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;



        public int score { get; private set; } = 0;



        public void GameOver()
        {
            _board.enabled         = false;
            _gameOver.interactable = true;

            StartCoroutine(Fade(_gameOver, 1f, 1f));

        }   // GameOver()


        public void IncreaseScore(int points)
        {
            SetScore(score + points);

        }   // IncreaseScore()


        public void NewGame()
        {
            // Button color:  #8F7A66

            // Reset score.
            SetScore(0);
            _highScoreText.text = LoadHighScore().ToString();

            // Hide game over screen.
            _gameOver.alpha        = 0f;
            _gameOver.interactable = false;

            // Update board state.
            _board.ClearBoard();
            _board.CreateTile();
            _board.CreateTile();
            _board.enabled = true;

        }   // NewGame()


        // ---------------------------------------------------------------------
        //   Method.......:  Quit()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void Quit()
        {
            // Button color:  #8F7A66

#if UNITY_EDITOR
            SceneManager.LoadScene("Scene_MainMenu");

            //UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();

        }   // Quit()



        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Instance = this;
            }

        }   // Awake()


        private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay = 0f)
        {
            yield return new WaitForSeconds(delay);

            float elapsed  = 0f;
            float duration = 0.5f;
            float from     = canvasGroup.alpha;

            while (elapsed < duration)
            {
                canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
                elapsed          += Time.deltaTime;

                yield return null;
            }

            canvasGroup.alpha = to;

        }   // Fade()


        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }

        }   //  OnDestroy()


        private int LoadHighScore()
        {
            return PlayerPrefs.GetInt("highscore", 0);

        }   // LoadHighScore()


        private void SaveHighScore()
        {
            int highscore = LoadHighScore();

            if (score > highscore)
            {
                PlayerPrefs.SetInt("highscore", score);
            }

        }   // SaveHighScore()


        private void SetScore(int score)
        {
            this.score      = score;
            _scoreText.text = score.ToString();

            SaveHighScore();

        }   // SetScore()


        private void Start()
        {
            NewGame();

        }   // Start()


    }   // class GameManager


}   // namespace Assets.Scenes.Game2048_Old.Scripts
