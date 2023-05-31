using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class PizzaStackControl : MonoBehaviour
{
    public PizzaMachine pizzaMachine;
    public Transform pizzaPos;
    public float yDiff;
    Transform obj;
    public Image image;
    bool isExit;
    bool c = true;
    private void Start()
    {
        pizzaPos = PlayerController.instance.PizzaStackPos;
            
     }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            c = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isExit = false;
            if (c && PlayerController.instance.PizzaStackObject.Count < 2 && pizzaMachine.pizzaList.Count != 0)
            {
                image.DOFillAmount(1, 1).OnComplete(() => { Control(); });
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            image.DOKill();
            image.DOFillAmount(0, 0.5f);
            isExit = true;
        }
    }
    public void Control()
    {
        if (PlayerController.instance.PizzaStackObject.Count < 2 && pizzaMachine.pizzaList.Count != 0 && !isExit)
        {
            c = false;

            image.DOKill();
            image.fillAmount = 0;
            //image.DOFillAmount(0, 0).OnComplete(() => { c = true; });
            Vector3 pos = Vector3.zero;
            int childCount = pizzaPos.childCount;
            float yPos = (childCount * yDiff);
            pos.y = yPos;
            obj = pizzaMachine.pizzaList[pizzaMachine.pizzaList.Count - 1].gameObject.transform;
            obj.gameObject.GetComponent<AudioSource>().Play();

            obj.SetParent(pizzaPos);
            obj.tag = "Untagged";
            obj.localRotation = Quaternion.Euler(-90, 90, 0);

            PlayerController.instance.PizzaStackObject.Add(obj.gameObject);
            pizzaMachine.pizzaList.Remove(obj.gameObject);
            obj.DOLocalJump(pos, 1, 1, .5f);
            c = true;

        }
    }
}
