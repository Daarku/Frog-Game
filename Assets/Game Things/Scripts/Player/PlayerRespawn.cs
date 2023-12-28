using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
   [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private AudioClip Orb;
    private UIManager uiManager;



    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindAnyObjectByType<UIManager>();
    }

    public void CheckRespawn()
    {
        if (currentCheckpoint != null) 
        {
            uiManager.GameOver();

            return;
        }
        playerHealth.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.Playsound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
    