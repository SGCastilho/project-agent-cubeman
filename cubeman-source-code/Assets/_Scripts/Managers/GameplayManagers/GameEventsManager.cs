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
        [SerializeField] private AudioController audioManager;
        [SerializeField] private DialogueManager dialogueManager;
        [SerializeField] private GamePauseManager gamePauseManager;
        [SerializeField] private GameStateManager gameStateManager;
        [SerializeField] private SceneLoaderManager sceneLoaderManager;
        [SerializeField] private ObjectPoolingManager poolingManager;

        [Space(12)]

        [SerializeField] private BossStatus bossStatus;

        [Space(12)]

        [SerializeField] private UIFade uiFade;
        [SerializeField] private UIBossHUD uiBossHUD;
        [SerializeField] private UIDialogue uiDialogue;
        [SerializeField] private UIPauseMenu uiPauseMenu;
        [SerializeField] private UIPlayerHUD uiPlayerHUD;
        [SerializeField] private UIStartLevel uiStartLevel;
        [SerializeField] private UIStartLevel uiEndLevel;

        [Space(12)]

        [SerializeField] private PauseMenuButtons pauseMenuButtons;

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
            EnableSceneLoaderEvents();

            EnableGameStateEvents();

            EnablePlayerEvents();

            EnablePauseMenuEvents();

            EnableBossUIEvents();

            EnableDialogueEvents();

            EnableMultiplusEvents();
        }

        private void EnableDialogueEvents()
        {
            dialogueManager.OnDialogueReady += uiDialogue.FadeIn;
            dialogueManager.OnDialogueLoad += uiDialogue.SetDialogue;
            dialogueManager.OnDialogueEnd += uiDialogue.FadeOut;
        }

        private void EnableBossUIEvents()
        {
            if (bossStatus != null && uiBossHUD != null)
            {
                bossStatus.OnDamageHealth += uiBossHUD.DamageHealthBar;
                bossStatus.OnRecoveryHealth += uiBossHUD.RecoveryHealthBar;
            }
        }

        private void EnablePauseMenuEvents()
        {
            gamePauseManager.OnPauseGame += uiPauseMenu.FadeIn;
            gamePauseManager.OnUnPauseGame += uiPauseMenu.FadeOut;

            pauseMenuButtons.OnResume += gamePauseManager.UnPause;
            pauseMenuButtons.OnQuit += sceneLoaderManager.LoadScene;

            uiPauseMenu.OnPauseEnd += _player.Input.PauseEnd;
            uiPauseMenu.OnUnPauseEnd += _player.Input.UnPauseEnd;
        }

        private void EnablePlayerEvents()
        {
            _player.Status.OnPlayerRecovery += uiPlayerHUD.RecoveryHealthBar;
            _player.Status.OnPlayerTakeDamage += uiPlayerHUD.DamageHealthBar;

            _player.Status.OnPlayerUltimateProgress += uiPlayerHUD.UltimateBar;
            _player.Status.OnPlayerUltimateReset += uiPlayerHUD.UltimateBarReset;

            _player.Status.OnPlayerDeathStart += gamePauseManager.BlockPauseEvent;
            _player.Status.OnPlayerDeath += audioManager.PlaySoundEffect;
            _player.Status.OnPlayerDeathComplete += gameStateManager.GameLose;

            _player.Input.SubscribePauseInput(gamePauseManager.Pause);
            _player.Input.SubscribeUnPauseInput(gamePauseManager.UnPause);

            resourceData.OnAddResources += uiPlayerHUD.UpdateResources;
        }

        private void EnableGameStateEvents()
        {
            gameStateManager.OnCompleteStageMessage += uiEndLevel.FadeIn;
            gameStateManager.OnWin += sceneLoaderManager.LoadScene;
            gameStateManager.OnLose += sceneLoaderManager.LoadActiveScene;

            uiStartLevel.OnMessageEnd += gameStateManager.GameStart;
        }

        private void EnableSceneLoaderEvents()
        {
            sceneLoaderManager.OnStartLoadScene += uiFade.LoadingFadeIn;
        }

        private void EnableMultiplusEvents()
        {
            if(_airShooters != null && _airShooters.Length > 0)
            {
                for(int i = 0; i < _airShooters.Length; i++)
                {
                    _airShooters[i].Attack.OnShoot += poolingManager.SpawnPrefabNoReturn;
                }
            }
        }

        private void DisableEvents()
        {
            DisableSceneLoaderEvents();

            DisableGameStateEvents();

            DisablePlayerEvents();

            DisablePauseManagerEvents();

            DisableBossUIEvents();

            DisableDialogueEvents();

            DisableMultiplusEvents();
        }

        private void DisableDialogueEvents()
        {
            dialogueManager.OnDialogueReady -= uiDialogue.FadeIn;
            dialogueManager.OnDialogueLoad -= uiDialogue.SetDialogue;
            dialogueManager.OnDialogueEnd -= uiDialogue.FadeOut;
        }

        private void DisableBossUIEvents()
        {
            if (bossStatus != null && uiBossHUD != null)
            {
                bossStatus.OnDamageHealth -= uiBossHUD.DamageHealthBar;
                bossStatus.OnRecoveryHealth -= uiBossHUD.RecoveryHealthBar;
            }
        }

        private void DisablePauseManagerEvents()
        {
            gamePauseManager.OnPauseGame -= uiPauseMenu.FadeIn;
            gamePauseManager.OnUnPauseGame -= uiPauseMenu.FadeOut;

            pauseMenuButtons.OnResume -= gamePauseManager.UnPause;
            pauseMenuButtons.OnQuit -= sceneLoaderManager.LoadScene;

            uiPauseMenu.OnPauseEnd -= _player.Input.PauseEnd;
            uiPauseMenu.OnUnPauseEnd -= _player.Input.UnPauseEnd;
        }

        private void DisablePlayerEvents()
        {
            _player.Status.OnPlayerRecovery -= uiPlayerHUD.RecoveryHealthBar;
            _player.Status.OnPlayerTakeDamage -= uiPlayerHUD.DamageHealthBar;

            _player.Status.OnPlayerUltimateProgress -= uiPlayerHUD.UltimateBar;
            _player.Status.OnPlayerUltimateReset -= uiPlayerHUD.UltimateBarReset;

            _player.Status.OnPlayerDeathStart -= gamePauseManager.BlockPauseEvent;
            _player.Status.OnPlayerDeath -= audioManager.PlaySoundEffect;
            _player.Status.OnPlayerDeathComplete -= gameStateManager.GameLose;

            resourceData.OnAddResources -= uiPlayerHUD.UpdateResources;

            _player.Input.UnSubscribePauseInput();
            _player.Input.UnSubscribeUnPauseInput();
        }

        private void DisableGameStateEvents()
        {
            gameStateManager.OnCompleteStageMessage -= uiEndLevel.FadeIn;
            gameStateManager.OnWin -= sceneLoaderManager.LoadScene;
            gameStateManager.OnLose -= sceneLoaderManager.LoadActiveScene;

            uiStartLevel.OnMessageEnd -= gameStateManager.GameStart;
        }

        private void DisableSceneLoaderEvents()
        {
            sceneLoaderManager.OnStartLoadScene -= uiFade.LoadingFadeIn;
        }

        private void DisableMultiplusEvents()
        {
            if (_airShooters != null && _airShooters.Length > 0)
            {
                for (int i = 0; i < _airShooters.Length; i++)
                {
                    _airShooters[i].Attack.OnShoot -= poolingManager.SpawnPrefabNoReturn;
                }
            }
        }
    }
}
