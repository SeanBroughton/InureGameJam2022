using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    bool notCollected = true;
    float volumeScale = 1f;
    Animator myAnimator;
    [SerializeField] AudioClip coinPickupSFX;
    AudioSource audioSource;

    //sets up collect animation for the coin
    void Start()
    {
        if(!notCollected) {return;}

        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    //destroys the coin when the player touches it
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!notCollected) {return;}
        
        if (other.tag == "Player")
        {
           StartCoroutine(CoinCollected());
        }
    }

    IEnumerator CoinCollected()
    {
        notCollected = false;
        myAnimator.SetTrigger("Dying");
        audioSource.PlayOneShot(coinPickupSFX, volumeScale);
        
       

        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }

    
}
