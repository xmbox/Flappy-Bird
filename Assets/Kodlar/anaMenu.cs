using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anaMenu : MonoBehaviour
{

    public Text puanText;
    public Text normalPuan;

    void Start()
    {
        int yuksekPuan = PlayerPrefs.GetInt("kayit");
        int puan = PlayerPrefs.GetInt("puans");
        int reklamSayac = PlayerPrefs.GetInt("reklams");

        puanText.text = "En Yuksek Puan = " + yuksekPuan;
        normalPuan.text = "Puan = " + puan;

        ////oyunda 2 puan alindiginda reklam goster dedik
        //if (puan == 2)
        //{
        //    GameObject.FindGameObjectWithTag("reklam").GetComponent<reklam>().reklamiGoster();
        //}

        if (reklamSayac == 3)
        {
            GameObject.FindGameObjectWithTag("reklam").GetComponent<reklam>().reklamiGoster();
            // 3 kez gosterdikten sonra kaydedip reklami sifirliyoruz
            PlayerPrefs.SetInt("reklams", 0);
        }
    }


    void Update()
    {
        
    }

    public void basla()
    {
        SceneManager.LoadScene("level1");
    }

    public void bitir()
    {
        Application.Quit();
    }
}
