using UnityEngine;

namespace Cubeman.Utilities
{
    public sealed class RotateObject : MonoBehaviour
    {
        #region Encapsulation
        public bool RotateRight { set => rotateRight = value; }
        #endregion

        [Header("Settings")]
        [SerializeField] [Range(10f, 360f)] private float rotateVelocity = 60f;
        private bool rotateRight;

        private Transform _transform;

        private void Awake() => _transform = GetComponent<Transform>();

        private void Update() => Rotate();

        private void Rotate()
        {
            if (rotateRight)
            {
                _transform.Rotate(Vector3.back * rotateVelocity * Time.deltaTime, Space.World);
            }
            else
            {
                _transform.Rotate(Vector3.forward * rotateVelocity * Time.deltaTime, Space.World);
            }
        }
    }
}
