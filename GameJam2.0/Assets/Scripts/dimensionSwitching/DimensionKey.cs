using System.Collections.Generic;
using UnityEngine;

public class DimensionKey : MonoBehaviour
{
    public GameObject maskingobject;
    private Vector3 scaleChange;
    public float minScale;
    public float maxScale;
    public float scaleAmount;
    public List<GameObject> ToDisableExplodedColliders;
    public List<GameObject> ToDisablePreExplosionColliders;
    private bool Toggle = false;
    public bool stopMaskShift = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scaleChange = new Vector3(-0.01f, -0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (stopMaskShift == true)
        {
            Toggle = true;
        }
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
                if (blown != null && blown.TryGetComponent<Collider2D>(out Collider2D collider))
                {
                    collider.enabled = true;
                }
            }
            foreach(GameObject notBlown in ToDisablePreExplosionColliders)
            {
                if (notBlown != null && notBlown.TryGetComponent<Collider2D>(out Collider2D collider))
                {
                    collider.enabled = false;
                }
            }
        }
        else if(Toggle == true)
        {
            scale.x = Mathf.Min(scale.x + scaleAmount, maxScale);
            scale.y = Mathf.Min(scale.y + scaleAmount, maxScale);
            maskingobject.transform.localScale = scale;
            foreach(GameObject blown in ToDisableExplodedColliders)
            {
                if (blown != null && blown.TryGetComponent<Collider2D>(out Collider2D collider))
                {
                    collider.enabled = false;
                }
            }
            foreach(GameObject notBlown in ToDisablePreExplosionColliders)
            {
                if (notBlown != null && notBlown.TryGetComponent<Collider2D>(out Collider2D collider))
                {
                    collider.enabled = true;
                }
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
