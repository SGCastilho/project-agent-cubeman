using TMPro;
using DG.Tweening;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIScreenMessage : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI messageTMP;

        [Header("Message Data")]
        [SerializeField] private ScreenMessageData messageData;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float fadeDuration = 0.2f;

        public void SubscribeMessageData(ScreenMessageData data)
        {
            messageData = data;
            LoadMessageData();
        }

        public void FadeIn()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, fadeDuration);
        }

        private void LoadMessageData()
        {
            if(messageData.SpriteAsset != null)
            {
                messageTMP.spriteAsset = messageData.SpriteAsset;
            }

            messageTMP.text = messageData.Message;
        }

        public void FadeOut()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, fadeDuration).OnComplete(UnLoadMessageData);
        }

        private void UnLoadMessageData()
        {
            messageTMP.spriteAsset = null;
            messageTMP.text = string.Empty;
        }
    }
}
