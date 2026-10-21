using TMPro;
using UnityEngine;

public class WireBox : MonoBehaviour
{
    public TextMeshProUGUI display;
    public int countingIt = 0;
    public GameObject disableWires;
    public Animator ShakeButts;
    public Animator Disapear;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void wrongWire()
    {
        countingIt++;
        if (countingIt > 3)
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
        display.text = "Yay";
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
        
    }
}
