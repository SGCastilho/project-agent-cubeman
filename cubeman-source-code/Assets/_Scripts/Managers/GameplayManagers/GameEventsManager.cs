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
            sceneLoaderManager.OnStartLoadScene += uiFade.LoadingFadeIn;

            gameStateManager.OnCompleteStageMessage += uiEndLevel.FadeIn;
            gameStateManager.OnWin += sceneLoaderManager.LoadScene;
            gameStateManager.OnLose += sceneLoaderManager.LoadActiveScene;

            uiStartLevel.OnMessageEnd += gameStateManager.GameStart;

            _player.Status.OnPlayerRecovery += uiPlayerHUD.RecoveryHealthBar;
            _player.Status.OnPlayerTakeDamage += uiPlayerHUD.DamageHealthBar;

            _player.Status.OnPlayerUltimateProgress += uiPlayerHUD.UltimateBar;
            _player.Status.OnPlayerUltimateReset += uiPlayerHUD.UltimateBarReset;

            _player.Status.OnPlayerDeathStart += gamePauseManager.BlockPauseEvent;
            _player.Status.OnPlayerDeath += audioManager.PlaySoundEffect;
            _player.Status.OnPlayerDeathComplete += gameStateManager.GameLose;

            _player.Input.SubscribePauseInput(gamePauseManager.Pause);
            _player.Input.SubscribeUnPauseInput(gamePauseManager.UnPause);

            gamePauseManager.OnPauseGame += uiPauseMenu.FadeIn;
            gamePauseManager.OnUnPauseGame += uiPauseMenu.FadeOut;

            pauseMenuButtons.OnResume += gamePauseManager.UnPause;
            pauseMenuButtons.OnQuit += sceneLoaderManager.LoadScene;
            
            uiPauseMenu.OnPauseEnd += _player.Input.PauseEnd;
            uiPauseMenu.OnUnPauseEnd += _player.Input.UnPauseEnd;

            if(bossStatus != null && uiBossHUD != null)
            {
                bossStatus.OnDamageHealth += uiBossHUD.DamageHealthBar;
                bossStatus.OnRecoveryHealth += uiBossHUD.RecoveryHealthBar;
            }

            resourceData.OnAddResources += uiPlayerHUD.UpdateResources;

            dialogueManager.OnDialogueReady += uiDialogue.FadeIn;
            dialogueManager.OnDialogueLoad += uiDialogue.SetDialogue;
            dialogueManager.OnDialogueEnd += uiDialogue.FadeOut;

            EnableMultiplusEvents();
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

        private void DisableMultiplusEvents()
        {
            if(_airShooters != null && _airShooters.Length > 0)
            {
                for(int i = 0; i < _airShooters.Length; i++)
                {
                    _airShooters[i].Attack.OnShoot -= poolingManager.SpawnPrefabNoReturn;
                }
            }
        }

        private void DisableEvents()
        {
            sceneLoaderManager.OnStartLoadScene -= uiFade.LoadingFadeIn;

            gameStateManager.OnCompleteStageMessage -= uiEndLevel.FadeIn;
            gameStateManager.OnWin -= sceneLoaderManager.LoadScene;
            gameStateManager.OnLose -= sceneLoaderManager.LoadActiveScene;

            uiStartLevel.OnMessageEnd -= gameStateManager.GameStart;

            _player.Status.OnPlayerRecovery -= uiPlayerHUD.RecoveryHealthBar;
            _player.Status.OnPlayerTakeDamage -= uiPlayerHUD.DamageHealthBar;

            _player.Status.OnPlayerUltimateProgress -= uiPlayerHUD.UltimateBar;
            _player.Status.OnPlayerUltimateReset -= uiPlayerHUD.UltimateBarReset;

            _player.Status.OnPlayerDeathStart -= gamePauseManager.BlockPauseEvent;
            _player.Status.OnPlayerDeath -= audioManager.PlaySoundEffect;
            _player.Status.OnPlayerDeathComplete -= gameStateManager.GameLose;

            _player.Input.UnSubscribePauseInput();
            _player.Input.UnSubscribeUnPauseInput();

            gamePauseManager.OnPauseGame -= uiPauseMenu.FadeIn;
            gamePauseManager.OnUnPauseGame -= uiPauseMenu.FadeOut;

            pauseMenuButtons.OnResume -= gamePauseManager.UnPause;
            pauseMenuButtons.OnQuit -= sceneLoaderManager.LoadScene;

            uiPauseMenu.OnPauseEnd -= _player.Input.PauseEnd;
            uiPauseMenu.OnUnPauseEnd -= _player.Input.UnPauseEnd;

            if (bossStatus != null && uiBossHUD != null)
            {
                bossStatus.OnDamageHealth -= uiBossHUD.DamageHealthBar;
                bossStatus.OnRecoveryHealth -= uiBossHUD.RecoveryHealthBar;
            }

            resourceData.OnAddResources -= uiPlayerHUD.UpdateResources;

            dialogueManager.OnDialogueReady -= uiDialogue.FadeIn;
            dialogueManager.OnDialogueLoad -= uiDialogue.SetDialogue;
            dialogueManager.OnDialogueEnd -= uiDialogue.FadeOut;

            DisableMultiplusEvents();
        }
    }
}
