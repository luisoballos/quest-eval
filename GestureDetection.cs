using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
//using System.IO;

[System.Serializable]

public struct Gesture
{
    public string name;
    public List<Vector3> fingerDatas;
}

public class GestureDetection : Singleton<GestureDetection>
{
    public bool debugMode;
    public int TrialNumber;
    public float threshold;
    public float sumDistGest;
    public string PatientName;
    public GameObject Pea;
    public GameObject Orange;
    public GameObject Pencil;
    public Vector3 OraOgnPos;
    public Vector3 PeaOgnPos;
    public Vector3 PenOgnPos;
    public OVRSkeleton skeleton;
    public List<Vector3> data;
    public List<Gesture> gestures;
    public List<OVRBone> fingerBones;
    public List<float> DistanceAverage;
    public List<float> PrecisionPercentage;
    public bool isDiscarded;
    public bool hasRecognized;
    public Gesture currentGesture;
    public Gesture previousGesture;
    public string path;
    public Gesture recognizedGesture;
    public bool[] attachedObject = new bool[3];

    [Header("Scripts")]
    public BoneAtt boneIdAtt;
    public GameObject FingerZone;

    // Start is called before the first frame update
    void Start()
    {
        debugMode = true;
        TrialNumber = 1;
        threshold = 0.025f;
        isDiscarded = false;
        attachedObject[0] = false;
        attachedObject[1] = false;
        attachedObject[2] = false;
        currentGesture = new Gesture();
        previousGesture = new Gesture();
        recognizedGesture = new Gesture();
        DistanceAverage = new List<float>();
        PeaOgnPos = Pea.transform.localPosition;
        OraOgnPos = Orange.transform.localPosition;
        PenOgnPos = Pencil.transform.localPosition;
        fingerBones = new List<OVRBone>(skeleton.Bones);
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (debugMode && Input.GetKeyDown(KeyCode.Space))
        {
            Save();
        }

        if (currentGesture.name == "Drop" && attachedObject[0] == true)
        {
            CheckHoldingPea();
        }
        else if (currentGesture.name == "Drop" && attachedObject[1] == true)
        {
            CheckHoldingOrange();
        }
        else if (currentGesture.name == "Drop" && attachedObject[2] == true)
        {
            CheckHoldingPencil();
        }
    }

    public void Createtxt()
    {
        path = Application.dataPath + "/" + PatientName + "_Trial_" + TrialNumber + ".txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Precision measurements\n");
        }

        if (skeleton.CompareTag("HandL"))
        {
            File.AppendAllText(path, "\nLeft hand:\n");
            File.AppendAllText(path, gestures[0].name + "\t" + DistanceAverage[0] + "\t" + PrecisionPercentage[0] + "\n");
            File.AppendAllText(path, gestures[14].name + "\t" + DistanceAverage[1] + "\t" + PrecisionPercentage[1] + "\n");
            File.AppendAllText(path, gestures[1].name + "\t" + DistanceAverage[2] + "\t"+ PrecisionPercentage[2] + "\n");
            File.AppendAllText(path, gestures[14].name + "\t" + DistanceAverage[3] + "\t" + PrecisionPercentage[3] + "\n");
            for (int i = 4; i < gestures.Count + 1; i++)
            {
                File.AppendAllText(path, gestures[i - 2].name + "\t" + DistanceAverage[i] + "\t" + PrecisionPercentage[i] + "\n");
            }
        }
        else if (skeleton.CompareTag("HandR"))
        {
            File.AppendAllText(path, "\nRight hand:\n");
            File.AppendAllText(path, gestures[0].name + "\t" + DistanceAverage[0] + "\t" + PrecisionPercentage[0] + "\n");
            File.AppendAllText(path, gestures[14].name + "\t" + DistanceAverage[1] + "\t" + PrecisionPercentage[1] + "\n");
            File.AppendAllText(path, gestures[1].name + "\t" + DistanceAverage[2] + "\t" + PrecisionPercentage[2] + "\n");
            File.AppendAllText(path, gestures[14].name + "\t" + DistanceAverage[3] + "\t" + PrecisionPercentage[3] + "\n");
            for (int i = 4; i < gestures.Count + 1; i++)
            {
                File.AppendAllText(path, gestures[i - 2].name + "\t" + DistanceAverage[i] + "\t" + PrecisionPercentage[i] + "\n");
            }
        }
    }

    private IEnumerator Wait()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            currentGesture = Recognize();
        }
    }

    public void HasCollided()
    {
        //Check if there's new gesture
        hasRecognized = !(currentGesture.Equals(new Gesture()));

        if (hasRecognized)
        {
            Debug.Log("New gesture found: " + currentGesture.name);

            //Evaluate actual gesture
            if (currentGesture.name == "ThumbGrasp" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(0, fingerBones, Pea, Orange, Pencil);
            }
            else if (currentGesture.name == "PalmGrasp" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(1, fingerBones, Pea, Orange, Pencil);
            }
            else if (currentGesture.name == "RadialDigital" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(2, fingerBones, Pea, Orange, Pencil);
            }
            else if (currentGesture.name == "RadialPalmar" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(3, fingerBones, Pea, Orange, Pencil);
            }
            else if (currentGesture.name == "Palmar" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(4, fingerBones, Pea, Orange, Pencil);
            }
            else if (currentGesture.name == "FinePincer" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(5, fingerBones, Pea, Orange, Pencil);
            }
            else if (currentGesture.name == "Pincer" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(6, fingerBones, Pea, Orange, Pencil);
            }
            else if (currentGesture.name == "InferiorPincer" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(7, fingerBones, Pea, Orange, Pencil);
            }
            else if (currentGesture.name == "Scissor" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(8, fingerBones, Pea, Orange, Pencil);
            }
            else if (currentGesture.name == "InferiorScissor" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(9, fingerBones, Pea, Orange, Pencil);
            }
            else if (currentGesture.name == "DynamicTripod" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(10, fingerBones, Pea, Orange, Pencil);
            }
            else if (currentGesture.name == "StaticTripod" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(11, fingerBones, Pea, Orange, Pencil);
            }
            else if (currentGesture.name == "DigitalPronate" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(12, fingerBones, Pea, Orange, Pencil);
            }
            else if (currentGesture.name == "PalmarSupinate" && isDiscarded == false)
            {
                boneIdAtt.AttachBone(13, fingerBones, Pea, Orange, Pencil);
            }

            previousGesture = currentGesture;
        }
    }

    void Save()
    {
        Gesture newg = new Gesture
        {
            name = "New gesture"
        };
        data = new List<Vector3>();
        fingerBones = new List<OVRBone>(skeleton.Bones);
        foreach (OVRBone bone in fingerBones)
        {
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }

        newg.fingerDatas = data;
        gestures.Add(newg);
    }

    public Gesture Recognize()
    {
        float currentMin = Mathf.Infinity;
        fingerBones = new List<OVRBone>(skeleton.Bones);

        //Compare actual gesture with each saved gesture
        foreach (var gesture in gestures)
        {
            float sumDistance = 0;
            //bool isDiscarded = false;

            //Compare bone positions
            for (int i = 0; i < fingerBones.Count; i++)
            {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentData, gesture.fingerDatas[i]);

                //if (distance > threshold)
                //{
                //    isDiscarded = true;
                //    break;
                //}

                sumDistance += distance;
            }

            sumDistance /= 24;
            if (sumDistance < threshold && sumDistance < currentMin)
            {
                sumDistGest = sumDistance;
                currentMin = sumDistance;
                recognizedGesture = gesture;
            }
        }

        return recognizedGesture;
    }

    public void CheckHoldingPea()
    {
        if (currentGesture.name == "Drop" && isDiscarded == false)
        {
            Unattach_Pea();
        }
    }

    public void CheckHoldingOrange()
    {
        if (currentGesture.name == "Drop" && isDiscarded == false)
        {
            Unattach_Orange();
        }
    }

    public void CheckHoldingPencil()
    {
        if (currentGesture.name == "Drop" && isDiscarded == false)
        {
            Unattach_Pencil();
        }
    }

    public void Unattach_Pea()
    {
        attachedObject[0] = false;
        Pea.transform.parent = FingerZone.transform;
        Pea.transform.position = PeaOgnPos;
        Pea.transform.rotation = Quaternion.Euler(0, 0, 0);
        Pea.GetComponent<Rigidbody>().isKinematic = true;
        Pea.GetComponent<Rigidbody>().useGravity = true;
    }

    public void Unattach_Orange()
    {
        attachedObject[1] = false;
        Orange.transform.parent = FingerZone.transform;
        Orange.transform.position = OraOgnPos;
        Orange.transform.rotation = Quaternion.Euler(0, 0, 0);
        Orange.GetComponent<Rigidbody>().isKinematic = true;
        Orange.GetComponent<Rigidbody>().useGravity = true;
    }

    public void Unattach_Pencil()
    {
        attachedObject[2] = false;
        Pencil.transform.parent = FingerZone.transform;
        Pencil.transform.position = PenOgnPos;
        Pencil.GetComponent<Rigidbody>().isKinematic = true;
        Pencil.GetComponent<Rigidbody>().useGravity = true;
    }
}