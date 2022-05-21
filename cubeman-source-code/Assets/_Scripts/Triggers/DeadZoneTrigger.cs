using Cubeman.Interfaces;
using UnityEngine;

namespace Cubeman.Trigger
{
    public sealed class DeadZoneTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) 
        {
            if(other.GetComponent<IDamageble>() != null)
            {
                var damageble = other.GetComponent<IDamageble>();
                damageble.InstaDeath();
            }
        }
    }
}
