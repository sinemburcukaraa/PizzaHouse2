using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
public class Customer : MonoBehaviour
{
    NavMeshAgent navMesh;
    public TextMeshPro foodText;
    public Animator animator;
    int randomChair;
    GameObject destinationObj;
    public string CustomerId;
    public GameObject customerPizzaPos;
    public bool customerHasPizza;
    public Transform startPos;
    public bool customerLeaveControl;
    public GameObject sprite;
    public List<GameObject> SpriteList = new List<GameObject>();
    public GameObject OrderBaloon;
    public Slider sliderTimer;
    bool RemoveCus;
    public bool CorrectOrWrongPizza;//false=wrong , true=correct
    bool sliderControlBool = true;
    public bool sliderBool;
    Order order;
    private void Start()
    {
        startPos = this.transform;
        navMesh = this.GetComponent<NavMeshAgent>();
        CustomerId = foodText.text;
    }
    private void Update()
    {
        if (RemoveCus)
        {
            float distance = Vector3.Distance(CustomerManager.instance.CustomerPos.position, this.transform.position);

            if (distance < 0.6f)
            {
                this.gameObject.SetActive(false);
                RemoveCus = false;
            }
        }

    }
    public void CustomerMove()//müþterinin hareket ve sandalye seçim kýsmý
    {
        DOVirtual.DelayedCall(.5f, () =>
        {
            SpriteControl();
            //Customer move with navmesh
            randomChair = Random.Range(0, CustomerChairControl.instance.emptyChairPos.Count);
            navMesh.destination = CustomerChairControl.instance.emptyChairPos[randomChair].transform.position;
            destinationObj = CustomerChairControl.instance.emptyChairPos[randomChair].gameObject;

            //Empty and Full list Control
            order = CustomerChairControl.instance.emptyChairPos[randomChair].parent.GetComponent<Order>();

            CustomerChairControl.instance.emptyChairPos[randomChair].parent.GetComponent<Order>().CustomerOnChair.Add(this.gameObject);//customer listesi
            CustomerChairControl.instance.fullChairPos.Add(CustomerChairControl.instance.emptyChairPos[randomChair]);
            CustomerChairControl.instance.emptyChairPos.RemoveAt(randomChair);

        });
    }

    public void SpriteControl()
    {

        for (int i = 0; i < SpriteList.Count; i++)
        {
            if (CustomerId == SpriteList[i].GetComponent<SpriteId>().id)
            {
                sprite.GetComponent<SpriteRenderer>().sprite = SpriteList[i].GetComponent<SpriteRenderer>().sprite;
            }
        }
    }
    public void customerLeft()//müþteri ilk pozisyonuna navmesh ile geri döner
    {
        navMesh.enabled = true;
        navMesh.destination = CustomerManager.instance.CustomerPos.position;
        animator.SetBool("SittingAngry", false);
        animator.SetBool("SittingTalking", false);
        CustomerChairControl.instance.fullChairPos.Remove(destinationObj.transform);
        CustomerChairControl.instance.emptyChairPos.Add(destinationObj.transform);
        CustomerManager.instance.StartCoroutine(CustomerManager.instance.RandomSeconds(CustomerManager.instance.Value()));
        RemoveCus = true;
    }
    private void OnTriggerEnter(Collider other)//müþterinin sandalyeye oturma kýsmý
    {
        if (other.tag == "Chair")
        {
            if (destinationObj == other.gameObject)
            {
                animator.SetBool("SittingTalking", true);//+90rot

                this.transform.DOMove(other.transform.GetChild(0).transform.position, 1).OnComplete(() =>
                {
                    this.gameObject.GetComponent<Collider>().enabled = false;
                    navMesh.enabled = false;
                });

                this.transform.DOLocalRotate(other.transform.GetChild(0).transform.rotation.eulerAngles, 1, RotateMode.FastBeyond360).OnComplete(() =>
                {
                    customerHasPizza = true;
                });

                if (sliderControlBool)
                {
                    sliderControlBool = false;
                    OrderBaloon.SetActive(true);
                    SliderControl();
                }
            }
        }
    }

    public void SliderControl()// problem ne?? bazen pizza veremiyorum onu çöz
    {
        sliderTimer.DOValue(0, 10, snapping: false).OnComplete(() =>
        {
            sliderTimer.gameObject.SetActive(false);
            order.StartCoroutine(order.CustomerLeave(this.gameObject, OrderBaloon));
            sprite.GetComponent<SpriteRenderer>().sprite = order.angry;
            customerHasPizza = false;
        });
    }

}
