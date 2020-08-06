using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaze : MonoBehaviour
{
    protected AudioSource audioSource;
    public float timeToCompleteGaze = 2;
    float gazeTime = 0;
    bool gazing;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gazing) gazeTime += Time.deltaTime;

        if (gazeTime >= timeToCompleteGaze)
        {
            GazeComplete();
            gazing = false;
            gazeTime = 0;
        }
    }

    public void GazeEnter()
    {
        gazing = true;
    }
    public void GazeExit()
    {
        gazing = false;
        gazeTime = 0;
    }
    protected virtual void GazeComplete()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
