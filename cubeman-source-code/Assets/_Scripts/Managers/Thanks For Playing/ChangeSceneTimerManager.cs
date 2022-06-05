using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Cubeman.Manager
{
    public sealed class ChangeSceneTimerManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] [Range(1f, 20f)] private float currentSceneDuration = 12f;

        [Header("Unity Events")]

        [Space(12)]

        [SerializeField] private UnityEvent OnCounterEnd;

        private void Start() => StartCoroutine(SceneDurationCourotine());

        IEnumerator SceneDurationCourotine()
        {
            yield return new WaitForSeconds(currentSceneDuration);

            OnCounterEnd?.Invoke();
        }
    }
}