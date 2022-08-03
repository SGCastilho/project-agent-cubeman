using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Projectile Data", menuName = "Scriptable Object/Projectile Data")]
    public sealed class ProjectileData : ScriptableObject
    {
        #region Encapsulation
        public string Key { get => projectileKey; }
        public string Name { get => projectileName; }
        public GameObject Prefab { get => projectilePrefab; }
        public int Instances { get => projectileInstances; }

        public bool DisableOnDamage { get => disableOnDamage; }

        public AudioClip ProjectileSFX { get => projectileAudioClip; }
        public float VolumeScale { get => projectileClipVolumeScale; }

        public Sprite UpgradePreview { get => upgradePreview; }

        public int Damage { get => projectileDamage; }
        public int Level { get => projectileLevel; }
        public int LevelMax { get => projectileMaxLevel; }
        public int DamageScaling { get => projectileFixedUpgrade; }

        public int[] AmountToResourcesToUpgrade { get => amountToResourcesToUpgrade; }
        public int[] AmountCapacitorsToUpgrade { get => amountCapacitorsToUpgrade; }

        public float Velocity { get => projectileVelocity; }
        public float Range { get => projectileRange; }
        #endregion

        [Header("Settings")]
        [Tooltip("The key used in Object Pooling.")]
        [SerializeField] private string projectileKey = "projectile_key";
        [SerializeField] private string projectileName = "Projectile Name";

        [Space(6)]

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

        [Header("Upgrade Details")]

        [SerializeField] private Sprite upgradePreview;

        [Space(6)]

        [SerializeField] [Range(1, 5)] private int projectileLevel = 1;
        [SerializeField] private int projectileMaxLevel = 5;
        [SerializeField] private int projectileFixedUpgrade;

        [Space(6)]

        [SerializeField] private int[] amountToResourcesToUpgrade;
        [SerializeField] private int[] amountCapacitorsToUpgrade;

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
                int currentDamage = projectileDamage;
                int newDamage = currentDamage + projectileFixedUpgrade;

                projectileDamage = newDamage;
                projectileLevel++;
            }
        }

        public int GetNextUpgrade()
        {
            var nextUpgrade = 0;

            int currentDamage = projectileDamage;
            int newDamage = currentDamage + projectileFixedUpgrade;

            nextUpgrade = newDamage;

            return nextUpgrade;
        }

        private void ResetData() 
        {
            projectileDamage = initProjectileDamage;
            projectileLevel = 1;
        }
    }
}
