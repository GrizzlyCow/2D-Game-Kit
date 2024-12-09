using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class AttackBox : MonoBehaviour
{

    void Start()
    {

    }


    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //If i touch an enemy, hurt the enemy!
        if (col.gameObject.GetComponent<Enemy>())
        {
            col.gameObject.GetComponent<Enemy>().health -= NewPlayer.Instance.attackPower;
        }
    }
}
