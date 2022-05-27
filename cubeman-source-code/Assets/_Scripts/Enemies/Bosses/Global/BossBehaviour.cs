using UnityEngine;

namespace Cubeman.Enemies
{
    public abstract class BossBehaviour : MonoBehaviour
    {
        #region Encapsulation
        internal BossSequencer Sequencer { get => sequencer; }
        internal EnemyCharacterMoviment Movement { get => movement; }
        internal EnemyCheckPlayerSide CheckPlayerSide { get => checkPlayerSide; }
        internal BossCheckWallInFront CheckWallInFront { get => checkWallInFront; }
        internal BossAnimator Animator { get => animator; }
        #endregion

        [Header("Classes")]
        [SerializeField] protected BossSequencer sequencer;
        [SerializeField] protected EnemyCharacterMoviment movement;
        [SerializeField] protected EnemyCheckPlayerSide checkPlayerSide;
        [SerializeField] protected BossCheckWallInFront checkWallInFront;
        [SerializeField] protected BossAnimator animator;
    }
}
