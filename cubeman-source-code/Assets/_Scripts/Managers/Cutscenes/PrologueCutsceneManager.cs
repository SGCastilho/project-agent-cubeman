using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class PrologueCutsceneManager : MonoBehaviour
    {
        #region Singleton
        public static PrologueCutsceneManager Instance;
        #endregion

        #region Encapsulation
        public Transform RoadPivot { get => scenarioRoadsPivot; }
        public GameObject CurrentPoolingRoad { set => _currentPoolingRoad = value; }
        #endregion

        private const string POOLING_ROAD_KEY = "cutscene-road";

        [Header("Settings")]
        [SerializeField] private Transform scenarioRoadsPivot;
        [SerializeField] private Vector3 roadStartPosistion;

        private GameObject _currentPoolingRoad;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            Instance = this;

            _currentPoolingRoad = ObjectPoolingManager.Instance
                .SpawnPrefab(POOLING_ROAD_KEY, roadStartPosistion, scenarioRoadsPivot);
        }

        public Vector3 CurrentRoadPosistion()
        {
            return _currentPoolingRoad.transform.localPosition;
        }
    }
}
