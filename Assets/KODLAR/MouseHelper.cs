using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MouseHelper : MonoBehaviour
{
    public ParticleSystem m_ParticleSystem;
    public float lightTime;
    public void MouseHelperStarterBoom(Vector2 mousePos)
    {
        if(GameObject.Find("OyuncuFireHelper").GetComponent<OyuncuFire>().fireUpdate >= OyuncuFire.FireBlastEksilecekSayi)
        {
            Instantiate(m_ParticleSystem, mousePos, Quaternion.identity);
            GameObject.Find("OyuncuFireHelper").GetComponent<OyuncuFire>().Fire(OyuncuFire.FireBlastEksilecekSayi);
        }

    }
    private void Update()
    {
        lightTime -= Time.deltaTime;
        GetComponent<Light2D>().intensity = lightTime;
        if(lightTime <= 0)
        {
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Fire Plate")
        {

            collision.GetComponent<FirePlate>().LitFire();
        }
        else
        {

        }
    }
}
