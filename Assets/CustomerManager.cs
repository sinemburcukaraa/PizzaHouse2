using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.AI;

public class CustomerManager : MonoBehaviour
{
    public GameObject CustomerPrefab;
    public Transform CustomerPos;
    public static CustomerManager instance;
    public List<string> FoodList = new List<string>();

    //private void Start()
    //{
    //    FoodList.Add("Domates");
    //    FoodList.Add("mantar");
    //}

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    public GameObject newCustomer;
    public void CreateCustomer()
    {
        //call a customer if there is an empty 
        if (CustomerChairControl.instance.emptyChairPos.Count != 0)
        {
            newCustomer = Instantiate(CustomerPrefab, CustomerPos.position, Quaternion.identity);
            Customer Customer = newCustomer.GetComponent<Customer>();
            int randomfood = Random.Range(0, FoodList.Count);
            Customer.foodText.text = FoodList[randomfood];
            Customer.CustomerMove();
        }

    }

    public int Value()
    {
        int random = Random.Range(3, 10);
        return random;
    }
    public IEnumerator RandomSeconds(int randomValue)
    {
        yield return new WaitForSeconds(randomValue);
        CustomerManager.instance.CreateCustomer();
    }


}
