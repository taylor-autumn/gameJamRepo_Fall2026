using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public float moveSpeed;

    private Transform currentPoint;
    private Transform destinationPos;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPoint = startPos;
        destinationPos = endPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(currentPoint.position, destinationPos.position, moveSpeed * Time.deltaTime);


        if (transform.position == destinationPos.position)
        {
            Vector3 lastDestination = currentPoint.position; 
            currentPoint = destinationPos;
            destinationPos;
        }
    }
}
