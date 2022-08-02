using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Stage Level Data", menuName = "Scriptable Object/Stage Level Data")]
    public sealed class StageLevelData : ScriptableObject
    {
        #region Encapsulation
        public string Key { get => stageLevelKey; }
        public string Name { get => stageName; }
        public Sprite Preview { get => stagePreview; }
        #endregion

        [Header("Settings")]
        [SerializeField] private string stageLevelKey = "scene-to-load-here";
        [SerializeField] private string stageName = "Stage Name";
        [SerializeField] private Sprite stagePreview;
    }
}