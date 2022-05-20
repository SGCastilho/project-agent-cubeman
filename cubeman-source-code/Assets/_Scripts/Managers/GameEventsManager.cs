using Cubeman.UI;
using Cubeman.Audio;
using Cubeman.Player;
using Cubeman.Enemies;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class GameEventsManager : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private ResourcesData resourceData;

        [Header("Classes")]
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private GameStateManager gameStateManager;
        [SerializeField] private SceneLoaderManager sceneLoaderManager;
        [SerializeField] private ObjectPoolingManager poolingManager;
        [Space(12)]
        [SerializeField] private UIFade uiFade;
        [SerializeField] private UIStartLevel uiStartLevel;
        [SerializeField] private UIPlayerHUD uiPlayerHUD;

        private PlayerBehaviour _player;

        private EnemyAirShooterBehaviour[] _airShooters;

        private void Awake() => CacheComponets();

        private void OnEnable() => EnableEvents();

        private void OnDestroy() => DisableEvents();

        private void CacheComponets()
        {
            _player = FindObjectOfType<PlayerBehaviour>();

            _airShooters = FindObjectsOfType<EnemyAirShooterBehaviour>();
        }

        private void EnableEvents()
        {
            sceneLoaderManager.OnStartLoadScene += uiFade.FadeIn;

            gameStateManager.OnLose += sceneLoaderManager.LoadActiveScene;

            uiStartLevel.OnMessageEnd += gameStateManager.GameStart;

            _player.Status.OnPlayerRecovery += uiPlayerHUD.RecoveryHealthBar;
            _player.Status.OnPlayerTakeDamage += uiPlayerHUD.DamageHealthBar;
            _player.Status.OnPlayerUltimateProgress += uiPlayerHUD.UltimateBar;
            _player.Status.OnPlayerUltimateReset += uiPlayerHUD.UltimateBarReset;
            _player.Status.OnPlayerDeath += audioManager.PlaySoundEffect;
            _player.Status.OnPlayerDeathComplete += gameStateManager.GameLose;

            resourceData.OnAddResources += uiPlayerHUD.UpdateResources;

            EnableMultiplusEvents();
        }

        private void EnableMultiplusEvents()
        {
            for(int i = 0; i < _airShooters.Length; i++)
            {
                _airShooters[i].Attack.OnShoot += poolingManager.SpawnPrefabNoReturn;
            }
        }

        private void DisableMultiplusEvents()
        {
            for (int i = 0; i < _airShooters.Length; i++)
            {
                _airShooters[i].Attack.OnShoot -= poolingManager.SpawnPrefabNoReturn;
            }
        }

        private void DisableEvents()
        {
            sceneLoaderManager.OnStartLoadScene -= uiFade.FadeIn;

            gameStateManager.OnLose -= sceneLoaderManager.LoadActiveScene;

            uiStartLevel.OnMessageEnd -= gameStateManager.GameStart;

            _player.Status.OnPlayerRecovery -= uiPlayerHUD.RecoveryHealthBar;
            _player.Status.OnPlayerTakeDamage -= uiPlayerHUD.DamageHealthBar;
            _player.Status.OnPlayerUltimateProgress -= uiPlayerHUD.UltimateBar;
            _player.Status.OnPlayerUltimateReset -= uiPlayerHUD.UltimateBarReset;
            _player.Status.OnPlayerDeath -= audioManager.PlaySoundEffect;
            _player.Status.OnPlayerDeathComplete -= gameStateManager.GameLose;

            resourceData.OnAddResources -= uiPlayerHUD.UpdateResources;

            DisableMultiplusEvents();
        }
    }
}
