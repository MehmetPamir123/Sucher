using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyuncuHareket : MonoBehaviour
{
    public bool key;
    public float moveSpeed; //x eksenindeki hareket hýzý
    public float jumpSpeed; //zýplama gücü


    Rigidbody2D rb2d;

    [SerializeField] bool touchingGround = false; //yere deðip deðmediðini kontrol ediyoruz

    public Animator playerAnim;
    public Sprite doorClosed;
    public Sprite doorOpened;

    float scaleCharacterX;
    private void Awake()
    {
        key = false;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        scaleCharacterX = this.gameObject.transform.GetChild(2).transform.localScale.x;
    }
    void FixedUpdate()
    {
        //Kullanýcý girdilerini alýr. moveInput ([a,d girdileri])
        float moveInput = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * moveSpeed;
        if(moveInput != 0)
        {
            playerAnim.SetBool("Walk", true);
            if(moveInput > 0)
                playerAnim.gameObject.GetComponent<Transform>().localScale = new Vector2(scaleCharacterX, playerAnim.gameObject.GetComponent<Transform>().localScale.y);
            else
                playerAnim.gameObject.GetComponent<Transform>().localScale = new Vector2(-scaleCharacterX, playerAnim.gameObject.GetComponent<Transform>().localScale.y);
        }
        else
        {
            playerAnim.SetBool("Walk", false);
        }
        //x ekseninde moveInput kadar hýz alýrken y ekseninde kendi hýzýyla devam ediyor. Kendi hýzýyla devam etmesinin nedeni yer çekimi de olmasý.
        rb2d.velocity = new Vector2(moveInput, rb2d.velocity.y);
        
        //eðer w basýldýysa ve yere deðiyorsa yukarýya doðru 10 kuvvetiyle çek.
        if(Input.GetKey(KeyCode.W) && touchingGround == true)
        {
            if(rb2d.velocity.y <= 0)
            {
                rb2d.AddForce(new Vector2(0, jumpSpeed));

            }
            else
            {
            }

            touchingGround = false; //zýpladýktan sonra havada tekrar zýplamamasý için
        }
        if(transform.position.y <= -50)
        {
            GameObject.Find("_MapMaker").GetComponent<MapMaker>().ReStarto();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CapsuleCollider2D c = collision.GetComponent<CapsuleCollider2D>();
        //eðer dokunduðu objenin "tag"ý "Ground"sa yere deðiyor.
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Grass")
        {
            touchingGround = true;
        }
        if (collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject);
            key = true;
            TryOpenDoor();
        }
        
    }
    public void TryOpenDoor()
    {
        if(key == true && MapMaker.FirePlateTotal == MapMaker.YakilanFirePlateTotal)
        {
            GameObject.FindGameObjectWithTag("Portal").GetComponent<SpriteRenderer>().sprite = doorOpened;
        }
    }
}
