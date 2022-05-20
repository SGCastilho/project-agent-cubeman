using Cubeman.Utilities;
using Cubeman.Character;
using UnityEngine;

namespace Cubeman.Enemies
{
    [RequireComponent(typeof(CharacterGravity))]
    public sealed class EnemyCharacterSpinRobotMoviment : MonoBehaviour
    {
        #region Encapsulation
        public bool MoveRight 
        {
            get => _moveRight;
            set 
            {
                _moveRight = value;
                rotateObject.RotateRight = value;
            }
        }
        #endregion

        [Header("Classes")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private CharacterGravity characterGravity;
        [SerializeField] private RotateObject rotateObject;

        [Header("Settings")]
        [SerializeField] [Range(2f, 20f)] private float movimentSpeed = 6f;
        [SerializeField] private bool startMoveRight;

        private bool _moveRight;

        private Vector3 _startPosistion;

        private Vector2 _xVelocity;
        private Vector2 _finalVelocity;

        private void Awake()
        {
            _startPosistion = transform.localPosition;
        }

        private void OnEnable() => SetupObject();

        private void SetupObject()
        {
            MoveRight = startMoveRight;

            characterGravity.FreezeGravity = false;
        }

        private void OnDisable() => ResetObject();

        private void ResetObject()
        {
            characterGravity.FreezeGravity = true;

            _finalVelocity = Vector2.zero;
            _xVelocity = Vector2.zero;

            transform.localPosition = _startPosistion;
        }

        private void Update() => Moviment();

        private void Moviment()
        {
            MovimentSide();
            characterGravity.Gravity();

            _finalVelocity = _xVelocity + characterGravity.YVelocity;

            characterController.Move(_finalVelocity * Time.deltaTime);
        }

        private void MovimentSide()
        {
            if (_moveRight)
            {
                _xVelocity = movimentSpeed * Vector2.right;
            }
            else
            {
                _xVelocity = movimentSpeed * Vector2.left;
            }
        }
    }
}
