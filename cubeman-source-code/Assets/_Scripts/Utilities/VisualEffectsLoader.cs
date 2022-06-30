using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Utilities
{
    public sealed class VisualEffectsLoader : MonoBehaviour
    {
        #region Encapsulation
        public VisualEffectsData Data { get => data; }
        #endregion

        [Header("Data")]
        [SerializeField] private VisualEffectsData data;
    }
}
