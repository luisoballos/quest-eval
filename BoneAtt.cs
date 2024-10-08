using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneAtt : MonoBehaviour
{
    public OVRHand hand;
    public int offset;
    public Vector3 grabOffset_X;
    public Vector3 grabOffset_Y;
    public Vector3 grabOffset_Z;

    public SphereCollider ThumbGrabber;
    public SphereCollider IndexGrabber;

    [Header("Scripts")]
    public GestureDetection GestDet;
    public GrabQUESTManager GrabQ;

    void Start()
    {
        grabOffset_X = new Vector3(0.05f, 0f, 0f);
        grabOffset_Y = new Vector3(0f, 0.05f, 0f);
        grabOffset_Z = new Vector3(0f, 0f, 0.05f);
        offset = 2;
    }

    public void AttachBone(int gestureID, List<OVRBone> fingerBones, GameObject Pea, GameObject Orange, GameObject Pencil)
    {
        if (GrabQ.actualStep != 3 || GrabQ.actualStep != 5)
        {
            if (gestureID + offset == GrabQ.actualStep)
            {
                switch (gestureID)
                {
                    case 0://ThumbGrasp
                        Orange.transform.parent = fingerBones[19].Transform;
                        Orange.transform.position = fingerBones[19].Transform.position;
                        Orange.GetComponent<Rigidbody>().isKinematic = true;
                        Orange.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[1] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        offset++;
                        break;

                    case 1://PalmGrasp
                        Orange.transform.parent = fingerBones[19].Transform;
                        Orange.transform.position = fingerBones[19].Transform.position;
                        Orange.GetComponent<Rigidbody>().isKinematic = true;
                        Orange.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[1] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        offset++;
                        break;

                    case 2://RadialDigital
                        Orange.transform.parent = fingerBones[19].Transform;
                        Orange.transform.position = fingerBones[19].Transform.position;
                        Orange.GetComponent<Rigidbody>().isKinematic = true;
                        Orange.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[1] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        break;

                    case 3://RadialPalmar
                        Orange.transform.parent = fingerBones[19].Transform;
                        Orange.transform.position = fingerBones[19].Transform.position;
                        Orange.GetComponent<Rigidbody>().isKinematic = true;
                        Orange.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[1] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        break;

                    case 4://Palm
                        Orange.transform.parent = fingerBones[9].Transform;
                        Orange.transform.position = fingerBones[9].Transform.position;
                        Orange.GetComponent<Rigidbody>().isKinematic = true;
                        Orange.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[1] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        break;

                    case 5://FinePincer
                        Pea.transform.parent = fingerBones[19].Transform;
                        Pea.transform.position = fingerBones[19].Transform.position;
                        Pea.GetComponent<Rigidbody>().isKinematic = true;
                        Pea.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[0] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        break;

                    case 6://Pincer
                        Pea.transform.parent = fingerBones[19].Transform;
                        Pea.transform.position = fingerBones[19].Transform.position;
                        Pea.GetComponent<Rigidbody>().isKinematic = true;
                        Pea.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[0] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        break;

                    case 7://InferiorPincer
                        Pea.transform.parent = fingerBones[19].Transform;
                        Pea.transform.position = fingerBones[19].Transform.position;
                        Pea.GetComponent<Rigidbody>().isKinematic = true;
                        Pea.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[0] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        break;

                    case 8://Scissor
                        Pea.transform.parent = fingerBones[19].Transform;
                        Pea.transform.position = fingerBones[19].Transform.position;
                        Pea.GetComponent<Rigidbody>().isKinematic = true;
                        Pea.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[0] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        break;

                    case 9://InferiorScissor
                        Pea.transform.parent = fingerBones[5].Transform;
                        Pea.transform.position = fingerBones[5].Transform.position;
                        Pea.GetComponent<Rigidbody>().isKinematic = true;
                        Pea.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[0] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        break;

                    case 10://DynamicTripod
                        Pencil.transform.parent = fingerBones[20].Transform;
                        Pencil.transform.position = fingerBones[20].Transform.position;
                        Pencil.GetComponent<Rigidbody>().isKinematic = true;
                        Pencil.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[2] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        break;

                    case 11://StaticTripod
                        Pencil.transform.parent = fingerBones[20].Transform;
                        Pencil.transform.position = fingerBones[20].Transform.position;
                        Pencil.GetComponent<Rigidbody>().isKinematic = true;
                        Pencil.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[2] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        break;

                    case 12://DigitalPronate
                        Pencil.transform.parent = fingerBones[7].Transform;
                        Pencil.transform.position = fingerBones[7].Transform.position;
                        Pencil.GetComponent<Rigidbody>().isKinematic = true;
                        Pencil.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[2] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        break;

                    case 13://PalmarSupinate
                        Pencil.transform.parent = fingerBones[21].Transform;
                        Pencil.transform.position = fingerBones[21].Transform.position;
                        Pencil.GetComponent<Rigidbody>().isKinematic = true;
                        Pencil.GetComponent<Rigidbody>().useGravity = false;

                        GestDet.attachedObject[2] = true;
                        GrabQ._step[GrabQ.actualStep] = true;
                        break;
                }
            }
        }
    }
}