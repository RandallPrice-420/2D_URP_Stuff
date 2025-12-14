using UnityEngine;
using DG.Tweening;


public class Follow : MonoBehaviour
{
    // Target to follow.
    public Transform target;
    public float     followSpeed = 2f;
    

    private Vector3 _targetLastPos;
    private Tweener _tween;


    private void Start()
    {
        // Create the "move to target" Tween and store it as a Tweener.
        // Set autoKill to FALSE so the Tween goes on forever (otherwise it will stop if it reaches the target).
        this._tween = this.transform.DOMove(this.target.position, followSpeed).SetAutoKill(false);

        // Store the target's last position to know if it changes (prevents changing if nothing changes).
        this._targetLastPos = this.target.position;

    }    // Start()


    private void Update()
    {
        // Update the Tween's endValue to the target's position each frame (if the target's position changed).
        if (this._targetLastPos == this.target.position) return;

        // Add a Restart in the end, so if the Tween was completed it will play again.
        this._tween.ChangeEndValue(this.target.position, true).Restart();
        this._targetLastPos = this.target.position;

    }    // Update()


}   // class Follow
