using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PizzaMachine : MonoBehaviour
{
    public CounterControl CounterC;
    bool check = true; GameObject LastGameobject;
    public Transform LastPos;

    public GameObject PizzaPrefab;
    public string Pizza›d;
    public List<GameObject> pizzaList = new List<GameObject>();
    public Transform PizzaPos;
    public float offset;
    private void Start()
    {
       CustomerManager.instance.FoodList.Add(Pizza›d);
    }
    private void Update()
    {
        if (CounterC.onCounterObject.Count > 0 && check)
        {
            PizzaMachineControl();
            check = false;
        }
    }
    public void PizzaMachineControl()
    {
        if (CounterC.onCounterObject.Count > 0)
        {
            StartCoroutine(pizzaCounter());
        }
        else
        {
            StopCoroutine(pizzaCounter());
        }
    }

    IEnumerator pizzaCounter()
    {
        yield return new WaitForSeconds(2f);
        check = true;

        if (pizzaList.Count<=4)
        {
            LastGameobject = CounterC.onCounterObject[CounterC.onCounterObject.Count - 1];
            CounterC.onCounterObject.Remove(LastGameobject);

            LastGameobject.transform.DOMove(LastPos.position, 0.5f).OnComplete(() =>
            {
                LastGameobject.SetActive(false);
                CreatePizza();
            });
        }
    }
    float pizzaStackPos;
    //public void pizzaListCheck()
    //{
    //    for (int i = 0; i < pizzaList.Count; i++)
    //    {
    //        if (PlayerController.instance.PizzaStackObject.Contains(pizzaList[i].gameObject))
    //        {
    //            pizzaList.Remove(pizzaList[i].gameObject);
    //        }
    //    }
    //}
    public void CreatePizza()
    {
        if (pizzaList.Count <= 4)
        {
            GameObject Pizza = Instantiate(PizzaPrefab, LastPos.position, Quaternion.Euler(-90, 90, 0));
            pizzaStackPos = PizzaPos.transform.position.y + (pizzaList.Count * offset);
            Pizza.transform.DOJump(new Vector3(PizzaPos.position.x, pizzaStackPos, PizzaPos.position.z), 1, 1, 0.5f);
            pizzaList.Add(Pizza);
        }
    }
}



