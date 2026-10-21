using UnityEngine;

public class scientists : MonoBehaviour
{
    [Header("Scientist Info")]
    public float speed;
    public Animator scientistAnim;

    [Header("Scientist Behaviors")]
    public behaviors behavior;
    public enum behaviors
    {
        idle,
        move,
        panic
    }

    [Header("Timer Stuff")]
    public float timer = 0;
    public float waitTime = 0;
    public float minTime = 5f;
    public float maxTime = 10f;

    [Header("Movement Stuff")]
    public bool atPoint = false;
    public bool justStarted = false;
    public float distanceThreshold = 0.5f;
    public Transform startTargetTransform;
    public Transform currentTargetTransform;
    public Transform leftPos;
    public Transform rightPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set just started to true
        justStarted = true;

        //set random speed
        speed = Random.Range(3, 7);

        //set random starting behavior
        int RandomStartEnum = Random.Range(0, System.Enum.GetNames(typeof(behaviors)).Length);
        behavior = (behaviors)RandomStartEnum;

        //set waitTime
        waitTime = Random.Range(minTime, maxTime);

        //get anim
        scientistAnim=GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        switch (behavior)
        {
            case behaviors.idle:
                onScientistIdle();
                break;
            case behaviors.move:
                onScientistMove();
                break;
            case behaviors.panic:
                onScientistPanic();
                break;
            default:
                Debug.LogError("Something wrong with Scientist behavior choosing");
                break;

        }

        if (timer > waitTime)
        {
            timer = 0;
            switchScientistBehavior();
        }

    }

    public void switchScientistBehavior()
    {
        //get whatever the current behavior is
        behaviors lastBehavior = behavior;

        //define the randomNum here
        int randomNumber;

        do
        {
            //get the random num to cycle through the behaviors
            //this is a better way to get the random cause it senses how long the enum is
            randomNumber = Random.Range(0, System.Enum.GetNames(typeof(behaviors)).Length);
        }
        while ((behaviors)randomNumber == lastBehavior);

        //reset wait time and set behavior as new Random behavior not same as last one
        waitTime = Random.Range(minTime, maxTime);
        behavior = (behaviors)randomNumber;

    }

    public void onScientistIdle()
    {
        scientistAnim.SetInteger("state", 0);
        //print("idle");
    }
    public void onScientistMove()
    {
        scientistAnim.SetInteger("state", 1);
        //print("move");

        if (justStarted)
        {
            //so you can choose if you want starting direction to be random or deliberately set by you in the inspector
            if (startTargetTransform == null) chooseRandomStartingTarget();
            else currentTargetTransform = startTargetTransform;

            justStarted = false;
            //set it so that the first target transform is whatever the first one set in the inspector is for this zombie
        }

        if (Vector3.Distance(currentTargetTransform.position, transform.position) < distanceThreshold)
        {
            if (!atPoint)
            {
                //only do it once
                //print("AT POINT");
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
            gameObject.GetComponent<SpriteRenderer>().flipX = directionX < 0; //WOW built in boolean
        }

    }
    public void onScientistPanic()
    {
        scientistAnim.SetInteger("state", 2);
        //print("panic");
    }

    //move stuff
    public void chooseRandomStartingTarget()
    {
        int randomStartingDirection = Random.Range(0, 2);

        if (randomStartingDirection == 0) currentTargetTransform = leftPos;
        else currentTargetTransform = rightPos;
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
