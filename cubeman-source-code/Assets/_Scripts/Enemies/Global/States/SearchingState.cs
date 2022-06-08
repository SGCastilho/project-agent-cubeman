using UnityEngine;

namespace Cubeman.Enemies 
{
    public class SearchingState : State
    {
        [Header("Settings")]
        [SerializeField] private State nextState;

        [Space(12)]

        [SerializeField] private Transform searchingTransform;
        [SerializeField] private Vector3 searchingBox = Vector3.zero;
        [SerializeField] private LayerMask searchingLayer;

        [Space(4)]

        [SerializeField] [Range(0.1f, 2f)] private float searchingPerSeconds = 2f;

        private bool _playerFinded;
        private float _currentSearchingPerSeconds;

        #region Editor Variable
#if UNITY_EDITOR
        [SerializeField] private bool showSearchingGizmos;
#endif
        #endregion

        public override State RunCurrentState()
        {
            SearchingPerSeconds();

            if(_playerFinded)
            {
                ResetState();

                return nextState;
            }

            return this;
        }

        private void SearchingPerSeconds()
        {
            _currentSearchingPerSeconds += Time.deltaTime;
            if (_currentSearchingPerSeconds >= searchingPerSeconds)
            {
                SearchingBox();
            }
        }

        private void SearchingBox()
        {
            Collider[] colliders = Physics.OverlapBox(searchingTransform.position, searchingBox,
                Quaternion.identity, searchingLayer);

            if (colliders != null)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].CompareTag("Player"))
                    {
                        _playerFinded = true;
                        _currentSearchingPerSeconds = 0;
                        break;
                    }
                }
            }
        }

        private void OnDisable() => ResetState();

        private void ResetState()
        {
            ResetTimers();
            ResetBooleans();
        }

        private void ResetTimers()
        {
            _currentSearchingPerSeconds = 0;
        }

        private void ResetBooleans()
        {
            _playerFinded = false;
        }

        #region Editor
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (showSearchingGizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(searchingTransform.position, searchingBox);
            }
        }
#endif
        #endregion
    }
}