using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : Gaze
{
    public static HashSet<MusicPlayer> musicPlayers = new HashSet<MusicPlayer>();

    private void Awake()
    {
        if(!musicPlayers.Contains(this))
        {
            musicPlayers.Add(this);
        }
    }

    protected override void GazeComplete()
    {
        if(audioSource == null)
            return;

        if(audioSource.isPlaying)
        {
            audioSource.Stop();
            return;
        }

        foreach (MusicPlayer mp in musicPlayers)
        {
            mp.audioSource.Stop();
        }

        audioSource.Play();

    }
}
