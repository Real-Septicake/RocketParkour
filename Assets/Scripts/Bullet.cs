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
            Destroy(gameObject, 0);
        }
    }
}
