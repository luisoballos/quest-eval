using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCollideL : Singleton<StartCollideL>
{
    public Collider[] hands;
    public Collider leftHand;
    public Collider rightHand;
    public bool enableStart;

    [Header("Scripts")]
    public GrabQUESTManager GQML;
    public GrabQUESTManager GQMR;
    public Wiggling WiggL;
    public Wiggling WiggR;

    public void Start()
    {
        hands = new Collider[2];
        hands[0] = leftHand;
        hands[1] = rightHand;
        enableStart = true;
    }

    private void OnTriggerEnter(Collider hands)
    {

        if(enableStart == true)
        {
            if (hands.CompareTag("HandL"))
            {
                Debug.Log("Button collide");
                rightHand.gameObject.SetActive(false);
                WiggR.gameObject.SetActive(false);
                GQMR.gameObject.SetActive(false);

                enableStart = false;
                leftHand.isTrigger = false;
                GQML.StartFunction();
            }
        }
    }
}
