using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {
    private Rigidbody2D playerRB;
    private Animator playerAnim;
    private SpriteRenderer playerSp;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask Obj;
    public GameObject building, windowPane;
    public static float jumpSpeed = 1.3f, jumpHeight = 12f; // 1.3, 12
    public static int Dir = 1;
    private float move, lastMove, runSpeedAtMoment, acc = 0.1f, yComponentOfP, jumpHeightTemp; // acc = runSpeed - intial speed / time taken 
    public static bool lockRun = true, jumpingAvailable = false, lockSuicide = false, shouldSuicideBool = false, isJumping = false, isGrounded = false, isMoving = false;
    [SerializeField] public static bool canmove;
    [SerializeField] float runSpeed = 3.5f;

    void Start()
    {
        Dir = 0;
        canmove = true;
        jumpHeightTemp = jumpHeight;
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSp = GetComponent<SpriteRenderer>();
        if (LevelTransition.playerPos != null && PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
            gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name), gameObject.transform.position.y, gameObject.transform.position.z);
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position - new Vector3(0, 1.4f, 0), new Vector3(Dir, 0, 0), Color.cyan);

        move = Input.GetAxisRaw("Horizontal");
        JumpHeightDetermine();
        Ground();// Dir is here tho 
        if (runSpeed < 0)
        {
            runSpeed = 3.5f;
            isMoving = false;
            Dir = 0;
        }
        if (lastMove != move)
            runSpeed = 3.5f;
        if (transform.position.y < yComponentOfP)
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 1.05f);
        if (canmove == true && lockSuicide == false && isJumping == false && isGrounded == true)
            Movement(move);
        if (jumpingAvailable == true && isGrounded == true && Input.GetKey(KeyCode.Space) == true)
            Jumping();
        if (shouldSuicideBool == true)
            Suicide();
        AnimationFunc();
        lastMove = move;
        yComponentOfP = transform.position.y;
    }

    void JumpHeightDetermine()
    {
        if (Physics2D.Raycast(transform.position - new Vector3(0, 1.4f, 0), new Vector3(Dir, 0, 0), 1.4f, Obj))
            jumpHeight = jumpHeightTemp / 2;
        else jumpHeight = jumpHeightTemp;
    }

    void Ground()
    {
        if (Physics2D.Raycast(transform.position - new Vector3(Dir * (playerSp.bounds.extents.x / 2), playerSp.bounds.extents.y, 0), Vector3.down, 1.8f, ground))
        {
            isJumping = false;
            isGrounded = true;

           // if (Daughter.daughtervanish)
                DirectionChange();
        }
        else isGrounded = false;
    }

    void Movement(float move)
    {
        if (Input.GetKey(KeyCode.LeftShift) == true && lockRun == false)
        {
            runSpeed += acc;
            runSpeed = Mathf.Min(8f, runSpeed);
        }

        if (runSpeed > 3.5f && Input.GetKey(KeyCode.LeftShift) == false)
        {
            runSpeedAtMoment = runSpeed;
            runSpeed -= acc;
            runSpeed = Mathf.Min(runSpeedAtMoment, runSpeed);
        }

        if (move == 0 && isMoving == true)
        {
            runSpeedAtMoment = runSpeed;
            runSpeed -= acc * 2;
            runSpeed = Mathf.Min(runSpeedAtMoment, runSpeed);
        }

        if (move != 0)
        {
            isMoving = true;
            playerRB.velocity = new Vector2((Dir * runSpeed), playerRB.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A) == true && Input.GetKey(KeyCode.D) == true)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y);
        }
        else
            playerRB.velocity = new Vector2(Dir * runSpeed, playerRB.velocity.y);
    }

    void Jumping()
    {
        isJumping = true;

        if (Input.GetKey(KeyCode.LeftShift) == true)
            playerRB.velocity = new Vector2(playerRB.velocity.x * jumpSpeed * 0.6f, ((jumpHeight * 1.15f * Mathf.Sin(35f * Mathf.Deg2Rad)) - (9.8f * Time.deltaTime)));
        else playerRB.velocity = new Vector2(playerRB.velocity.x * jumpSpeed, ((jumpHeight * Mathf.Sin(35f * Mathf.Deg2Rad)) - (9.8f * Time.deltaTime)));
    }

    void Suicide()
    {
        if (lockSuicide == false)
            playerRB.velocity = Vector3.zero;
        playerSp.flipX = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            lockSuicide = true;
            playerRB.velocity = new Vector2((5f * Mathf.Cos(0.01f * Mathf.Deg2Rad)), ((5f * Mathf.Sin(0.01f * Mathf.Deg2Rad)) - (9.8f * Time.deltaTime)));
        }
    }

    void DirectionChange()
    {
        if (move == -1)
        {
            playerSp.flipX = true;
            Dir = -1;
        }
        if (move == +1)
        {
            playerSp.flipX = false;
            Dir = 1;
        }
    }

    void AnimationFunc()
    {
        if (shouldSuicideBool == true)
            playerAnim.SetFloat("Velocity", 0);
        else playerAnim.SetFloat("Velocity", Mathf.Abs(playerRB.velocity.x));

        if (Physics2D.Raycast(transform.position - new Vector3(0, 1.4f, 0), new Vector3(Dir, 0, 0), 0.5f, Obj))
            playerAnim.SetFloat("Velocity", 0);

        if (isJumping == true || isGrounded == false)
            playerAnim.SetBool("Jumping", true);
        if (isJumping == false || isGrounded == true)
            playerAnim.SetBool("Jumping", false);
    }
}
