using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AutoJumpScene : MonoBehaviour
{
    public static float Speed
    {
        get { return I._worldSpeed; }
    }


    // Serialized
    [Range(1, 20)]
    [SerializeField] private bool      _autoplayOnStartup = true;
    [SerializeField] private float     _worldSpeed        = 3;      // Increase, also jump speed
    [SerializeField] private Transform _movingWorld;


    private static   AutoJumpScene I;

    private bool     _isPlaying;
    private Renderer _lastPlatformRenderer;
    private Vector3  _worldStartP;



    private void Awake()
    {
        I = this;

    }   // Awake()


    private void Start()
    {
        this._worldStartP = this._movingWorld.localPosition;
        
        // Find last platform to the furthest positive X
        Renderer[] platformsRenderers = this._movingWorld.GetComponentsInChildren<Renderer>();

        this._lastPlatformRenderer = platformsRenderers[0];

        for (int i = 1; i < platformsRenderers.Length; ++i)
        {
            if (platformsRenderers[i].transform.position.x > this._lastPlatformRenderer.transform.position.x)
            {
                this._lastPlatformRenderer = platformsRenderers[i];
            }
        }

        if (this._autoplayOnStartup) TogglePlay();

    }   // Start()


    private void TogglePlay()
    {
        _isPlaying = !_isPlaying;

        if (_isPlaying)
        {
            DOTween.PlayAll();
        }
        else
        {
            DOTween.PauseAll();
        }

    }   // TogglePlay()


    private void Update()
    {
        // Play/pause game, reload scene controls.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePlay();
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (!this._isPlaying) return;

        // Move world.
        Vector3 toWorldP = this._movingWorld.localPosition;
        toWorldP.x      -= this._worldSpeed * Time.deltaTime;
        this._movingWorld.localPosition = toWorldP;

        // Check if world is not visible anymore, reset its X so it can loop (dirty trick but ok for this case).
        if ((this._lastPlatformRenderer.transform.position.x < 0) &&
           (!this._lastPlatformRenderer.isVisible))
        {
            this._movingWorld.localPosition = this._worldStartP;
        }

    }   // Update()


}   // class AutoJumpScene
