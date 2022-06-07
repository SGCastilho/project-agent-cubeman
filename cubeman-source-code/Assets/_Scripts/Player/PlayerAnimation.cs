using UnityEngine;

namespace Cubeman.Player
{
    public sealed class PlayerAnimation : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private PlayerBehaviour behaviour;

        [Space(12)]

        [SerializeField] private Animator animator;

        internal bool DashAnimation
        {
            set
            {
                animator.SetBool("isDashing", value);
            }
        }

        internal bool TakeDamageAnimation
        {
            set
            {
                animator.SetBool("takeDamage", value);
            }
        }

        internal bool AutomaticMoveAnimation
        {
            set
            {
                animator.SetBool("inAutomaticMove", value);
            }
        }

        internal bool IsDeadAnimation
        {
            set
            {
                animator.SetBool("isDead", value);
            }
        }

        private void Update() => ConstantAnimation();

        private void ConstantAnimation()
        {
            animator.SetBool("jumped", behaviour.Moviment.Gravity.IsJumped);
            animator.SetBool("isGrounded", behaviour.Moviment.Gravity.IsGrounded);
            animator.SetFloat("horizontalInput", Mathf.Abs(behaviour.Input.HorizontalAxis));
        }

        internal void CallAnimatorTrigger(string triggerKey)
        {
            animator.SetTrigger(triggerKey);
        }
    }
}
