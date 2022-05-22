using UnityEngine;

namespace Cubeman.Scenario
{
    public sealed class ScenarioCivilianPointCheck : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private ScenarioCivilianBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] private Transform finalDestinationTransform;

        [Space(12)]

        [SerializeField] [Range(0.1f, 1f)] private float checkDistanceDuration = 0.4f;
        [SerializeField] [Range(0.1f, 0.2f)] private float minDistanceStop = 0.1f;

        private float _currentCheckDistanceDuration;
        private float _currentDistance;

        #region  Editor Variables
    #if UNITY_EDITOR
        [Header("Editor Settings")]
        [SerializeField] private Transform civilianTransform;
    #endif
        #endregion

        private void OnDisable() => ResetObject();

        private void ResetObject()
        {
            _currentCheckDistanceDuration = 0;
            _currentDistance = 0;
        }

        private void Update() => CheckDistanceTimer();

        private void CheckDistanceTimer()
        {
            _currentCheckDistanceDuration += Time.deltaTime;
            if (_currentCheckDistanceDuration >= checkDistanceDuration)
            {
                CalculateDistance();
                _currentCheckDistanceDuration = 0;
            }
        }

        private void CalculateDistance()
        {
            _currentDistance = CalculateDistancePoints(behaviour.Moviment.CivilianTransform.position.x, finalDestinationTransform.position.x);
            if (_currentDistance <= minDistanceStop)
            {
                gameObject.SetActive(false);
            }
        }

        private float CalculateDistancePoints(float pointA, float pointB)
        {
            var distance = pointA - pointB;
            if(distance < 0)
            {
                distance = 0;
                return distance;
            }

            return Mathf.Abs(distance);
        }

        #region  Editor Functions
    #if UNITY_EDITOR
        private void OnDrawGizmosSelected() 
        {
            if(finalDestinationTransform != null && civilianTransform != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(finalDestinationTransform.position, 0.2f);

                Gizmos.color = Color.red;
                var newCivilianTransform = new Vector3(civilianTransform.position.x, civilianTransform.position.y + 0.6f, 
                    civilianTransform.position.z);

                Gizmos.DrawLine(newCivilianTransform, finalDestinationTransform.position);
            }
        }
    #endif
        #endregion
    }
}
