using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerCollion : MonoBehaviour
{
    public PlayerScript playerScript;
    public Score score;
    public GameController gameController;
    public AudioSource audioSource;
    public AudioSource audioSource1;
    //private void OnCollisionEnter(Collision other)
    //{
    //    //Debug.Log(other.gameObject.name);
    //    if (other.gameObject.tag == "Collectables") 
    //    {
    //        Destroy(other.gameObject);

    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectables")
        {
            Destroy(other.gameObject);
            score.Addscore(1);
            audioSource.Play();

        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacles")
        {
            gameController.GameOver();
            playerScript.enabled = false;
            audioSource1.Play();
        }
    }
}
