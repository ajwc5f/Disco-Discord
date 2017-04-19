using UnityEngine;
using System.Collections;

public class CameraStartup : MonoBehaviour {
    private float newAspectRatio = 16.0f / 9.0f;

    // Use this for initialization
    void Start()
    {
        float variance = newAspectRatio / Camera.main.aspect;
        if (variance < 1.0)
            Camera.main.rect = new Rect((1.0f - variance) / 2.0f, 0, variance, 1.0f);
        else
        {
            variance = 1.0f / variance;
            Camera.main.rect = new Rect(0, (1.0f - variance) / 2.0f, 1.0f, variance);
        }
    }
}
