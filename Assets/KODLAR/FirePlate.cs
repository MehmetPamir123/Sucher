using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FirePlate : MonoBehaviour
{
    public float lifeTime;
    public bool isFire;
    private void Start()
    {
        MapMaker.FirePlateTotal++;
        Debug.Log(MapMaker.FirePlateTotal);
    }
    public void LitFire()
    {
        MapMaker.YakilanFirePlateTotal++;
        Debug.Log(MapMaker.YakilanFirePlateTotal + " / " + MapMaker.FirePlateTotal);
        transform.GetChild(0).gameObject.SetActive(true);
        isFire = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<OyuncuHareket>().TryOpenDoor();
        //StartCoroutine(LightLife(lifeTime));
    }
    /*IEnumerator LightLife(float time)
    {
        yield return new WaitForSeconds(time);
        transform.GetChild(0).gameObject.SetActive(false);
        isFire = false;
    }*/
}
