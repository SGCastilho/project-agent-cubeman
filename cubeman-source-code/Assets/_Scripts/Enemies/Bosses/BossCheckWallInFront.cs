using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class BossCheckWallInFront : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Transform rightRayPoint;
        [SerializeField] private Transform leftRayPoint;

        [Space(12)]

        [SerializeField] [Range(0.1f, 1f)] private float rayRange = 0.4f;
        [SerializeField] private LayerMask rayLayer;

        internal bool HasWallInFront(bool lookingRight)
        {
            if(lookingRight)
            {
                return Physics.Raycast(rightRayPoint.position, Vector3.right, rayRange, rayLayer);
            }

            return Physics.Raycast(leftRayPoint.position, Vector3.left, rayRange, rayLayer);
        }

        internal bool HasWallInBackwords(bool lookingRight)
        {
            if(lookingRight)
            {
                return Physics.Raycast(leftRayPoint.position, Vector3.left, rayRange, rayLayer);
            }

            return Physics.Raycast(rightRayPoint.position, Vector3.right, rayRange, rayLayer);
        }

        #region Editor Function
    #if UNITY_EDITOR
        private void OnDrawGizmosSelected() 
        {
            if(rightRayPoint != null && leftRayPoint != null)
            {
                Gizmos.color = Color.red;

                var rightLine = new Vector3(rightRayPoint.position.x + rayRange, rightRayPoint.position.y, 
                    rightRayPoint.position.z);

                Gizmos.DrawLine(rightRayPoint.position, rightLine);

                var leftLine = new Vector3(leftRayPoint.position.x - rayRange, leftRayPoint.position.y, 
                    leftRayPoint.position.z);

                Gizmos.DrawLine(leftRayPoint.position, leftLine);
            }
        }
    #endif
        #endregion
    }
}
