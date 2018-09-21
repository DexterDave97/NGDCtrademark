using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private Animator playerAnim;
    private SpriteRenderer playerSp;
    Animator fadePanel;
    GameObject cutsceneManager;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask Obj;
    public static float jumpSpeed = 1.8f, jumpHeight = 20f; // 1.3, 12
    public static int Dir = 1;
    private float move, lastMove, acc = 0.1f, yComponentOfP, runSpeed, maxMoveSpeed = 25f; // acc = runSpeed - intial speed / time taken 
    public static bool lockRun = true, jumpingAvailable = false, lockSuicide = false, shouldSuicideBool = false, isJumping = false, isGrounded = false, isMoving = false;
    [SerializeField] public static bool canmove;
    [SerializeField] float moveSpeed = 5f;

    void Start()
    {
        runSpeed = moveSpeed;
        Dir = 0;
        canmove = true;
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSp = GetComponent<SpriteRenderer>();
        fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
        cutsceneManager = GameObject.FindGameObjectWithTag("Cutscene");
        if (LevelTransition.playerPos != null && PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
        {
            gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name), gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if(Cutscene.firstScene)
        {
            StartCoroutine(TriggerCutscene());
        }
    }

    private void FixedUpdate()
    {
        move = Input.GetAxisRaw("Horizontal");
        JumpHeightDetermine();
        Ground();// Dir is here tho 
        if (runSpeed < 0)
        {
            runSpeed = moveSpeed;
            isMoving = false;
            Dir = 0;
        }
        if (lastMove != move)
            runSpeed = moveSpeed;
        if (transform.position.y < yComponentOfP)
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 1.05f);
        if (canmove == true && lockSuicide == false && isJumping == false && isGrounded == true)
            Movement(move);
        if (jumpingAvailable == true && isGrounded == true && Input.GetKey(KeyCode.Space) == true)
            Jumping();
        AnimationFunc();
        lastMove = move;
        yComponentOfP = transform.position.y;
    }

    void JumpHeightDetermine()
    {
        if (Physics2D.Raycast(transform.position - new Vector3(0, playerSp.bounds.extents.y - 0.5f, 0), Dir * Vector3.one, 1.8f, Obj))
            jumpHeight = 12.5f;
        else jumpHeight = 25f;
    }

    void Ground()
    {
        Debug.DrawRay(transform.position - new Vector3(0.5f, playerSp.bounds.extents.y, 0), Vector3.down,Color.cyan);
        Debug.DrawRay(transform.position + new Vector3(0.5f, -playerSp.bounds.extents.y, 0), Vector3.down, Color.red);
        if (Physics2D.Raycast(transform.position - new Vector3(0.5f, playerSp.bounds.extents.y + 0.5f, 0), Vector3.down, 0.1f, ground) || Physics2D.Raycast(transform.position + new Vector3(0.5f, - playerSp.bounds.extents.y + 0.5f, 0), Vector3.down, 0.1f, ground))
        {
            isJumping = false;
            isGrounded = true;

            if (canmove)
                DirectionChange();
        }
        else isGrounded = false;
    }

    void Movement(float move)
    {
        if (Input.GetKey(KeyCode.LeftShift) == true && lockRun == false)
        {
            runSpeed += acc;
            runSpeed = Mathf.Clamp(runSpeed, runSpeed, maxMoveSpeed);
        }

        if (runSpeed > moveSpeed && Input.GetKey(KeyCode.LeftShift) == false)
        {
            runSpeed -= acc;
            runSpeed = Mathf.Clamp(runSpeed, runSpeed, moveSpeed);
        }

        if (move == 0 && isMoving == true)
        {
            runSpeed -= acc * 2;
            runSpeed = Mathf.Clamp(runSpeed, -0.1f, runSpeed);
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

        if (Physics2D.Raycast(transform.position - new Vector3(0, playerSp.bounds.extents.y - 0.5f, 0), Dir * Vector3.one, 1.8f, Obj))
            playerAnim.SetFloat("Velocity", 0);

        if (isJumping == true || isGrounded == false)
            playerAnim.SetBool("Jumping", true);
        if (isJumping == false || isGrounded == true)
            playerAnim.SetBool("Jumping", false);
    }

    IEnumerator TriggerCutscene()
    {
        fadePanel.SetBool("out", false);
        yield return new WaitForSeconds(0);
        cutsceneManager.SetActive(true);
        Cutscene.cutsceneIndex = 1;
        Cutscene.playCutscene = true;
        Cutscene.firstScene = false;
    }
}