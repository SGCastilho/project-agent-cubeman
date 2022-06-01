using UnityEngine;
using UnityEngine.Events;

namespace Cubeman.Trigger
{
    public sealed class LoadSceneGroupTrigger : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject oppositeTrigger;
        [SerializeField] private bool disableTriggerWhenStart;

        [Header("Unity Events")]

        [Space(12)]

        [SerializeField] private UnityEvent OnPlayerFinded;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            if (disableTriggerWhenStart)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                OnPlayerFinded?.Invoke();

                gameObject.SetActive(false);
                oppositeTrigger.SetActive(true);
            }
        }
    }
}