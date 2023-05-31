using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public List<GameObject> AreaList = new List<GameObject>();
    public static AreaManager instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            AreaList.Add(this.transform.GetChild(i).gameObject);
        }
    }

}
