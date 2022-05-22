using UnityEngine;

namespace Cubeman.Scenario
{
    public sealed class ScenarioCivilianBehaviour : MonoBehaviour
    {
        #region Encapsulation
        internal ScenarioCivilianPointCheck PointCheck { get => pointCheck; }
        internal ScenarioCivilianMoviment Moviment { get => moviment; }
        #endregion

        [Header("Classes")]
        [SerializeField] private ScenarioCivilianPointCheck pointCheck;
        [SerializeField] private ScenarioCivilianMoviment moviment;
    }
}
