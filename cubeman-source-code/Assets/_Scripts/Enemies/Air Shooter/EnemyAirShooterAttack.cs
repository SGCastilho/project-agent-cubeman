using Cubeman.Audio;
using System.Collections;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class EnemyAirShooterAttack : MonoBehaviour
    {
        public delegate void Shoot(string projectileKey, Vector3 instancePoint);
        public event Shoot OnShoot;

        [Header("Classes")]
        [SerializeField] private EnemyAirShooterBehaviour behaviour;
        [SerializeField] private AudioSoundEffects soundEffects;

        [Header("Settings")]
        [SerializeField] private Transform nextShootingPointTransform;

        [Space(6)]

        [SerializeField] private Transform[] shootingLeftPoints;
        [SerializeField] private Transform[] shootingRightPoints;

        [Space(12)]

        [SerializeField] private Transform shootPoint;

        [Space(6)]

        [SerializeField] [Range(0.1f, 2f)] private float recoveryDelay = 1f;

        private AudioClipList _shootSFX;

        private int _currentShootingPoint;

        private bool _canShoot;
        private bool _pointReached;
        private bool _sidePointsCompleted;
        private bool _usingRightPoints;
        private bool _backingCurrentFromPoints;

        private float _currentRecoveryDelay;
        private float _distanceFromShootingPoint;

        private string _projectileKey;

        private void Awake() => LoadData();

        private void OnEnable() => SetupInitialPoints();

        private void OnDisable() => ResetGameObject();

        private void LoadData()
        {
            _projectileKey = behaviour.DataLoader.Data.Projectile.Key;
            LoadSoundEffects();
        }

        private void LoadSoundEffects()
        {
            _shootSFX = new AudioClipList("audio_shoot", behaviour.DataLoader.Data.Projectile.ProjectileSFX,
                behaviour.DataLoader.Data.Projectile.VolumeScale);
        }

        private void ResetGameObject()
        {
            _currentShootingPoint = 0;
            _distanceFromShootingPoint = 0;
            _backingCurrentFromPoints = false;
            SetupInitialPoints();
        }

        private void SetupInitialPoints()
        {
            _usingRightPoints = behaviour.Moviment.StarMoveRight;

            if (_usingRightPoints)
            {
                nextShootingPointTransform = shootingRightPoints[_currentShootingPoint];
            }
            else
            {
                nextShootingPointTransform = shootingLeftPoints[_currentShootingPoint];
            }

            _canShoot = true;
        }

        private float CalculateDistanceAbs(float pointA, float pointB)
        {
            return Mathf.Abs(pointA - pointB);
        }

        private void Update() => DistanceBetweenPoints();

        private void DistanceBetweenPoints()
        {
            if (_pointReached) return;

            _distanceFromShootingPoint = CalculateDistanceAbs(behaviour.BehaviourTransform.position.x, 
                nextShootingPointTransform.position.x);

            if(_distanceFromShootingPoint <= 0.1f)
            {
                behaviour.Moviment.IsMoving = false;

                _distanceFromShootingPoint = 1f;
                _pointReached = true;

                Shooting();
            }
        }

        private void Shooting()
        {
            if (!_pointReached) return;

            if(_canShoot)
            {
                OnShoot(_projectileKey, shootPoint.position);
                behaviour.AudioManager.PlaySoundEffectInOrder(ref _shootSFX);

                _canShoot = false;

                StartCoroutine(ShootingRecoveryCoroutine());
            }
        }

        IEnumerator ShootingRecoveryCoroutine()
        {
            yield return new WaitForSeconds(recoveryDelay);

            GoToNextShootingPoint();

            yield return new WaitForSeconds(0.1f);

            _canShoot = true;
            _pointReached = false;
        }

        private void ChangeShootingPointDirection()
        {
            if (_usingRightPoints)
            {
                if (_currentShootingPoint >= shootingRightPoints.Length)
                {
                    _currentShootingPoint--;
                    _backingCurrentFromPoints = true;
                }
            }
            else
            {
                if (_currentShootingPoint >= shootingLeftPoints.Length)
                {
                    _currentShootingPoint--;
                    _backingCurrentFromPoints = true;
                }
            }

            if (_sidePointsCompleted) { InverseShotingPoint(); }
        }

        private void InverseShotingPoint()
        {
            _currentShootingPoint = 0;
            _usingRightPoints = !_usingRightPoints;
            _backingCurrentFromPoints = false;

            if(_usingRightPoints)
            {
                nextShootingPointTransform = shootingRightPoints[_currentShootingPoint];
            }
            else
            {
                nextShootingPointTransform = shootingLeftPoints[_currentShootingPoint];
            }
        }

        private void GoToNextShootingPoint()
        {
            if(_usingRightPoints)
            {
                CheckIfIsBackingFromPoints();

                nextShootingPointTransform = shootingRightPoints[_currentShootingPoint];
            }
            else
            {
                CheckIfIsBackingFromPoints();

                nextShootingPointTransform = shootingLeftPoints[_currentShootingPoint];
            }

            behaviour.Moviment.IsMoving = true;
        }

        private void CheckIfIsBackingFromPoints()
        {
            if (_backingCurrentFromPoints)
            {
                _currentShootingPoint--;
                if(_currentRecoveryDelay <= 0)
                {
                    _sidePointsCompleted = true;
                }
            }
            else
            {
                if(_sidePointsCompleted)
                {
                    _sidePointsCompleted = false;
                }
                else
                {
                    _currentShootingPoint++;
                }
            }

            ChangeShootingPointDirection();
        }

        #region Editor Gizmos
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if(shootingLeftPoints != null && shootingRightPoints != null)
            {
                Gizmos.color = Color.yellow;

                for (int i = 0; i < shootingLeftPoints.Length; i++)
                {
                    Gizmos.DrawSphere(shootingLeftPoints[i].position, 0.2f);
                }

                for (int i = 0; i < shootingRightPoints.Length; i++)
                {
                    Gizmos.DrawSphere(shootingRightPoints[i].position, 0.2f);
                }
            }
        }
#endif
        #endregion
    }
}
