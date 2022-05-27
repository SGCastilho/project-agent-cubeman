using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class BossGreaterBusterRobotBehaviour : BossBehaviour
    {
        #region Encapsulation
        internal BossGreaterBusterRobotAnimator ExclusiveAnimator { get => exclusiveAnimator; }
        #endregion

        [Header("Exclusive Classes")]
        [SerializeField] private BossGreaterBusterRobotAnimator exclusiveAnimator;
    }
}
