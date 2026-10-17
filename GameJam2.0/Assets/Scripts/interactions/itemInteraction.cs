using UnityEngine;

public class itemInteraction : MonoBehaviour
{
    public bool hasKeyCard = false;
    public bool canTryDoor = false;

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
        if (collision.CompareTag("keyCard"))
        {
            Destroy(collision.gameObject);
            print("Picked up keycard");
        }
    }

}
