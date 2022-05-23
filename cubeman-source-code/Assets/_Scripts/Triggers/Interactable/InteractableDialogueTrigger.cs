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

        [SerializeField] private UnityEvent RangeToInteract;
        [SerializeField] private UnityEvent OutOfRangeToInteract;

        public void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                RangeToInteract.Invoke();

                if(_player == null) 
                {
                    _player = other.GetComponent<PlayerBehaviour>();
                }

                _player.Input.SubscribeInteractInput(Interact);
            }
        }

        public void Interact()
        {
            _player.Input.GameplayInputs(false);
            _player.Input.UIInputs(true);
            _player.Input.UnSubscribeInteractInput(Interact);
        }

        public void OnTriggerExit(Collider other)
        {
            OutOfRangeToInteract.Invoke();

            if(other.CompareTag("Player"))
            {
                _player.Input.UnSubscribeInteractInput(Interact);
            }
        }
    }
}
