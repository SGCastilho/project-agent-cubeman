using UnityEngine;

namespace Cubeman.Scenario
{
    public sealed class ScenarioCivilianMoviment : MonoBehaviour
    {
        #region Encapsulation
        internal bool IsMoving { set => isMoving = value; }

        internal Transform CivilianTransform { get => _transform; }
        #endregion

        [Header("Settings")]
        [SerializeField] private Transform graphicsTransform;

        [Space(12)]

        [SerializeField] [Range(1f, 12f)] private float movimentSpeed = 4f;
        [SerializeField] private bool moveRight;
        [SerializeField] private bool isMoving;

        private Vector3 _startPosistion;
        private Transform _transform;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            _transform = GetComponent<Transform>();
            _startPosistion = _transform.localPosition;

            FlipGraphics(moveRight);
        }

        private void FlipGraphics(bool flipRight)
        {
            if(flipRight)
            {
                var newScale = new Vector3(1f, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
                graphicsTransform.localScale = newScale;
            }
            else
            {
                var newScale = new Vector3(-1f, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
                graphicsTransform.localScale = newScale;
            }
        }

        private void OnDisable() => ResetObject();

        private void ResetObject()
        {
            _transform.localPosition = _startPosistion;
        }

        private void Update() => Moviment();

        private void Moviment()
        {
            if (!isMoving) return;

            if (moveRight)
            {
                _transform.Translate(movimentSpeed * Vector2.right * Time.deltaTime);
            }
            else
            {
                _transform.Translate(movimentSpeed * Vector2.left * Time.deltaTime);
            }
        }
    }
}
