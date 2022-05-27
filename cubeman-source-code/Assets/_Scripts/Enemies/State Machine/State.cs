using UnityEngine;

namespace Cubeman.Enemies
{
    public abstract class State : MonoBehaviour
    {
        public abstract State RunCurrentState();
    }
}
