using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField]
    private Vector2 mousePos;

    public GameObject player;

    public GameObject projectile;
    public float projVelocity;
    public float cooldown;
    public float timer;
    public float damage;

    //public AudioSource windUp;
    //public AudioSource shoot;

    //public Animator animator;

    private bool clicking;
    [SerializeField]
    private Vector2 mousePin;

    private void Start()
    {
        //animator = GameObject.Find("Player Animation").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = player.transform.position;
        if (Input.GetAxis("Fire1") == 1)
        {
            if (!clicking)
            {
                // MouseDown
                MouseDown();
                //animator.SetBool("Slingshot Drawn", true);
                //animator.SetBool("False Draw", false);
            }

            
        }
        else if (clicking)
        {
            // MouseUp
            MouseUp();
            //animator.SetBool("Slingshot Drawn", false);
            //animator.SetBool("Slingshot Attack", true);
            mousePin = Vector2.zero;
        }

        // MouseDrag
        MouseDrag();

        timer = Mathf.Max(0f, timer - Time.fixedDeltaTime);
    }

    private void MouseDown()
    {
        mousePin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        // Cursor.visible = false;
        clicking = true;
        if (timer <= 0f)
        {
            // Audio
            //windUp.Play();
        }
    }

    private void MouseDrag()
    {
        var mouseScreenPos = Input.mousePosition;
        var startingScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        mouseScreenPos.x -= startingScreenPos.x;
        mouseScreenPos.y -= startingScreenPos.y;
        var angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localPosition += Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //transform.localPosition *= 2f; /* Make the mouse more sensitive to compensate for the Camera's STVP function in the line above */
        /*if (transform.localPosition.magnitude > 5f)
        {
            transform.localPosition = Vector2.ClampMagnitude(transform.localPosition, 5f);
        }
        // transform.localPosition *= -1f;*/

    }

    private void MouseUp()
    {
        if (transform.localPosition != Vector3.zero)
        {
            ShootBullet();
            //animator.SetBool("False Draw", true);
        }
        clicking = false;
    }

    private void ShootBullet()
    {
        // Shoot the bullet
        if (timer <= 0f)
        {
            // Create the bullet
            GameObject bullet = Instantiate(projectile);
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody2D>().velocity = transform.localPosition.normalized * projVelocity;
            bullet.GetComponent<Bullet>().damage = damage;
            timer = cooldown;

            // Audio
            //windUp.Stop();
            //shoot.Play();
        }

        // Reset position
        // TEMPORARY - replace with an Ienumerator that pulls it back with force
        //transform.localPosition = Vector2.zero;
    }
}
