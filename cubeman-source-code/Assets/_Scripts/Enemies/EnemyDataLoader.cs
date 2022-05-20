using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class EnemyDataLoader : MonoBehaviour
    {
        #region Encapsulation
        public EnemyData Data { get => data; }
        #endregion

        [Header("Data")]
        [SerializeField] private EnemyData data;
    }
}
