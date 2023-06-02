using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pineappleAreaManager : MonoBehaviour
{
    public List<GameObject> PineappleList = new List<GameObject>();
    public static pineappleAreaManager instance;

    private void Awake()
    {

        for (int i = 0; i < this.transform.childCount; i++)
        {
            PineappleList.Add(this.transform.GetChild(i).gameObject);
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
