using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class EnemyCharacterSpinRobotBehaviour : MonoBehaviour
    {
        #region Encapsulation
        public EnemyCharacterSpinRobotMoviment Moviment { get => moviment; }
        #endregion

        [Header("Classes")]
        [SerializeField] private EnemyCharacterSpinRobotMoviment moviment;
    }
}
