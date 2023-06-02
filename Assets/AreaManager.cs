using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AreaManager : MonoBehaviour
{
    public List<GameObject> AreaList = new List<GameObject>();
    public List<GameObject> CoounterList = new List<GameObject>();
    public List<GameObject> FieldList = new List<GameObject>();
    public List<GameObject> FieldAreaList = new List<GameObject>();
    public List<GameObject> MushroomAreaList = new List<GameObject>();

    public static AreaManager instance;
    public bool win;
    public GameObject gamePanel;
    bool winbool;
    public void CreatePlayerPrefs() 
    {

        for (int i = 0; i < PlayerPrefs.GetInt("Chair"); i++)
        {
            AreaList[i].SetActive(true);
            AreaList[i].GetComponent<GameArea>().TakeObj();
        }
        //AreaList[PlayerPrefs.GetInt("Chair") + 1].SetActive(true);

        for (int i = 0; i < PlayerPrefs.GetInt("Field"); i++)
        {
            FieldList[i].SetActive(true);
            FieldList[i].GetComponent<GameArea>().TakeObj();
        }

        for (int i = 0; i < PlayerPrefs.GetInt("Counter"); i++)
        {
            CoounterList[i].SetActive(true);
            CoounterList[i].GetComponent<GameArea>().TakeObj();
        }

        for (int i = 0; i < PlayerPrefs.GetInt("FieldArea"); i++)
        {
            FieldAreaList[i].SetActive(true);
            FieldAreaList[i].GetComponent<GameArea>().TakeObj();
        }

        for (int i = 0; i < PlayerPrefs.GetInt("MushroomArea"); i++)
        {
            MushroomAreaList[i].SetActive(true);
            MushroomAreaList[i].GetComponent<GameArea>().TakeObj();
        }

        for (int i = 0; i < PlayerPrefs.GetInt("pepperArea"); i++)
        {
            pepperFieldManager.instance.PepperList[i].SetActive(true);
            pepperFieldManager.instance.PepperList[i].GetComponent<GameArea>().TakeObj();
        }

        for (int i = 0; i < PlayerPrefs.GetInt("PineappleArea"); i++)
        {
            pineappleAreaManager.instance.PineappleList[i].SetActive(true);
            pineappleAreaManager.instance.PineappleList[i].GetComponent<GameArea>().TakeObj();
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        for (int i = 0; i < this.transform.childCount; i++)
        {
            AreaList.Add(this.transform.GetChild(i).gameObject);
        }
        CreatePlayerPrefs();

    }

    private void Start()
    {
        if (this.transform.childCount!=0)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        DOVirtual.DelayedCall(0.5f, () =>
        {
        });
    }
    private void Update()
    {
        if (this.transform.childCount==0 && PlayerController.instance.MoneyCount>=1000 && !winbool)
        {
            winbool = true;
            UIManager.instance.OpenWinPanel();
            gamePanel.SetActive(false);
        }

    }
}
