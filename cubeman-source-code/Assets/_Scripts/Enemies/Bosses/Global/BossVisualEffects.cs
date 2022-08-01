using Cubeman.Utilities;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class BossVisualEffects : MonoBehaviour
    {
        #region Encapsulation
        internal PlayParticleInSequencePoints ExplosionParticle { get => explosionParticle; }
        #endregion

        [Header("Classes")]
        [SerializeField] private PlayParticleInSequencePoints explosionParticle;
    }
}
