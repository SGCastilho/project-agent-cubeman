using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Cubeman.Manager
{
    public sealed class BootManager : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private LanguageLoaderManager languageLoaderManager;

        [Header("Unity Events")]

        [Space(12)]

        [SerializeField] private UnityEvent OnBootingEnd;

        private async void Awake()
        {
            await languageLoaderManager.LoadTranslations();

            await Task.Delay(1000);

            OnBootingEnd?.Invoke();
        }
    }
}