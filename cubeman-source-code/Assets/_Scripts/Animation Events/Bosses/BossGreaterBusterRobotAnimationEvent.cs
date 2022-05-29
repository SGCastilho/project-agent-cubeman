using Cubeman.Audio;
using Cubeman.Enemies;
using Cubeman.Manager;
using UnityEngine;

namespace Cubeman.AnimationEvents
{
    public sealed class BossGreaterBusterRobotAnimationEvent : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private BossGreaterBusterRobotBehaviour behaviour;

        private AudioController audioController;
        private ObjectPoolingManager poolingManager;

        private void Awake()
        {
            CacheComponets();
        }

        private void CacheComponets()
        {
            audioController = AudioController.Instance;
            poolingManager = ObjectPoolingManager.Instance;
        }

        public void ShootEvent()
        {

        }

        public void ShockWaveEvent()
        {

        }

        public void JumpInEvent()
        {

        }

        public void JumpOutEvent()
        {

        }

        public void DangerAttackWarningEvent()
        {

        }

        public void DeathLaserStartEvent()
        {

        }

        public void DeathLaserEndEvent()
        {

        }
    }
}