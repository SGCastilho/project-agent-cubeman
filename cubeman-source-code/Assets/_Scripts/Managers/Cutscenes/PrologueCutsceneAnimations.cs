using DG.Tweening;
using Cubeman.Audio;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class PrologueCutsceneAnimations : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private LocalAudioController audioController;

        [Header("Settings")]
        [SerializeField] private AudioClip radioTurnOffSFX;
        [SerializeField] [Range(0.1f, 1f)] private float radioTurnOffVolumeScale = 0.6f;

        [Space(6)]

        [SerializeField] private AudioClip radioTurningSFX;
        [SerializeField] [Range(0.1f, 1f)] private float radioTurningVolumeScale = 0.6f;

        [Space(6)]

        [SerializeField] private AudioClip centralRadioSFX;
        [SerializeField] [Range(0.1f, 1f)] private float centralRadioVolumeScale = 0.6f;

        [Space(12)]

        [SerializeField] private Transform cubemanCarGraphicsTransform;

        [Space(6)]

        [SerializeField] private float carTranslocationSpeed = 2f;
        [SerializeField] private float carLocalZFinalDestination = -18f;

        public void StopRadioMusicEvent()
        {
            audioController.StopSoundTrack();
            audioController.PlaySoundEffect(radioTurnOffSFX, radioTurnOffVolumeScale);
        }

        public void RadioTuningEvent()
        {
            audioController.PlaySoundEffect(radioTurningSFX, radioTurningVolumeScale);
        }

        public void ContactWithCentralEvent()
        {
            audioController.PlaySoundEffect(centralRadioSFX, centralRadioVolumeScale);
        }

        public void GoingToCubecityEvent()
        {
            cubemanCarGraphicsTransform.DOLocalMoveZ(carLocalZFinalDestination, carTranslocationSpeed)
                .SetEase(Ease.InSine);
        }
    }
}
