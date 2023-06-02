using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldAreaManager : MonoBehaviour
{
    void Start()
    {
        if (this.transform.childCount!=0)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
