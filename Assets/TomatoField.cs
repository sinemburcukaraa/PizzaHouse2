using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoField : MonoBehaviour
{
    public List<GameObject> FieldArea = new List<GameObject>();
    public static TomatoField instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
 



}
