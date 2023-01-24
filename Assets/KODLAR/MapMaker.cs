using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class MapMaker : MonoBehaviour
{
    static public int FirePlateTotal;
    static public int YakilanFirePlateTotal;
    private void Start()
    {
        Debug.LogError("Oyuncu alt�nda alan olmadan da z�playabiliyor.");
        //BUGLAR
        ExistingMaps texture = existingMaps[currentLevel];
        map = texture.map;
        Player = GameObject.FindGameObjectWithTag("Player");
        GenerateLevel();
        if(currentLevel == 1)
        {
            GameObject.Find("TutorialItems").SetActive(false);
            StartCoroutine(Tutorial());
        }else if(currentLevel == 0)
        {
            StartCoroutine(Tutorial());
        }
    }
    IEnumerator Tutorial() //Tutorial itemlerini kapatmak i�in
    {
        //seviye 0'dan de�i�ene kadar bekle ve de�i�ince TutorialItems'leri sil.
        yield return new WaitUntil(() => currentLevel != 0);
        GameObject.Find("TutorialItems").SetActive(false);
    }

    static public int currentLevel;
    public Texture2D map; //ekleyece�imiz harita
    GameObject Player; //yerini belirleyece�imiz oyuncu
    public Color playerTpColor; //Hangi renk "Player"� ���nlayacak?

    //Buras� i�in ColorPrefab koduna ihtiyac�m�z var. Her rengin hangi objeye denk gelece�ini ise Unity "Editor" yerinde ayarlayaca��z.
    public ColorPrefab[] colorMappings;
    public ExistingMaps[] existingMaps; 





    //Bu kodu �al��t�rarak ekledi�imiz haritay� direkt �izece�iz.
    public void GenerateLevel()
    {

        //Haritay� ba�tan a�a�� x ve y eksenlerinde tarayacak ve her blok i�in "GenerateTile" kodu �al��acak.
        for (int x = 0; x < map.width; x++)
        {
            for(int y= 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }
    
    void GenerateTile(int x, int y) //Yaz�l� bir pixel buldu�unda o konumu al�yor.
    {
        Color pixelColor = map.GetPixel(x, y);
        if(pixelColor.a == 0) 
        {
            return;
        }

        //Bulunan her bir renk i�in "colorMapping"deki her elementi tek tek deniyor. Tutan elementin alt�na koydu�umuz objeyi ise o konuma ���nl�yor.
        foreach (ColorPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
                //Kirlilik olmas�n diye olu�turdu�umuz objenin alt�na 'child' olarak ekliyoruz.
                return;
            } 
        }
        if (pixelColor.Equals(playerTpColor))
        {
            Vector2 position = new Vector2(x, y);
            Player.transform.position = position;
        }
        
    }

    //B�l�m bitince tekrardan yeni harita olu�turmak i�in. Olu�turamazsa Oyun sonuna atacak.
    public void ReStarto()
    {

        if(currentLevel <= existingMaps.Length)
        {
            ExistingMaps texture = existingMaps[currentLevel];
            map = texture.map;
            GameObject.Find("GlobalLight").GetComponent<Light2D>().intensity = 0;

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            FirePlateTotal = 0;
            YakilanFirePlateTotal = 0;
            GenerateLevel();

        }
        else
        {
            SceneManager.LoadScene("End");
        }
        GameObject.FindGameObjectWithTag("OyuncuFireHelper").GetComponent<OyuncuFire>().NewLevel();
        GameObject.FindGameObjectWithTag("Player").GetComponent<OyuncuHareket>().key = false;
    }
}
