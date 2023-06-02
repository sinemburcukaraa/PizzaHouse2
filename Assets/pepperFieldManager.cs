using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pepperFieldManager : MonoBehaviour
{
    public List<GameObject> PepperList = new List<GameObject>();
    public static pepperFieldManager instance;

    private void Awake()
    {
      
        for (int i = 0; i < this.transform.childCount; i++)
        {
            PepperList.Add(this.transform.GetChild(i).gameObject);
        }
        if (!instance)
        {
            instance = this;
        }

     

    }
    private void Start()
    {
        if (this.transform.childCount != 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);

        }
    }

}
