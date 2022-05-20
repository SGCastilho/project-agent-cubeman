using System.Collections;
using UnityEngine;

namespace Cubeman.Utilities
{
    public sealed class DisableObjectOverTime : MonoBehaviour
    {
        #region Encapsulation
        public float DisableTime { set => disableTime = value; }
        #endregion

        [Header("Settings")]
        [SerializeField] [Range(1f, 20f)] private float disableTime = 6f;

        private void OnEnable() => StartCoroutine(DisableTimeCoroutine());

        private void OnDisable() => StopCoroutine(DisableTimeCoroutine());

        IEnumerator DisableTimeCoroutine()
        {
            yield return new WaitForSeconds(disableTime);
            gameObject.SetActive(false);
        }
    }
}
