using Cubeman.Interfaces;
using UnityEngine;

namespace Cubeman.Projectile
{
    public class EnemyProjectileDamage : ProjectileDamage
    {
        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && other.GetComponent<IDamageble>() != null)
            {
                var damageble = other.GetComponent<IDamageble>();
                damageble.ApplyDamage(_damage);

                if (_disableOnDamage)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
