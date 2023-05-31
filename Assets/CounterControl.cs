using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CounterControl : MonoBehaviour
{
    public Vector3 firstPos;
    public Vector3 firstPos2;
    bool isExit;
    public List<GameObject> onCounterObject = new List<GameObject>();
    public GameObject counterColT;
    public float objDistanceZ;
    public float objDistanceX;
    public string id;
    public List<GameObject> arrangement = new List<GameObject>();
    private void Start()
    {
        counterColT = this.transform.GetChild(0).gameObject;
        firstPos = counterColT.transform.position;
        firstPos2 = counterColT.transform.position;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isExit = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isExit = true;
            StartCoroutine(RemoveObject());
        }
    }
    IEnumerator RemoveObject()//OBJELERÝ TEZGAHA BIRAKMA (domates)
    {
        while (isExit)
        {
            yield return new WaitForSeconds(0.1f);
            leaveOnCounter();
        }
    }
    public void leaveOnCounter()// OBJELERÝ TEZGAHA BIRAKMA (domates)
    {
        if (PlayerController.instance.StackObject.Count > 0 && onCounterObject.Count < 4)
        {
            //other operation
            GameObject nextObject = null;
            for (int i = PlayerController.instance.StackObject.Count - 1; i >= 0; i--) //listedeki ilk domatesi belirle ve onu nextObject e ata
            {
                if (PlayerController.instance.StackObject[i].GetComponent<CreateObject>().id == id )
                {

                    nextObject = PlayerController.instance.StackObject[i];
                    PlayerController.instance.StackObject.Remove(nextObject);
                    break;
                }
                else
                    nextObject = null;
            }
            if (nextObject != null) // nextObject bulunduysa gir
            {

                onCounterObject.Add(nextObject);
                int objCount = onCounterObject.Count-1;//4
                int mod = objCount % 2;//4
                int div = objCount / 2;//0

                firstPos = new Vector3(firstPos.x + div * 0.4f, firstPos.y, firstPos.z + mod * 0.4f);
                nextObject.transform.DOJump(firstPos, 1, 1, 1);
                firstPos = firstPos2;
                nextObject.transform.parent = null;
                nextObject.tag = "Untagged";

                PlayerController.instance.ReSort(PlayerController.instance.StackObject);
            }
        }
    }
}
