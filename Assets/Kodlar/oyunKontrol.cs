using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyunKontrol : MonoBehaviour
{
    public GameObject gokyuzuBir;
    public GameObject gokyuzuIki;
    public float gokyuzuHiz = -1.5f;

    Rigidbody2D fizikBir;
    Rigidbody2D fizikIki;

    float uzunluk = 0;

    public GameObject engel;
    public int kacEngel;
    GameObject[] engeller;

    float engelZaman = 0;
    int sayac = 0;

    bool oyunBittimi = true;

    void Start()
    {
        fizikBir = gokyuzuBir.GetComponent<Rigidbody2D>();
        fizikIki = gokyuzuIki.GetComponent<Rigidbody2D>();

        fizikBir.velocity = new Vector2(gokyuzuHiz, 0);
        fizikIki.velocity = new Vector2(gokyuzuHiz, 0);

        uzunluk = gokyuzuBir.GetComponent<BoxCollider2D>().size.x;

        engeller = new GameObject[kacEngel];

        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i] = Instantiate(engel, new Vector2(-20, -20), Quaternion.identity);
            // engeller'olusturuyoruz
            Rigidbody2D fizikEngel = engeller[i].AddComponent<Rigidbody2D>();
            // asagi dusmelerini engeleiyoruz
            fizikEngel.gravityScale = 0;
            fizikEngel.velocity = new Vector2(gokyuzuHiz, 0);
        }

    }


    void Update()
    {
        if (oyunBittimi)
        {
            if (gokyuzuBir.transform.position.x <= -uzunluk)
            {
                gokyuzuBir.transform.position += new Vector3(uzunluk * 2, 0);
            }

            if (gokyuzuIki.transform.position.x <= -uzunluk)
            {
                gokyuzuIki.transform.position += new Vector3(uzunluk * 2, 0);
            }


            //---------------------------------

            engelZaman += Time.deltaTime;
            if (engelZaman > 4f)
            {
                engelZaman = 0;
                float Yeksenim = Random.Range(-0.50f, 1.10f);
                engeller[sayac].transform.position = new Vector3(10, Yeksenim);
                sayac++;

                if (sayac >= engeller.Length)
                {
                    sayac = 0;
                }
            }
        }

    }

    public void oyunBitti()
    {
        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            fizikBir.velocity = Vector2.zero;
            fizikIki.velocity = Vector2.zero;
        }
        oyunBittimi = false;
    }
}
