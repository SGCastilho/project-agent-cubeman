using UnityEngine;

[System.Serializable]
public struct VideoOptions
{
    public Resolution clientResolution;
    public int clientResolutionIndex;

    public FullScreenMode clientFullScreenMode;
    public int clientFullScreenModeIndex;

    public int clientTargetFrameRate;
    public int clientVsync;
    public int clientTextureQuality;

    public AnisotropicFiltering clientAnisotropic;

    public int clientAntialiasing;
}