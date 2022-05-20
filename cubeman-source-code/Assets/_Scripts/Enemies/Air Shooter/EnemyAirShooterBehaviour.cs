using Cubeman.Audio;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class EnemyAirShooterBehaviour : MonoBehaviour
    {
        #region Encapsulation
        internal EnemyDataLoader DataLoader { get => dataLoader; }
        internal EnemyMovementBetweenTwoPoints Moviment { get => movement; }
        internal LocalAudioManager AudioManager { get => localAudioManager; }

        public EnemyAirShooterAttack Attack { get => attack; }
        #endregion

        [Header("Classes")]
        [SerializeField] private EnemyDataLoader dataLoader;
        [SerializeField] private EnemyMovementBetweenTwoPoints movement;
        [SerializeField] private EnemyAirShooterAttack attack;
        [SerializeField] private LocalAudioManager localAudioManager;
    }
}
