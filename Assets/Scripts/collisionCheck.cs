using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionCheck : MonoBehaviour
{
    FMOD.Studio.EventInstance tapSound;


    private void Awake()
    {
        tapSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/pointerCollision");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="circleCollider")
        {
            tapSound.start();
        }
    }
}
