using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class EnemyCheckPlayerSide : MonoBehaviour
    {
        private GameObject _player;
        private Transform _transform;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            _transform = GetComponent<Transform>();
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        internal bool IsInRightSide()
        {
            if(_player.transform.position.x > _transform.position.x)
            {
                return true;
            }

            return false;
        }
    }
}
