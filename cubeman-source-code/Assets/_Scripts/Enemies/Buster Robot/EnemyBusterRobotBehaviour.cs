using Cubeman.Audio;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class EnemyBusterRobotBehaviour : MonoBehaviour
    {
        #region Encapsulation
        public EnemyDataLoader DataLoader { get => dataLoader; }
        public EnemyCharacterMoviment Moviment { get => moviment; }

        internal EnemyCheckPlayerSide PlayerSide { get => checkPlayerSide; }
        internal CheckPlayerDistanceFromEnemy CheckPlayerDistance { get => checkPlayerDistance; }

        internal EnemyBusterRobotAnimator Animator { get => animator; }

        internal Transform EnemyTransform { get => _transform; }
        #endregion

        [Header("Classes")]
        [SerializeField] private EnemyDataLoader dataLoader;

        [Space(6)]

        [SerializeField] private EnemyCharacterMoviment moviment;

        [Space(6)]

        [SerializeField] private EnemyCheckPlayerSide checkPlayerSide;
        [SerializeField] private CheckPlayerDistanceFromEnemy checkPlayerDistance;

        [Space(6)]

        [SerializeField] private EnemyBusterRobotAnimator animator;
        [SerializeField] private AudioSoundEffects soundEffects;

        private Vector3 _startPosistion;

        private Transform _transform;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            _transform = GetComponent<Transform>();
            _startPosistion = _transform.position;
        }

        private void OnDisable() => ResetObject();

        private void ResetObject()
        {
            _transform.position = _startPosistion;
        }
    }
}
