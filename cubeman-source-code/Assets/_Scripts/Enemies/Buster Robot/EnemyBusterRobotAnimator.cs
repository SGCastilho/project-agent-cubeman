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

        private void OnDisable() => ResetAnimations();

        private void ResetAnimations()
        {
            RunningAnimation = false;
            ShootingPoseAnimation = false;
        }

        public void CallAnimationTrigger(string trigger)
        {
            animator.SetTrigger(trigger);
        }
    }
}
