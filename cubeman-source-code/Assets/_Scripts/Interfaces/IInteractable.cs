using UnityEngine;

namespace Cubeman.Interfaces
{
    public interface IInteractable
    {
        public void OnTriggerEnter(Collider other);
        public void Interact();
        public void OnTriggerExit(Collider other);
    }
}
