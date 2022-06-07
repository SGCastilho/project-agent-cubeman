using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Cubeman.Trigger
{
    public sealed class ScreenMessageTrigger : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] [Range(0.1f, 6f)] private float messageDelay = 4f;

        [Header("Unity Events")]

        [Space(12)]

        [SerializeField] private UnityEvent loadMessageEvent;
        [SerializeField] private UnityEvent showMessageEvent;
        [SerializeField] private UnityEvent hideMessageEvent;

        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                loadMessageEvent.Invoke();
                StartCoroutine(MessageDelayCoroutine());
            }
        }

        IEnumerator MessageDelayCoroutine()
        {
            yield return new WaitForSeconds(messageDelay);
            showMessageEvent.Invoke();
        }

        private void OnTriggerExit(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                hideMessageEvent.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}
