using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    // Components
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D coll;
    [SerializeField] private SpriteRenderer spriteRenderer;
    //[SerializeField] private CapsuleCollider2D bodyColl;
    //[SerializeField] private SpriteRenderer sr;

    // Movement
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    public float horizontalMove;
    [UnityEngine.Serialization.FormerlySerializedAs("CanMove")]
    public bool canMove = true;
    public KeyCode faceDirKey;
    public KeyCode oppositeFaceDirKey;


    // Jump
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    //[SerializeField] private float jumpHoldForce = 2f;
    [SerializeField] private float jumpHoldDuration = 0.1f;
    [SerializeField] private float coyoteTime = 0.2f;
    [UnityEngine.Serialization.FormerlySerializedAs("OnGround")]
    public bool onGround;
    [UnityEngine.Serialization.FormerlySerializedAs("AbleToJump")]
    public bool ableToJump;
    [UnityEngine.Serialization.FormerlySerializedAs("isjump")]
    public bool isJump;
    public bool infiniteJump;
    private float jumpTimer = 0f;
    private float coyoteTimeCounter;
    private bool jumpPressed;
    private bool jumpHeld;

    //Platform Bind
    [Header("Platform Bind")]
    public GameObject currentPlatform;
    public Vector3 lastPlatformPos;


    // Wall movement
    [Header("Wall Movement(Not in used)")]
    [SerializeField] private float climbSpeed;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float wallJumpDuration = 0.01f;
    [UnityEngine.Serialization.FormerlySerializedAs("WallCollide")]
    public bool wallCollide = false;
    [UnityEngine.Serialization.FormerlySerializedAs("AbleToClimb")]
    public bool ableToClimb;
    [UnityEngine.Serialization.FormerlySerializedAs("DuringWallJump")]
    public bool duringWallJump;
    [UnityEngine.Serialization.FormerlySerializedAs("DuringClimbing")]
    public bool duringClimbing;
    private float wallJumpTime;

    // Dash
    [Header("Dash (Not in used)")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    public Vector2 dashDire;
    [UnityEngine.Serialization.FormerlySerializedAs("DuringDash")]
    public bool duringDash;
    [UnityEngine.Serialization.FormerlySerializedAs("AbleToDash")]
    public bool ableToDash;
    private float dashTime;
    private Vector2 currentDashDire = new Vector2(0, 0);

    // Collision layers
    [Header("Collision layers")]
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask spike;
    // Appearance
    [SerializeField] private Vector2 bodySqueezeSize;
    [SerializeField] private Vector2 bodySize;
    [SerializeField] private float squeezeAmount;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color noDashColor;

    // Respawn
    [Header("Respawn")]
    [UnityEngine.Serialization.FormerlySerializedAs("Die")]
    public bool isDead;
    public Vector3 reSpawnPos;
    public int dieTime = 0;
    // UI
    [SerializeField] private GameObject dieTimeText;
    [SerializeField] private GameObject highScore;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject button;
    private Vector3 lobbyButtonPos;

    // Audio
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource dash;
    [SerializeField] private AudioSource die;
    [SerializeField] private AudioSource collect;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource bgm;

    // Reserved fields (no active references in this script)
    public float velPower;
    public BoxCollider2D feetColl;
    public Vector2 feetSqueezeSize;
    public PolygonCollider2D currentRoom;
    public int score = 0;
    public GameObject scoreText;

    private void Start()
    {
        //lobbyButtonPos = button.transform.position;
        //button.transform.position += new Vector3(1000, 1000, 1000);
        //highScoreText = highScore.GetComponent<Text>();
        //dieTimeText.GetComponent<Text>().text = "Time Dead: " + dieTime;

        //highScoreText.text = "Least Dead: " + PlayerPrefs.GetInt("HighScore", 999);
        //if (PlayerPrefs.GetInt("HighScore", 999) == 999)
        //{
        //    highScoreText.text = "Least Dead: ";
        //}
        //bgm.Play();
    }

    private void Update()
    {
        if (onGround)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //print(rb.linearVelocity);
        //if (Input.GetKey(KeyCode.R))
        //{
        //    PlayerPrefs.DeleteKey("HighScore");
        //}

        //jumpPressed = Input.GetButtonDown("Jump");
        //jumpHeld = Input.GetButton("Jump");

        DashDirection();
        //Dash();
        //WallJump();
        //if (wallCollide && !onGround && rb.linearVelocity.y <= 0 && !duringWallJump && Input.GetKey(faceDirKey))
        //{
        //    WallSlide();
        //}

        JumpControl();

        //Climb();

        if (infiniteJump == true)
        {
            ableToJump = true;
        }
    }




    private void FixedUpdate()
    {
        if (!duringDash)
        {
            Movement();
        }
        JumpAnimation();
        DieDetect();

        if (horizontalMove < 0)
        {
            faceDirKey = KeyCode.LeftArrow;
            oppositeFaceDirKey = KeyCode.RightArrow;
        }
        else if (horizontalMove > 0)
        {
            faceDirKey = KeyCode.RightArrow;
            oppositeFaceDirKey = KeyCode.LeftArrow;
        }

        MoveWithPlatform();

    }


    private void MoveWithPlatform()
    {
        if (currentPlatform == null)
        {
            return;
        }
        onGround = true;
        anim.SetBool("OnTheGround", true);
        ableToJump = true;
        Vector3 platformDelta = currentPlatform.transform.position - lastPlatformPos;
        lastPlatformPos = currentPlatform.transform.position;
        transform.position = transform.position + platformDelta;

    }

    private void Movement()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");
        float faceDirectionAir = Input.GetAxisRaw("Air Horizontal");
        //Debug.Log(horizontalMove);
        //Debug.Log(faceDirection);
        //Debug.Log(faceDirectionAir);

        if (canMove)
        {
            if (duringWallJump == false)
            {
                if (onGround == true)
                {
                    if (horizontalMove != 0)
                    {
                        anim.SetFloat("Running", Mathf.Abs(horizontalMove));
                        rb.linearVelocity = new Vector2((horizontalMove * moveSpeed * Time.fixedDeltaTime), rb.linearVelocity.y);
                    }
                }
                else
                {
                    horizontalMove = Input.GetAxis("Air Horizontal");
                    rb.linearVelocity = new Vector2(horizontalMove * moveSpeed * Time.fixedDeltaTime, rb.linearVelocity.y);
                    anim.SetFloat("Running", Mathf.Abs(horizontalMove));
                }

                if (faceDirection != 0 && faceDirectionAir != 0)
                {
                    //transform.localScale = new Vector3(faceDirection, 1, 1);
                    spriteRenderer.flipX = faceDirection < 0;
                    
                }

            }
        }
    }

    private void JumpAnimation()
    {
        anim.SetBool("OnTheGround", false);
        onGround = false;
        ableToJump = false;
        anim.SetFloat("VerticalSpeed", rb.linearVelocity.y);

        if (coll.IsTouchingLayers(ground))
        {
            ableToJump = true;
            onGround = true;
            //sr.color = normalColor;
            anim.SetBool("OnTheGround", true);
            if (duringDash)
            {
                ableToDash = false;
            }
            else
            {
                ableToDash = true;
            }
        }

        if (onGround == false)
        {
            if (duringClimbing)
            {
                //anim.SetBool("DuringClimbing", true);
            }
        }
        else
        {
            //anim.SetBool("DuringClimbing", false);
        }
    }

    private void JumpControl()
    {

        if (Input.GetButton("Jump"))
        {
            jumpTimer += Time.deltaTime;
            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, jumpForce));
        }
        if (Input.GetButton("Jump") && isJump && jumpTimer <= jumpHoldDuration)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, jumpForce));
        }
        else if (Input.GetButtonDown("Jump"))
        {

            if (jumpTimer <= jumpHoldDuration && coyoteTime > 0f && !duringClimbing && ableToJump && onGround)
            {
                //Debug.Log("Jumping");
                //jump.Play();
                isJump = true;
                //onGround = false;

                //jumpTimer = Time.time + jumpHoldDuration;

                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, jumpForce));
                //if (rb.linearVelocity.y > 0)
                //{
                //    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                //}
                //if (rb.linearVelocity.y <= 0)
                //{
                //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                //}
            }
        }
        else if (Input.GetButtonUp("Jump"))
        {
            isJump = false;
            //Debug.Log("stop jump");
            jumpTimer = 0;

            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }


        if (Input.GetButtonUp("Jump"))
        {
            coyoteTimeCounter = 0f;
        }
    }

    private void Climb()
    {
        if (rb.linearVelocity.y <= 0)
        {
            //anim.SetFloat("VerticalSpeed", -0.1f);
            ableToClimb = true;
        }

        if (wallCollide && ableToClimb && !duringDash)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                if (duringWallJump == false)
                {
                    duringClimbing = true;
                    canMove = false;
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        transform.position += new Vector3(0, climbSpeed, 0);
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        if (onGround == false)
                        {
                            transform.position -= new Vector3(0, climbSpeed, 0);
                        }
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                duringClimbing = false;
                canMove = true;
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        else
        {
            canMove = true;
            duringClimbing = false;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void WallSlide()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, -slideSpeed);
    }

    private void WallJump()
    {
        if (wallCollide && !onGround)
        {
            if ((Input.GetKey(oppositeFaceDirKey) && Input.GetButtonDown("Jump")) || (Input.GetButtonDown("Jump") && !Input.GetKey(KeyCode.Z)))
            {
                jump.Play();
                duringWallJump = true;
                wallJumpTime = Time.time + wallJumpDuration;
                duringClimbing = false;
                canMove = true;
                ableToJump = false;
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                if (rb.linearVelocity.y > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, 1, 1);
                    rb.AddForce(new Vector2(jumpForce * transform.localScale.x, jumpForce), ForceMode2D.Impulse);
                }
                else if (rb.linearVelocity.y <= 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, 1, 1);
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    rb.AddForce(new Vector2(jumpForce * transform.localScale.x, 0), ForceMode2D.Impulse);
                }
            }
            else if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.Z) || (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.Z) && Input.GetKey(faceDirKey)))
            {
                ableToClimb = false;
                duringClimbing = false;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
        else if (duringWallJump)
        {
            if (wallJumpTime < Time.time)
            {
                horizontalMove = rb.linearVelocity.x / moveSpeed;
                duringWallJump = false;
            }
        }
    }

    private void DashDirection()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            dashDire = new Vector2(0, dashDire.y);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                dashDire = new Vector2(1, dashDire.y);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                dashDire = new Vector2(-1, dashDire.y);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            dashDire = new Vector2(1, dashDire.y);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dashDire = new Vector2(-1, dashDire.y);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            dashDire = new Vector2(dashDire.x, 0);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                dashDire = new Vector2(dashDire.x, 1);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                dashDire = new Vector2(dashDire.x, -1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            dashDire = new Vector2(dashDire.x, 1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dashDire = new Vector2(dashDire.x, -1);
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.X) && ableToDash)
        {
            dash.Play();
            duringDash = true;
            ableToDash = false;
            dashTime = Time.time + dashDuration;

            currentDashDire = dashDire;
            SqueezeVertical();
            //sr.color = noDashColor;
        }
        else if (duringDash)
        {
            if (currentDashDire == new Vector2(0, 0))
            {
                currentDashDire = new Vector2(transform.localScale.x, currentDashDire.y);
            }

            if (currentDashDire.x != 0)
            {
                transform.localScale = new Vector3(currentDashDire.x, transform.localScale.y, 1);
            }

            if (Mathf.Abs(currentDashDire.x) == 1 && Mathf.Abs(currentDashDire.y) == 1)
            {
                rb.linearVelocity = new Vector2(currentDashDire.x * dashSpeed * Time.fixedDeltaTime * (Mathf.Sqrt(2f) / 2), currentDashDire.y * dashSpeed * Time.fixedDeltaTime * (Mathf.Sqrt(2f) / 2));
            }
            else
            {
                rb.linearVelocity = new Vector2(currentDashDire.x * dashSpeed * Time.fixedDeltaTime, currentDashDire.y * dashSpeed * Time.fixedDeltaTime);
            }

            if (dashTime < Time.time)
            {
                if (dashDire.y == -1)
                {
                    rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                    duringDash = false;
                }
                else
                {
                    rb.linearVelocity = new Vector2(0, 0);
                    duringDash = false;
                }
                SqueezeVertical();
            }
        }
    }

    private void SqueezeVertical()
    {
        if (duringDash)
        {
            transform.localScale = bodySqueezeSize;
            transform.localScale = new Vector3(transform.localScale.x, squeezeAmount, 1);
        }
        else
        {
            transform.localScale = bodySize;
            transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        }
    }

    private void DieDetect()
    {
        if (isDead)
        {
            //die.Play();
            dieTime += 1;
            //dieTimeText.GetComponent<Text>().text = "Time Dead: " + dieTime;
            isDead = false;
            transform.position = reSpawnPos;
            return;
        }

        //dieTimeText.GetComponent<Text>().text = "Time Dead: " + dieTime;
        //die.Play();

        //if (coll.IsTouchingLayers(spike)) //|| bodyColl.IsTouchingLayers(spike))
        //{
        //    transform.position = reSpawnPos;
        //    dieTime += 1;
        //    dieTimeText.GetComponent<Text>().text = "Time Dead: " + dieTime;
        //    die.Play();
        //}

        if (transform.position.y <= -10f)
        {
            transform.position = reSpawnPos;
            dieTime += 1;
            //dieTimeText.GetComponent<Text>().text = "Time Dead: " + dieTime;
            die.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (currentPlatform != null)
            {
                onGround = false;
                currentPlatform = null;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.layer == ground)
        //{
        //    onGround = true;

        //}
        //else
        //{
        //    onGround = false;
        //}

        if (other.CompareTag("Platform"))
        {
            Debug.Log("touch platform");
            if (currentPlatform == null)
            {
                Debug.Log("Set Platform");
                currentPlatform = other.gameObject;
                lastPlatformPos = other.transform.position;


            }
            
            MoveWithPlatform();
        }

        if (other.gameObject.CompareTag("Item"))
        {
            collect.Play();
            Destroy(other.gameObject);
        }

        //Check collide with Enemy

        if (other.gameObject.CompareTag("Enemy"))
        {
            isDead = true;
            DieDetect();
        }

        //Check and Update Respawn Point
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            reSpawnPos = transform.position;
        }
        //else if (other.gameObject.CompareTag("Goal"))
        //{
        //    win.Play();
        //    bgm.Stop();
        //    button.transform.position = lobbyButtonPos;
        //    if (dieTime < PlayerPrefs.GetInt("HighScore", 999))
        //    {
        //        PlayerPrefs.SetInt("HighScore", dieTime);
        //        highScoreText.text = "Least Dead: " + dieTime;
        //    }
        //}
    }
}

