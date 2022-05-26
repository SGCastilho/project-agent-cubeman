using Cubeman.Player;
using UnityEngine;

namespace Cubeman.Trigger
{
    public sealed class AutomaticMoveTrigger : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] [Range(0.1f, 4f)] private float movimentDuration = 2f;
        [SerializeField] private bool moveRight = true;

        [Space(12)]

        [SerializeField] private bool enableInputsOnComplete;

        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
                player.Moviment.StartAutomaticMove(moveRight, enableInputsOnComplete, movimentDuration);
            }
        }
    }
}
