using Cubeman.UI;
using Cubeman.Audio;
using Cubeman.Player;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class SafezoneEventsManager : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private ResourcesData resourceData;

        [Header("Classes")]
        [SerializeField] private AudioController audioManager;
        [SerializeField] private DialogueManager dialogueManager;
        [SerializeField] private GamePauseManager gamePauseManager;
        [SerializeField] private SceneLoaderManager sceneLoaderManager;
        [SerializeField] private UpgradeZoneManager upgradeZoneManager;
        [SerializeField] private LevelSelectorManager levelSelectorManager;
        [SerializeField] private SafezoneInteractionsManager safeZoneInteractionManager;
        [SerializeField] private SafezoneScenarioTriggersManager safezoneScenarioTriggersManager;

        [Space(12)]

        [SerializeField] private UIFade uiFade;
        [SerializeField] private UIDialogue uiDialogue;
        [SerializeField] private UIPauseMenu uiPauseMenu;
        [SerializeField] private UIUpgradeZone uiUpgradeZone;
        [SerializeField] private UILevelSelector uiLevelSelector;
        [SerializeField] private UISafezoneInteractions uiSafezoneInteractions;

        [Space(12)]

        [SerializeField] private PauseMenuButtons pauseMenuButtons;

        private PlayerBehaviour _player;

        private UIButtonLevelSelect[] _buttonsLevelSelect;

        private void Awake() => CacheComponets();

        private void OnEnable() => EnableEvents();

        private void OnDestroy() => DisableEvents();

        private void CacheComponets()
        {
            _player = FindObjectOfType<PlayerBehaviour>();
        }

        private void EnableEvents()
        {
            EnableSceneLoaderEvents();

            EnablePlayerEvents();

            EnablePauseMenuEvents();

            EnableDialogueEvents();

            EnableLevelSelectorEvents();

            EnableUpgradeZoneEvents();

            EnableSafezoneInteractions();
        }

        private void DisableEvents()
        {
            DisableSceneLoaderEvents();

            DisablePlayerEvents();

            DisablePauseMenuEvents();

            DisableDialogueEvents();

            DisableLevelSelectorEvents();

            DisableUpgradeZoneEvents();

            DisableSafezoneInteractions();
        }

        private void EnableSceneLoaderEvents()
        {
            sceneLoaderManager.OnStartLoadScene += uiFade.LoadingFadeIn;
        }

        private void EnablePlayerEvents()
        {
            _player.Status.OnPlayerDeathStart += gamePauseManager.BlockPauseEvent;
            _player.Status.OnPlayerDeath += audioManager.PlaySoundEffect;

            _player.Input.SubscribePauseInput(gamePauseManager.Pause);
            _player.Input.SubscribeUnPauseInput(gamePauseManager.UnPause);
        }

        private void EnablePauseMenuEvents()
        {
            gamePauseManager.OnPauseGame += uiPauseMenu.FadeIn;
            gamePauseManager.OnUnPauseGame += uiPauseMenu.FadeOut;

            pauseMenuButtons.OnResume += gamePauseManager.UnPause;
            pauseMenuButtons.OnStartQuit += _player.ResetData;
            pauseMenuButtons.OnStartQuit += safezoneScenarioTriggersManager.DestroySafezoneState;
            pauseMenuButtons.OnQuit += sceneLoaderManager.LoadScene;

            uiPauseMenu.OnPauseEnd += _player.Input.PauseEnd;
            uiPauseMenu.OnUnPauseEnd += _player.Input.UnPauseEnd;
        }

        private void EnableDialogueEvents()
        {
            dialogueManager.OnDialogueReady += uiDialogue.FadeIn;
            dialogueManager.OnDialogueLoad += uiDialogue.SetDialogue;
            dialogueManager.OnDialogueComplete += uiDialogue.FadeOut;
        }

        private void EnableLevelSelectorEvents()
        {
            levelSelectorManager.OnShowLevelSelector += uiLevelSelector.FadeIn;
            levelSelectorManager.OnHideLevelSelector += uiLevelSelector.FadeOut;

            levelSelectorManager.OnCreateStages += uiLevelSelector.CreateStages;

            uiLevelSelector.OnSetupButtonsAction += EnableButtonLevelSelectEvents;
        }

        private void EnableUpgradeZoneEvents()
        {
            upgradeZoneManager.OnShowUpgradeWindow += uiUpgradeZone.FadeIn;
            upgradeZoneManager.OnHideUpgradeWindow += uiUpgradeZone.FadeOut;

            upgradeZoneManager.OnSendPlayerDataInformation += uiUpgradeZone.RefreshUpgrade;
            upgradeZoneManager.OnSendProjectileDataInformation += uiUpgradeZone.RefreshUpgrade;
        }

        private void EnableSafezoneInteractions()
        {
            safeZoneInteractionManager.OnShowInteractionsWindow += uiSafezoneInteractions.ShowInteractions;
            safeZoneInteractionManager.OnHideInteractionWindow += uiSafezoneInteractions.HideInteractions;

            uiSafezoneInteractions.OnSwitchingInputs += _player.SwitchInputs;
        }

        private void DisableSceneLoaderEvents()
        {
            sceneLoaderManager.OnStartLoadScene -= uiFade.LoadingFadeIn;
        }

        private void DisablePlayerEvents()
        {
            _player.Status.OnPlayerDeathStart -= gamePauseManager.BlockPauseEvent;
            _player.Status.OnPlayerDeath -= audioManager.PlaySoundEffect;

            _player.Input.UnSubscribePauseInput();
            _player.Input.UnSubscribeUnPauseInput();
        }

        private void DisablePauseMenuEvents()
        {
            gamePauseManager.OnPauseGame -= uiPauseMenu.FadeIn;
            gamePauseManager.OnUnPauseGame -= uiPauseMenu.FadeOut;

            pauseMenuButtons.OnResume -= gamePauseManager.UnPause;
            pauseMenuButtons.OnStartQuit -= _player.ResetData;
            pauseMenuButtons.OnStartQuit -= safezoneScenarioTriggersManager.DestroySafezoneState;
            pauseMenuButtons.OnQuit -= sceneLoaderManager.LoadScene;

            uiPauseMenu.OnPauseEnd -= _player.Input.PauseEnd;
            uiPauseMenu.OnUnPauseEnd -= _player.Input.UnPauseEnd;
        }

        private void DisableDialogueEvents()
        {
            dialogueManager.OnDialogueReady -= uiDialogue.FadeIn;
            dialogueManager.OnDialogueLoad -= uiDialogue.SetDialogue;
            dialogueManager.OnDialogueComplete -= uiDialogue.FadeOut;
        }

        private void DisableLevelSelectorEvents()
        {
            levelSelectorManager.OnShowLevelSelector -= uiLevelSelector.FadeIn;
            levelSelectorManager.OnHideLevelSelector -= uiLevelSelector.FadeOut;

            levelSelectorManager.OnCreateStages -= uiLevelSelector.CreateStages;

            uiLevelSelector.OnSetupButtonsAction -= EnableButtonLevelSelectEvents;

            DisableButtonLevelSelectEvents();
        }

        private void DisableUpgradeZoneEvents()
        {
            upgradeZoneManager.OnShowUpgradeWindow -= uiUpgradeZone.FadeIn;
            upgradeZoneManager.OnHideUpgradeWindow -= uiUpgradeZone.FadeOut;

            upgradeZoneManager.OnSendPlayerDataInformation -= uiUpgradeZone.RefreshUpgrade;
            upgradeZoneManager.OnSendProjectileDataInformation -= uiUpgradeZone.RefreshUpgrade;
        }

        private void DisableSafezoneInteractions()
        {
            safeZoneInteractionManager.OnShowInteractionsWindow -= uiSafezoneInteractions.ShowInteractions;
            safeZoneInteractionManager.OnHideInteractionWindow -= uiSafezoneInteractions.HideInteractions;

            uiSafezoneInteractions.OnSwitchingInputs -= _player.SwitchInputs;
        }

        public void EnableButtonLevelSelectEvents(UIButtonLevelSelect[] buttons) 
        {
            if (buttons == null) return;

            _buttonsLevelSelect = buttons;

            for(int i = 0; i < _buttonsLevelSelect.Length; i++)
            {
                _buttonsLevelSelect[i].OnLoadSelectedStage += sceneLoaderManager.LoadScene;
            }
        }

        private void DisableButtonLevelSelectEvents()
        {
            if (_buttonsLevelSelect == null || _buttonsLevelSelect.Length <= 0) return;

            for (int i = 0; i < _buttonsLevelSelect.Length; i++)
            {
                _buttonsLevelSelect[i].OnLoadSelectedStage -= sceneLoaderManager.LoadScene;
            }
        }
    }
}
