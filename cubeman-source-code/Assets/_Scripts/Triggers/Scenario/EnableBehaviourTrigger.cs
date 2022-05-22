using UnityEngine;

namespace Cubeman.Trigger
{
    public sealed class EnableBehaviourTrigger : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject[] behaviourToEnable;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            if(behaviourToEnable != null && behaviourToEnable.Length > 0)
            {
                EnableBehaviours(false);
            }
            else { gameObject.SetActive(false); }
        }

        private void EnableBehaviours(bool enable)
        {
            for (int i = 0; i < behaviourToEnable.Length; i++)
            {
                behaviourToEnable[i].SetActive(enable);
            }
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                EnableBehaviours(true);

                gameObject.SetActive(false);
            }
        }
    }
}
