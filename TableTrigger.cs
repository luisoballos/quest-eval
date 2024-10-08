using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTrigger : MonoBehaviour
{
    public int contWig;
    public int Wigcont;
    public int Thumbcont;
    public bool PinkyCheck;
    public bool RingCheck;
    public bool MiddleCheck;
    public bool IndexCheck;
    public bool ThumbCheck;
    public bool enableTableTriggerL;
    public bool enableTableTriggerR;
    public SphereCollider LThumb2;
    public SphereCollider LIndex2;
    public SphereCollider LMiddle2;
    public SphereCollider LRing2;
    public SphereCollider LPinky2;
    public SphereCollider RThumb2;
    public SphereCollider RIndex2;
    public SphereCollider RMiddle2;
    public SphereCollider RRing2;
    public SphereCollider RPinky2;

    [Header("Scripts")]
    public GrabQUESTManager GQManagerL;
    public GrabQUESTManager GQManagerR;

    void Start()
    {
        Wigcont = 0;
        Thumbcont = 0;
        PinkyCheck = false;
        RingCheck = false;
        MiddleCheck = false;
        IndexCheck = false;
        ThumbCheck = false;
    }

    public void Update()
    {
        if (GQManagerL.actualStep == 0 || GQManagerL.actualStep == 1)
        {
            enableTableTriggerL = true;
        }
        else if(GQManagerR.actualStep == 0 || GQManagerR.actualStep == 1)
        {
            enableTableTriggerR = true;
        }
        else
        {
            enableTableTriggerL = false;
            enableTableTriggerR = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (enableTableTriggerL)
        {
            switch (GQManagerL.actualStep)
            {
                case 0:
                    if (other.CompareTag("LPinky") && PinkyCheck == false && RingCheck == false && MiddleCheck == false && IndexCheck == false)
                    {
                        PinkyCheck = true;
                    }
                    else if (other.CompareTag("LRing") && PinkyCheck == true && RingCheck == false && MiddleCheck == false && IndexCheck == false)
                    {
                        RingCheck = true;
                    }
                    else if (other.CompareTag("LMiddle") && PinkyCheck == true && RingCheck == true && MiddleCheck == false && IndexCheck == false)
                    {
                        MiddleCheck = true;
                    }
                    else if (other.CompareTag("LIndex") && PinkyCheck == true && RingCheck == true && MiddleCheck == true && IndexCheck == false)
                    {
                        IndexCheck = true;
                        
                        if (Wigcont == 0)
                        {
                            PinkyCheck = false;
                            RingCheck = false;
                            MiddleCheck = false;
                            IndexCheck = false;
                            ThumbCheck = false;
                            Wigcont++;
                        }
                        else if (Wigcont == 1)
                        {
                            Wigcont++;
                            GQManagerL._step[GQManagerL.actualStep] = true;
                        }
                    }
                    break;
                case 1:
                    if (other.CompareTag("LThumb"))
                    {
                        ThumbCheck = true;

                        if (Thumbcont == 1)
                        {
                            Thumbcont++;
                            GQManagerL._step[GQManagerL.actualStep] = true;
                        }
                    }
                    break;
            }
        }
        else if (enableTableTriggerR)
        {
            switch (GQManagerR.actualStep)
            {
                case 0:
                    if (other.CompareTag("RPinky") && PinkyCheck == false && RingCheck == false && MiddleCheck == false && IndexCheck == false)
                    {
                        PinkyCheck = true;
                    }
                    else if (other.CompareTag("RRing") && PinkyCheck == true && RingCheck == false && MiddleCheck == false && IndexCheck == false)
                    {
                        RingCheck = true;
                    }
                    else if (other.CompareTag("RMiddle") && PinkyCheck == true && RingCheck == true && MiddleCheck == false && IndexCheck == false)
                    {
                        MiddleCheck = true;
                    }
                    else if (other.CompareTag("RIndex") && PinkyCheck == true && RingCheck == true && MiddleCheck == true && IndexCheck == false)
                    {
                        IndexCheck = true;

                        if (Wigcont == 0)
                        {
                            PinkyCheck = false;
                            RingCheck = false;
                            MiddleCheck = false;
                            IndexCheck = false;
                            ThumbCheck = false;
                            Wigcont++;
                        }
                        else if (Wigcont == 1)
                        {
                            Wigcont++;
                            GQManagerR._step[GQManagerR.actualStep] = true;
                        }
                    }
                    break;
                case 1:
                    if (other.CompareTag("RThumb"))
                    {
                        ThumbCheck = true;

                        if (Thumbcont == 1)
                        {
                            Thumbcont++;
                            GQManagerR._step[GQManagerR.actualStep] = true;
                        }
                    }
                    break;
            }
        }
    }
}
