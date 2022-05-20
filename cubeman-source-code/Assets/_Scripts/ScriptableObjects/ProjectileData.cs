using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Projectile Data", menuName = "Scriptable Object/Projectile Data")]
    public sealed class ProjectileData : ScriptableObject
    {
        #region Encapsulation
        public string Key { get => projectileKey; }
        public GameObject Prefab { get => projectilePrefab; }
        public int Instances { get => projectileInstances; }

        public bool DisableOnDamage { get => disableOnDamage; }

        public AudioClip ProjectileSFX { get => projectileAudioClip; }
        public float VolumeScale { get => projectileClipVolumeScale; }

        public int Damage { get => projectileDamage; }
        public int Level { get => projectileLevel; }
        public float DamageScaling { get => projectileDamageScaling; }

        public float Velocity { get => projectileVelocity; }
        public float Range { get => projectileRange; }
        #endregion

        [Header("Settings")]
        [Tooltip("The key used in Object Pooling.")]
        [SerializeField] private string projectileKey = "projectile_key";
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private int projectileInstances = 20;

        [Space(12)]

        [SerializeField] private bool disableOnDamage;

        [Space(12)]

        [SerializeField] private AudioClip projectileAudioClip;
        [SerializeField] [Range(0.1f, 1f)] private float projectileClipVolumeScale = 1f;

        [Space(12)]

        [SerializeField] private int projectileDamage = 2;
        [SerializeField] private int initProjectileDamage = 2;
        [SerializeField] [Range(1, 5)] private int projectileLevel = 1;
        [Tooltip("Determines the scaling when upgrade weapon.")]
        [SerializeField] [Range(0.1f, 1f)] private float projectileDamageScaling = 0.6f;

        [Space(12)]

        [SerializeField] [Range(1f, 20f)] private float projectileVelocity = 4f;
        [SerializeField] 
        [Tooltip("Determines how much time the projectile will stay active.")] 
        [Range(1f, 12f)] private float projectileRange = 6f;

        #region Editor Variable
#if UNITY_EDITOR
        [Space(12)]

        [SerializeField] [Multiline(4)] private string devNotes = "Put your dev notes here.";
#endif
        #endregion

        private void OnEnable() => ResetData();

        public void UpgradeDamage()
        {
            if(projectileLevel <= 4) 
            {
                float currentDamage = projectileDamage;
                float incrementDamage = currentDamage * projectileDamageScaling;
                float newDamage = currentDamage + incrementDamage;

                projectileDamage = (int)newDamage;
                projectileLevel++;
            }
        }

        private void ResetData() 
        {
            projectileDamage = initProjectileDamage;
            projectileLevel = 1;
        }
    }
}
