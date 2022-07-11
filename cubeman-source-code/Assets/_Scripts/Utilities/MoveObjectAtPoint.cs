using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Cubeman.Utilities
{
    public sealed class MoveObjectAtPoint : MonoBehaviour
    {
        private enum MoveDirection { UP, DOWN, RIGHT, LEFT }

        [Header("Settings")]
        [SerializeField] private Transform objectToMove;
        [SerializeField] private Vector3 objectMoveDirection;

        [Space(12)]

        [SerializeField] private MoveDirection moveDirection;
        [SerializeField][Range(1f, 20f)] private float moveVelocity = 2f;

        [Space(12)]

        [SerializeField] private UnityEvent OnReverseMoveEnd;

        private Vector3 _startPosistion;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            _startPosistion = objectToMove.localPosition;
        }

        public void StartMove()
        {
            objectToMove.DOKill();

            switch (moveDirection)
            {
                case MoveDirection.UP:
                    objectToMove.DOMoveY(objectMoveDirection.y, moveVelocity);
                    break;
                case MoveDirection.DOWN:
                    objectToMove.DOMoveY(-objectMoveDirection.y, moveVelocity);
                    break;
                case MoveDirection.RIGHT:
                    objectToMove.DOMoveX(objectMoveDirection.x, moveVelocity);
                    break;
                case MoveDirection.LEFT:
                    objectToMove.DOMoveX(-objectMoveDirection.x, moveVelocity);
                    break;
            }
        }

        public void StartReverseMove()
        {
            if (objectToMove.position == _startPosistion) return;

            objectToMove.DOKill();

            switch (moveDirection)
            {
                case MoveDirection.UP:
                    objectToMove.DOMoveY(_startPosistion.y - 0.5f, moveVelocity).OnComplete(ReverseMoveEnd);
                    break;
                case MoveDirection.DOWN:
                    objectToMove.DOMoveY(_startPosistion.y + 0.5f, moveVelocity).OnComplete(ReverseMoveEnd);
                    break;
                case MoveDirection.RIGHT:
                    objectToMove.DOMoveX(_startPosistion.x + 0.5f, moveVelocity).OnComplete(ReverseMoveEnd);
                    break;
                case MoveDirection.LEFT:
                    objectToMove.DOMoveX(_startPosistion.x - 0.5f, moveVelocity).OnComplete(ReverseMoveEnd);
                    break;
            }
        }

        private void ReverseMoveEnd() => OnReverseMoveEnd?.Invoke();
    }
}
