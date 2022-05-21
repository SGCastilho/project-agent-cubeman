using Cubeman.Interfaces;
using UnityEngine;

namespace Cubeman.Projectile
{
    public class ProjectileDamage : MonoBehaviour
    {
        #region Encapsulation
        internal int Damage { set => _damage = value; }
        internal bool DisableOnDamage { set => _disableOnDamage = value; }
        #endregion

        protected int _damage;
        protected bool _disableOnDamage;

        public virtual void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<IDamageble>() != null)
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
