using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10;
    public int enemyHealth = 50;
    private GameObject player;

    //public ProjectileBehaviour projectilePrefab;
    //public Transform launchOffSet;
    void Start()
    {
        player = GameObject.Find("Player");
        //InvokeRepeating("shootLaser", 2.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();

    }

    public void followPlayer()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        transform.position += lookDirection * Time.deltaTime * speed;

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Projectile"){
            enemyHealth -= 10;
            if(enemyHealth < 0){
                Destroy(this.gameObject);
            }
        }    
    }
}
