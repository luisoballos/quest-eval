using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;
using UnityEngine.Video;

public class GrabQUESTManager : Singleton<GrabQUESTManager>
{
    public bool[] _step;


    [Header("Items")]
    public int timesUp;
    public int actualStep = -2;
    public int TotalScore = -2;
    public int previousStep;
    public float timer;
    public float instanceTime;
    public List<bool> Score = new List<bool>();

    [Header("UI")]
    public bool calib = true;
    public TMP_Text countdown;
    public TMP_Text titlePanel;
    public TMP_Text textPanel1;
    public TMP_Text timerPanel;
    public VideoClip[] videos;
    public VideoPlayer vp;
    public GameObject OrangeG;
    public GameObject PeaG;
    public GameObject PencilG;

    //public Reticula reticula;
    private readonly Stopwatch sw = new Stopwatch();

    [Header("Hands")]
    public GameObject HandPrefab;

    [Header("Scripts")]
    public Wiggling Wig;
    public GestureDetection GDet;
    public Collide OrangeGQ;
    public Collide PeaGQ;
    public Collide PencilGQ;
    public TableTrigger TTrigger;

    public void Start()
    {
        calib = false;
    }

    void Update()
    {
        if (calib && timer >= 0)
        {
            timer -= Time.deltaTime;
            timerPanel.text = "Time left: \n" + (int)timer + " seconds.";
        }
    }

    public void StartFunction()
    {
        countdown.text = "Hi. Welcome!";
        _step = new bool[18];
        timesUp = 21;
        timer = timesUp;
        instanceTime = 2.0f;
        SetFingerColliders();
        //reticula.gameObject.SetActive(false);

        foreach (bool step in _step)
        {
            step.Equals(false);
        }

        StartCoroutine(StartGrabEvaluation());
    }

    IEnumerator StartGrabEvaluation()
    {
        ResetEvaluation();
        SetFingerColliders();
        OrangeG.SetActive(false);
        PeaG.SetActive(false);
        PencilG.SetActive(false);
        yield return new WaitForSeconds(4f);
        countdown.text = "LET'S BEGIN!";
        yield return new WaitForSeconds(4f);
        countdown.text = " ";
        calib = true;

        //////Independent finger wiggling
        ChangePanelInfo();
        SetFingerColliders();
        Debug.Log("Grab: " + actualStep + " " + _step[actualStep]);
        Wig.enableIsWig = true;

        timer = timesUp;
        yield return new WaitUntil(() => TTrigger.IndexCheck == true || timer <= 0);
        Debug.Log("Finger");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => TTrigger.Wigcont == 2 || timer <= 0);
        Debug.Log("Finger");

        Wig.enableIsWig = false;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Grab: " + actualStep + " " + _step[actualStep]);
        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        Score.Add(_step[actualStep]);

        //Independent thumb movement
        ChangePanelInfo();
        SetFingerColliders();
        Debug.Log("Grab: " + actualStep + " " + _step[actualStep]);
        Wig.enableIsWig = true;
        Wig.sphereBone[0].GetComponent<SphereCollider>().enabled = true;

        timer = timesUp;
        yield return new WaitUntil(() => TTrigger.ThumbCheck == true || timer <= 0);
        Debug.Log("Finger");
        TTrigger.ThumbCheck = false;
        yield return new WaitForSeconds(0.5f);
        TTrigger.Thumbcont++;
        timer = timesUp;
        yield return new WaitUntil(() => TTrigger.Thumbcont == 2 || timer <= 0);
        Debug.Log("Finger");

        Wig.enableIsWig = false;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Grab: " + actualStep + " " + _step[actualStep]);
        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        Score.Add(_step[actualStep]);

        //Grasp using thumb
        ChangePanelInfo();
        OrangeG.SetActive(true);
        Debug.Log("Grab: " + actualStep + " " + _step[actualStep]);
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        Score.Add(_step[actualStep]);

        //Release from thumb
        ChangePanelInfo();
        Debug.Log("Grab: " + actualStep + " " + _step[actualStep]);
        timer = timesUp;
        countdown.text = "Drop it.";
        yield return new WaitUntil(() => GDet.currentGesture.name == "Drop" || timer <= 0);

        ScoreRelease();
        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Orange();
        Score.Add(_step[actualStep]);

        //Grasp using palm
        ChangePanelInfo();
        Debug.Log("Grab: " + actualStep + " " + _step[actualStep]);
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        Score.Add(_step[actualStep]);

        //Release from palm
        ChangePanelInfo();
        Debug.Log("Grab: " + actualStep + " " + _step[actualStep]);
        timer = timesUp;
        countdown.text = "Drop it.";
        yield return new WaitUntil(() => GDet.currentGesture.name == "Drop" || timer <= 0);

        ScoreRelease();
        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Orange();
        Score.Add(_step[actualStep]);

        //Radial digital
        ChangePanelInfo();
        Debug.Log("Grab: " + actualStep + " " + _step[actualStep]);
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Orange();
        Score.Add(_step[actualStep]);

        //RadialPalmar
        ChangePanelInfo();
        Debug.Log("Grab: " + actualStep + " " + _step[actualStep]);
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Orange();
        Score.Add(_step[actualStep]);

        //Palmar
        ChangePanelInfo();
        Debug.Log("Grab: " + actualStep + " " + _step[actualStep]);
        SetFingerColliders();
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Orange();
        Score.Add(_step[actualStep]);

        //FinePincer
        ChangePanelInfo();
        SetFingerColliders();
        Debug.Log("Grasp: " + actualStep + " " + _step[actualStep]);
        OrangeG.SetActive(false);
        PeaG.SetActive(true);
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Pea();
        Score.Add(_step[actualStep]);

        //Pincer
        ChangePanelInfo();
        Debug.Log("Grasp: " + actualStep + " " + _step[actualStep]);
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Pea();
        Score.Add(_step[actualStep]);

        //InferiorPincer
        ChangePanelInfo();
        Debug.Log("Grap: " + actualStep + " " + _step[actualStep]);
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Pea();
        Score.Add(_step[actualStep]);

        //Scissor
        ChangePanelInfo();
        Debug.Log("Grasp: " + actualStep + " " + _step[actualStep]);
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Pea();
        Score.Add(_step[actualStep]);

        //InferiorScissor
        ChangePanelInfo();
        Debug.Log("Grasp: " + actualStep + " " + _step[actualStep]);
        SetFingerColliders();
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Pea();
        Score.Add(_step[actualStep]);

        //DynamicTripod
        ChangePanelInfo();
        SetFingerColliders();
        Debug.Log("Grasp: " + actualStep + " " + _step[actualStep]);
        PeaG.SetActive(false);
        PencilG.SetActive(true);
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Pencil();
        PencilGQ.transform.rotation = Quaternion.Euler(0, 0, 90);
        Score.Add(_step[actualStep]);

        //StaticTripod
        ChangePanelInfo();
        Debug.Log("Grasp: " + actualStep + " " + _step[actualStep]);
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Pencil();
        PencilGQ.transform.rotation = Quaternion.Euler(-45, 90, 0);
        Score.Add(_step[actualStep]);

        //DigitalPronate
        ChangePanelInfo();
        Debug.Log("Grasp: " + actualStep + " " + _step[actualStep]);
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Pencil();
        PencilGQ.transform.rotation = Quaternion.Euler(-45, 90, 0);
        Score.Add(_step[actualStep]);

        //PalmarSupinate
        ChangePanelInfo();
        Debug.Log("Grasp: " + actualStep + " " + _step[actualStep]);
        SetFingerColliders();
        timer = timesUp;
        yield return new WaitUntil(() => _step[actualStep] == true || timer <= 0);

        Congratulate();
        SetFingerColliders();
        yield return new WaitForSeconds(instanceTime);
        countdown.text = " ";
        GDet.Unattach_Pencil();
        PencilGQ.transform.rotation = Quaternion.Euler(0, 0, 90);
        Score.Add(_step[actualStep]);

        //End
        countdown.text = "FINISHED!";
        yield return new WaitForSeconds(1.0f);
        EndGrabFood();
        yield return new WaitForSeconds(instanceTime);
    }

    public void EndGrabFood()
    {
        StopCoroutine("StartGrabEvaluation");
        calib = false;
        vp.Stop();
        actualStep++;
        GDet.Createtxt();
        SetFingerColliders();
        ResetEvaluation();
        Evaluate();
    }

    public void Evaluate()
    {
        for (int i = 0; i < Score.Count; i++)
        {
            if (Score[i] == true)
            {
                TotalScore++;
            }
        }
        countdown.text = "Total score: " + TotalScore + "/18";
    }

    public void ScoreRelease()
    {
        if (_step[actualStep - 1] == true && timer <= 0)
        {
            _step[actualStep] = false;
        }
        else if (_step[actualStep - 1] == true && GDet.attachedObject[1] == false)
        {
            _step[actualStep] = true;
        }
        else if (_step[actualStep - 1] == false && GDet.currentGesture.name == "Drop")
        {
            _step[actualStep] = true;//if no grab on 2, still can realease on 3
        }
    }

    public void Congratulate()
    {
        if (_step[actualStep] == true)
        {
            countdown.text = "WELL DONE!";
            if (!(actualStep == 0 || actualStep == 1))
            {
                GDet.PrecisionPercentage.Add((Mathf.Abs(GDet.sumDistGest - GDet.threshold) / GDet.threshold) *100);
                GDet.DistanceAverage.Add(GDet.sumDistGest);
            }
        }
        else if (_step[actualStep] == false)
        {
            countdown.text = "Ups! Time's up...";
            GDet.PrecisionPercentage.Add(0);
            GDet.DistanceAverage.Add(0);
        }
    }

    private void ResetEvaluation()
    {
        actualStep = -1;
        previousStep = -1;
        //reticula.gameObject.SetActive(false);
        titlePanel.text = "Finger Items";
        textPanel1.text = " ";
        StopCoroutine(PlayGif());

        foreach (bool step in _step)
        {
            step.Equals(false);
        }
    }

    public void SetFingerColliders()
    {
        switch (actualStep)
        {
            case -1:
                Wig.sphereBone[0].GetComponent<SphereCollider>().enabled = false;
                Wig.sphereBone[1].GetComponent<SphereCollider>().enabled = false;
                Wig.sphereBone[2].GetComponent<SphereCollider>().enabled = false;
                Wig.sphereBone[3].GetComponent<SphereCollider>().enabled = false;
                Wig.sphereBone[4].GetComponent<SphereCollider>().enabled = false;
                break;
            case 0:
                Wig.sphereBone[0].GetComponent<SphereCollider>().enabled = false;
                Wig.sphereBone[1].GetComponent<SphereCollider>().enabled = true;
                Wig.sphereBone[2].GetComponent<SphereCollider>().enabled = true;
                Wig.sphereBone[3].GetComponent<SphereCollider>().enabled = true;
                Wig.sphereBone[4].GetComponent<SphereCollider>().enabled = true;
                break;
            case 1:
                Wig.sphereBone[0].GetComponent<SphereCollider>().enabled = true;
                Wig.sphereBone[1].GetComponent<SphereCollider>().enabled = false;
                Wig.sphereBone[2].GetComponent<SphereCollider>().enabled = false;
                Wig.sphereBone[3].GetComponent<SphereCollider>().enabled = false;
                Wig.sphereBone[4].GetComponent<SphereCollider>().enabled = false;
                break;
            case 8:
                Wig.sphereBone[0].GetComponent<SphereCollider>().enabled = false;
                Wig.sphereBone[1].GetComponent<SphereCollider>().enabled = true;
                break;
            case 9:
                Wig.sphereBone[0].GetComponent<SphereCollider>().enabled = true;
                Wig.sphereBone[1].GetComponent<SphereCollider>().enabled = false;
                break;
            case 18:
                Wig.sphereBone[0].GetComponent<SphereCollider>().enabled = false;
                Wig.sphereBone[1].GetComponent<SphereCollider>().enabled = false;
                Wig.sphereBone[2].GetComponent<SphereCollider>().enabled = false;
                Wig.sphereBone[3].GetComponent<SphereCollider>().enabled = false;
                Wig.sphereBone[4].GetComponent<SphereCollider>().enabled = false;
                break;
        }
    }

    public void ChangePanelInfo()
    {
        actualStep++;

        if (!(actualStep == 3 || actualStep == 5) && (GDet.attachedObject[0] == true || GDet.attachedObject[1] == true || GDet.attachedObject[2] == true))
        {
            GDet.Unattach_Orange();
            GDet.Unattach_Pea();
            GDet.Unattach_Pencil();
        }

        if (actualStep < _step.Length)
            StartCoroutine(PlayGif());
        switch (actualStep)
        {
            case 0:
                titlePanel.text = "\"Independent finger wiggling\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                break;
            case 1:
                titlePanel.text = "\"Independent thumb movement\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                break;
            case 2:
                titlePanel.text = "\"Grasp using thumb\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                OrangeGQ.cont++;
                break;
            case 3:
                titlePanel.text = "\"Release from thumb\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                OrangeGQ.cont++;
                break;
            case 4:
                titlePanel.text = "\"Grasp using palm\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                OrangeGQ.cont++;
                break;
            case 5:
                titlePanel.text = "\"Release from palm\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                OrangeGQ.cont++;
                break;
            case 6:
                titlePanel.text = "\"Radial digital\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                OrangeGQ.cont++;
                break;
            case 7:
                titlePanel.text = "\"Radial palmar\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                OrangeGQ.cont++;
                break;
            case 8:
                titlePanel.text = "\"Palmar\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                OrangeGQ.cont++;
                break;
            case 9:
                titlePanel.text = "\"Fine pincer\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                PeaGQ.cont++;
                break;
            case 10:
                titlePanel.text = "\"Pincer\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                PeaGQ.cont++;
                break;
            case 11:
                titlePanel.text = "\"Inferior pincer\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                PeaGQ.cont++;
                break;
            case 12:
                titlePanel.text = "\"Scissor\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                PeaGQ.cont++;
                break;
            case 13:
                titlePanel.text = "\"Inferior scissor\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                PeaGQ.cont++;
                break;
            case 14:
                titlePanel.text = "\"Dynamic tripod\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                PencilGQ.cont++;
                break;
            case 15:
                titlePanel.text = "\"Static tripod\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                PencilGQ.cont++;
                break;
            case 16:
                titlePanel.text = "\"Digital pronate\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                PencilGQ.cont++;
                break;
            case 17:
                titlePanel.text = "\"Palmar supinate\"";
                textPanel1.text = "Perform the following gesture:";
                previousStep = actualStep;
                PencilGQ.cont++;
                break;
        }
    }

    public IEnumerator PlayGif()
    {
        while (actualStep != previousStep)
        {
            switch (actualStep)
            {
                case 0:
                    vp.clip = videos[0];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 1:
                    vp.clip = videos[1];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 2:
                    vp.clip = videos[2];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 3:
                    vp.clip = videos[3];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 4:
                    vp.clip = videos[4];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 5:
                    vp.clip = videos[5];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 6:
                    vp.clip = videos[6];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 7:
                    vp.clip = videos[7];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 8:
                    vp.clip = videos[8];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 9:
                    vp.clip = videos[9];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 10:
                    vp.clip = videos[10];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 11:
                    vp.clip = videos[11];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 12:
                    vp.clip = videos[12];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 13:
                    vp.clip = videos[13];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 14:
                    vp.clip = videos[14];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 15:
                    vp.clip = videos[15];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 16:
                    vp.clip = videos[16];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 17:
                    vp.clip = videos[17];
                    vp.Play();
                    yield return new WaitForSeconds(0.8f);
                    break;
            }

            //Countdown
            //Activar canvas posicion partida
            //reticula.gameObject.SetActive(true);
            //reticula.enabled = true;
            //countdown.text = "3";
            //yield return new WaitForSeconds(1f);
            //reticula.enabled = false;
            //reticula.enabled = true;
            //countdown.text = "2";
            //yield return new WaitForSeconds(1f);
            //reticula.enabled = false;
            //reticula.enabled = true;
            //countdown.text = "1";
            //yield return new WaitForSeconds(1f);
            //reticula.enabled = false;
            //reticula.enabled = true;
            //countdown.text = "0";
            //yield return new WaitForSeconds(1f);
            //reticula.enabled = false;
            //reticula.gameObject.SetActive(false);
            //countdown.text = "";
            //yield return new WaitForSeconds(1f);
            //Calibration();
        }
    }
}