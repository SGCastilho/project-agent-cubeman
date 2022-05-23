using Cubeman.Player;
using Cubeman.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Cubeman.Trigger
{
    public sealed class InteractableDialogueTrigger : MonoBehaviour, IInteractable
    {
        private PlayerBehaviour _player;

        [Header("Unity Events")]

        [Space(6)]

        [SerializeField] private UnityEvent OnRangeToInteract;
        [SerializeField] private UnityEvent OnInteract;
        [SerializeField] private UnityEvent OnOutOfRangeToInteract;

        public void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                OnRangeToInteract.Invoke();

                if(_player == null) 
                {
                    _player = other.GetComponent<PlayerBehaviour>();
                }

                _player.Input.SubscribeInteractInput(Interact);
            }
        }

        public void Interact() => OnInteract.Invoke();

        public void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                _player.Input.UnSubscribeInteractInput();
                
                OnOutOfRangeToInteract.Invoke();
            }
        }
    }
}
