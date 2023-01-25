using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MouseHelper : MonoBehaviour
{
    public GameObject LeftClickFire;
    public ParticleSystem m_ParticleSystem;
    public float lightTime;
    public float leftClickTargetGitmeSüresi;
    public Vector2 mousePos;
    public void MouseHelperStarterBoom(Vector2 mousePos1)
    {
        mousePos = mousePos1;
        Yolda();
        GetComponent<Light2D>().intensity = 0;
    }
    public void Yolda()
    {
        Vector2 positionOnScreen = GameObject.FindWithTag("Player").transform.position;
        float angle = Mathf.Atan2(positionOnScreen.y - mousePos.y, positionOnScreen.x - mousePos.x) * Mathf.Rad2Deg;
        GameObject a = Instantiate(LeftClickFire, GameObject.FindWithTag("Player").transform.position, Quaternion.Euler(new Vector3(0,0,angle+180)));
        a.GetComponent<LeftClickFire>().SetDestination(mousePos,leftClickTargetGitmeSüresi);
        StartCoroutine(Patlayacak());
    }
    public void Patladý()
    {
        GetComponent<Light2D>().intensity = lightTime;
        if (GameObject.Find("OyuncuFireHelper").GetComponent<OyuncuFire>().fireUpdate >= OyuncuFire.FireBlastEksilecekSayi)
        {
            Instantiate(m_ParticleSystem, mousePos, Quaternion.identity);
            GameObject.Find("OyuncuFireHelper").GetComponent<OyuncuFire>().Fire(OyuncuFire.FireBlastEksilecekSayi);
        }
        
    }
    IEnumerator Patlayacak()
    {
        yield return new WaitForSeconds(leftClickTargetGitmeSüresi);
        Patladý();
        while (lightTime > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime*2);
            lightTime -= Time.deltaTime*2;
            GetComponent<Light2D>().intensity = lightTime;
            if (lightTime <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    
}
