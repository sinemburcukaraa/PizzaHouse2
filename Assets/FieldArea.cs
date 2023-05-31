using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class FieldArea : MonoBehaviour
{
    public int areaCount;
    public GameObject money;
    public TextMeshPro areaText;
    public GameObject fieldPrefab;
    bool control = true;
    private void Start()
    {
  
        areaText.text = areaCount.ToString();

        AreaScaleControl();
    }
    public void AreaScaleControl()
    {
        this.gameObject.transform.DOScale(new Vector3(0.23f, 0.23f, 0.23f), 0.5f).OnComplete(() => {
            this.gameObject.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f).OnComplete(() =>
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
        while (countCheck)
        {
            yield return new WaitForSeconds(0.01f);
            AreaCountControl();

        }
    }

    public void AreaCountControl()
    {
        if (areaCount != 0 && PlayerController.instance.MoneyCount != 0)
        {
            //Money spawn
            GameObject cloneMoney = Instantiate(money, PlayerController.instance.Player.transform.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 90, 0));
            cloneMoney.transform.DOMove(this.transform.position, 0.2f).OnComplete(() =>
            {
                cloneMoney.SetActive(false);
            });

            //area Count Control

            PlayerController.instance.MoneyCount -= 1;
            PlayerController.instance.MoneyText.text = PlayerController.instance.MoneyCount.ToString();

            areaCount -= 1;
            areaText.text = areaCount.ToString();
            if (areaCount == 0)
            {
                TakeField();
                StopCoroutine(MoneyTextControl());
            }
        }
        else if (areaCount == 0)
        {
            TakeField();
            StopCoroutine(MoneyTextControl());
        }
    }
    public void TakeField()
    {
        if (control && TomatoField.instance.FieldArea.Count!=0)
        {
            //fidan oluþturma
            this.gameObject.SetActive(false);
            GameObject Field = Instantiate(fieldPrefab, this.transform.position, Quaternion.identity);

            //List Control 
            TomatoField.instance.FieldArea.RemoveAt(0); 
            if (TomatoField.instance.FieldArea.Count != 0)
            {
            TomatoField.instance.FieldArea[0].SetActive(true);
            }

            control = false;
        }

    }

}
