using Cubeman.Player;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class UpgradeZoneManager : MonoBehaviour
    {
        public delegate void ShowUpgradeWindow();
        public event ShowUpgradeWindow OnShowUpgradeWindow;

        public delegate void SendPlayerDataInformation(PlayerData playerData, ResourcesData resourcesData);
        public event SendPlayerDataInformation OnSendPlayerDataInformation;

        public delegate void SendProjectileDataInformation(ProjectileData projectileData, ResourcesData resourcesData);
        public event SendProjectileDataInformation OnSendProjectileDataInformation;

        public delegate void HideUpgradeWindow();
        public event HideUpgradeWindow OnHideUpgradeWindow;

        [Header("Upgrade Datas")]
        [SerializeField] private ResourcesData resourcesData;

        [Space(6)]

        [SerializeField] private PlayerData defaultPlayerData;

        [Space(6)]

        [SerializeField] private ProjectileData defaultProjectileData;
        [SerializeField] private ProjectileData defaultUltimateProjectileData;

        [Header("Settings")]
        [SerializeField] private int maxUpgradeToSelect = 2;

        private PlayerBehaviour _playerBehaviour;

        private int _currentUpgradeSelected;

        private void Awake() => SetupManager();

        private void SetupManager()
        {
            RefreshUI();
            CacheComponents();
        }

        private void RefreshUI()
        {
            switch (_currentUpgradeSelected)
            {
                case 0:
                    OnSendProjectileDataInformation?.Invoke(defaultProjectileData, resourcesData);
                    break;
                case 1:
                    OnSendProjectileDataInformation?.Invoke(defaultUltimateProjectileData, resourcesData);
                    break;
                case 2:
                    OnSendPlayerDataInformation?.Invoke(defaultPlayerData, resourcesData);
                    break;
            }
        }

        private void CacheComponents()
        {
            _playerBehaviour = PlayerBehaviour.Instance;
        }

        public void OpenUpgradeZone()
        {
            _playerBehaviour.Input.SubscribeCancelInput(CloseUpgradeZone);

            _playerBehaviour.Input.UIInputs(true);
            _playerBehaviour.Input.GameplayInputs(false);

            _playerBehaviour.CursorState(false);

            OnShowUpgradeWindow?.Invoke();
        }

        public void CloseUpgradeZone()
        {
            OnHideUpgradeWindow?.Invoke();

            _playerBehaviour.Input.UnSubscribeCancelInput();

            _playerBehaviour.Input.UIInputs(false);
            _playerBehaviour.Input.GameplayInputs(true);

            _playerBehaviour.CursorState(true);
        }

        public void NextUpgrade()
        {
            if (_currentUpgradeSelected >= maxUpgradeToSelect)
            {
                _currentUpgradeSelected = 0;
            }
            else
            {
                _currentUpgradeSelected++;
            }

            RefreshUI();
        }

        public void DoUpgrade()
        {
            switch (_currentUpgradeSelected)
            {
                case 0:
                    DefaultProjectileUpgrade();
                    break;
                case 1:
                    DefaultUltimateProjectileUpgrade();
                    break;
                case 2:
                    PlayerHealthUpgrade();
                    break;
            }

            RefreshUI();
        }

        private void PlayerHealthUpgrade()
        {
            var convertLevelToIndex = defaultPlayerData.Level - 1;

            var resourcesToUse = defaultPlayerData.AmountToResourcesToUpgrade[convertLevelToIndex];
            var capacitorsToUse = defaultPlayerData.AmountCapacitorsToUpgrade[convertLevelToIndex];

            resourcesData.RemoveResources(resourcesToUse);
            resourcesData.RemoveCapacitors(capacitorsToUse);

            defaultPlayerData.UpgradeHealth();
        }

        private void DefaultProjectileUpgrade()
        {
            var convertLevelToIndex = defaultProjectileData.Level - 1;

            var resourcesToUse = defaultProjectileData.AmountToResourcesToUpgrade[convertLevelToIndex];
            var capacitorsToUse = defaultProjectileData.AmountCapacitorsToUpgrade[convertLevelToIndex];

            resourcesData.RemoveResources(resourcesToUse);
            resourcesData.RemoveCapacitors(capacitorsToUse);

            defaultProjectileData.UpgradeDamage();
        }

        private void DefaultUltimateProjectileUpgrade()
        {
            var convertLevelToIndex = defaultUltimateProjectileData.Level - 1;

            var resourcesToUse = defaultUltimateProjectileData.AmountToResourcesToUpgrade[convertLevelToIndex];
            var capacitorsToUse = defaultUltimateProjectileData.AmountCapacitorsToUpgrade[convertLevelToIndex];

            resourcesData.RemoveResources(resourcesToUse);
            resourcesData.RemoveCapacitors(capacitorsToUse);

            defaultUltimateProjectileData.UpgradeDamage();
        }

        public void PreviousUpgrade()
        {
            if (_currentUpgradeSelected <= 0)
            {
                _currentUpgradeSelected = maxUpgradeToSelect;
            }
            else
            {
                _currentUpgradeSelected--;
            }

            RefreshUI();
        }
    }
}
