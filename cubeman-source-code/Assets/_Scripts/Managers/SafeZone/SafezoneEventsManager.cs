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

        [Space(12)]

        [SerializeField] private UIFade uiFade;
        [SerializeField] private UIDialogue uiDialogue;
        [SerializeField] private UIPauseMenu uiPauseMenu;

        [Space(12)]

        [SerializeField] private PauseMenuButtons pauseMenuButtons;

        private PlayerBehaviour _player;

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
        }

        private void DisableEvents()
        {
            DisableSceneLoaderEvents();

            DisablePlayerEvents();

            DisablePauseMenuEvents();

            DisableDialogueEvents();
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
    }
}
