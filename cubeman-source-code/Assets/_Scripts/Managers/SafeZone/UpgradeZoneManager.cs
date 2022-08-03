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

        [SerializeField] private ProjectileData defaultProjectile;
        [SerializeField] private ProjectileData defaultUltimateProjectile;

        private PlayerBehaviour _playerBehaviour;

        private void Awake() => SetupManager();

        private void SetupManager()
        {
            CacheComponents();
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
    }
}
