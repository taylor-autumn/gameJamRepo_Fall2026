using Unity.VisualScripting;
using UnityEngine;

public class zombieBehavior : MonoBehaviour
{

    [Header("Main Stats")]
    public float speed = 5f;
    public Animator zombieAnim;

    [Header("Target stuff")]
    public bool justStarted = false;
    public Transform leftPos;
    public Transform rightPos;
    public float distanceThreshold = 0.5f;
    public bool atPoint = false;
    public Transform startTargetTransform;
    public Transform currentTargetTransform = null;

    public enum zombieState
    {
        moving,
        attacking
    }
    [Header("Zombie Status")]
    public zombieState state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        justStarted = true;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case zombieState.moving:
                onZombieMove();
                break;
            case zombieState.attacking:



                break;
        }

    }

    public void onZombieMove()
    {

        //anim set here

        if (justStarted)
        {
            currentTargetTransform = startTargetTransform;
            justStarted = false;
            //set it so that the first target transform is whatever the first one set in the inspector is for this zombie
        }

        if (Vector3.Distance(currentTargetTransform.position, transform.position) < distanceThreshold)
        {
            if (!atPoint)
            {
                //only do it once
                print("AT POINT");
                atPoint = true;
                setNextTarget();
            }

        }
        else //if not at point, move towards it
        {
            atPoint = false;
            transform.position = Vector3.MoveTowards(transform.position, currentTargetTransform.position, speed * Time.deltaTime);

            //make sure it's walking the right direction
            float directionX = currentTargetTransform.position.x - transform.position.x;
            gameObject.GetComponent<SpriteRenderer>().flipX = directionX > 0; //WOW built in boolean
        }
    }

    public void setNextTarget()
    {

        if (currentTargetTransform == leftPos) currentTargetTransform = rightPos;
        else if (currentTargetTransform == rightPos) currentTargetTransform = leftPos;
        else
        {
            Debug.LogError("DOES NOT EQUAL EITHER");
        }

    }
}
