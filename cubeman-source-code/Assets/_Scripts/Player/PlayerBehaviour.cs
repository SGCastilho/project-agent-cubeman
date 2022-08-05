using Cubeman.Audio;
using UnityEngine;

namespace Cubeman.Player
{
    public sealed class PlayerBehaviour : MonoBehaviour
    {
        #region Encapsulation
        public static PlayerBehaviour Instance { get; private set; }

        public PlayerStatus Status { get => status; }
        public PlayerInput Input { get => input; }
        public PlayerMoviment Moviment { get => moviment; }
        public PlayerShoot Shoot { get => shoot; }
        public PlayerVisualEffects VisualEffects { get => visualEffects; }
        public AudioSoundEffects SoundEffect { get => soundEffects; }
        public Transform Transform { get => _transform; }
        
        internal PlayerAnimation Animation { get => anim; }
        #endregion

        [Header("Classes")]
        [SerializeField] private PlayerStatus status;
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerMoviment moviment;
        [SerializeField] private PlayerShoot shoot;
        [SerializeField] private PlayerAnimation anim;
        [SerializeField] private PlayerVisualEffects visualEffects;

        [Space(12)]

        [SerializeField] private AudioSoundEffects soundEffects;

        [Header("Settings")]
        [SerializeField] private bool safeZoneMode;

        private Transform _transform;

        private void Awake() => SetupBehaviour();
        
        private void SetupBehaviour()
        {
            Instance = this;

            _transform = transform;

            if(safeZoneMode)
            {
                status.UltimateReady = true;
                shoot.BlockShooting = true;
            }
        }

        private void Start() => CursorState(true);

        public void CursorState(bool lockCursor)
        {
            if(lockCursor)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public void BlockAction(bool enable)
        {
            moviment.BlockMoviment(enable);
            moviment.Gravity.BlockJump = enable;

            shoot.BlockShooting = enable;
        }

        public void ResetData()
        {
            status.Data.ResetData();

            shoot.ProjectileData.ResetData();
            shoot.UltimateData.ResetData();
        }
    }
}
