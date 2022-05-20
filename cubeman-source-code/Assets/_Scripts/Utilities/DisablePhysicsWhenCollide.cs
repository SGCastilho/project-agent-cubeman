using UnityEngine;

namespace Cubeman.Utilities
{
    public sealed class DisablePhysicsWhenCollide : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private Rigidbody rigidBody;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 2f)] private float groundCheckUpdateTime = 0.2f;
        [SerializeField] [Range(0.1f, 1f)] private float groundCheckRange;
        [SerializeField] private LayerMask groundLayer;

        private float _currentGroundCheckUpdateTime;
        private bool _physicDisable;

        private Transform _transform;

        private void OnDisable() => EnablePhysics();

        private void Start() => _transform = GetComponent<Transform>();

        private void Update() => GroundCheckTimer();

        private void GroundCheckTimer()
        {
            if (!_physicDisable)
            {
                _currentGroundCheckUpdateTime += Time.deltaTime;
                if (_currentGroundCheckUpdateTime >= groundCheckUpdateTime)
                {
                    GroundRay();
                    _currentGroundCheckUpdateTime = 0;
                }
            }
        }

        private void GroundRay()
        {
            Ray ray = new Ray(_transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, groundCheckRange, groundLayer))
            {
                DisablePhysics();
            }
        }

        private void EnablePhysics()
        {
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
            _physicDisable = false;
        }

        private void DisablePhysics()
        {
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;
            _physicDisable = true;
        }

        #region Editor
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, Vector3.down * groundCheckRange);
        }
#endif
        #endregion
    }
}
