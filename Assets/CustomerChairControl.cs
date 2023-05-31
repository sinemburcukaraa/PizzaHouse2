using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerChairControl : MonoBehaviour
{
    public List<Transform> emptyChairPos = new List<Transform>();
    public List<GameObject> ChairCount = new List<GameObject>();
    public List<Transform> fullChairPos = new List<Transform>();
    public static CustomerChairControl instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

}
