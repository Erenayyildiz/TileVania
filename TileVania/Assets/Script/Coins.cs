using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    [SerializeField] int pointForCoinPickup = 100;

    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<Gamesession>().AddToScore(pointForCoinPickup);
            AudioSource.PlayClipAtPoint(coinSFX,Camera.main.transform.position);
            Destroy(gameObject);   
        }
    }

}
