using UnityEngine;

namespace Cubeman.Enemies
{
    public enum BusterRobotStates { SEARCHING, CHASING, SHOOTING, BACK_TO_ORIGIN }

    public sealed class EnemyBusterRobotState : MonoBehaviour
    {
        private delegate void EnemyState();
        private EnemyState CurrentEnemyState;

        private StatesBusterRobot _busterRobotStates;

        #region Encapsulation
        internal EnemyBusterRobotBehaviour Behaviour { get => behaviour; }
        internal Transform PlayerTransform { get => _playerTransform; }

        internal Transform SearchingTransform { get => searchingTransform; }
        internal Vector3 SerachingBox { get => serachingBox; }
        internal LayerMask SearchingLayer { get => serachingLayer; }
        internal float SearchingPerSeconds { get => searchingPerSeconds; }
        internal float CurrentSearchingPerSeconds { get => _currentSearchingPerSeconds; set => _currentSearchingPerSeconds = value; }

        internal float ShootingRange { get => shootingRange; }
        internal float DistanceFromPlayer { get => _distanceFromPlayer; set => _distanceFromPlayer = value; }

        internal float ShootingPoseDelay { get => shootingPoseDelay; }
        internal float CurrentShootingPoseDelay { get => _currentShootingPoseDelay; set => _currentShootingPoseDelay = value; }
        internal bool ShootingPose
        {
            get => _shootingPose;
            set
            {
                _shootingPose = value;
                behaviour.Animator.ShootingPoseAnimation = _shootingPose;
            }
        }
        
        internal float ShootDelay { get => shootDelay; }
        internal float CurrentShootDelay { get => _currentShootDelay; set => _currentShootDelay = value; }

        internal float ShootingFireRate { get => shootingFireRate; }
        internal float CurrentShootingFireRate { get => _currentShootingFireRate; set => _currentShootingFireRate = value; }
        internal bool Shooted
        {
            get => _shooted;
            set
            {
                _shooted = value;
                if (value == true) { behaviour.Animator.CallAnimationTrigger("shoot"); }
            }
        }

        internal float BackChasingDelay { get => backChasingDelay; }
        internal float CurrentBackChasingDelay { get => _currentBackChasingDelay; set => _currentBackChasingDelay = value; }
        internal bool InBackChasingDelay { get => _inBackChasingDelay; set => _inBackChasingDelay = value; }

        internal Transform OriginPointTransform { get => originPointTransform; }
        internal float MaxOriginDistance { get => maxOriginDistance; }
        internal float DistanceFromOriginPoint { get => _distanceFromOriginPoint; set => _distanceFromOriginPoint = value; }

        internal float BackToOriginDelay { get => backToOriginDelay; }
        internal float CurrentBackToOriginDelay { get => _currentBackToOriginDelay; set => _currentBackToOriginDelay = value; }
        #endregion

        [Header("Classes")]
        [SerializeField] private EnemyBusterRobotBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] private BusterRobotStates startState;
        private BusterRobotStates _currentState;

        [Space(12)]

        [SerializeField] private Transform searchingTransform;
        [SerializeField] private Vector3 serachingBox = Vector3.zero;
        [SerializeField] private LayerMask serachingLayer;
        [Space(4)]
        [SerializeField] [Range(0.1f, 2f)] private float searchingPerSeconds = 2f;
        private float _currentSearchingPerSeconds;

        #region Editor Variable
#if UNITY_EDITOR
        [SerializeField] private bool showSearchingGizmos;
#endif
        #endregion

        [Space(12)]

        [SerializeField] [Range(4f, 12f)] private float shootingRange = 4f;
        private float _distanceFromPlayer;

        [SerializeField] [Range(0.1f, 1f)] private float shootingPoseDelay = 1f;
        private float _currentShootingPoseDelay;
        private bool _shootingPose;

        [SerializeField] [Range(0.1f, 2f)] private float shootingFireRate = 1.2f;
        private float _currentShootingFireRate;
        private bool _shooted;
        [SerializeField] [Range(0.1f, 2f)] private float shootDelay = 0.4f;
        private float _currentShootDelay;

        [SerializeField] [Range(0.1f, 1f)] private float backChasingDelay = 1f;
        private float _currentBackChasingDelay;
        private bool _inBackChasingDelay;

        #region Editor Variable
#if UNITY_EDITOR
        [SerializeField] private bool showShootingGizmos;
#endif
        #endregion

        [Space(12)]

        [SerializeField] private Transform originPointTransform;
        [SerializeField] [Range(20f, 60f)] private float maxOriginDistance = 26f;
        private float _distanceFromOriginPoint;
        [Space(4)]
        [SerializeField] [Range(0.4f, 1f)] private float backToOriginDelay = 0.4f;
        private float _currentBackToOriginDelay;

        #region Editor Variable
#if UNITY_EDITOR
        [SerializeField] private bool showOriginPointGizmos;
#endif
        #endregion

        private Transform _playerTransform;

        private void Awake() 
        {
            SetupStateMachine(new StatesBusterRobot(this));
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void OnEnable() => SetState(startState);

        private void OnDisable()
        {
            ResetDistances();
            ResetBooleans();
            ResetTimers();
        }

        private void SetupStateMachine(StatesBusterRobot nextState)
        {
            _busterRobotStates = nextState;
        }

        private void Update() => CurrentEnemyState();

        public void SetState(BusterRobotStates nextState) 
        {
            _currentState = nextState;

            switch(_currentState)
            {
                case BusterRobotStates.SEARCHING:
                    CurrentEnemyState = null;
                    CurrentEnemyState += _busterRobotStates.SearchingForPlayer;
                    behaviour.Animator.RunningAnimation = false;
                    break;
                case BusterRobotStates.CHASING:
                    CurrentEnemyState = null;
                    CurrentEnemyState += _busterRobotStates.ChasePlayer;
                    behaviour.Animator.RunningAnimation = true;
                    break;
                case BusterRobotStates.SHOOTING:
                    CurrentEnemyState = null;
                    CurrentEnemyState += _busterRobotStates.PrepareShooting;
                    behaviour.Animator.RunningAnimation = false;
                    break;
                case BusterRobotStates.BACK_TO_ORIGIN:
                    CurrentEnemyState = null;
                    CurrentEnemyState += _busterRobotStates.BackToOriginalPosistion;
                    behaviour.Animator.RunningAnimation = true;
                    break;
            }
        }

        internal void ResetDistances()
        {
            _distanceFromOriginPoint = 0;
            _distanceFromPlayer = 0;
        }

        internal void ResetBooleans()
        {
            _shooted = false;
            _shootingPose = false;
        }

        internal void ResetTimers()
        {
            _currentBackChasingDelay = 0;
            _currentBackToOriginDelay = 0;
            _currentSearchingPerSeconds = 0;
            _currentShootDelay = 0;
            _currentShootingFireRate = 0;
            _currentShootingPoseDelay = 0;
        }

        #region Editor
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if(showSearchingGizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(searchingTransform.position, serachingBox);
            }

            if(showShootingGizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, shootingRange);
            }

            if(showOriginPointGizmos)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(originPointTransform.position, 0.4f);
                Gizmos.DrawWireSphere(originPointTransform.position, maxOriginDistance);
            }
        }
#endif
        #endregion
    }
}
