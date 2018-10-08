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
    Animator d1, d2, d3;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask Obj;
    public static float jumpSpeed = 1.5f, jumpHeight, move; // 1.3, 12
    public static int Dir = 1, lives = 0;
    private float lastMove, acc = 0.1f, yComponentOfP, runSpeed, maxMoveSpeed = 12f;
    private bool lockShiftJump;
    public static bool lockRun = false, jumpingAvailable = false, lockSuicide = false, shouldSuicideBool = false, isJumping = false, isGrounded = false, isMoving = false, jumpoverride = true;
    [SerializeField] public static bool canmove;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField]
    AudioSource Steps;
    [SerializeField]
    Sounds s;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        s = GameObject.FindGameObjectWithTag("Primary Audio").GetComponent<Sounds>();
        Steps = GameObject.FindGameObjectWithTag("Footstep").GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().buildIndex >= 1 && SceneManager.GetActiveScene().buildIndex <= 3)
        {
            playerAnim.SetBool("VTrigger", false);
            playerAnim.SetBool("FireTrigger", false);
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 4 && SceneManager.GetActiveScene().buildIndex <= 9)
        {
            playerAnim.SetBool("VTrigger", true);
            playerAnim.SetBool("FireTrigger", false);
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 10 && SceneManager.GetActiveScene().buildIndex <= 11)
        {
            playerAnim.SetBool("VTrigger", false);
            playerAnim.SetBool("FireTrigger", true);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            playerAnim.SetBool("Black", true);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 13)
        {
            playerAnim.SetBool("VTrigger", false);
            playerAnim.SetBool("FireTrigger", false);
            playerAnim.SetBool("Black", false);
        }

            if (SceneManager.GetActiveScene().buildIndex > 4 && SceneManager.GetActiveScene().buildIndex < 12)
        {
            d1 = GameObject.FindGameObjectWithTag("Death1").GetComponent<Animator>();
            d2 = GameObject.FindGameObjectWithTag("Death2").GetComponent<Animator>();
            d3 = GameObject.FindGameObjectWithTag("Death3").GetComponent<Animator>();
        }
    }

    void Start()
    {
        runSpeed = moveSpeed;
        Dir = 0;
        canmove = true;
        if (SceneManager.GetActiveScene().name == "BuildingsAfterCafe")
            lockShiftJump = true;
        else lockShiftJump = false;
        playerRB = GetComponent<Rigidbody2D>();
        playerSp = GetComponent<SpriteRenderer>();
        fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();

        if ((SceneManager.GetActiveScene().buildIndex >= 1 && SceneManager.GetActiveScene().buildIndex <= 3) || (SceneManager.GetActiveScene().buildIndex >= 7 && SceneManager.GetActiveScene().buildIndex <= 9) || (SceneManager.GetActiveScene().buildIndex >= 13 && SceneManager.GetActiveScene().buildIndex <= 14))
            Steps.clip = s.audioDict["Player"][0];
        if ((SceneManager.GetActiveScene().buildIndex >= 4 && SceneManager.GetActiveScene().buildIndex <= 5) || SceneManager.GetActiveScene().buildIndex == 12)
            Steps.clip = s.audioDict["Player"][1];
        if (SceneManager.GetActiveScene().buildIndex == 11)
            Steps.clip = s.audioDict["Player"][2];

        if (LevelTransition.playerPos != null && PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
        {
            gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name), gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (SceneManager.GetActiveScene().name == "House" && LevelTransition.playerPos == null)
        {
            jumpingAvailable = false;
            lockRun = true;
            shouldSuicideBool = false;
            StartCoroutine(TriggerCutscene());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.8f); 
        SceneManager.LoadScene("Credits");
    }

    private void Update()
    {
        if (lives > 3)
        {
            fadePanel.SetBool("out", true);
            StartCoroutine("Delay");
        }

        if (SceneManager.GetActiveScene().buildIndex > 4 && SceneManager.GetActiveScene().buildIndex < 12)
        {
            if (lives > 0)
            {
                d1.SetBool("out", true);
                if (lives > 1)
                {
                    d2.SetBool("out", true);
                    if (lives > 2)
                    {
                        d3.SetBool("out", true);
                    }
                }
            }
        }

        if (!canmove && !PlayerController.lockSuicide)
            playerRB.velocity = new Vector3(0, playerRB.velocity.y, 0);

        if(Time.timeSinceLevelLoad < 0.01f)
            fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "House" || SceneManager.GetActiveScene().name == "SchoolHouse" || SceneManager.GetActiveScene().name == "HouseAfterFire")
            jumpingAvailable = false;
        if (SceneManager.GetActiveScene().name == "School" && jumpoverride)
            jumpingAvailable = true;
    }

    private void FixedUpdate()
    {
        move = Input.GetAxisRaw("Horizontal");
        JumpHeightDetermine();

        //if (SceneManager.GetActiveScene().name != "School")
            Ground();
        //else if(isGrounded)
          //  DirectionChange();

        if (lastMove != move)
            runSpeed = moveSpeed;

        if (transform.position.y < yComponentOfP)
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 1.05f);

        if (canmove == true && lockSuicide == false && isJumping == false && isGrounded == true)
            Movement(move);

        if (jumpingAvailable == true && isGrounded == true && Input.GetKeyDown(KeyCode.Space) == true)
            Jumping();

        if (transform.position.y > yComponentOfP/* && SceneManager.GetActiveScene().name != "School"Time.timeSinceLevelLoad > 0.05f*/)
            playerAnim.SetBool("Jumping", true);

        AnimationFunc();
        lastMove = move;
        yComponentOfP = transform.position.y;
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(SceneManager.GetActiveScene().name == "School")
        {
            if(collision.IsTouchingLayers(9))
            {
                Debug.Log("Entered");
                playerAnim.SetBool("Jumping", false);
                isJumping = false;
                isGrounded = true;
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "School")
        {
            if (collision.IsTouchingLayers(9) && collision.tag != "DoorAtSchool")
            {
                Debug.Log("Exited");
                isGrounded = false;
                playerAnim.SetBool("Jumping", true);
            }
        }
    }*/

    void JumpHeightDetermine()
    {
        if (Physics2D.Raycast(transform.position - new Vector3(0, playerSp.bounds.extents.y - 0.5f, 0), new Vector3(Dir, 0, 0), 2.2f, Obj))
        {
            jumpHeight = 10f;
            isMoving = false;
        }
        else jumpHeight = 30f;

        if (SceneManager.GetActiveScene().name != "BuildingsAfterCafe")
            jumpHeight = 25f;
    }

    void Ground()
    {
        RaycastHit2D hit1 = Physics2D.Linecast(transform.position - new Vector3(0.5f, 0, 0), transform.position - new Vector3(0.5f, playerSp.bounds.extents.y + 0.01f, 0), ground);
        RaycastHit2D hit2 = Physics2D.Linecast(transform.position + new Vector3(0.5f, 0, 0), transform.position - new Vector3(-0.5f, playerSp.bounds.extents.y + 0.01f, 0), ground);

        if (hit1 || hit2)
        {
            playerAnim.SetBool("Jumping", false);
            yComponentOfP = transform.position.y;
            isJumping = false;
            isGrounded = true;

            if (canmove)
                DirectionChange();
        }
        else
        {
            isGrounded = false;
        }
    }

    void Movement(float move)
    {
        if (SceneManager.GetActiveScene().name == "ToCafe")
        {
            playerAnim.SetBool("CafeRunTrigger", true);
            maxMoveSpeed = 7;
        }
        else
        {
            playerAnim.SetBool("CafeRunTrigger", false);
            maxMoveSpeed = 12;
        }

        if (Input.GetKey(KeyCode.LeftShift) == true && lockRun == false)
        {
            runSpeed += acc;
            runSpeed = Mathf.Clamp(runSpeed, runSpeed, maxMoveSpeed);
        }

        if (maxMoveSpeed == 7 && runSpeed > 5f && Input.GetKey(KeyCode.LeftShift) == false)
        {
            runSpeed -= 0.05f;
            runSpeed = Mathf.Clamp(runSpeed, runSpeed, moveSpeed);
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
        if (Input.GetKey(KeyCode.LeftShift) && lockShiftJump)
            playerRB.velocity = new Vector2(playerRB.velocity.x * jumpSpeed, ((jumpHeight * 1.05f * Mathf.Sin(35f * Mathf.Deg2Rad)) - (9.8f * Time.deltaTime)));
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
        {
            GameObject.FindGameObjectWithTag("Footstep").GetComponent<Animator>().SetFloat("Velocity", playerAnim.GetFloat("Velocity"));
            playerAnim.SetFloat("Velocity", 0);
        }
        else
        {
            playerAnim.SetFloat("Velocity", Mathf.Abs(playerRB.velocity.x));
            GameObject.FindGameObjectWithTag("Footstep").GetComponent<Animator>().SetFloat("Velocity", playerAnim.GetFloat("Velocity"));
            GameObject.FindGameObjectWithTag("Footstep").GetComponent<Animator>().SetBool("Jumping", isJumping);
        }
    }

    IEnumerator TriggerCutscene()
    {
        fadePanel.SetBool("out", true);
        yield return null;
        Cutscene.cutsceneIndex = 1;
        Cutscene.playCutscene = true;
    }
}