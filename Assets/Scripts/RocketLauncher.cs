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
    
    private void Start()
    {
        //animator = GameObject.Find("Player Animation").GetComponent<Animator>();
    }
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = mousePos.x - transform.position.x;
        mousePos.y = mousePos.y - transform.position.y;

        var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        //transform.position += Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
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
        }


        timer = Mathf.Max(0f, timer - Time.fixedDeltaTime);
    }

    private void MouseDown()
    {
        // Cursor.visible = false;
        clicking = true;
        if (timer <= 0f)
        {
            // Audio
            //windUp.Play();
        }
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
            bullet.GetComponent<Rigidbody2D>().velocity = mousePos.normalized * projVelocity;
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
