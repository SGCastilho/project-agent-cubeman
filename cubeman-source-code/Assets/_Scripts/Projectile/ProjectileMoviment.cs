using System;
using UnityEngine;

namespace Cubeman.Projectile
{
    public sealed class ProjectileMoviment : MonoBehaviour
    {
        private Action MovimentSide;

        #region Encapsulation
        public bool MoveRight { set => moveRight = value; }
        public bool MoveUp { set => moveUp = value; }

        internal float Velocity { set => _velocity = value; }
        #endregion

        [Header("Settings")]
        [SerializeField] private Transform graphicsTransform;

        [Space(12)]

        [SerializeField] private bool horizontalProjectile = true;
        [SerializeField] private bool verticalProjectile;

        [Space(12)]

        [SerializeField] private bool moveUp;
        [SerializeField] private bool moveRight;

        private float _velocity;

        private Transform _transform;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            _transform = GetComponent<Transform>();
            EnableMoviment();
        }

        private void OnDisable() => ResetObject();

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

        private void ResetObject()
        {
            _transform.position = Vector3.zero;
        }

        private void Update() => Moviment();

        private void Moviment() => MovimentSide();

        private void HorizontalMoviment()
        {
            if (moveRight)
            {
                FlipHorizontalGraphics(true);
                _transform.Translate(_velocity * Time.deltaTime * Vector3.right);
            }
            else
            {
                FlipHorizontalGraphics(false);
                _transform.Translate(_velocity * Time.deltaTime * Vector3.left);
            }
        }

        private void FlipHorizontalGraphics(bool flip)
        {
            if (graphicsTransform == null) return;

            if(flip)
            {
                graphicsTransform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                graphicsTransform.localScale = new Vector3(-1f, 1f, 1f);
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
