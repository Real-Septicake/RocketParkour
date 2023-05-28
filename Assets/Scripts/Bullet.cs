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
            CircleCollider2D blast = gameObject.AddComponent<CircleCollider2D>();
            blast.radius = 10;
            blast.isTrigger = true;
            Destroy(gameObject, 0.5f);
        }
    }
}
