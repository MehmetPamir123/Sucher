using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraHareketleri : MonoBehaviour
{
    public GameObject MouseHelperBoom;
    public GameObject MouseHelperLaser;
    public AudioSource CampFireSource;
    float playerSpriteScaleX;
    float CooldownForSeconds;
    float spaceDownSeconds=0;
    float mouseDownSeconds=0;
    private void Update()
    {
        if (mouseDownSeconds<=0 &&Input.GetMouseButton(0) && GameObject.Find("OyuncuFireHelper").GetComponent<OyuncuFire>().fireUpdate >= OyuncuFire.FireBlastEksilecekSayi)
        {
            mouseDownSeconds = 1;
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject lightHelper = Instantiate(MouseHelperBoom, mouseWorldPos,Quaternion.identity);
            lightHelper.GetComponent<MouseHelper>().MouseHelperStarterBoom(mouseWorldPos);
            PlayerHandRotateToMouse(mouseWorldPos);
            FindObjectOfType<GlobalMusic>().CustomeudioPlayAudio("FireballFire");
        }
        if (CooldownForSeconds <= 0 &&Input.GetMouseButton(1) && GameObject.FindGameObjectsWithTag("FireCircle").Length<=40 && GameObject.Find("OyuncuFireHelper").GetComponent<OyuncuFire>().fireUpdate >= OyuncuFire.FireParticleEksilecekSayi)
        {
            CooldownForSeconds = 0.04f;
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(MouseHelperLaser, mouseWorldPos, Quaternion.identity);
            GameObject.Find("OyuncuFireHelper").GetComponent<OyuncuFire>().Fire(OyuncuFire.FireParticleEksilecekSayi);
            PlayerHandRotateToMouse(mouseWorldPos);
            StartCoroutine(SpawnCooldown());
            CampFireSource.Play();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            spaceDownSeconds-=Time.deltaTime;
            if(spaceDownSeconds <= 0)
            {
                GameObject.Find("_MapMaker").GetComponent<MapMaker>().ReStarto();
                spaceDownSeconds = 2;
            }
        }
        else
        {
            spaceDownSeconds = 2;
        }
        mouseDownSeconds -= Time.deltaTime;
        CooldownForSeconds -= Time.deltaTime;
        if (Input.GetMouseButtonUp(1))
        {
            GameObject.Find("PlayerHandFire").transform.rotation = Quaternion.Euler(new Vector3(GameObject.Find("PlayerHandFire").transform.rotation.x, GameObject.Find("PlayerHandFire").transform.rotation.y, 0));
            CampFireSource.Stop();
        }
    }
    Transform transPlayer;
    public float smoothSpeed = 0.125f;
    private void Start()
    {
        playerSpriteScaleX = GameObject.Find("PlayerSprite").GetComponent<Transform>().localScale.x;
        transPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        float smoothSpeed1 = smoothSpeed * Time.deltaTime;
        Vector3 desiredPosition = new Vector3(transPlayer.position.x, transPlayer.position.y, transform.position.z);
        Vector3 takipPos = Vector3.Lerp(transform.position, desiredPosition,smoothSpeed1);
        transform.position = takipPos;
    }
    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(CooldownForSeconds);
    }
    void PlayerHandRotateToMouse(Vector2 mousePos)
    {
        GameObject targetObject = GameObject.Find("PlayerHandFire");

        Vector2 positionOnScreen = targetObject.transform.position;
        //Camera.main.WorldToViewportPoint(targetObject.transform.position)

        //Get the angle between the points
        float angle = Mathf.Atan2(positionOnScreen.y - mousePos.y, positionOnScreen.x - mousePos.x) * Mathf.Rad2Deg;

        if (-90 < angle && angle < 90)
        {
            GameObject.Find("PlayerSprite").GetComponent<Transform>().localScale = new Vector3(-playerSpriteScaleX, GameObject.Find("PlayerSprite").GetComponent<Transform>().localScale.y, GameObject.Find("PlayerSprite").GetComponent<Transform>().localScale.z);
        }
        else
        {
            GameObject.Find("PlayerSprite").GetComponent<Transform>().localScale = new Vector3(playerSpriteScaleX, GameObject.Find("PlayerSprite").GetComponent<Transform>().localScale.y, GameObject.Find("PlayerSprite").GetComponent<Transform>().localScale.z);

        }
        targetObject.transform.rotation = Quaternion.Euler(new Vector3(targetObject.transform.rotation.x, targetObject.transform.rotation.y, angle - 90));
    }
}
