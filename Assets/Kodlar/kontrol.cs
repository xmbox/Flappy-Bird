using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class kontrol : MonoBehaviour
{
    // kusun 3 hareketi icin dizi olusturuyoruz
    public Sprite[] KusSprite;
    SpriteRenderer spriteRenderer;
    // kusun ileri geri gitmesi
    bool ileriGeriKontrol = true;
    int kusSayac = 0;
    // kusun cok hizli kanat cirpmamasi icin bi degisken olusturalim
    float kusKanat = 0;

    Rigidbody2D fizik;
    int puan = 0;

    public Text PuanText;

    bool oyunbitti = true;

    oyunKontrol oyunKontrol;

    //AudioSource ses;
    //public AudioClip carpmaSesi;
    //public AudioClip kanatSesi;
    //public AudioClip puanSesi;

    AudioSource[] sesler;

    int enYuksekPuan = 0;

    int reklamSayac = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();

        oyunKontrol = GameObject.FindGameObjectWithTag("oyunkontrol").GetComponent<oyunKontrol>();

        //sesleri olsuturuyoruz
        //ses = GetComponent<AudioSource>();

        sesler = GetComponents<AudioSource>();

        // en yuksek puani oyun  asinda cekiyoruz

        PlayerPrefs.GetInt("kayit");

        //reklam olaylari
        reklamSayac = PlayerPrefs.GetInt("reklams");
        reklamSayac++;
        PlayerPrefs.SetInt("reklams", reklamSayac);
       
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && oyunbitti)
        {
            // yercekimini dusurup hizi 0 yaptik
            fizik.velocity = new Vector2(0,0);
            // ondan sonra kuvvet uyguladik
            fizik.AddForce(new Vector2(0,250));
            //kanat cirpma sesi veriyoruz
            //ses.clip = kanatSesi;
            //ses.Play();
            sesler[0].Play();
        }

        if (fizik.velocity.y>0)
        {
            transform.eulerAngles = new Vector3(0,0,45);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -45);
        }

        Animasyon();
    }

    void Animasyon()
    {
        kusKanat += Time.deltaTime;

        if (kusKanat > 0.2f)
        {
            kusKanat = 0;

            if (ileriGeriKontrol)
            {
                spriteRenderer.sprite = KusSprite[kusSayac];
                kusSayac++; // 0,1,2,3 olunca if gircek

                if (kusSayac == KusSprite.Length)
                {
                    kusSayac--;
                    ileriGeriKontrol = false;
                }
            }
            else
            {
                kusSayac--;
                spriteRenderer.sprite = KusSprite[kusSayac];

                if (kusSayac == 0)
                {
                    kusSayac++;
                    ileriGeriKontrol = true;
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "puan")
        {
            puan++;
            PuanText.text = "Puan = " + puan;
            //ses.clip = puanSesi;
            //ses.Play();
            sesler[2].Play();
        }

        if (col.gameObject.tag=="engel")
        {
            oyunbitti = false;
            oyunKontrol.oyunBitti();
            //ses.clip = carpmaSesi;
            //ses.Play();
            sesler[1].Play();
            //bir seferden fazla carpisma sesi olmamasi icin birkez carpinca collideri kapatiyoruz.
            GetComponent<CircleCollider2D>().enabled = false;

            //puani kaydedip en yuksek puani bulma

            if (puan> enYuksekPuan)
            {
                enYuksekPuan = puan;
                PlayerPrefs.SetInt("kayit", enYuksekPuan);
            }
            // invoke baska bir metodu ve kac saniye sonra cagrilacagini belirten bir metod 
            Invoke("menuyeDon", 2);
        }
    }

    void menuyeDon()
    {
        PlayerPrefs.SetInt("puans", puan);
        SceneManager.LoadScene("AnaMenu");
    }
}
