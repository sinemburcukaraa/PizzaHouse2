using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pizza : MonoBehaviour
{
    public string id;
    public void Particle()
    {
        this.transform.GetChild(1).transform.gameObject.SetActive(true);
    }
}
