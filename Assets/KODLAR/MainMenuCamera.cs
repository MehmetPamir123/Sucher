using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCamera : MonoBehaviour
{
    public GameObject lightFollow;
    GameObject button;

    private void Start()
    {
        button = GameObject.Find("F");
        Vector2 buttonTrans = new Vector2(Random.Range(-460, 460), Random.Range(-270, 270));
        button.GetComponent<RectTransform>().localPosition = buttonTrans;
        
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        lightFollow.transform.position = mousePosition;
    }
    public void FirstStart()
    {
        MapMaker.currentLevel = 1;
        SceneManager.LoadScene("SampleScene");
        FindObjectOfType<GlobalMusic>().CustomeudioPlayAudio("Button");
        FindObjectOfType<GlobalMusic>().CustomeudioPlayAudio("Calm");

    }
    public void TutorialStart()
    {
        MapMaker.currentLevel = 0;
        SceneManager.LoadScene("SampleScene");
        FindObjectOfType<GlobalMusic>().CustomeudioPlayAudio("Button");
        FindObjectOfType<GlobalMusic>().CustomeudioPlayAudio("Calm");

    }
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
        FindObjectOfType<GlobalMusic>().CustomeudioPlayAudio("Button");
    }
}
