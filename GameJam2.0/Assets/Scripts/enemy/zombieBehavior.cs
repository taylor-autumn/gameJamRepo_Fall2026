using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class zombieBehavior : MonoBehaviour
{

    [Header("Main Stats")]
    public float speed = 5f;
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

    public enum zombieType
    {
        passive,
        mover,
    }
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
        print("Zombie type is " + type);
    }

    // Update is called once per frame
    void Update()
    {

        if (type == zombieType.passive)
        {
            //trigger passive zombie type
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
                    //PUT PLAYER RESPAWN HERE???======================
                    break;
            }
        }

    }

    public void onZombieMove()
    {

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
        yield return new WaitForSeconds(1.5f); //maybe change this for player die===============

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
