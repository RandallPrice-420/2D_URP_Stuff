using System;
using DG.Tweening;
using UnityEngine;


public class AutoJumpActor : MonoBehaviour
{
    // Serialized
    [SerializeField]                  private bool  _distortAnimation = true;
    [SerializeField, Range(0.2f, 2f)] private float _jumpDuration     = 0.75f;
    [SerializeField, Range(0.1f, 1f)] private float _jumpPower        = 0.5f;

    private Sequence  _distortTween;
    private Tween     _jumpTween;
    private Transform _parent;



    private void Awake()
    {
        this._parent = this.transform.parent;

    }   // Awake()


    private void JumpTo(float toY)
    {
        this._jumpTween.Kill();

        if (this._distortAnimation) {
            this._distortTween.timeScale = AutoJumpScene.Speed; // Set timeScale of tween based on world speed
            this._distortTween.Restart();
        }

        Vector3 currP = this._parent.position;
        Vector3 to    = currP;
        to.y          = toY;

        // Find a harmonic jumpPower
        // (so that even when going down we always jump a little higher than where we are)
        float diffY     = to.y - currP.y;
        float jumpPower = diffY < 0 ? Mathf.Abs(diffY) + this._jumpPower * 0.5f : this._jumpPower;

        // Jump
        this._jumpTween           = this._parent.DOJump(to, jumpPower, 1, this._jumpDuration);
        this._jumpTween.timeScale = AutoJumpScene.Speed; // Set timeScale of tween based on world speed

    }   // JumpTo()


    private void OnDestroy()
    {
        // Clear tweens (always a good place to do that)
        this._jumpTween.Kill();
        this._distortTween.Kill();

    }   // OnDestroy()


    private void OnTriggerEnter(Collider other)
    {
        // Find the platform we triggered
        AutoJumpPlatform platform = other.GetComponentInParent<AutoJumpPlatform>();
        if (platform == null)
            return; // Not a platform collider, ignore

        bool isAlreadyJumping = _jumpTween.IsActive();
        if (isAlreadyJumping)
            return; // Don't jump again while already jumping

        // Determine if it's a jump-to or a jump-out-of platform
        // by direction-of-motion (considering we move left-to-right)
        bool isJumpToPlatform = other.transform.position.x < platform.transform.position.x;

        // Jump
        if (isJumpToPlatform)
        {
            JumpTo(platform.transform.position.y);
        }
        else
        {
            JumpTo(0);
        }

    }   // OnTriggerEnter()


    private void Start()
    {
        // Create distortion tween to give impression of "bending and pushing upwards, then landing"
        // (this is not the actual jump movement, just the "body distortion")
        float defScaleY    = this._parent.localScale.y;
        this._distortTween = DOTween.Sequence().SetAutoKill(false).Pause()
                                    .Append(_parent.DOScaleY(defScaleY * 0.25f, 0.05f))
                                    .Append(_parent.DOScaleY(defScaleY * 1.25f, 0.15f))
                                    .Insert(_jumpDuration * 0.5f, _parent.DOScaleY(defScaleY * 0.5f, _jumpDuration * 0.5f - 0.1f).SetEase(Ease.InQuad))
                                    .Append(_parent.DOScaleY(defScaleY, 0.1f))
                                    .Append(_parent.DOPunchScale(new Vector3(0, 0.2f), 0.45f, 3))
                                    .Insert(0, _parent.DOLocalRotate(new Vector3(0, 0, -20), _jumpDuration * 0.2f))
                                    .Insert(_jumpDuration * 0.4f, _parent.DOLocalRotate(new Vector3(0, 0, 10), _jumpDuration * 0.35f))
                                    .Insert(_jumpDuration * 0.75f, _parent.DOLocalRotate(new Vector3(0, 0, 0), _jumpDuration * 0.25f));

    }   // Start()


}   // class AutoJumpActor
