using System.Collections.Generic;
using UnityEngine;

public class BrokenPipe : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> hotSteam;
    public AudioClip wrenchAudioClip;
    private AudioSource myAudioSource;
    public bool fixing = false;
    public GameObject otherPipe;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        fixing = false;
    }

     void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") 
            && playerStatus.instance.hasWrench == true
            && Input.GetKey(KeyCode.E))
        { //player press E and has wrench then okay
                myAudioSource.PlayOneShot(wrenchAudioClip);
                fixing = true;
                GetComponent<Collider2D>().enabled = false;
                if (otherPipe.GetComponent<BrokenPipe>().fixing == true)
                {
                    GoalLight.pipeCompleted = true;
                }
                foreach(GameObject steamobject in hotSteam)
                {
                    steamobject.SetActive(false);
                }
        }
    }
}
