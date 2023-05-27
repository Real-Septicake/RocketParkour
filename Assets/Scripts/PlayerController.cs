using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Tooltip("How fast the player moves")]
    private float speed;
    private readonly float ACCEL = 0.25f;
    private readonly float PRESS_DECEL = 0.15f, RELEASE_DECEL = 0.2f;
    private readonly float MAX_VEL = 30;
    private readonly float GRAVITY = 0.075f; 

    private bool isGrounded = false;


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
        bool inputY = Input.GetKey("space");

        //Apply acceleration
        velocities.x += inputX*ACCEL;
        if(inputY) isGrounded = false;
        velocities.y += ACCEL*((inputY)?1:0);

        //Apply deceleration based on if the player is actively accelerating
        velocities.x = Mathf.Sign(velocities.x) * Mathf.Max(Mathf.Abs(velocities.x) - ((inputX == 0)?RELEASE_DECEL:PRESS_DECEL), 0);
        // velocities.y = Mathf.Sign(velocities.y) * (Mathf.Abs(velocities.y) - ((inputY == 0)?RELEASE_DECEL:PRESS_DECEL));

        if(!isGrounded){
            velocities.y -= GRAVITY;
        } 

        //Clamp velocity
        velocities = Vector2.ClampMagnitude(velocities, MAX_VEL);

        myRB2D.velocity = velocities;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isGrounded){
            velocities.y = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        isGrounded = true;
    }

    // private void OnTriggerExit2D(Collider2D collision){
    //     isGrounded = false;
    // }
}
