using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance;
    public List<Image> NotificationImage = new List<Image>();
    public int notificationId;
    public List<GameObject> uýObject = new List<GameObject>();
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    

}
