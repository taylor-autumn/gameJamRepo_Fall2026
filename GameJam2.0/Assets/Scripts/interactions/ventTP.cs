using UnityEngine;

public class ventTP : MonoBehaviour
{
    public GameObject player;
    public Transform teleports;
    public Animator transitionAnimator;
    public Animator ventingit;
    public AudioClip ventAudioClip;
    private AudioSource myAudioSource;
    private bool disable;
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        disable = false;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.E) && disable == false)
        { //player press E and has screwdriver then okay
                print("E pressed");
                transitionAnimator.SetTrigger("go");
                ventingit.SetTrigger("go");
                myAudioSource.PlayOneShot(ventAudioClip);
                print("ok");
                Invoke("smallDelay", 1f);
                disable = true;
        }
    }
    void smallDelay()
    {
        player.transform.position = teleports.position;
    }
}
