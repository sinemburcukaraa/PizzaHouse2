using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreateObject : MonoBehaviour
{
    public bool isEmpty = true;
    bool Check;
    public Transform pos;
    public float bigScaleValue;//0.0025f
    public float smallScaleValue;//0.002f
    public string id;
    private void Start()
    {
        Check = true;
    }

    private void Update()
    {
        emptyControl();
    }
    public void emptyControl()
    {
        if (PlayerController.instance.StackObject.Contains(this.gameObject))
        {
            isEmpty = false;
            StartCoroutine(CreateNum());
        }

    }
    public float xRot, Yrot;
    public void Create()
    {
        if (!isEmpty && Check)
        {
            GameObject clone = Instantiate(this.gameObject, pos.position, Quaternion.Euler(xRot,Yrot,this.gameObject.transform.rotation.z));
            clone.transform.DOScale(new Vector3(bigScaleValue, bigScaleValue, bigScaleValue), 0.09F).OnComplete(() =>
            {
                clone.transform.DOScale(new Vector3(smallScaleValue, smallScaleValue, smallScaleValue), 0.09F).OnComplete(() =>
                {
                    clone.tag = "StackObject";
                });
            });
            isEmpty = true;
            Check = false;

        }
    }
   
    IEnumerator CreateNum()
    {
        yield return new WaitForSeconds(2f);
        Create();
        StopCoroutine(CreateNum());
    }
}
