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
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask Obj;
    public static float jumpSpeed = 1.5f, jumpHeight, move; // 1.3, 12
    public static int Dir = 1;
    private float lastMove, acc = 0.1f, yComponentOfP, runSpeed, maxMoveSpeed = 12f; // acc = runSpeed - intial speed / time taken 
    public static bool lockRun = true, jumpingAvailable = false, lockSuicide = false, shouldSuicideBool = false, isJumping = false, isGrounded = false, isMoving = false;
    [SerializeField] public static bool canmove;
    [SerializeField] float moveSpeed = 5f;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().buildIndex >= 0 && SceneManager.GetActiveScene().buildIndex <= 2)
        {
            playerAnim.SetBool("VTrigger", false);
            playerAnim.SetBool("FireTrigger", false);
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 3 && SceneManager.GetActiveScene().buildIndex <= 7)
        {
            playerAnim.SetBool("VTrigger", true);
            playerAnim.SetBool("FireTrigger", false);
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 8 && SceneManager.GetActiveScene().buildIndex <= 9)
        {
            playerAnim.SetBool("VTrigger", false);
            playerAnim.SetBool("FireTrigger", true);
        }
    }

    void Start()
    {
        runSpeed = moveSpeed;
        Dir = 0;
        canmove = true;
        playerRB = GetComponent<Rigidbody2D>();
        playerSp = GetComponent<SpriteRenderer>();
        fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
        if (LevelTransition.playerPos != null && PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
        {
            gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name), gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (SceneManager.GetActiveScene().name == "House")
        {
            StartCoroutine(TriggerCutscene());
        }
    }

    private void Update()
    {
        if(!canmove && !PlayerController.lockSuicide)
        {
            playerRB.velocity = new Vector3(0, playerRB.velocity.y, 0);
        }
        if(Time.timeSinceLevelLoad < 0.01f)
            fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position - new Vector3(0, playerSp.bounds.extents.y - 0.5f, 0), Vector3.left, Color.red);
        Debug.DrawRay(transform.position - new Vector3(0, playerSp.bounds.extents.y - 0.5f, 0), Vector3.right, Color.blue);

        move = Input.GetAxisRaw("Horizontal");
        JumpHeightDetermine();
        Ground();// Dir is here tho 
        /*if (runSpeed < 0)
        {
            runSpeed = moveSpeed;
            isMoving = false;
            Dir = 0;
        }*/
        if (lastMove != move)
            runSpeed = moveSpeed;
        if (transform.position.y < yComponentOfP)
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 1.05f);
        if (canmove == true && lockSuicide == false && isJumping == false && isGrounded == true)
            Movement(move);
        if (jumpingAvailable == true && isGrounded == true && Input.GetKeyDown(KeyCode.Space) == true)
            Jumping();
        AnimationFunc();
        lastMove = move;
        yComponentOfP = transform.position.y;
    }

    void JumpHeightDetermine()
    {
        if (Physics2D.Raycast(transform.position - new Vector3(0, playerSp.bounds.extents.y - 0.5f, 0), new Vector3(Dir, 0, 0), 2.2f, Obj))
        {
            jumpHeight = 10f;
            isMoving = false;
        }
        else jumpHeight = 20f;
    }

    void Ground()
    {
        Debug.DrawLine(transform.position - new Vector3(0.5f, 0, 0), transform.position - new Vector3(0.5f, playerSp.bounds.extents.y + 0.001f, 0));
        Debug.DrawLine(transform.position + new Vector3(0.5f, 0, 0), transform.position - new Vector3(- 0.5f, playerSp.bounds.extents.y - 0.001f, 0));

        if (Physics2D.Linecast(transform.position - new Vector3(0.5f, 0, 0), transform.position - new Vector3(0.5f, playerSp.bounds.extents.y + 0.001f, 0), ground) || 
            Physics2D.Linecast(transform.position + new Vector3(0.5f, 0, 0), transform.position - new Vector3(- 0.5f, playerSp.bounds.extents.y - 0.001f, 0), ground))
        //if (Physics2D.Raycast(transform.position - new Vector3(0.5f, playerSp.bounds.extents.y, 0), Vector3.down, 0.01f, ground) || Physics2D.Raycast(transform.position + new Vector3(0.5f, - playerSp.bounds.extents.y, 0), Vector3.down, 0.01f, ground))
        {
            Debug.Log("Grounded");
            playerAnim.SetBool("Jumping", false);
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

        if (runSpeed > 10f && Input.GetKey(KeyCode.LeftShift) == false)
        {
            runSpeed -= acc;
            runSpeed = Mathf.Clamp(runSpeed, runSpeed, moveSpeed);
        }

        if (move == 0 && isMoving == true)
        {
            runSpeed -= acc * 5;
            runSpeed = Mathf.Clamp(runSpeed, -0.1f, runSpeed);
        }

        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y);
        else playerRB.velocity = new Vector2(Dir * runSpeed, playerRB.velocity.y);
    }

    void Jumping()
    {
        isJumping = true;

        playerAnim.SetBool("Jumping", true);

        if (Input.GetKey(KeyCode.LeftShift) == true)
            playerRB.velocity = new Vector2(playerRB.velocity.x * jumpSpeed, ((jumpHeight * 1.25f * Mathf.Sin(35f * Mathf.Deg2Rad)) - (9.8f * Time.deltaTime)));
        else playerRB.velocity = new Vector2(playerRB.velocity.x * jumpSpeed, ((jumpHeight * Mathf.Sin(35f * Mathf.Deg2Rad)) - (9.8f * Time.deltaTime)));
    }

    void DirectionChange()
    {
        if(move == 0)
        {
            isMoving = false;
            Dir = 0;
        }

        if (move == -1 && !Physics2D.Raycast(transform.position - new Vector3(0, playerSp.bounds.extents.y - 0.5f, 0), Vector3.left, 2.2f, Obj))
        {
            isMoving = true;
            Dir = 0;
        }
        if (move == 1 && !Physics2D.Raycast(transform.position - new Vector3(0, playerSp.bounds.extents.y - 0.5f, 0), Vector3.right, 2, Obj))
        {
            isMoving = true;
            Dir = 0;
        }
        
        if (move == -1)
        {
            Dir = -1;
            playerSp.flipX = true;
        }
        if (move == 1)
        {
            Dir = 1;
            playerSp.flipX = false;
        }
    }

    void AnimationFunc()
    {
        if (shouldSuicideBool == true)
            playerAnim.SetFloat("Velocity", 0);
        else playerAnim.SetFloat("Velocity", Mathf.Abs(playerRB.velocity.x));
        /*
        if (move == -1 && Physics2D.Raycast(transform.position - new Vector3(0, playerSp.bounds.extents.y - 0.5f, 0), Vector3.left, 2.2f, Obj) ||
            move == 1 && Physics2D.Raycast(transform.position - new Vector3(0, playerSp.bounds.extents.y - 0.5f, 0), Vector3.right, 2, Obj))
            playerAnim.SetFloat("Velocity", 0);*/
    }

    IEnumerator TriggerCutscene()
    {
        fadePanel.SetBool("out", true);
        yield return null;
        Cutscene.cutsceneIndex = 1;
        Cutscene.playCutscene = true;
    }
}