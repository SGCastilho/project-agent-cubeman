using Cubeman.Audio;
using Cubeman.Damagable;
using UnityEngine;

namespace Cubeman.Destructable
{
    public sealed class DestructableBehaviour : MonoBehaviour
    {
        #region Encapsulation
        internal AudioSoundEffects SoundEffects { get => soundEffects; }
        #endregion

        [Header("Classes")]
        [SerializeField] private AudioSoundEffects soundEffects;
        [SerializeField] private DestructableObject destructableObject;
        [SerializeField] private LocalAudioController localAudioManager;
        [SerializeField] private DamagableCollectableSpawn collectableSpawn;

        private Vector3 _startPosistion;

        private void OnEnable() => EnableEvents();

        private void OnDisable() => ResetPosistion();

        private void OnDestroy() => DisableEvents();

        private void Start() => CachePosistion();

        private void CachePosistion()
        {
            _startPosistion = transform.position;
        }

        private void EnableEvents()
        {
            destructableObject.OnImpactObject += localAudioManager.PlaySoundEffect;

            destructableObject.OnDestructObject += collectableSpawn.SpawnCollectable;
            destructableObject.OnDestructObjectFinish += localAudioManager.PlaySoundEffectInOrder;
        }

        private void ResetPosistion()
        {
            transform.position = _startPosistion;
        }

        private void DisableEvents()
        {
            destructableObject.OnImpactObject -= localAudioManager.PlaySoundEffect;

            destructableObject.OnDestructObject -= collectableSpawn.SpawnCollectable;
            destructableObject.OnDestructObjectFinish -= localAudioManager.PlaySoundEffectInOrder;
        }
    }
}
