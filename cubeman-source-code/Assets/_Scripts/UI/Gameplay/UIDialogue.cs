using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Cubeman.UI
{
    public sealed class UIDialogue : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private CanvasGroup canvasGroup;

        [Space(12)]

        [SerializeField] private Image characterFace;
        [SerializeField] private TextMeshProUGUI dialogueTMP;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float fadeDuration = 0.2f;

        public void FadeIn()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, fadeDuration);
        }

        public void SetDialogue(ref Sprite face, ref string name, ref string dialogue)
        {
            characterFace.sprite = face;
            dialogueTMP.text = $"{name}: {dialogue}";
        }

        public void FadeOut()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, fadeDuration);
        }
    }
}
