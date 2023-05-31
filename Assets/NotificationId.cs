using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationId : MonoBehaviour
{
    public int id;
    public static NotificationId instance;

    private void Awake()
    {
        instance = this;
    }
}
