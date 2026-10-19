using System.Collections.Generic;
using UnityEngine;

public class BrokenPipe : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> hotSteam;
    public AudioClip wrenchAudioClip;
    private AudioSource myAudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

     void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") 
            && player.GetComponent<tempVentPipe>().hasWrench == true 
            && Input.GetKey(KeyCode.E))
        { //player press E and has wrench then okay
                myAudioSource.PlayOneShot(wrenchAudioClip);
                GetComponent<Collider2D>().enabled = false;
                foreach(GameObject steamobject in hotSteam)
                {
                    steamobject.SetActive(false);
                }
        }
    }
}
