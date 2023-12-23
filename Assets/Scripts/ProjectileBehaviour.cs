using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    private UnityEngine.Vector3 targetPosition;

    //private UnityEngine.Vector3 lookDirection;

    void Start()
    {
        //lookDirection = (Input.mousePosition - transform.position).normalized; 

        // Fare pozisyonunu al ve hedef konumunu ayarla
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        targetPosition.z = 0f;
        Debug.Log("Target position x: " + targetPosition.x + "Target position y: " + targetPosition.y + "Target position z: " + targetPosition.z);

        // Projectile'ın hedefe doğru dönmesi
        UnityEngine.Vector2 direction = (targetPosition - transform.position).normalized;

        targetPosition.x += 10 * direction.x;
        targetPosition.y += 10 * direction.y;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = UnityEngine.Quaternion.AngleAxis(angle, UnityEngine.Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += lookDirection * Time.deltaTime * speed;  

        // Projectile'ı hedefe doğru hareket ettir
        transform.position = UnityEngine.Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Eğer projectile hedefe ulaştıysa yok et
        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Border" || collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
