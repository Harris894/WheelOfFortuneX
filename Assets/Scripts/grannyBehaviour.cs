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

    public void StartDance()
    {
        
        anim.SetBool("jackpot", true);
        jackpot.start();
        
    }

    public void StopDance()
    {
        anim.SetBool("jackpot", false);
        jackpot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void LookBehind()
    {
        anim.SetTrigger("spun");
    }


    public void NotJackpot()
    {
        int x = Random.Range(1, 3);
        anim.SetInteger("defeat",x);
        StartCoroutine(GoToIdle());
        Debug.Log(x);
    }

    IEnumerator GoToIdle()
    {
        yield return new WaitForSeconds(1f);
        anim.SetInteger("defeat", 0);
    }

}
