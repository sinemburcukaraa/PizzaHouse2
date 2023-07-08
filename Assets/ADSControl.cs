using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GoogleMobileAds.Api;

public class ADSControl : MonoBehaviour
{

    public Image image;
    bool isExit;
    bool c = true;
    public BankManager BD;

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
            if (c)
            {
                image.DOFillAmount(1, 1).OnComplete(() =>
                {
                    MobileAds.Initialize((InitializationStatus initStatus) =>
                    {
                        BD.ShowRewardedAd();
                    });
                });
                c = false;
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
    public void Rewarded()
    {
        PlayerController.instance.MoneyCount += 50;
        PlayerPrefs.SetInt("Money", PlayerController.instance.MoneyCount);
        PlayerController.instance.MoneyText.text = PlayerPrefs.GetInt("Money").ToString();
    }
}
