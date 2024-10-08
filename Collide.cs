using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : Singleton<Collide>
{
    public int contL;
    public int contR;
    public int cont;

    public GameObject LThumb;
    public GameObject RThumb;
    public GameObject LIndex;
    public GameObject RIndex;
    public GameObject GOb;

    [Header("Scripts")]
    public GestureDetection[] GD;
    public GrabQUESTManager GQuestL;
    public GrabQUESTManager GQuestR;

    public void Start()
    {
        cont = 1;

        if (GOb.name == "Pea")
        {
            cont = 9;
        }
        else if (GOb.name == "Pencil")
        {
            cont = 14;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LThumb") || other.CompareTag("LIndex"))
        {
            RThumb.gameObject.SetActive(false);
            RIndex.gameObject.SetActive(false);
            GD[1].gameObject.SetActive(false);

            GD[0].HasCollided();
        }

        else if (other.CompareTag("RThumb") || other.CompareTag("RIndex"))
        {
            LThumb.gameObject.SetActive(false);
            LIndex.gameObject.SetActive(false);
            GD[0].gameObject.SetActive(false);

            GD[1].HasCollided();
        }
    }
}