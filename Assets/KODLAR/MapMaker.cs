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
        Debug.LogError("Oyuncu altýnda alan olmadan da zýplayabiliyor.");
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
    IEnumerator Tutorial() //Tutorial itemlerini kapatmak için
    {
        //seviye 0'dan deðiþene kadar bekle ve deðiþince TutorialItems'leri sil.
        yield return new WaitUntil(() => currentLevel != 0);
        GameObject.Find("TutorialItems").SetActive(false);
    }

    static public int currentLevel;
    public Texture2D map; //ekleyeceðimiz harita
    GameObject Player; //yerini belirleyeceðimiz oyuncu
    public Color playerTpColor; //Hangi renk "Player"ý ýþýnlayacak?

    //Burasý için ColorPrefab koduna ihtiyacýmýz var. Her rengin hangi objeye denk geleceðini ise Unity "Editor" yerinde ayarlayacaðýz.
    public ColorPrefab[] colorMappings;
    public ExistingMaps[] existingMaps; 





    //Bu kodu çalýþtýrarak eklediðimiz haritayý direkt çizeceðiz.
    public void GenerateLevel()
    {

        //Haritayý baþtan aþaðý x ve y eksenlerinde tarayacak ve her blok için "GenerateTile" kodu çalýþacak.
        for (int x = 0; x < map.width; x++)
        {
            for(int y= 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }
    
    void GenerateTile(int x, int y) //Yazýlý bir pixel bulduðunda o konumu alýyor.
    {
        Color pixelColor = map.GetPixel(x, y);
        if(pixelColor.a == 0) 
        {
            return;
        }

        //Bulunan her bir renk için "colorMapping"deki her elementi tek tek deniyor. Tutan elementin altýna koyduðumuz objeyi ise o konuma ýþýnlýyor.
        foreach (ColorPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
                //Kirlilik olmasýn diye oluþturduðumuz objenin altýna 'child' olarak ekliyoruz.
                return;
            } 
        }
        if (pixelColor.Equals(playerTpColor))
        {
            Vector2 position = new Vector2(x, y);
            Player.transform.position = position;
        }
        
    }

    //Bölüm bitince tekrardan yeni harita oluþturmak için. Oluþturamazsa Oyun sonuna atacak.
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
