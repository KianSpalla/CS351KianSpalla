using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{
    bool isActive = true;

    //set in inspector
    public AudioClip scoreSound;
    private AudioSource scoreAudio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
        
            isActive = false;

            ScoreManager.score++;

            // Optional: Play a sound effect when scoring
            AudioSource.PlayClipAtPoint(scoreSound, transform.position);

            // Optional: Destroy the trigger zone after scoring to prevent multiple scores

            Destroy(gameObject);
        }
    }
}
