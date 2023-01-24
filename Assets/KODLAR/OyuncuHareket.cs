using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyuncuHareket : MonoBehaviour
{
    public bool key;
    public float moveSpeed; //x eksenindeki hareket h�z�
    public float jumpSpeed; //z�plama g�c�


    Rigidbody2D rb2d;

    [SerializeField] bool touchingGround = false; //yere de�ip de�medi�ini kontrol ediyoruz

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
        //Kullan�c� girdilerini al�r. moveInput ([a,d girdileri])
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
        //x ekseninde moveInput kadar h�z al�rken y ekseninde kendi h�z�yla devam ediyor. Kendi h�z�yla devam etmesinin nedeni yer �ekimi de olmas�.
        rb2d.velocity = new Vector2(moveInput, rb2d.velocity.y);
        
        //e�er w bas�ld�ysa ve yere de�iyorsa yukar�ya do�ru 10 kuvvetiyle �ek.
        if(Input.GetKey(KeyCode.W) && touchingGround == true)
        {
            if(rb2d.velocity.y <= 0)
            {
                rb2d.AddForce(new Vector2(0, jumpSpeed));

            }
            else
            {
            }

            touchingGround = false; //z�plad�ktan sonra havada tekrar z�plamamas� i�in
        }
        if(transform.position.y <= -50)
        {
            GameObject.Find("_MapMaker").GetComponent<MapMaker>().ReStarto();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CapsuleCollider2D c = collision.GetComponent<CapsuleCollider2D>();
        //e�er dokundu�u objenin "tag"� "Ground"sa yere de�iyor.
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
