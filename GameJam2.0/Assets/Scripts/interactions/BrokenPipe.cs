using UnityEngine;

public class BrokenPipe : MonoBehaviour
{
    public GameObject player;
    public AudioClip wrenchAudioClip;
    private AudioSource myAudioSource;
    public GameObject otherPipe;
    public bool fixedPipe = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        fixedPipe = false;
    }

     void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") 
            && playerStatus.instance.hasWrench == true
            && Input.GetKey(KeyCode.E))
        { 
                myAudioSource.PlayOneShot(wrenchAudioClip);
                GetComponent<Collider2D>().enabled = false;
                fixedPipe = true;
                if (otherPipe.GetComponent<BrokenPipe>().fixedPipe == true)
                {
                    GoalLight.pipeCompleted = true;
                }
        }
    }
}
