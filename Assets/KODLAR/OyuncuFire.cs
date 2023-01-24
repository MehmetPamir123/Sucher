using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class OyuncuFire : MonoBehaviour
{
    public static float FireBlastEksilecekSayi = 50;
    public static float FireParticleEksilecekSayi = 2;

    public bool fire = true;
    public float fireUpdate;
    public float fireUpdateSpeed;
    public float fireLimit;
    public float fireAzalmaSpeed;

    public GameObject PlayerFlameParticle;
    GameObject InstantiateParticle;
    public Image mask;
    float defaultMaskY;

    private void Start()
    {
        fireUpdate = fireLimit;
        mask = GameObject.Find("Masker").GetComponent<Image>();
        defaultMaskY = mask.transform.position.y;
        InstantiateParticle = Instantiate(PlayerFlameParticle, transform.parent.position, Quaternion.identity, transform.parent);
        MaskPosRestorer();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fire Plate" && fireUpdate<fireLimit)
        {

            if (collision.gameObject.GetComponent<FirePlate>().isFire == true)
            {
                fireUpdate += fireUpdateSpeed * Time.deltaTime;
                MaskPosRestorer();
                GameObject.Find("PlayerLight").GetComponent<Light2D>().intensity = fireUpdate/fireLimit*0.9f;
                if (fireUpdate >= FireBlastEksilecekSayi)
                {
                    if (GameObject.FindGameObjectWithTag("PlayerFlameParticle") == null)
                    {
                        InstantiateParticle = Instantiate(PlayerFlameParticle, transform.parent.position, Quaternion.identity, transform.parent);
                    }
                    fire = true;
                }
            }

        }
        if (fireUpdate >= fireLimit)
            fireUpdate = fireLimit;
    }

    public void Fire(float azaltilacakSayi)
    {
        if(fireUpdate - azaltilacakSayi >= 0)
        {
            fireUpdate -= azaltilacakSayi;
 

            
            MaskPosRestorer();
        }


    }
    public void FireAzaltici()
    {
        fireUpdate -= Time.deltaTime * fireAzalmaSpeed;
        MaskPosRestorer();
    }
    void MaskPosRestorer()
    {
        if(fireUpdate < FireBlastEksilecekSayi)
        {
            fire = false;
            Destroy(InstantiateParticle);
        }
        GameObject.Find("PlayerLight").GetComponent<Light2D>().intensity = fireUpdate / fireLimit * 0.9f;
        Vector2 maskPos = new Vector2(mask.transform.position.x, defaultMaskY + (fireUpdate* 100 / fireLimit));
        mask.transform.position = maskPos;
    }
    public void NewLevel()
    {
        if(GameObject.FindGameObjectWithTag("PlayerFlameParticle") == null)
        {
            InstantiateParticle = Instantiate(PlayerFlameParticle, transform.parent.position, Quaternion.identity, transform.parent);
        }
        fireUpdate = fireLimit;
        GameObject.Find("PlayerLight").GetComponent<Light2D>().intensity = fireUpdate / fireLimit * 0.9f;
        fire = true;
        MaskPosRestorer();
    }
}
