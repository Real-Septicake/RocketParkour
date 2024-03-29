using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Tooltip("How long the bullet lives:")]
    public float lifeTime = 5f;
    public float damage = 0;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("floor")){
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 3, Vector2.zero);
            foreach (RaycastHit2D r in hits){
                if(r.rigidbody.gameObject.layer == LayerMask.NameToLayer("player")){
                    Vector2 f = r.normal;
                    r.rigidbody.gameObject.GetComponent<PlayerController>().ApplyForce(new Vector2(f.x, f.y).normalized*-10);
                    break;
                }
            }
        }
        Destroy(gameObject, 0.1f);
    }
}
