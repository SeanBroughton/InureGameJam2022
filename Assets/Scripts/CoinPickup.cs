using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    bool notCollected = true;
    bool wasCollected = false;
    float volumeScale = 1f;
    Animator myAnimator;
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 100;
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
        
        if (other.tag == "Player" && !wasCollected)
        {
           StartCoroutine(CoinCollected());
        }
    }

    IEnumerator CoinCollected()
    {
        notCollected = false;
        wasCollected = true;
        FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
        myAnimator.SetTrigger("Dying");
        audioSource.PlayOneShot(coinPickupSFX, volumeScale);
        
       

        yield return new WaitForSecondsRealtime(1f);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    
}
