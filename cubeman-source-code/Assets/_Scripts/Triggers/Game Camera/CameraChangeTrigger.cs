using Cubeman.GameCamera;
using UnityEngine;

#region EditorReference
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace Cubeman.Trigger
{
    public sealed class CameraChangeTrigger : MonoBehaviour
    {
        #region EditorVariables
#if UNITY_EDITOR
        internal GameplayCamera Camera { get => _camera; }
        internal CameraState NextCameraState { get => nextCameraState; set => nextCameraState = value; }
        internal Vector3 CorrectCameraPos { get => correctCameraPos; set => correctCameraPos = value; }
        internal Transform CameraFixedPoint { get => cameraFixedPoint; set => cameraFixedPoint = value; }
#endif
        #endregion

        private GameplayCamera _camera;

        [Header("Settings")]
        [SerializeField] private CameraState nextCameraState;
        [Space(12)]
        [SerializeField] private Vector3 correctCameraPos;
        [Space(12)]
        [SerializeField] private Transform cameraFixedPoint;

        private void Awake() => _camera = FindObjectOfType<GameplayCamera>();

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                if(nextCameraState == CameraState.FIXED_POINT)
                {
                    _camera.FixedCameraPoint = cameraFixedPoint;
                }
                else { _camera.CorrectCameraPos = correctCameraPos; }
                _camera.ChangeCameraState(nextCameraState);
            }
        }
    }

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(CameraChangeTrigger))]
    public class CameraChangeTriggerEditor : Editor 
    {
        private Vector3 _cameraOriginalPos;

        private bool _cameraInPreview;

        public override void OnInspectorGUI()
        {
            CameraChangeTrigger camTrigger = (CameraChangeTrigger)target;

            GUILayout.Label("Settings");

            InspectorAtributtes(camTrigger);

            PreviewButton(camTrigger);
        }

        private void InspectorAtributtes(CameraChangeTrigger camTrigger)
        {
            GUILayout.BeginVertical();
            camTrigger.NextCameraState = (CameraState)EditorGUILayout.EnumPopup("Camera State:", camTrigger.NextCameraState);

            if (camTrigger.NextCameraState == CameraState.HORIZONTAL || camTrigger.NextCameraState == CameraState.VERTICAL
                || camTrigger.NextCameraState == CameraState.COMPLETE)
            {
                camTrigger.CorrectCameraPos = (Vector3)EditorGUILayout.Vector3Field("Camera Correction:", camTrigger.CorrectCameraPos);
            }

            if (camTrigger.NextCameraState == CameraState.FIXED_POINT)
            {
                camTrigger.CameraFixedPoint = (Transform)EditorGUILayout.ObjectField("Fixed Transform:", camTrigger.CameraFixedPoint, typeof(Transform), true);
            }
            GUILayout.EndVertical();
        }

        private void PreviewButton(CameraChangeTrigger camTrigger)
        {
            GUILayout.Space(12);
            if (_cameraInPreview)
            {
                if (GUILayout.Button("Stop Preview Camera"))
                {
                    Camera.main.transform.position = _cameraOriginalPos;
                    _cameraInPreview = false;
                }
            }
            else
            {
                if (GUILayout.Button("Preview Camera"))
                {
                    _cameraOriginalPos = Camera.main.transform.position;

                    Vector3 setPreview = Vector3.zero;

                    if (camTrigger.NextCameraState == CameraState.FIXED_POINT)
                    {
                        if(camTrigger.CameraFixedPoint == null)
                        {
                            Debug.LogWarning("Fixed Camera Point n�o foi encontrado, n�o � possivel usar o preview.");
                            return;
                        }
                        else
                        {
                            setPreview = new Vector3(camTrigger.CameraFixedPoint.transform.position.x,
                                camTrigger.CameraFixedPoint.transform.position.y, _cameraOriginalPos.z);
                        }
                    }
                    else
                    {
                        setPreview = new Vector3(camTrigger.CorrectCameraPos.x,
                            camTrigger.CorrectCameraPos.y, _cameraOriginalPos.z);
                    }

                    Camera.main.transform.position = setPreview;
                    _cameraInPreview = true;
                }
            }
        }
    }
#endif
    #endregion
}
