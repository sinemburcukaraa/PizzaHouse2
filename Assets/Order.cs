using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using Sirenix.OdinInspector;

public class Order : MonoBehaviour
{
    public List<GameObject> CustomerOnChair = new List<GameObject>();
    int pizzalistCount;
    int CustomerlistCount;
    public bool hasPizza;
    public GameObject money;
    public Transform tablePos;
    public List<GameObject> CustomerMoneyList = new List<GameObject>(); float a;
    bool moneyControlbool;
    public Sprite happy;
    public Sprite angry;
    public static Order instance;
    public int id = 0;


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")// müþteriye doðru pizzanýn verildiði kontrol kýsým
        {
            CustomerRequests();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            NumCheck = true;
            TakeCustomerMoney(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            NumCheck = false;
        }
    }
    public void TakeCustomerMoney(Collider other) //müþteri parasýný alma
    {
        if (CustomerMoneyList.Count > 0)
        {
            GameObject lastMoney = CustomerMoneyList[CustomerMoneyList.Count - 1];
            CustomerMoneyList.Remove(lastMoney);
            lastMoney.transform.DOMove(other.transform.position + new Vector3(0, 0.9f, 0), 0.1f).OnComplete(() =>
               {
                   lastMoney.SetActive(false);
                   PlayerController.instance.MoneyCount += 10;
                   PlayerController.instance.MoneyText.text = PlayerController.instance.MoneyCount.ToString();
               });

            StartCoroutine(TakeMoneyNum(other));
        }
        else
            NumCheck = false;
    }
    public bool NumCheck;
    IEnumerator TakeMoneyNum(Collider other)
    {
        while (NumCheck)
        {
            this.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            TakeCustomerMoney(other);
            if (moneyControlbool)
            {
                moneyControlbool = false;
            }
        }

    }

    public void CustomerRequests()//müþteri Ýstekleri 
    {
        if (CustomerOnChair.Count != 0 && PlayerController.instance.PizzaStackObject.Count != 0)
        {
            pizzalistCount = PlayerController.instance.PizzaStackObject.Count;
            CustomerlistCount = CustomerOnChair.Count;
            for (int i = pizzalistCount - 1; i >= 0; i--)
            {
                if (PlayerController.instance.PizzaStackObject[i].GetComponent<pizza>().id == CustomerOnChair[0].GetComponent<Customer>().CustomerId && CustomerOnChair[0].GetComponent<Customer>().customerHasPizza)
                {
                    CustomerOnChair[0].GetComponent<Customer>().customerHasPizza = false;
                    CustomerOnChair[0].GetComponent<Customer>().CorrectOrWrongPizza = true;
                    GivePizza(0, i);
                    break;
                }
                if (CustomerOnChair.Count > 1)
                {
                    if (PlayerController.instance.PizzaStackObject[i].GetComponent<pizza>().id == CustomerOnChair[1].GetComponent<Customer>().CustomerId && CustomerOnChair[1].GetComponent<Customer>().customerHasPizza)
                    {
                        CustomerOnChair[1].GetComponent<Customer>().customerHasPizza = false;
                        CustomerOnChair[1].GetComponent<Customer>().CorrectOrWrongPizza = true;
                        GivePizza(1, i);
                        break;
                    }
                }
               
                if (i == 0)
                {
                    if (PlayerController.instance.PizzaStackObject[i].GetComponent<pizza>().id != CustomerOnChair[0].GetComponent<Customer>().CustomerId && CustomerOnChair[0].GetComponent<Customer>().customerHasPizza)
                    {
                        CustomerOnChair[0].GetComponent<Customer>().customerHasPizza = false;
                        CustomerOnChair[0].GetComponent<Customer>().CorrectOrWrongPizza = false;
                        GivePizza(0, i);
                        break;
                    }
                    if (CustomerOnChair.Count > 1)
                    {
                        if (PlayerController.instance.PizzaStackObject[i].GetComponent<pizza>().id != CustomerOnChair[1].GetComponent<Customer>().CustomerId && CustomerOnChair[1].GetComponent<Customer>().customerHasPizza)
                        {
                            CustomerOnChair[1].GetComponent<Customer>().customerHasPizza = false;
                            CustomerOnChair[1].GetComponent<Customer>().CorrectOrWrongPizza = false;
                            GivePizza(1, i);
                            break;
                        }
                    }
                }
            }
        }
    }
    GameObject removeObj;
    public void GivePizza(int index, int i)// müþteriye pizza verme 
    {
        CustomerOnChair[index].GetComponent<Customer>().sliderTimer.DOKill();
        CustomerOnChair[index].GetComponent<Customer>().sliderTimer.gameObject.SetActive(false);
        PlayerController.instance.PizzaStackObject[i].gameObject.GetComponent<AudioSource>().Play();

        Transform pos = CustomerOnChair[index].GetComponent<Customer>().customerPizzaPos.transform;
        PlayerController.instance.PizzaStackObject[i].transform.DOJump(pos.position, 1, 1, 1);
        PlayerController.instance.PizzaStackObject[i].transform.rotation = Quaternion.Euler(-90, 90, 0);

        if (CustomerOnChair[index].GetComponent<Customer>().CorrectOrWrongPizza)
        {
            CustomerOnChair[index].GetComponent<Customer>().sprite.GetComponent<SpriteRenderer>().sprite = happy;
        }
        else
        {
            CustomerOnChair[index].GetComponent<Customer>().sprite.GetComponent<SpriteRenderer>().sprite = angry;
        }
        //CustomerOnChair[index].GetComponent<Customer>().OrderBaloon.SetActive(false);

        PlayerController.instance.PizzaStackObject[i].transform.parent = null;
        removeObj = PlayerController.instance.PizzaStackObject[i];
        PlayerController.instance.PizzaStackObject.Remove(PlayerController.instance.PizzaStackObject[i]);
        PlayerController.instance.ReSort(PlayerController.instance.PizzaStackObject);

        StartCoroutine(CustomerLeave(CustomerOnChair[index], removeObj));

    }
    public IEnumerator CustomerLeave(GameObject customer, GameObject removeObj)//yemek yedikten sonra müþterinin ayrýlma kýsmý
    {
        if (customer.GetComponent<Customer>().CorrectOrWrongPizza)
        {
            yield return new WaitForSeconds(10);
            customer.GetComponent<Customer>().customerLeaveControl = true;
            moneyControl(customer);


            yield return new WaitForSeconds(0.1f);
            customer.GetComponent<Customer>().customerLeft();
            CustomerOnChair.Remove(customer);
            removeObj.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            //customer.GetComponent<Customer>().animator.SetBool("SittingTalking", false);//+90rot
            customer.GetComponent<Customer>().animator.SetBool("SittingAngry", true);//+90rot
            PlayerController.instance.MoneyCount -= 10;
            PlayerController.instance.MoneyText.text = PlayerController.instance.MoneyCount.ToString();

            yield return new WaitForSeconds(2f);
            customer.GetComponent<Customer>().customerLeaveControl = true;
            customer.GetComponent<Customer>().customerLeft();
            CustomerOnChair.Remove(customer);
            removeObj.SetActive(false);

        }
    }


    public void moneyControl(GameObject customer)// müþterinin para ödeme sistemi
    {
        for (int i = 0; i < 2; i++)//2 = býrakýlacak para sayýsý
        {
            GameObject Money = Instantiate(money, customer.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            float PosY = tablePos.position.y + (CustomerMoneyList.Count * 0.25f);
            Money.transform.DOJump(new Vector3(tablePos.position.x, PosY, tablePos.position.z), 1, 1, 1);
            CustomerMoneyList.Add(Money);
        }
    }
}
//public void CustomerRequests()//müþteri Ýstekleri 
//{
//    if (CustomerOnChair.Count != 0 && PlayerController.instance.PizzaStackObject.Count != 0)
//    {
//        pizzalistCount = PlayerController.instance.PizzaStackObject.Count;
//        CustomerlistCount = CustomerOnChair.Count;
//        for (int i = pizzalistCount - 1; i >= 0; i--)
//        {
//            if (PlayerController.instance.PizzaStackObject[i].GetComponent<pizza>().id == CustomerOnChair[0].GetComponent<Customer>().CustomerId && CustomerOnChair[0].GetComponent<Customer>().customerHasPizza)
//            {
//                GivePizza(0, i);
//                CustomerOnChair[0].GetComponent<Customer>().customerHasPizza = false;
//                break;
//            }

//            if (CustomerOnChair.Count == 1)
//                break;
//            else
//            {
//                if (PlayerController.instance.PizzaStackObject[i].GetComponent<pizza>().id == CustomerOnChair[1].GetComponent<Customer>().CustomerId && CustomerOnChair[1].GetComponent<Customer>().customerHasPizza)
//                {
//                    GivePizza(1, i);
//                    CustomerOnChair[1].GetComponent<Customer>().customerHasPizza = false;
//                    break;
//                }
//            }
//        }
//    }
//}