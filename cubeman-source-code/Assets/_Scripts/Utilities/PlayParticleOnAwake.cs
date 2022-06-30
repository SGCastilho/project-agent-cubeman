using UnityEngine;

namespace Cubeman.Utilities
{
    public sealed class PlayParticleOnAwake : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private ParticleSystem particle;

        private void OnEnable() => particle.Play();

        private void OnDisable() => particle.Stop();
    }
}