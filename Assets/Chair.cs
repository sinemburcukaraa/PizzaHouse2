using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    bool control;
    void Start()
    {
        CustomerChairControl.instance.emptyChairPos.Add(this.transform);
        CustomerChairControl.instance.ChairCount.Add(this.gameObject);
        CustomerManager.instance.CreateCustomer();
    }



}
