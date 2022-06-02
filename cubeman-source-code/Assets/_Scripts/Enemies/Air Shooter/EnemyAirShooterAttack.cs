using Cubeman.Audio;
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
        [SerializeField] private Transform[] shootingLeftPoints;
        [SerializeField] private Transform[] shootingRightPoints;
        private Transform _nextShootingPointTransform;
        private int _currentShootingPoint;
        private bool _usingRightPoints;
        private bool _backingCurrentFromPoints;
        private float _distanceFromShootingPoint;

        [Space(12)]

        [SerializeField] private Transform shootPoint;
        [Space(6)]
        [SerializeField] [Range(0.1f, 2f)] private float recoveryDelay = 1f;
        private bool _canShoot;
        private float _currentRecoveryDelay;
        private string _projectileKey;

        private const string SHOOT_AUDIO_KEY = "audio_shoot";
        private AudioClipList _shootSFX;

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
            _shootSFX = soundEffects.GetSoundEffect(SHOOT_AUDIO_KEY);
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
                _nextShootingPointTransform = shootingRightPoints[_currentShootingPoint];
            }
            else
            {
                _nextShootingPointTransform = shootingLeftPoints[_currentShootingPoint];
            }

            _canShoot = true;
        }

        private float CalculateDistanceAbs(float pointA, float pointB)
        {
            return Mathf.Abs(pointA - pointB);
        }

        private void Update() => TravelBetweenShootingPoints();

        private void FixedUpdate() => DistanceCalculation();

        private void DistanceCalculation()
        {
            if(_canShoot)
            {
                _distanceFromShootingPoint = CalculateDistanceAbs(behaviour.Moviment.EnemyTransform.position.x,
                    _nextShootingPointTransform.position.x);
            }
        }

        private void TravelBetweenShootingPoints()
        {
            if (_canShoot)
            {
                if (_distanceFromShootingPoint < 0.2f)
                {
                    behaviour.Moviment.IsMoving = false;

                    if (OnShoot != null) 
                    { 
                        OnShoot(_projectileKey, shootPoint.position);
                        behaviour.AudioManager.PlaySoundEffectInOrder(ref _shootSFX);
                    }

                    _canShoot = false;
                    _distanceFromShootingPoint = 0;
                }
            }
            else
            {
                _currentRecoveryDelay += Time.deltaTime;
                if (_currentRecoveryDelay >= recoveryDelay)
                {
                    behaviour.Moviment.IsMoving = true;
                    _currentRecoveryDelay = 0;
                    NextShootingPoint();
                }
            }
        }

        private void NextShootingPoint()
        {
            if(_backingCurrentFromPoints)
            {
                if (_usingRightPoints)
                {
                    _currentShootingPoint--;
                    if (_currentShootingPoint < 0)
                    {
                        _backingCurrentFromPoints = false;
                        _usingRightPoints = false;
                        return;
                    }
                    _nextShootingPointTransform = shootingRightPoints[_currentShootingPoint];
                }
                else
                {
                    _currentShootingPoint--;
                    if (_currentShootingPoint < 0)
                    {
                        _backingCurrentFromPoints = false;
                        _usingRightPoints = true;
                        return;
                    }
                    _nextShootingPointTransform = shootingLeftPoints[_currentShootingPoint];
                }
            }
            else
            {
                if (_usingRightPoints)
                {
                    _currentShootingPoint++;
                    if (_currentShootingPoint == shootingRightPoints.Length)
                    {
                        _backingCurrentFromPoints = true;
                        return;
                    }
                    _nextShootingPointTransform = shootingRightPoints[_currentShootingPoint];
                }
                else
                {
                    _currentShootingPoint++;
                    if(_currentShootingPoint == shootingLeftPoints.Length)
                    {
                        _backingCurrentFromPoints = true;
                        return;
                    }
                    _nextShootingPointTransform = shootingLeftPoints[_currentShootingPoint];
                }
            }

            _canShoot = true;
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
