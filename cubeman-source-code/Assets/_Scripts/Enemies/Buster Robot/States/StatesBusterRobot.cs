using UnityEngine;

namespace Cubeman.Enemies
{
    public class StatesBusterRobot
    {
        private readonly EnemyBusterRobotState _busterRobot;

        public StatesBusterRobot(EnemyBusterRobotState state) 
        {
            _busterRobot = state;
        }

        public void SearchingForPlayer() => SearchingPerSeconds();

        private void SearchingPerSeconds()
        {
            _busterRobot.CurrentSearchingPerSeconds += Time.deltaTime;
            if (_busterRobot.CurrentSearchingPerSeconds >= _busterRobot.SearchingPerSeconds)
            {
                SearchingBox();
                _busterRobot.CurrentSearchingPerSeconds = 0;
            }
        }

        private void SearchingBox()
        {
            Collider[] colliders = Physics.OverlapBox(_busterRobot.SearchingTransform.position, _busterRobot.SerachingBox,
                Quaternion.identity, _busterRobot.SearchingLayer);

            if (colliders != null)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].CompareTag("Player"))
                    {
                        _busterRobot.SetState(BusterRobotStates.CHASING);
                        _busterRobot.CurrentSearchingPerSeconds = 0;
                        break;
                    }
                }
            }
        }

        public void ChasePlayer()
        {
            _busterRobot.Behaviour.Moviment.MoveRight = 
                CheckObjectSide(_busterRobot.PlayerTransform.position.x, 
                _busterRobot.Behaviour.EnemyTransform.position.x);

            _busterRobot.Behaviour.Moviment.IsMoving = true;

            CheckBackToOriginMaxRange();
            CheckShootingRange();
        }

        private float CalculateDistanceX(float firstPosistion, float secondPosistion)
        {
            return Mathf.Abs(firstPosistion - secondPosistion);
        }

        private void CheckBackToOriginMaxRange()
        {
            _busterRobot.DistanceFromOriginPoint = 
                CalculateDistanceX(_busterRobot.Behaviour.EnemyTransform.position.x, 
                _busterRobot.OriginPointTransform.position.x);

            if(_busterRobot.DistanceFromOriginPoint > _busterRobot.MaxOriginDistance)
            {
                _busterRobot.SetState(BusterRobotStates.BACK_TO_ORIGIN);
            }
        }

        private void CheckShootingRange()
        {
            _busterRobot.DistanceFromPlayer = 
                CalculateDistanceX(_busterRobot.Behaviour.EnemyTransform.position.x, _busterRobot.PlayerTransform.position.x);

            if(_busterRobot.DistanceFromPlayer <= _busterRobot.ShootingRange)
            {
                _busterRobot.Behaviour.Moviment.IsMoving = false;
                _busterRobot.SetState(BusterRobotStates.SHOOTING);
            }
        }

        private bool CheckObjectSide(float firstPosistion, float secondPosistion)
        {
            if(firstPosistion > secondPosistion)
            {
                return true;
            }

            return false;
        }

        public void PrepareShooting()
        {
            BackChasingTimer();
            Shooting();
            BackChasingCheck();
        }

        private void Shooting()
        {
            if (!_busterRobot.ShootingPose)
            {
                _busterRobot.CurrentShootingPoseDelay += Time.deltaTime;
                if (_busterRobot.CurrentShootingPoseDelay >= _busterRobot.ShootingPoseDelay)
                {
                    _busterRobot.ShootingPose = true;
                    _busterRobot.CurrentShootingPoseDelay = 0;
                }
            }
            else
            {
                ShootingFireRate();
            }
        }

        private void ShootingFireRate()
        {
            if (_busterRobot.Shooted)
            {
                _busterRobot.CurrentShootingFireRate += Time.deltaTime;
                if (_busterRobot.CurrentShootingFireRate >= _busterRobot.ShootingFireRate)
                {
                    _busterRobot.Shooted = false;
                    _busterRobot.CurrentShootingFireRate = 0;
                    _busterRobot.Behaviour.Moviment.MoveRight = 
                        CheckObjectSide(_busterRobot.PlayerTransform.position.x, 
                        _busterRobot.Behaviour.EnemyTransform.position.x);
                }
            }
            else
            {
                _busterRobot.CurrentShootDelay += Time.deltaTime;
                if(_busterRobot.CurrentShootDelay > _busterRobot.ShootDelay)
                {
                    _busterRobot.Shooted = true;
                    _busterRobot.CurrentShootDelay = 0;
                }
            }
        }

        private void BackChasingCheck()
        {
            if (_busterRobot.InBackChasingDelay)
            {
                CheckIfPlayerEnterShootingRange();
            }
            else
            {
                CheckIfPlayerExitShootingRange();
            }
        }

        private void CheckIfPlayerEnterShootingRange()
        {
            _busterRobot.DistanceFromPlayer =
                CalculateDistanceX(_busterRobot.Behaviour.EnemyTransform.position.x, _busterRobot.PlayerTransform.position.x);

            if (_busterRobot.DistanceFromPlayer < _busterRobot.ShootingRange)
            {
                _busterRobot.InBackChasingDelay = false;
            }
        }

        private void CheckIfPlayerExitShootingRange()
        {
            _busterRobot.DistanceFromPlayer =
                CalculateDistanceX(_busterRobot.Behaviour.EnemyTransform.position.x, _busterRobot.PlayerTransform.position.x);

            if (_busterRobot.DistanceFromPlayer > _busterRobot.ShootingRange)
            {
                _busterRobot.InBackChasingDelay = true;
            }
        }

        private void BackChasingTimer()
        {
            if(_busterRobot.InBackChasingDelay)
            {
                _busterRobot.CurrentBackChasingDelay += Time.deltaTime;
                if(_busterRobot.CurrentBackChasingDelay >= _busterRobot.BackChasingDelay)
                {
                    _busterRobot.SetState(BusterRobotStates.CHASING);
                    _busterRobot.Shooted = false;
                    _busterRobot.CurrentShootingFireRate = 0;
                    _busterRobot.CurrentShootDelay = 0;
                    _busterRobot.CurrentShootingPoseDelay = 0;
                    _busterRobot.ShootingPose = false;
                    _busterRobot.CurrentBackChasingDelay = 0;
                }
            }
            else { _busterRobot.CurrentBackChasingDelay = 0; }
        }

        public void BackToOriginalPosistion()
        {
            _busterRobot.Behaviour.Moviment.MoveRight = 
                CheckObjectSide(_busterRobot.OriginPointTransform.position.x, 
                _busterRobot.Behaviour.EnemyTransform.position.x);

            _busterRobot.Behaviour.Moviment.IsMoving = true;

            CheckBackToOriginMinRange();
        }

        private void CheckBackToOriginMinRange()
        {
            _busterRobot.DistanceFromOriginPoint =
                CalculateDistanceX(_busterRobot.Behaviour.EnemyTransform.position.x,
                _busterRobot.OriginPointTransform.position.x);

            if (_busterRobot.DistanceFromOriginPoint < 0.1f)
            {
                _busterRobot.Behaviour.Moviment.IsMoving = false;
                _busterRobot.SetState(BusterRobotStates.SEARCHING);

                _busterRobot.ResetDistances();
                _busterRobot.ResetBooleans();
                _busterRobot.ResetTimers();
            }
        }
    }
}
