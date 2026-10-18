using System.Collections.Generic;
using UnityEngine;

public class DimensionKey : MonoBehaviour
{
    public GameObject maskingobject;
    private Vector3 scaleChange;
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public float scaleAmount = 0.01f;
    public List<GameObject> BlownUpInteractables;
    public List<GameObject> NotBlownInteractables;
    private bool Toggle = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scaleChange = new Vector3(-0.01f, -0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Toggle = !Toggle;
            print(Toggle);
        }
        Vector3 scale = maskingobject.transform.localScale;
        if (Toggle == false)
        {   
            print("increasing");
            scale.x = Mathf.Max(scale.x - scaleAmount, minScale);
            scale.y = Mathf.Max(scale.y - scaleAmount, minScale);
            maskingobject.transform.localScale = scale;
            foreach(GameObject blown in BlownUpInteractables)
            {
                blown.GetComponent<Collider2D>().enabled = false;
            }
            foreach(GameObject blown in NotBlownInteractables)
            {
                blown.GetComponent<Collider2D>().enabled = true;
            }
        }
        else if(Toggle == true)
        {
            print("decreasing");
            scale.x = Mathf.Min(scale.x + scaleAmount, maxScale);
            scale.y = Mathf.Min(scale.y + scaleAmount, maxScale);
            maskingobject.transform.localScale = scale;
            foreach(GameObject blown in BlownUpInteractables)
            {
                blown.GetComponent<Collider2D>().enabled = true;
            }
            foreach(GameObject blown in NotBlownInteractables)
            {
                blown.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
