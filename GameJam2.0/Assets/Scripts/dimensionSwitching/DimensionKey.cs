using System.Collections.Generic;
using UnityEngine;

public class DimensionKey : MonoBehaviour
{
    public GameObject maskingobject;
    private Vector3 scaleChange;
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public float scaleAmount = 0.01f;
    public List<GameObject> ToDisableExplodedColliders;
    public List<GameObject> ToDisablePreExplosionColliders;
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
        }
        Vector3 scale = maskingobject.transform.localScale;
        if (Toggle == false)
        {   
            scale.x = Mathf.Max(scale.x - scaleAmount, minScale);
            scale.y = Mathf.Max(scale.y - scaleAmount, minScale);
            maskingobject.transform.localScale = scale;
            foreach(GameObject blown in ToDisableExplodedColliders)
            {
                blown.GetComponent<Collider2D>().enabled = true;
            }
            foreach(GameObject notBlown in ToDisablePreExplosionColliders)
            {
                notBlown.GetComponent<Collider2D>().enabled = false;
            }
        }
        else if(Toggle == true)
        {
            scale.x = Mathf.Min(scale.x + scaleAmount, maxScale);
            scale.y = Mathf.Min(scale.y + scaleAmount, maxScale);
            maskingobject.transform.localScale = scale;
            foreach(GameObject blown in ToDisableExplodedColliders)
            {
                blown.GetComponent<Collider2D>().enabled = false;
            }
            foreach(GameObject notBlown in ToDisablePreExplosionColliders)
            {
                notBlown.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
            //     foreach(GameObject notBlown in ToDisablePreExplosionColliders)
            // {
            //     foreach (Collider2D col in notBlown.GetComponentsInChildren<Collider2D>())
            //     {
            //         col.enabled = true;
            //     }
            // }
}
