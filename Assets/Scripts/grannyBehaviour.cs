using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grannyBehaviour : MonoBehaviour
{

    Animator anim;
    meshGenerator reward;
    FMOD.Studio.EventInstance jackpot;

    private void Start()
    {
        anim = GetComponent<Animator>();
        reward = GetComponent<meshGenerator>();
        jackpot = FMODUnity.RuntimeManager.CreateInstance("event:/Music/jackpot");
    }

    //Start the animation and the sound.
    public void StartDance()
    {
        
        anim.SetBool("jackpot", true);
        jackpot.start();
        
    }

    //Stop the animation and the sound.
    public void StopDance()
    {
        anim.SetBool("jackpot", false);
        jackpot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    //Trigger the spun animation whenever you spin the wheel.
    public void LookBehind()
    {
        anim.SetTrigger("spun");
    }

    //Chooses randomly between 2 animations for when the price is not the jackpot.
    public void NotJackpot()
    {
        int x = Random.Range(1, 3);
        anim.SetInteger("defeat",x);
        StartCoroutine(GoToIdle());

    }

    //Reset the value of the animator parameter otherwise it will keep running the animation.
    IEnumerator GoToIdle()
    {
        yield return new WaitForSeconds(1f);
        anim.SetInteger("defeat", 0);
    }

}
