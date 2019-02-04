using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionCheck : MonoBehaviour
{
    FMOD.Studio.EventInstance tapSound;

    //Create the sound instance for an event.
    private void Awake()
    {
        tapSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/pointerCollision");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Upon collision with specific tag, plays the sound instance once.
        if (other.gameObject.tag=="circleCollider")
        {
            tapSound.start();
        }
    }
}
