using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class forceRotator : MonoBehaviour
{
    [Header("Settings")] 
    [Tooltip("Increase the incrimental value of the speed")]
    public float rotationSpeed=15f;
    [Tooltip("Increasing this value will increase the amount of time elapsed between the start of the spin and the finish")]
    [Space]
    public float velocityLimit = 2000f;
    public bool spinning;

    meshGenerator reward;
    int score;
    [HideInInspector]
    public TextMeshProUGUI scoreText;

    
    float rotY;
    Rigidbody2D rb;

    float timeWanted = 10f;
    CircleCollider2D circleCol;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCol = GetComponent<CircleCollider2D>();
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

            transform.RotateAround(Vector3.forward, rotY);
        }
        
    }

    //Starts spinning after the release of the mouse button
    private void OnMouseUp()
    {
        if (!spinning)
        {
            spinning = true;
         
            if (rb.angularDrag > 0.5)
            {
                rb.angularDrag = 0.05f;
            }
        }
        
    }

    private void FixedUpdate()
    {
        //code to perform the spin
        if (spinning)
        {
            rb.AddTorque(-rotationSpeed*3);

            //code to stop the spin after a certain velocity reached. Can be tied to time. more velocity, longer spin.
            if (rb.angularVelocity < -velocityLimit)
            {
                spinning = false;
                rb.angularDrag = Mathf.Lerp(rb.angularDrag, 5, timeWanted * Time.deltaTime);
                StartCoroutine(endRound(7));

            }
        }

        scoreText.text = "Score: " + score;
    }

    IEnumerator endRound(float t)
    {
        yield return new WaitForSeconds(t);
        score += reward.GetReward().value;

    }
}
