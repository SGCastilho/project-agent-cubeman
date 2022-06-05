using UnityEngine;

namespace Cubeman.Utilities
{
    public sealed class DontDestroyOnLoadObject : MonoBehaviour
    {
        private void Start() => DontDestroyOnLoad(this);
    }
}