using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class CheckPlayerDistanceFromEnemy : MonoBehaviour
    {
        private Transform _playerTransform;
        private Transform _transform;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            _transform = GetComponent<Transform>();
            _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        internal float PlayerDistance()
        {
            return Mathf.Abs(_transform.position.x - _playerTransform.position.x);
        }
    }
}
