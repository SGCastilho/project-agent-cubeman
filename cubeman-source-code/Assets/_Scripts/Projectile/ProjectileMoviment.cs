using UnityEngine;

namespace Cubeman.Projectile
{
    public sealed class ProjectileMoviment : MonoBehaviour
    {
        private delegate void MoveSide();
        private MoveSide MovimentSide;

        #region Encapsulation
        public bool MoveRight { set => moveRight = value; }
        public bool MoveUp { set => moveUp = value; }

        internal float Velocity { set => _velocity = value; }
        #endregion

        [Header("Settings")]
        [SerializeField] private bool horizontalProjectile = true;
        [SerializeField] private bool verticalProjectile;

        [Space(12)]

        [SerializeField] private bool moveRight;
        [SerializeField] private bool moveUp;

        private float _velocity;

        private Transform _transform;

        private void Awake() 
        {
            _transform = GetComponent<Transform>();
            EnableMoviment();
        }

        private void OnDisable() => ResetPosistion();

        private void OnDestroy() => DisableMoviment();

        private void EnableMoviment()
        {
            if(horizontalProjectile)
            {
                MovimentSide += HorizontalMoviment;
            }
            else if(verticalProjectile)
            {
                MovimentSide += VerticalMoviment;
            }
        }

        private void DisableMoviment()
        {
            if (horizontalProjectile)
            {
                MovimentSide -= HorizontalMoviment;
            }
            else if (verticalProjectile)
            {
                MovimentSide -= VerticalMoviment;
            }
        }

        private void ResetPosistion()
        {
            _transform.position = Vector3.zero;
        }

        private void Update() => Moviment();

        private void Moviment()
        {
            MovimentSide();
        }

        private void HorizontalMoviment()
        {
            if (moveRight)
            {
                _transform.Translate(_velocity * Time.deltaTime * Vector3.right);
            }
            else
            {
                _transform.Translate(_velocity * Time.deltaTime * Vector3.left);
            }
        }

        private void VerticalMoviment()
        {
            if (moveUp)
            {
                _transform.Translate(_velocity * Time.deltaTime * Vector3.up);
            }
            else
            {
                _transform.Translate(_velocity * Time.deltaTime * Vector3.down);
            }
        }
    }
}
