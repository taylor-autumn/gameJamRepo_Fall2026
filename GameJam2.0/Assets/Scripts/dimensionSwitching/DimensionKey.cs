using UnityEngine;

public class DimensionKey : MonoBehaviour
{
    private bool Toggle = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Toggle = !Toggle;
        }
        if (Toggle == false)
        {
        }
    }
}
