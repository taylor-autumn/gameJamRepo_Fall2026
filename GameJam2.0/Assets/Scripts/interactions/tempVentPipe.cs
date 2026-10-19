using UnityEngine;

public class tempVentPipe : MonoBehaviour
{
    public bool hasScrewdriver;
    public bool hasWrench;
    // public Animator transitionAnimator;
    // public AudioClip ventAudioClip;
    // public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.CompareTag("wrench"))
        // {
        //     Destroy(collision.gameObject);
        //     hasWrench = true;
        // }
        // if (collision.CompareTag("screwDriver"))
        // {
        //     Destroy(collision.gameObject);
        //     hasScrewdriver = true;
        // }
        // if (collision.CompareTag("brokenPipe") && Input.GetKeyDown(KeyCode.E))
        // {
            
        // }
        // if (collision.CompareTag("vent") && Input.GetKeyDown(KeyCode.E))
        // {
        //     audioSource.PlayOneShot(ventAudioClip);
        //     transitionAnimator.SetTrigger("Transition");
        // }

    }
}
