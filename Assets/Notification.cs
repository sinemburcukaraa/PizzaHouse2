using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Notification : MonoBehaviour
{
    public static Notification instance;
    public Image NotificationPanel;
    public TextMeshProUGUI notification;
    public int id;
    void Start()
    {
        NotificationPanel = NotificationManager.instance.NotificationImage[id];

        notification = NotificationPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        NotificationPanel.gameObject.SetActive(true);
        StartCoroutine(Num());
    }
    public IEnumerator Num()
    {
        yield return new WaitForSeconds(1);
        NotificationOptions();
    }
    public void NotificationOptions()
    {
        NotificationPanel.DOFade(0, 1);
        notification.DOFade(0, 1).OnComplete(() => 
        {
            NotificationPanel.DOFade(0.5f, 0);
            notification.DOFade(1, 0);
            NotificationPanel.gameObject.SetActive(false);
        });
    }
}
