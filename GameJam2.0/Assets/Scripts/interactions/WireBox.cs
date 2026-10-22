using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WireBox : MonoBehaviour
{
    public TextMeshProUGUI display;
    public int countingIt = 0;
    public GameObject disableWires;
    public Animator ShakeButts;
    public Animator Disapear;
    public Sprite fixedWire;
    public Image yellowWire;
    public GameObject wireBox;
    public GameObject wireBoxcollider;
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.E))
        { //player press E and opens
            wireBox.SetActive(true);
        }
    }
    public void wrongWire()
    {
        countingIt++;
        if (countingIt < 3)
        {
            display.text = "No the obivous one!";
            ShakeButts.SetTrigger("go");
        }
        else
        {
            display.text = "R u trying to be a goofball?";
            ShakeButts.SetTrigger("go");
        }
    }
    public void correctWire()
    {
        display.text = "";
        yellowWire.sprite = fixedWire;
        disableWires.SetActive(true);
        Invoke("delay", 1f);
    }

    void delay()
    {
        Disapear.SetBool("go", true);
        Invoke("Bye", 1f);
    }

    void Bye()
    {
        wireBoxcollider.GetComponent<Collider2D>().enabled = false;
        GoalLight.ventCompleted = true;
        Destroy(wireBox);
    }
}
