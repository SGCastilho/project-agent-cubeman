using Cubeman.Audio;
using UnityEngine;

namespace Cubeman.Enemies
{
    public abstract class BossBehaviour : MonoBehaviour
    {
        #region Encapsulation
        public BossDataLoader DataLoader { get => dataLoader; }
        public EnemyCharacterMoviment Movement { get => movement; }
        public AudioSoundEffects SoundEffects { get => soundEffects; }

        internal BossSequencer Sequencer { get => sequencer; }
        internal EnemyCheckPlayerSide CheckPlayerSide { get => checkPlayerSide; }
        internal CheckPlayerDistanceFromEnemy CheckPlayerDistance { get => checkPlayerDistance; }
        internal BossCheckWallInFront CheckWallInFront { get => checkWallInFront; }
        internal BossAnimator Animator { get => animator; }
        #endregion

        [Header("Classes")]
        [SerializeField] protected BossDataLoader dataLoader;

        [Space(12)]

        [SerializeField] protected BossSequencer sequencer;
        [SerializeField] protected EnemyCharacterMoviment movement;
        [SerializeField] protected EnemyCheckPlayerSide checkPlayerSide;
        [SerializeField] protected CheckPlayerDistanceFromEnemy checkPlayerDistance;
        [SerializeField] protected BossCheckWallInFront checkWallInFront;
        [SerializeField] protected BossAnimator animator;
        [SerializeField] protected AudioSoundEffects soundEffects;
    }
}
