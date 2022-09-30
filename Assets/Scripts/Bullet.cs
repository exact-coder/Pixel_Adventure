using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject enemyDestroyVFX;
    private CircleCollider2D bulletCollider;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        bulletCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy")
        {
            //Enemy Destroy
            Destroy(collision.gameObject);

            //show some particles 
            GameObject vfx = Instantiate(enemyDestroyVFX,collision.gameObject.transform.position, Quaternion.identity);
            Destroy(vfx,1f);


            //Bullet Destroy
            Destroy(gameObject);
        }
        if (bulletCollider.IsTouchingLayers(groundLayer))
        {
            Destroy(gameObject);
        }

    }
}
