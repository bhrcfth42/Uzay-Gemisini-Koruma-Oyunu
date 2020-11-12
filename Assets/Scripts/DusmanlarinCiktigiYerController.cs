using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanlarinCiktigiYerController : MonoBehaviour
{
    public GameObject dusmanPrefabi;
    float yukseklik = 5, genislik = 15;
    float hiz = 5f;
    bool sagaHareket = true;
    float xMax, xMin;
    float yaratmayiGecitirmeSuresi = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        float uzaklikZ = transform.position.z - Camera.main.transform.position.z;
        Vector3 solKameraKenarı = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, uzaklikZ));
        Vector3 sagKameraKenarı = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, uzaklikZ));
        xMin = solKameraKenarı.x;
        xMax = sagKameraKenarı.x;
        DusmanlarınTekTekUretimi();
    }

    // Update is called once per frame
    void Update()
    {
        if (sagaHareket)
        {
            transform.position += Vector3.right * hiz * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * hiz * Time.deltaTime;
        }
        float sagSinir = transform.position.x + genislik * 0.5f;
        float solSinir = transform.position.x - genislik * 0.5f;
        if (sagSinir > xMax)
        {
            sagaHareket = false;
        }
        else if (solSinir < xMin)
        {
            sagaHareket = true;
        }
        if (DusmanVarMi())
        {
            DusmanlarınTekTekUretimi();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(genislik, yukseklik));
    }
    bool DusmanVarMi()
    {
        foreach (Transform item in transform)
        {
            if (item.childCount > 0)
            {
                return false;
            }

        }
        return true;
    }
    // void DusmanUretimi()
    // {
    //     foreach (Transform item in transform)
    //     {
    //         GameObject dusman = Instantiate(dusmanPrefabi, item.transform.position, Quaternion.identity) as GameObject;
    //         dusman.transform.parent = item;
    //     }
    // }
    Transform SonrakiUygunPozisyon()
    {
        foreach (Transform item in transform)
        {
            if (item.childCount == 0)
            {
                return item;
            }

        }
        return null;
    }
    void DusmanlarınTekTekUretimi()
    {
        Transform uygunPozisyon = SonrakiUygunPozisyon();
        if (uygunPozisyon)
        {
            GameObject dusman = Instantiate(dusmanPrefabi, uygunPozisyon.transform.position, Quaternion.identity) as GameObject;
            dusman.transform.parent = uygunPozisyon;
        }
        if (SonrakiUygunPozisyon())
        {
            Invoke("DusmanlarınTekTekUretimi", yaratmayiGecitirmeSuresi);
        }
    }
}
