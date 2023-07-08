using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BinControl : MonoBehaviour
{
    public ParticleSystem explosion;
    private void Start()
    {
        explosion = this.transform.GetChild(0).GetComponent<ParticleSystem>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerController.instance.PizzaStackObject.Count != 0)
                explosion.Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerController.instance.PizzaStackObject.Count != 0)
            {
                PlayerController.instance.PizzaStackObject[PlayerController.instance.PizzaStackObject.Count - 1].gameObject.SetActive(false);
                PlayerController.instance.PizzaStackObject[PlayerController.instance.PizzaStackObject.Count - 1].gameObject.transform.parent = null;
                PlayerController.instance.PizzaStackObject.RemoveAt(PlayerController.instance.PizzaStackObject.Count - 1);
            }
        }
    }
}
//if (PlayerController.instance.PizzaStackObject.Count != 0)
//{
//    PlayerController.instance.PizzaStackObject[PlayerController.instance.PizzaStackObject.Count - 1].GetComponent<pizza>().Particle();

//    DOVirtual.DelayedCall(0.1f, () =>
//    {
//        PlayerController.instance.PizzaStackObject[PlayerController.instance.PizzaStackObject.Count - 1].gameObject.SetActive(false);
//        PlayerController.instance.PizzaStackObject[PlayerController.instance.PizzaStackObject.Count - 1].gameObject.transform.parent = null;
//        PlayerController.instance.PizzaStackObject.RemoveAt(PlayerController.instance.PizzaStackObject.Count - 1);
//    });

//}