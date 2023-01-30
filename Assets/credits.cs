using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class credits : MonoBehaviour
{
   public void ReturnMenu()
    {
        SceneManager.LoadScene("MainMenu");
        GameObject.FindGameObjectWithTag("Music").GetComponent<GlobalMusic>().CustomeudioPlayAudio("Button");
    }
}
