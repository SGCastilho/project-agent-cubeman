using Cubeman.Manager;
using UnityEngine;

namespace Cubeman.Trigger
{
    public sealed class CutscenePrologueSpawnRoadTrigger : MonoBehaviour
    {
        private const string POOLING_ROAD_KEY = "cutscene-road";
        private const int POOLING_ROAD_SPACING = 60;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                SpawnNewRoad();
            }
        }

        private void SpawnNewRoad()
        {
            var scenarioManager = PrologueCutsceneManager.Instance;
            var poolingManager = ObjectPoolingManager.Instance;

            var currentRoadPosistion = scenarioManager.CurrentRoadPosistion();
            var newRoadPosistion = new Vector2(currentRoadPosistion.x + POOLING_ROAD_SPACING,
                currentRoadPosistion.y);

            scenarioManager.CurrentPoolingRoad = poolingManager.SpawnPrefab(POOLING_ROAD_KEY, newRoadPosistion,
                scenarioManager.RoadPivot);
        }
    }
}
