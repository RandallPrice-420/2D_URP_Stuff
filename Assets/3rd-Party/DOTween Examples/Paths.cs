using UnityEngine;
using DG.Tweening;


public class Paths : MonoBehaviour
{
    public Transform target;
    public PathType  pathType = PathType.CatmullRom;
    public Ease      easeType = Ease.InBack;
    public float     delay    = 4;
    public float     duration = 4;
    public int       loops    = 4;
    public bool      infinite = true;

    public Vector3[] waypoints = new[]
    {
        new Vector3( 2,  5, 0),
        new Vector3( 4,  7, 0),
        new Vector3( 6,  9, 0),
        new Vector3( 8,  6, 1),
        new Vector3( 6,  2, 1),
        new Vector3( 5, -4, 1),
        new Vector3( 1, -5, 2),
        new Vector3(-2, -3, 2),
        new Vector3(-4,  0, 2),
        new Vector3(-3,  5, 3),
        new Vector3(-2,  9, 3)
    };


    void Start()
    {
        // Create a path Tween using the given pathType:  Linear or CatmullRom (curved).
        Tween t = this.target.DOPath(this.waypoints, this.duration, this.pathType)
                             .SetDelay(this.delay)        // Delay the startup
                             .SetLookAt(0.001f)           // Orient the target to the path
                             .SetOptions(true)            // Close the path
                             ;

        // Then set the ease and loops.
        t.SetEase(this.easeType).SetLoops((this.infinite) ? -1 : this.loops);

    }    // Start()


}   // class Paths
