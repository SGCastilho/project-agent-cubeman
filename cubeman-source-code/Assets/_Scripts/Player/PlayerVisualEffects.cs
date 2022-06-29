using DG.Tweening;
using UnityEngine;

namespace Cubeman.Player
{
    public sealed class PlayerVisualEffects : MonoBehaviour
    {
        private const string FLASH_HIT_AMOUNT_PROPRIETY = "_FlashAmount";

        [Header("Materials")]
        [SerializeField] private Material flashHitMaterial;

        [Header("Settings")]
        [SerializeField] [Range(0f, 1f)] private float flashHitAmount = 0.6f;
        [SerializeField] [Range(0.1f, 1f)] private float flashHitDuration = 0.4f;

        private void OnDisable() => ResetVisualEffects();

        private void ResetVisualEffects()
        {
            flashHitMaterial.SetFloat(FLASH_HIT_AMOUNT_PROPRIETY, 0);
        }

        public void DoFlashHit()
        {
            flashHitMaterial.DOKill();
            flashHitMaterial.DOFloat(flashHitAmount, FLASH_HIT_AMOUNT_PROPRIETY, 
                flashHitDuration).OnComplete(FinishFlashHit);
        }

        private void FinishFlashHit()
        {
            flashHitMaterial.DOFloat(0f, FLASH_HIT_AMOUNT_PROPRIETY, flashHitDuration).OnComplete(FinishFlashHit);
        }
    }
}
