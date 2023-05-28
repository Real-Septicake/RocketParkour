using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Tooltip("How fast the player moves")]
    private float speed;
    private readonly float X_ACCEL = 0.25f, JUMP_FORCE = 10;
    private readonly float PRESS_DECEL = 0.15f, RELEASE_DECEL = 0.2f;
    private readonly float MAX_VEL = 30;
    private readonly float GRAVITY = 0.075f; 

    private bool isGrounded = false;

    private readonly Vector3 START_POS = new Vector3(0,1.5f,0);


    Rigidbody2D myRB2D;
    Animator myAnim;
    SpriteRenderer myRender;
    Vector2 velocities;

    private PhysicsMaterial2D FLOOR_MAT;
    private PhysicsMaterial2D WALL_MAT;

    // Start is called before the first frame update
    void Start()
    {
        FLOOR_MAT = (PhysicsMaterial2D) Resources.Load("Materials/Floor");
        WALL_MAT = (PhysicsMaterial2D) Resources.Load("Materials/Wall");

        myAnim = GetComponent<Animator>();
        myRB2D = GetComponent<Rigidbody2D>();
        myRender = GetComponent<SpriteRenderer>();
        velocities = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Values are used several times, this just makes code cleaner
        float inputX = Input.GetAxisRaw("Horizontal");
        bool inputY = Input.GetKeyDown("space");

        //Apply acceleration
        velocities.x += inputX*X_ACCEL;

        velocities.y += JUMP_FORCE*((inputY&&isGrounded)?1:0);
        if(inputY) isGrounded = false;
      

        //Apply deceleration based on if the player is actively accelerating
        velocities.x = Mathf.Sign(velocities.x) * Mathf.Max(Mathf.Abs(velocities.x) - ((inputX == 0)?RELEASE_DECEL:PRESS_DECEL), 0);
        // velocities.y = Mathf.Sign(velocities.y) * (Mathf.Abs(velocities.y) - ((inputY == 0)?RELEASE_DECEL:PRESS_DECEL));

        if(!isGrounded){
            velocities.y -= GRAVITY;
        } 

        //Clamp velocity
        velocities = Vector2.ClampMagnitude(velocities, MAX_VEL);

        myRB2D.velocity = velocities;

        if(Input.GetKeyDown(KeyCode.R)){
            transform.position = START_POS;
            velocities = Vector2.zero;
            myRB2D.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isGrounded){
            velocities.y = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("floor")){
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        isGrounded = false;
    }
}
