using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggling : MonoBehaviour
{
    public int counter;
    public bool enableGFP;
    public bool enableIsWig;
    public List<OVRBone> fingerBones2;
    public SphereCollider[] sphereBone = new SphereCollider[5];

    [Header("Scripts")]
    public GrabQUESTManager GrabQMan;
    public OVRSkeleton skton;
    public TableTrigger TableT;

    public void Start()
    {
        enableGFP = true;
        enableIsWig = false;
    }

    public void Update()
    {
        if(GrabQMan.actualStep == -1 && enableGFP == true)
        {
            fingerBones2 = new List<OVRBone>(skton.Bones);
            
            for(int i = 0; i < sphereBone.Length; i++)
            {
                fingerBones2 = new List<OVRBone>(skton.Bones);

                sphereBone[i].transform.parent = fingerBones2[19 + i].Transform;
                sphereBone[i].transform.position = fingerBones2[19 + i].Transform.position;
            }
            enableGFP = false;
        }

        if(enableIsWig == true)
        {
            switch(GrabQMan.actualStep)
            {
                case 0:
                    if (TableT.Thumbcont == 2)
                    {
                        GrabQMan._step[GrabQMan.actualStep] = true;
                    }
                    break;
                case 1:
                    if (TableT.Thumbcont == 2)
                    {
                        GrabQMan._step[GrabQMan.actualStep] = true;
                    }
                    break;
            }
        }
    }
}