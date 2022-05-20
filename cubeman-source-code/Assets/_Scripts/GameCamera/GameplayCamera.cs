using System;
using UnityEngine;

namespace Cubeman.GameCamera
{
    public enum CameraState { HORIZONTAL, VERTICAL, COMPLETE, FIXED_POINT }

    public sealed class GameplayCamera : MonoBehaviour
    {
        #region Encapsulation
        public Transform FixedCameraPoint { set => fixedCameraTransform = value; }
        public Vector3 CorrectCameraPos { set => _correctCameraPos = value; }
        #endregion

        private Action CameraAction;

        [Header("Settings")]
        [SerializeField] private CameraState currentState;
        [Space(12)]
        [SerializeField] private Transform trackTransform;
        [SerializeField] private Transform fixedCameraTransform;
        [SerializeField] [Range(2f, 6f)] private float trackSmooth = 2.128f;
        [Space(6)]
        [SerializeField] [Range(-38f, -28f)] private float cameraDistance = -28f;
        [SerializeField] [Range(0.1f, 2f)] private float verticalCameraYOffset = 0.6f;

        private Transform _transform;

        private Vector3 _correctCameraPos;

        private Vector3 _nextPos;
        private Vector3 _smoothPos;

        private void Awake() => _transform = GetComponent<Transform>();

        private void Start() 
        {
            _correctCameraPos = _transform.position;
            CheckState(); 
        }

        private void LateUpdate() => CameraAction();

        public void ChangeCameraState(CameraState nextState)
        {
            if(nextState != currentState)
            {
                currentState = nextState;
                CheckState();
            }
        }

        private void CheckState()
        {
            if (CameraAction != null) { CameraAction = null; }

            switch (currentState)
            {
                case CameraState.HORIZONTAL:
                    CameraAction += HorizontalCamera;
                    break;
                case CameraState.VERTICAL:
                    CameraAction += VerticalCamera;
                    break;
                case CameraState.COMPLETE:
                    CameraAction += CompleteCamera;
                    break;
                case CameraState.FIXED_POINT:
                    CameraAction += FixedCamera;
                    break;
            }
        }

        private void HorizontalCamera()
        {
            _nextPos = new Vector3(trackTransform.position.x, _correctCameraPos.y, cameraDistance);
            _smoothPos = Vector3.Lerp(_transform.position, _nextPos, trackSmooth * Time.deltaTime);
            _transform.position = _smoothPos;
        }

        private void VerticalCamera()
        {
            _nextPos = new Vector3(_correctCameraPos.x, trackTransform.position.y - verticalCameraYOffset, cameraDistance);
            _smoothPos = Vector3.Lerp(_transform.position, _nextPos, trackSmooth * Time.deltaTime);
            _transform.position = _smoothPos;
        }

        private void CompleteCamera()
        {
            _nextPos = new Vector3(trackTransform.position.x, trackTransform.position.y, cameraDistance);
            _smoothPos = Vector3.Lerp(_transform.position, _nextPos, trackSmooth * Time.deltaTime);
            _transform.position = _smoothPos;
        }

        private void FixedCamera()
        {
            if(fixedCameraTransform != null)
            {
                _nextPos = new Vector3(fixedCameraTransform.position.x, fixedCameraTransform.position.y, cameraDistance);
                _smoothPos = Vector3.Lerp(_transform.position, _nextPos, trackSmooth * Time.deltaTime);
                _transform.position = _smoothPos;
            }
        }
    }
}
