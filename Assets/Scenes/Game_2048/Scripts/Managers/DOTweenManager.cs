using System.Collections;
using DG.Tweening;
using UnityEngine;


namespace Assets.Scenes.Game2048.Scripts.Managers
{
    public class DOTweenManager : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _targetLocation = Vector3.zero;

        [Range(0.5f, 10.0f), SerializeField]
        private float _moveDuration = 1.0f;

        [SerializeField]
        private Ease _moveEase = Ease.Linear;

        [SerializeField]
        private Color _targetColor;

        [Range(1.0f, 500.0f), SerializeField]
        private float _scaleMultiplier = 3.0f;

        [Range(1.0f, 10.0f), SerializeField]
        private float _colorChangeDuration = 1.0f;

        [SerializeField]
        private DoTweenType _doTweenType = DoTweenType.MovementOneWay;


        private enum DoTweenType
        {
            MovementOneWay,
            MovementTwoWay,
            MovementTwoWayWithSequence,
            MovementOneWayColorChange,
            MovementOneWayColorChangeAndScale
        }


        // Start is called before the first frame update
        private void Start()
        {
            if (_targetLocation == Vector3.zero) _targetLocation = transform.position;

            if (_doTweenType == DoTweenType.MovementOneWay)
            {
                transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase);
            }
            else if (_doTweenType == DoTweenType.MovementTwoWay)
            {
                StartCoroutine(MoveWithBothWays());
            }
            else if (_doTweenType == DoTweenType.MovementTwoWayWithSequence)
            {
                Vector3 originalLocation = transform.position;
                DOTween.Sequence()
                    .Append(transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase))
                    .Append(transform.DOMove(originalLocation, _moveDuration).SetEase(_moveEase));
            }
            else if (_doTweenType == DoTweenType.MovementOneWayColorChange)
            {
                DOTween.Sequence()
                    .Append(transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase))
                    .Append(transform.GetComponent<Renderer>().material
                    .DOColor(_targetColor, _colorChangeDuration).SetEase(_moveEase));
            }
            else if (_doTweenType == DoTweenType.MovementOneWayColorChangeAndScale)
            {
                DOTween.Sequence()
                    .Append(transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase))
                    .Append(transform.DOScale(_scaleMultiplier, _moveDuration / 2.0f).SetEase(_moveEase))
                    .Append(transform.GetComponent<Renderer>().material
                    .DOColor(_targetColor, _colorChangeDuration).SetEase(_moveEase));
            }

        }   // Start()


        private IEnumerator MoveWithBothWays()
        {
            Vector3 originalLocation = transform.position;
            transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase);
            yield return new WaitForSeconds(_moveDuration);
            transform.DOMove(originalLocation, _moveDuration).SetEase(_moveEase);

        }   // MoveWithBothWays()


    }   // class DOTweenManager

}   // namespace Assets.Scenes.Game2048.Scripts
