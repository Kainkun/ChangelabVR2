using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : Gaze
{
    public Transform teleportPosition;
    Transform player;

    private void Awake()
    {
        player = Camera.main.transform.root;
        if(teleportPosition == null)
            teleportPosition = transform.GetChild(0);
    }

    protected override void GazeComplete()
    {
        player.position = teleportPosition.position;
        base.GazeComplete();
    }
}
