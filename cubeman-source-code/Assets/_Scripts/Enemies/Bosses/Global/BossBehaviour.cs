using Cubeman.Audio;
using UnityEngine;

namespace Cubeman.Enemies
{
    public abstract class BossBehaviour : MonoBehaviour
    {
        #region Encapsulation
        public BossDataLoader DataLoader { get => dataLoader; }
        public EnemyCharacterMoviment Moviment { get => moviment; }
        public AudioSoundEffects SoundEffects { get => soundEffects; }

        internal BossStatus Status { get => status; }
        internal BossSequencer Sequencer { get => sequencer; }
        internal EnemyCheckPlayerSide CheckPlayerSide { get => checkPlayerSide; }
        internal CheckPlayerDistanceFromEnemy CheckPlayerDistance { get => checkPlayerDistance; }
        internal BossCheckWallInFront CheckWallInFront { get => checkWallInFront; }
        internal BossAnimator Animator { get => animator; }
        internal BossVisualEffects VisualEffects { get => visualEffects; }
        #endregion

        [Header("Classes")]
        [SerializeField] protected BossDataLoader dataLoader;

        [Space(12)]

        [SerializeField] protected BossStatus status;
        [SerializeField] protected BossSequencer sequencer;
        [SerializeField] protected EnemyCharacterMoviment moviment;
        [SerializeField] protected EnemyCheckPlayerSide checkPlayerSide;
        [SerializeField] protected CheckPlayerDistanceFromEnemy checkPlayerDistance;
        [SerializeField] protected BossCheckWallInFront checkWallInFront;
        [SerializeField] protected BossAnimator animator;
        [SerializeField] protected AudioSoundEffects soundEffects;
        [SerializeField] protected BossVisualEffects visualEffects;
    }
}
