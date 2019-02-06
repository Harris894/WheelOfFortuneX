using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class newRotator : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Increase the incrimental value of the speed")]
    public float rotationSpeed = 15f;
    [Tooltip("Increasing this value will increase the amount of time elapsed between the start of the spin and the finish")]
    [Space]
    public float spinTime=4f;
    public float velocityLimit = 2000f;
    public bool spinning;

    meshGenerator reward;
    int score;
    [HideInInspector]
    public TextMeshProUGUI scoreText;
    [HideInInspector]
    public grannyBehaviour grannyAnim;


    float rotY;
    Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reward = GetComponent<meshGenerator>();
        spinning = false;
        score = 0;
    }

    //Moves the wheel while holding the mouse button - swipping
    private void OnMouseDrag()
    {
        if (!spinning)
        {
            rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;

            transform.Rotate(Vector3.forward, rotY*25);
        }

    }

    //Starts spinning after the release of the mouse button
    private void OnMouseUp()
    {
        if (!spinning)
        {
            StartCoroutine(startSpin(spinTime));
            grannyAnim.StopDance();
            grannyAnim.LookBehind();

            if (rb.angularDrag > 0.3)
            {
                rb.angularDrag = 0.05f;
            }
        }

    }

    private void FixedUpdate()
    {
        scoreText.text = "Score: " + score;
        //Start the end round procedure when the wheel has slowed down enough
        if (rb.angularDrag==0.2f && rb.angularVelocity>=-40f)
        {
            StartCoroutine(endRound(5f));
        }
    }
    
   
    //Adds an initial amount of torque, after t seconds, increase the angularDrag to start slowing down.
    IEnumerator startSpin(float t)
    {
        rb.AddTorque(-velocityLimit);
        spinning = true;
        yield return new WaitForSeconds(t);
        rb.angularDrag = 0.2f;
    }

    //Increases the angular drag even more, after time seconds stops the wheel snapping it into place.
    IEnumerator endRound(float time)
    {
        rb.angularDrag = 0.4f;
        yield return new WaitForSeconds(time);
        rb.angularVelocity = 0;
        spinning = false;
        CalculateScore();
    }

    //Checks what is the reward and sets the granny animations.
    private void CalculateScore()
    {
        score += reward.GetReward().value;
        if (reward.GetReward().name == "jackpot")
        {
            grannyAnim.StartDance();
        }
        else
        {
            grannyAnim.NotJackpot();
        }
    }

}
