using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Cubeman.Utilities
{
    public sealed class MoveObjectTween : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Vector3 finalDestination;
        [SerializeField] [Range(0.1f, 2f)] private float finalDestinationDuration = 1f;

        [Space(6)]

        [SerializeField] private Ease tweenEase;

        [Space(12)]

        [SerializeField] private UnityEvent OnMoveFinish;

        private Transform _transform;

        private void Awake() => CacheComponets();

        private void CacheComponets()
        {
            _transform = GetComponent<Transform>();
        }

        public void StartTween()
        {
            _transform.DOMove(finalDestination, finalDestinationDuration).SetEase(tweenEase).OnComplete(MoveFinish);
        }

        private void MoveFinish()
        {
            OnMoveFinish?.Invoke();
        }
    }
}
