using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class SceneGroupsLoader : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject[] sceneGroups;

        public void StartLoadGroup()
        {
            if(sceneGroups.Length > 1)
            {
                LoadGroup(0);

                for(int i = 1; i < sceneGroups.Length; i++)
                {
                    UnLoadGroup(i);
                }
            }
        }

        public void LoadGroup(int groupIndex)
        {
            sceneGroups[groupIndex].SetActive(true);
        }

        public void UnLoadGroup(int groupIndex)
        {
            sceneGroups[groupIndex].SetActive(false);
        }

        public void DestroyAllGroupsWithoutBossArena()
        {
            for(int i = 0; i < sceneGroups.Length-1; i++)
            {
                Destroy(sceneGroups[i].gameObject, 0.2f);
            }
        }
    }
}