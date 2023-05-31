using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameControl : MonoBehaviour
{
    public int ChairCount;
    public static GameControl instance;
    public GameObject NewObjectArea;
    public List<GameObject> newAreaList = new List<GameObject>();
    public int areaC;
    bool control;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            newAreaList.Add(this.transform.GetChild(i).gameObject);
        }
    }
    public void newArea()
    {

        if (newAreaList.Count != 0)
        {
            if (CustomerChairControl.instance.ChairCount.Count == 2 && !control)//2 sandalye == 1 masa
            {
                control = true;
                this.transform.GetChild(0).gameObject.SetActive(true); newAreaList.RemoveAt(0);
                //this.transform.GetChild(1).gameObject.SetActive(true); newAreaList.RemoveAt(1);
            }
            //if (CustomerChairControl.instance.ChairCount.Count == 4 && !control1)
            //{
            //    control1 = true;
            //    this.transform.GetChild(0).gameObject.SetActive(true);
            //    //this.transform.GetChild(1).gameObject.SetActive(true);
            //}
        }


    }



}


