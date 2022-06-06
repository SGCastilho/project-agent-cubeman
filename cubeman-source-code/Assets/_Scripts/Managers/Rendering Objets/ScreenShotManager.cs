using System.Collections;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class ScreenShotManager : MonoBehaviour
    {
        private bool _inScreenShot;

        private void Update()
        {
            if (_inScreenShot) return;

            if(Input.GetKeyDown(KeyCode.F12))
            {
                StartCoroutine(ScreenShotCoroutine());
            }
        }

        IEnumerator ScreenShotCoroutine()
        {
            _inScreenShot = true;

            yield return new WaitForEndOfFrame();

            int width = Screen.width / 2;
            int height = Screen.height;

            Texture2D screenShotTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);

            Rect screenRect = new Rect(512, 0, width, height);
            screenShotTexture.ReadPixels(screenRect, 0, 0);
            screenShotTexture.Apply();

            byte[] screenShotBytes = screenShotTexture.EncodeToTGA();

            System.IO.File.WriteAllBytes(Application.dataPath + "/Screenshots/Screenshot.tga", screenShotBytes);

            _inScreenShot = false;
        }
    }
}