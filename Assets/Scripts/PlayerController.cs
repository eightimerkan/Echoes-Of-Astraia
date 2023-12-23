using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 5;
    public int health = 3;
    public bool invincible = false;
    public float invincibleTimeout = 2f;
    public float invincibleTimer = 0f;
    public Rigidbody2D rb;
    public bool insideEnemy = false;

    public GameObject projectilePrefab;
    public Transform launchOffSet;
    public float projectileSpeed = 10f;


    //public SpriteRenderer yaralanmazEfekti;
    void Start()
    {

    }

    void FixedUpdate()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new UnityEngine.Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;

        var movement2 = Input.GetAxis("Vertical");
        transform.position += new UnityEngine.Vector3(0, movement2, 0) * Time.deltaTime * movementSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        invincibleControl();
        if (Input.GetMouseButtonDown(0))
        {
            /*Debug.Log("mouse tık");
            Instantiate(projectilePrefab, launchOffSet.position, transform.rotation);*/

            // Mouse pozisyonunu al
            UnityEngine.Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // Yaratılacak projectile'ın rotasyonunu ve yönünü ayarla
            UnityEngine.Vector2 direction = (mousePosition - launchOffSet.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            UnityEngine.Quaternion rotation = UnityEngine.Quaternion.AngleAxis(angle, UnityEngine.Vector3.forward);

            // Projectile'ı yarat ve hedefine doğru gönder
            GameObject newProjectile = Instantiate(projectilePrefab, launchOffSet.position, rotation);
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }


        }

    }

    /*private void OnTriggerStay2D(Collider2D collider)
    {
        loseHealth();
        if(collider.tag == "Enemy")
        {
            Debug.Log("çarpışma gerçekleşti");
            health -= 1;
            
            if(health == 0){
                
                Destroy(this.gameObject);
            }
            
        }
        
    }*/

    public void loseHealth()
    {
        // Eğer karakter yaralanmaz değilse
        if (!invincible)
        {
            // Canı miktar kadar azalt
            health -= 1;

            // Eğer can sıfırsa
            if (health == 0)
            {
                // Oyunu bitir
                Debug.Log("Oyun bitti!");
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("şuan invicibleız");

                // Karakteri yaralanmaz yap
                invincible = true;


                // Yaralanmaz sayacını yaralanmaz süresine eşitle
                invincibleTimer = invincibleTimeout;
            }
        }
    }

    public void invincibleControl()
    {
        // Eğer karakter yaralanmazsa
        if (invincible)
        {
            // Yaralanmaz sayacını azalt
            invincibleTimer -= Time.deltaTime;

            // Eğer yaralanmaz sayacı sıfırdan küçükse
            if (invincibleTimer < 0)
            {
                // Karakterin yaralanmazlığını kapat
                invincible = false;

                // Karakterin rengini beyaz yap
                //yaralanmazEfekti.color = Color.white;
            }
            else
            {
                // Karakterin rengini rastgele değiştir
                //yaralanmazEfekti.color = new Color(Random.value, Random.value, Random.value);
            }
        }
        else if (insideEnemy)
        {
            loseHealth();
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        // Eğer temas edilen nesne düşman ise
        if (other.tag == "Enemy")
        {
            insideEnemy = true;
            // Eğer karakter yaralanmaz değilse
            if (!invincible)
            {
                // Canı saniyede 5 azalt
                loseHealth();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            insideEnemy = false;

        }
    }


}
