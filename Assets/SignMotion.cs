using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SignMotion : MonoBehaviour
{
    Vector3 startPos;
    public Transform lastPos;
    private void Start()
    {
        startPos = this.transform.position;
        StartCoroutine(MoveNum());
    }

    IEnumerator MoveNum()
    {
        yield return new WaitForSeconds(0.2f);//para yat�r�l�nca ok kapans�n ve hata veriyor onu d�zelt
        //move();

    }

    //public void move()
    //{
    //    transform.DOMove(lastPos.position, 0.7f).OnComplete(() =>
    //    {
    //        transform.DOMove(startPos, 0.7f).OnComplete(() =>
    //        {
    //            StartCoroutine(MoveNum());
    //        });
    //    });
    //}
}
