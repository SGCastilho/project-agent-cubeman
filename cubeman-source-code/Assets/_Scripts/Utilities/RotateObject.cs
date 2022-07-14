using UnityEngine;

namespace Cubeman.Utilities
{
    public sealed class RotateObject : MonoBehaviour
    {
        #region Encapsulation
        public bool RotateRight { set => rotateRight = value; }
        #endregion

        [Header("Settings")]
        [SerializeField] private Space rotationSpace = Space.World;

        [Space(12)]

        [SerializeField] [Range(10f, 360f)] private float rotateVelocity = 60f;
        [SerializeField] private bool startRotationSide;

        private Transform _transform;

        private bool rotateRight;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            CacheComponets();

            rotateRight = startRotationSide;
        }

        private void CacheComponets()
        {
            _transform = GetComponent<Transform>();
        }

        private void Update() => Rotate();

        private void Rotate()
        {
            if (rotateRight)
            {
                _transform.Rotate(Vector3.back * rotateVelocity * Time.deltaTime, rotationSpace);
            }
            else
            {
                _transform.Rotate(Vector3.forward * rotateVelocity * Time.deltaTime, rotationSpace);
            }
        }
    }
}
