using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class BossDataLoader : MonoBehaviour
    {
        #region Encapsulation
        public BossData Data { get => data; }
        #endregion

        [Header("Data")]
        [SerializeField] private BossData data;
    }
}