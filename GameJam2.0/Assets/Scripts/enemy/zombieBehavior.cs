using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class zombieBehavior : MonoBehaviour
{

    [Header("Main Stats")]
    public float speed;
    public Animator zombieAnim;
    public bool isAttacking = false;

    [Header("Target stuff")]
    public bool justStarted = false;
    public Transform leftPos;
    public Transform rightPos;
    public float distanceThreshold = 0.5f;
    public bool atPoint = false;
    public Transform startTargetTransform;
    public Transform currentTargetTransform = null;

    [Header("Autonomous Zombie Shit")]
    public float timer = 0f;
    public float waitTime = 0f;
    public float minTime = 5f;
    public float maxTime = 11f;
    public float minIdle = 2f;
    public float maxIdle = 5f;

    public enum zombieType
    {
        autonomous,
        mover,
    }
    [Header("Zombie Type")]
    public zombieType type;
    public enum zombieState
    {
        idle,
        moving,
        attacking
    }
    [Header("Zombie Status")]
    public zombieState state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        justStarted = true;

        //set random speed
        speed = Random.Range(3, 6);

        //print("Zombie type is " + type);
        if (type == zombieType.autonomous)
        {
            waitTime = Random.Range(minIdle, maxIdle);
            //set wait time if autonomous
            state = zombieState.idle;
            //set state to idle at start if autonomous
        }

        //get anim
        zombieAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (type == zombieType.autonomous)
        {
            //trigger passive zombie type
            timer += Time.deltaTime;

            switch (state)
            {
                case zombieState.idle:
                    onZombieIdle();
                    break;
                case zombieState.moving:
                    onZombieMove();
                    break;
                case zombieState.attacking:
                    StartCoroutine(zombieAttack());
                    //PUT PLAYER RESPAWN/damage HERE???======================
                    break;
                default:
                    Debug.LogError("something wrong with autonomous");
                    break;
            }

            if (timer > waitTime)
            {
                timer = 0; //reset timer
                switchZombieBehavior();
            }
        }
        else if (type == zombieType.mover)
        {
            switch (state)
            {
                case zombieState.moving:
                    onZombieMove();
                    break;
                case zombieState.attacking:
                    StartCoroutine(zombieAttack());
                    //PUT PLAYER RESPAWN/damage HERE???======================
                    break;
                default:
                    Debug.LogError("something wrong with autonomous");
                    break;
            }
        }

    }

    public void switchZombieBehavior()
    {
        if (state == zombieState.idle)
        {
            //reset wait time
            waitTime=Random.Range(minTime, maxTime);
            //set to moving
            state = zombieState.moving;
        }
        else
        {
            //reset idle wait time
            waitTime=Random.Range(minIdle, maxIdle);
            //set to idle
            state = zombieState.idle;
        }
    }

    public void onZombieIdle()
    {
        //idle anim set
        zombieAnim.SetInteger("state", 0);
    }
    public void onZombieMove()
    {
        //move anim set
        zombieAnim.SetInteger("state", 1);

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

    public void chooseRandomStartingTarget()
    {
        int randomStartingDirection = Random.Range(0, 2);

        if (randomStartingDirection == 0) currentTargetTransform = leftPos;
        else currentTargetTransform = rightPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("HIT PLAYER!");
            state = zombieState.attacking;
        }
    }

    public IEnumerator zombieAttack()
    {
        isAttacking = true;
        print("is attacking = " + isAttacking);
        zombieAnim.SetTrigger("attack");
        //SOUND FOR ATTACK HERE===============================
        yield return new WaitForSeconds(0.45f); //maybe change this for player die===============

        isAttacking = false;
        if (type == zombieType.mover)
        {
            state = zombieState.moving;
        }
        else
        {
            //function to pick / go back to idle?
        }

    }

}
