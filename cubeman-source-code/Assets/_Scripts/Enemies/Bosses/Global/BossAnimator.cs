using UnityEngine;

namespace Cubeman.Enemies
{
    public abstract class BossAnimator : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] protected Animator animator;

        internal bool EncounterAnimation 
        {
            set => animator.SetBool("encounter", value);
        }

        internal void CallAnimationTrigger(string trigger)
        {
            animator.SetTrigger(trigger);
        }
    }
}
