using TMPro;
using DG.Tweening;
using System.Text;
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

        private StringBuilder _dialogueStringBuilder = new StringBuilder();

        public void FadeIn()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, fadeDuration);
        }

        public void SetDialogue(ref Sprite face, ref string name, ref string dialogue)
        {
            characterFace.sprite = face;

            _dialogueStringBuilder.Length = 0;

            _dialogueStringBuilder.Append(name);
            _dialogueStringBuilder.Append(": ");
            _dialogueStringBuilder.Append(dialogue);

            dialogueTMP.text = _dialogueStringBuilder.ToString();
        }

        public void FadeOut()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, fadeDuration);
        }
    }
}
