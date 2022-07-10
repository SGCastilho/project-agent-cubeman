using UnityEngine;

namespace Cubeman.Utilities
{
    public sealed class DisableObjectAtDistance : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Transform objectTransform;
        [SerializeField] private GameObject objectToDisable;

        [Space(12)]

        [SerializeField] [Range(20f, 60f)] private float distanceToDisable = 40f;
        [SerializeField] [Range(0.1f, 4f)] private float checkDistanteTimer = 1f;

        #region Editor Variable
#if UNITY_EDITOR
        [Space(12)]

        [SerializeField] private bool showDistanceGizmos;
#endif
        #endregion

        private Transform _transform;

        private float _currentDistance;
        private float _currentCheckDistanceTime;

        private void Awake()
        {
            CacheComponents();
            CheckIfObjectIsAttacthed();
        }

        private void CacheComponents()
        {
            _transform = GetComponent<Transform>();
        }

        private void CheckIfObjectIsAttacthed()
        {
            if (objectTransform == null)
            {
                enabled = false;
            }
        }

        private void Update() => CheckDistanceTimer();

        private void CheckDistanceTimer()
        {
            _currentCheckDistanceTime += Time.deltaTime;
            if (_currentCheckDistanceTime >= checkDistanteTimer)
            {
                CheckDistance();
            }
        }

        private void CheckDistance()
        {
            _currentDistance = CalculateDistanceX(_transform.position.x, objectTransform.position.x);

            if (_currentDistance > distanceToDisable)
            {
                _currentDistance = 0;
                _currentCheckDistanceTime = 0;

                if(objectToDisable == null)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    objectToDisable.SetActive(false);
                }
            }
        }

        private float CalculateDistanceX(float pointA, float pointB)
        {
            return Mathf.Abs(pointA - pointB);
        }

        #region Editor Function
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if(showDistanceGizmos)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, distanceToDisable);
            }
        }
#endif
        #endregion
    }
}
