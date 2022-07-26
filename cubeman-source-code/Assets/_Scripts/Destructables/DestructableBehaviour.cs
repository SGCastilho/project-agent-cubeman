using Cubeman.Audio;
using Cubeman.Damagable;
using UnityEngine;

namespace Cubeman.Destructable
{
    public sealed class DestructableBehaviour : MonoBehaviour
    {
        #region Encapsulation
        internal LocalAudioController AudioManager { get => localAudioManager; }
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
            destructableObject.OnDestructObject += collectableSpawn.SpawnCollectable;
        }

        private void ResetPosistion()
        {
            transform.position = _startPosistion;
        }

        private void DisableEvents()
        {
            destructableObject.OnDestructObject -= collectableSpawn.SpawnCollectable;
        }
    }
}
