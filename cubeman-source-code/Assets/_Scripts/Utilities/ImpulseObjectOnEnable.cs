using UnityEngine;

namespace Cubeman.Utilities
{
    public sealed class ImpulseObjectOnEnable : MonoBehaviour
    {
        #region Encapsulation
        public Vector3 XImpulseSide { set => _xImpulseSide = value; }
        #endregion

        [Header("Classes")]
        [SerializeField] private Rigidbody rigidBody;

        [Header("Settings")]
        [SerializeField] [Range(4f, 12f)] private float yImpulseForce = 6f;
        [SerializeField] [Range(1f, 12f)] private float xImpulseForceMin = 1f;
        [SerializeField] [Range(1f, 12f)] private float xImpulseForceMax = 4f;
        
        private float _xImpulseForce;
        private Vector3 _xImpulseSide;

        public void ImpulseObject()
        {
            _xImpulseForce = Random.Range(xImpulseForceMin, xImpulseForceMax);

            rigidBody.AddForceAtPosition(Vector3.up * yImpulseForce, transform.position, ForceMode.Impulse);
            rigidBody.AddForceAtPosition(_xImpulseSide * _xImpulseForce, transform.position, ForceMode.Impulse);
        }
    }
}
