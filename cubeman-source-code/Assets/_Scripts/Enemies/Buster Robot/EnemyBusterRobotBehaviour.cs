using Cubeman.Audio;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class EnemyBusterRobotBehaviour : MonoBehaviour
    {
        #region Encapsulation
        public EnemyDataLoader DataLoader { get => dataLoader; }

        internal EnemyBusterRobotState State { get => state; }
        public EnemyCharacterMoviment Moviment { get => moviment; }
        internal EnemyBusterRobotAnimator Animator { get => animator; }

        internal Transform EnemyTransform { get => _transform; }
        #endregion

        [Header("Classes")]
        [SerializeField] private EnemyDataLoader dataLoader;
        [SerializeField] private EnemyBusterRobotState state;
        [SerializeField] private EnemyCharacterMoviment moviment;
        [SerializeField] private EnemyBusterRobotAnimator animator;
        [SerializeField] private AudioSoundEffects soundEffects;

        private const string SHOOT_AUDIO_KEY = "audio_shoot";

        [HideInInspector]
        public AudioClipList _shootSFX;

        private Transform _transform;

        private void Awake() => _transform = GetComponent<Transform>();

        private void Start()
        {
            _shootSFX = soundEffects.GetSoundEffect(SHOOT_AUDIO_KEY);
        }

        private void OnDisable() => ResetPosistion();

        private void ResetPosistion()
        {
            _transform.position = state.OriginPointTransform.position;
        }
    }
}
