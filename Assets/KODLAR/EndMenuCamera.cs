using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenuCamera : MonoBehaviour
{
    public GameObject lightFollow;
    

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        lightFollow.transform.position = mousePosition;
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
