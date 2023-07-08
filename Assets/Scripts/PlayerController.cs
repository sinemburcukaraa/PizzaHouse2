using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateJoystickExample;
using DG.Tweening;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public int moveSpeed = 5;
    public UltimateJoystick ultimateJoystick;
    public bool Active;
    public static PlayerController instance;
    public Animator animator;
    public Transform PlayerStackPos;
    public Transform PizzaStackPos;
    public List<GameObject> StackObject = new List<GameObject>();
    public List<GameObject> PizzaStackObject = new List<GameObject>();
    public List<GameObject> onCounterObject = new List<GameObject>();
    public float yDiff;
    Transform obj;
    int count = 0;
    public float objDistanceZ;
    public float objDistanceY;
    GameObject counterColT;
    public bool isExit;
    public int MoneyCount = 50;
    public TextMeshProUGUI MoneyText;
    public GameObject Player;
    public bool startMoneyCont = false;


    private void Start()
    {
        StartMoney();
        Player = this.gameObject;

        int prefs = PlayerPrefs.GetInt("Money");
        MoneyText.text = prefs.ToString();

    }
    public void StartMoney()
    {
        print(PlayerPrefs.GetInt("SM") == 0);
        if (PlayerPrefs.GetInt("SM") == 0)
        {
            PlayerPrefs.SetInt("Money", MoneyCount);
            PlayerPrefs.SetInt("SM", 1);
        }
    }
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    public void speedControl()
    {
        if (CustomerChairControl.instance.ChairCount.Count >= 4 && CustomerChairControl.instance.ChairCount.Count <= 6)
        {
            moveSpeed = 6;
        }
        else if (CustomerChairControl.instance.ChairCount.Count >= 7 && CustomerChairControl.instance.ChairCount.Count <= 10)
        {
            moveSpeed = 7;
        }
        else if (CustomerChairControl.instance.ChairCount.Count >= 11 && CustomerChairControl.instance.ChairCount.Count <= 12)
        {
            moveSpeed = 8;
        }
        else if (CustomerChairControl.instance.ChairCount.Count >= 13)
        {
            moveSpeed = 9;
        }
    }
    private void FixedUpdate()
    {
        if (Active)//*active && GameManager.instance.gameSituation == GameManager.GameSituation.isStarted*/ 
        {
            speedControl();
            rigidbody.velocity = new Vector3(ultimateJoystick.GetVerticalAxis() * (-moveSpeed), rigidbody.velocity.y, ultimateJoystick.GetHorizontalAxis() * (moveSpeed));
            if (ultimateJoystick.GetHorizontalAxis() != 0 || ultimateJoystick.GetVerticalAxis() != 0)
            {
                transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
                animator.SetBool("walk", true);

                //if (PizzaStackObject.Count>0)
                //{
                //    animator.SetBool("WalkingWithObj", true);
                //    animator.SetBool("idle", false);
                //    animator.SetBool("walk", false);

                //}
                //else
                //{
                //    animator.SetBool("walk", true);
                //    animator.SetBool("idle", false); animator.SetBool("WalkingWithObj", false);

                //}
            }
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
            animator.SetBool("walk", false);
            //animator.SetBool("idle",true);

        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "StackObject")
        {
            if (StackObject.Count < 5)
            {
                Ystack(other, PlayerStackPos, StackObject);
                other.gameObject.GetComponent<AudioSource>().Play();
                //other.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        //if (other.tag == "Pizza")
        //{
        //    if (PizzaStackObject.Count < 7)
        //    {
        //        other.transform.DOKill();
        //        //Ystack(other, PizzaStackPos, PizzaStackObject);
        //        obj.localRotation = Quaternion.Euler(-90, 90, 0);

        //    }

        //}
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Pizza")
    //    {
    //        if (other.gameObject.GetComponent<pizza>().id == "Domates")
    //        {
    //            pizzaMachineDomates.pizzaList.Remove(other.gameObject);
    //        }
    //        if (other.gameObject.GetComponent<pizza>().id == "mantar")
    //        {
    //            pizzaMachineMantar.pizzaList.Remove(other.gameObject);
    //        }
    //    }
    //}
    public void Ystack(Collider other, Transform StackPos, List<GameObject> StackList)//OBJELERÝN SIRTTA STACKLENMESÝ (domates)
    {
        Vector3 pos = Vector3.zero;
        int childCount = StackPos.childCount;
        float yPos = (childCount * yDiff);
        pos.y = yPos;

        obj = other.transform;
        obj.SetParent(StackPos);
        obj.tag = "Untagged";
        StackList.Add(other.gameObject);
        obj.DOLocalJump(pos, 1, 1, .5f);


    }


    public void ReSort(List<GameObject> StackObject)
    {
        for (int i = 0; i < StackObject.Count; i++)
        {
            float y = yDiff * i;
            StackObject[i].transform.DOLocalMoveY(y, 0.25f);
        }
    }
}
