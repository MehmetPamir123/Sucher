using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLevel : MonoBehaviour
{
    public Sprite doorClosed;
    public Sprite doorOpened;
    bool isColliding;
    IEnumerator Wait(int timeSeconds)
    {
        yield return new WaitForSeconds(timeSeconds);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && this.gameObject.GetComponent<SpriteRenderer>().sprite == doorOpened)
        {
            MapMaker.currentLevel++;
            collision.gameObject.GetComponent<OyuncuHareket>().key = false;

            GameObject.FindGameObjectWithTag("MapMaker").GetComponent<MapMaker>().ReStarto();
            Wait(1);
        }
    }
}
