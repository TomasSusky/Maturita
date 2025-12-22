using UnityEngine;

public class Ember : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    BoxCollider2D myBoxCollider;
    CapsuleCollider2D myCapsuleCollider;
    bool canChangeDirection = true;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    
    void Update()
    {
        myRigidbody.linearVelocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")) || 
           myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            canChangeDirection = false;
        }
        else
        {
            canChangeDirection = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (canChangeDirection)
        {
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }  
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.linearVelocity.x)), 1f);
    }

}

