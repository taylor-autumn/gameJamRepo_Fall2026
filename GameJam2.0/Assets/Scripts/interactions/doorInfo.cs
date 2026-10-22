using UnityEngine;

public class doorInfo : MonoBehaviour
{
    public Animator doorAnim;
    public bool isOpen = false;
    public bool doorOpened = false; //actually checks for animation run to only run one time

    private void Start()
    {
        doorAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isOpen && !doorOpened)
        {
            doorAnim.SetTrigger("open");
            doorOpened= true;
        }
    }

}
