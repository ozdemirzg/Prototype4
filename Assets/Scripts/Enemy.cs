using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   //Bu script i takip için yazdýk.
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Bunu kodu düzenlemek için yazdýk.
        Vector3 lookDirection= (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection*speed);
        // enemyRb.AddForce((player.transform.position-transform.position).normalized*speed);
        //normalized ne kadar uzaktan gelirse gelsin ayný þiddetle çarpmasýný saðladý.

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
