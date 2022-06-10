using Cubeman.Audio;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class EnemyAirShooterBehaviour : MonoBehaviour
    {
        #region Encapsulation
        internal EnemyDataLoader DataLoader { get => dataLoader; }
        internal EnemyMovementBetweenTwoPoints Moviment { get => movement; }
        internal LocalAudioController AudioManager { get => localAudioManager; }

        internal Transform BehaviourTransform { get => _transform; }

        public EnemyAirShooterAttack Attack { get => attack; }
        #endregion

        [Header("Classes")]
        [SerializeField] private EnemyDataLoader dataLoader;
        [SerializeField] private EnemyMovementBetweenTwoPoints movement;
        [SerializeField] private EnemyAirShooterAttack attack;

        [Space(6)]

        [SerializeField] private LocalAudioController localAudioManager;

        private Transform _transform;

        private void Awake() => CacheComponets();

        private void CacheComponets()
        {
            _transform = GetComponent<Transform>();
        }
    }
}
