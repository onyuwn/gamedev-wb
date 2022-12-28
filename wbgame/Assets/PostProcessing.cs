using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessing : MonoBehaviour
{
    public Shader Fisheye;
    private Material fisheyeMat;

    private void OnEnable()
    {
        fisheyeMat = new Material(Fisheye);
        fisheyeMat.hideFlags = HideFlags.HideAndDontSave;
    }

    private void OnDisable()
    {
        fisheyeMat = null;
    }
    // runs rendered frames through a custom shader
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        int width = source.width;
        int height = source.height;

        RenderTexture currentSource = source;
        Graphics.Blit(currentSource, destination, fisheyeMat, 0); // copy rendered image to first shader pass
    }
}
