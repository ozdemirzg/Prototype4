using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    //
    private GameObject focalPoint;
    //
    public bool hasPowerup = false;
    public float powerupStrength = 15;
    //
    public GameObject powerupIndicator;
    void Start()
    {
       playerRb=GetComponent<Rigidbody>();
       focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput); //Vector3 � silip focalPoint.transform yazd�k.
        //Indicator �n g�r�nd��� pozisyon i�in indicator �n pozisyonunu player�nkine e�itledik.player inspectorunda da koyduk bunu.
        powerupIndicator.gameObject.transform.position = transform.position+ new Vector3(0,-0.5f,0);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        { 
            hasPowerup = true;
            Destroy(other.gameObject);
            //Bunu IEnumerator � ne zaman �al��t�raca��m�z� s�ylemek i�in yazd�k.
            StartCoroutine(PowerupCountdownRoutine());

            //indicator � a�mak i�in
            powerupIndicator.gameObject.SetActive(true);

            
        }
    }
    //Bu method hasPowerup � 7 saniye sonra false yapmak i�in yazd�ld�.
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        //7 saniye bekledikten sonra:
        hasPowerup=false;
        //indicator � kapamak i�in
        powerupIndicator.gameObject.SetActive(false);

    }

    //physics ile ilgili bir�ey yap�yorsan collider � kullan.
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            //Bu k�s�m ile d��man� h�zl� ittik.
            Rigidbody enemyRigidbody =collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer= collision.gameObject.transform.position-transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            
            Debug.Log("Collided with" + collision.gameObject.name+ "with powerup set to " + hasPowerup);

        }
                
    }
}
