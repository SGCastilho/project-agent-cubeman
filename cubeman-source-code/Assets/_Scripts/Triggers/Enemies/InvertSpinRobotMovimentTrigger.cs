using Cubeman.Enemies;
using UnityEngine;

namespace Cubeman.Trigger
{
    public sealed class InvertSpinRobotMovimentTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Enemy") && other.GetComponent<EnemyCharacterSpinRobotBehaviour>() != null)
            {
                var behaviour = other.GetComponent<EnemyCharacterSpinRobotBehaviour>();
                behaviour.Moviment.MoveRight = !behaviour.Moviment.MoveRight;
            }
        }
    }
}
