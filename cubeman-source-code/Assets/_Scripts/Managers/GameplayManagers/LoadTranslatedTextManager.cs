using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class LoadTranslatedTextManager : MonoBehaviour
    {
        private void Awake() 
        {
            if(LanguageLoaderManager.Instance != null)
            {
                LanguageLoaderManager.Instance.LoadScreenMessageText();
            }
        }
    }
}