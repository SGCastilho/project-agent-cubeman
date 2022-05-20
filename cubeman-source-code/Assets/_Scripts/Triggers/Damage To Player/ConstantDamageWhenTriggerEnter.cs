using Cubeman.Player;
using UnityEngine;

namespace Cubeman.Trigger
{
    public class ConstantDamageWhenTriggerEnter : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int damage = 6;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
                player.Status.ApplyDamage(damage);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
                player.Status.ApplyDamage(damage);
            }
        }
    }
}
