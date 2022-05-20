using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class EnemyBusterRobotAnimator : MonoBehaviour
    {
        #region Encapsulation
        internal bool RunningAnimation 
        { 
            set 
            {
                animator.SetBool("running", value);
            } 
        }

        internal bool ShootingPoseAnimation 
        {
            set
            {
                animator.SetBool("shootingPose", value);
            }
        }
        #endregion

        [Header("Classes")]
        [SerializeField] private Animator animator;

        public void CallAnimationTrigger(string trigger)
        {
            animator.SetTrigger(trigger);
        }
    }
}
