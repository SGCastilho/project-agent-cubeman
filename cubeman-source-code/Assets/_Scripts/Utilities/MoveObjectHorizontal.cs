using UnityEngine;

namespace Cubeman.Utilities
{
    public sealed class MoveObjectHorizontal : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] [Range(1f, 20f)] private float movimentSpeed = 4f;
        [SerializeField] private bool moveRight = true;

        private Vector2 _movimentSide;

        private Transform _transform;

        private void Awake() => CacheComponets();

        private void CacheComponets()
        {
            _transform = GetComponent<Transform>();
        }

        private void OnEnable() => SetupMovimentSide();

        private void SetupMovimentSide()
        {
            if (moveRight)
            {
                _movimentSide = Vector2.right;
            }
            else
            {
                _movimentSide = Vector2.left;
            }
        }

        private void Update()
        {
            _transform.Translate(movimentSpeed * _movimentSide * Time.deltaTime, Space.World);
        }
    }
}