using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 25f);
    private DialogueUI dialogueUI;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    float gravityScaleAtStart;
    BoxCollider2D myFeetCollider;

    void Awake()
    {
        dialogueUI = FindFirstObjectByType<DialogueUI>();
    }

    bool isAlive = true;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueUI.IsOpen) { return; }  
        if(!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (dialogueUI.IsOpen) { return; }
        if(!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (dialogueUI.IsOpen) { return; }
        if(!isAlive) { return; }
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if(value.isPressed)
        {
            myRigidbody.linearVelocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.linearVelocity.y);
        myRigidbody.linearVelocity = playerVelocity;

        myAnimator.SetBool("isRunning", Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon);
    }

    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;
        if(hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.linearVelocity.x), 1f);
        }
        
    }

    void ClimbLadder()
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        myRigidbody.gravityScale = 0f;
        Vector2 climbVelocity = new Vector2(myRigidbody.linearVelocity.x, moveInput.y * climbSpeed);
        myRigidbody.linearVelocity = climbVelocity;
    
        myAnimator.SetBool("isClimbing", Mathf.Abs(myRigidbody.linearVelocity.y) > Mathf.Epsilon);
    }

    void Die()
    {
       if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.linearVelocity = deathKick;
            FindAnyObjectByType<GameSession>().ProcessPlayerDeath();
        }
    }

}
