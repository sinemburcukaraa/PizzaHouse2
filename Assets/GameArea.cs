using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class GameArea : MonoBehaviour
{
    public TextMeshPro areaText;
    public int areaCount;
    public GameObject Money;
    public GameObject Prefab;
    public float bigScaleValue;
    public float smallScaleValue;
    public List<GameObject> AreaList = new List<GameObject>();


    private void Start()
    {
        for (int i = 0; i < this.transform.parent.childCount; i++)
        {
            AreaList.Add(this.transform.parent.GetChild(i).gameObject);
        }
        areaText.text = areaCount.ToString();
        AreaScaleControl();
    }

    public void AreaScaleControl()
    {
        this.gameObject.transform.DOScale(new Vector3(bigScaleValue, bigScaleValue, bigScaleValue), 0.5f).OnComplete(() =>
        {
            this.gameObject.transform.DOScale(new Vector3(smallScaleValue, smallScaleValue, smallScaleValue), 0.5f).OnComplete(() =>
            {
                AreaScaleControl();

            });
        });
    }
    bool countCheck;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            countCheck = true;
            StartCoroutine(MoneyTextControl());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            countCheck = false;
        }
    }

    IEnumerator MoneyTextControl()
    {

        yield return new WaitForSeconds(0.5f);

        while (countCheck)
        {
            yield return new WaitForSeconds(0.1f);
            if (areaCount != 0 && PlayerController.instance.MoneyCount != 0)
            {
                AreaCountControl();
            }

        }
    }

    public void AreaCountControl()
    {
        if (areaCount != 0 && PlayerController.instance.MoneyCount != 0)
        {
            //Money spawn
            GameObject cloneMoney = Instantiate(Money, PlayerController.instance.Player.transform.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 90, 0));

            cloneMoney.transform.DOMove(this.transform.position, 0.2f).OnComplete(() =>
            {
                cloneMoney.SetActive(false);
            });

            //area Count Control

            PlayerController.instance.MoneyCount -= 10;
            PlayerController.instance.MoneyText.text = PlayerController.instance.MoneyCount.ToString();

            areaCount -= 10;
            areaText.text = areaCount.ToString();
            if (areaCount == 0)
            {
                TakeObj();
                StopCoroutine(MoneyTextControl());
            }
        }
        if (areaCount == 0)
        {
            TakeObj();
            StopCoroutine(MoneyTextControl());
        }
    }
    

    public Vector3 posControl;//new Vector3(0, 0, 0.35f) chair
    public float yRot, xRot;
    public Vector3 scale;
    bool control = true;
    public bool scaleBool;
    public void TakeObj()
    {
        this.gameObject.SetActive(false);
        this.transform.SetParent(null);

        //Arealar listesi kontrolü
        if (AreaList.Count != 0)
        {
            AreaList[0].SetActive(true);
            AreaList.RemoveAt(0);
        }

        if (control)
        {
            GameObject clonePrefab = Instantiate(Prefab, this.transform.position + posControl, Quaternion.Euler(xRot, yRot, 0));
            //if (scaleBool)
            //{
            //    //scale = clonePrefab.transform.localScale;
            //    //clonePrefab.transform.localScale = new Vector3(0, 0, 0);
            //    //clonePrefab.transform.DOScale(scale, 0.1f);

            //}

            GameControl.instance.newArea();
            control = false;
        }


    }


}
