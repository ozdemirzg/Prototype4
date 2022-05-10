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
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput); //Vector3 ü silip focalPoint.transform yazdýk.
        //Indicator ýn göründüðü pozisyon için indicator ýn pozisyonunu playerýnkine eþitledik.player inspectorunda da koyduk bunu.
        powerupIndicator.gameObject.transform.position = transform.position+ new Vector3(0,-0.5f,0);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        { 
            hasPowerup = true;
            Destroy(other.gameObject);
            //Bunu IEnumerator ý ne zaman çalýþtýracaðýmýzý söylemek için yazdýk.
            StartCoroutine(PowerupCountdownRoutine());

            //indicator ý açmak için
            powerupIndicator.gameObject.SetActive(true);

            
        }
    }
    //Bu method hasPowerup ý 7 saniye sonra false yapmak için yazdýldý.
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        //7 saniye bekledikten sonra:
        hasPowerup=false;
        //indicator ý kapamak için
        powerupIndicator.gameObject.SetActive(false);

    }

    //physics ile ilgili birþey yapýyorsan collider ý kullan.
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            //Bu kýsým ile düþmaný hýzlý ittik.
            Rigidbody enemyRigidbody =collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer= collision.gameObject.transform.position-transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            
            Debug.Log("Collided with" + collision.gameObject.name+ "with powerup set to " + hasPowerup);

        }
                
    }
}
