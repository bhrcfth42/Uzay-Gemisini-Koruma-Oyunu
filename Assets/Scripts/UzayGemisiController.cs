using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UzayGemisiController : MonoBehaviour
{
    private float xMax, xMin;
    private float inceAyar = 0.7f;
    public GameObject mermi;
    float mermiHizi = 8f;
    float atesEtmeAraligi = 2f;
    float can = 200f;
    public AudioClip atesSesi,olumSesi;
    private void OnTriggerEnter2D(Collider2D other)
    {
        MermiController carpanMermi = other.gameObject.GetComponent<MermiController>();
        if (carpanMermi)
        {
            carpanMermi.CarptigindaYokOlma();
            can -= carpanMermi.ZararVerme();
            if (can <= 0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(olumSesi,transform.position);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        float uzaklikZ = transform.position.z - Camera.main.transform.position.z;
        Vector3 solKameraKenarı = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, uzaklikZ));
        Vector3 sagKameraKenarı = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, uzaklikZ));
        xMin = solKameraKenarı.x + inceAyar;
        xMax = sagKameraKenarı.x - inceAyar;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InvokeRepeating("AtesEtme", 0.001f, atesEtmeAraligi);
        }
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("AtesEtme");
        }
        FareHareket();
    }
    void AtesEtme()
    {
        GameObject gemiMermisi = Instantiate(mermi, transform.position + new Vector3(0, 0.9f, 0f), Quaternion.identity) as GameObject;
        gemiMermisi.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, mermiHizi, 0f);
        AudioSource.PlayClipAtPoint(atesSesi,transform.position);
    }
    void FareHareket()
    {
        Vector3 uzayGemisiKonumu = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);//Oyun barı y değerini tutuyoz
        float mouseKonumX = Input.mousePosition.x / Screen.width * 18;//mouse positionunu al ama bunu 18 kare yanyana olduğu için ona göre hesapla x değerini
        uzayGemisiKonumu.x = Mathf.Clamp(mouseKonumX, xMin, xMax);
        this.transform.position = uzayGemisiKonumu;
    }
}
