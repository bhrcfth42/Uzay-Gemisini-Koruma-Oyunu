using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanController : MonoBehaviour
{
    public GameObject mermi;
    float can = 100f;
    float mermiHizi = 8f;
    float saniyeBasiMermiAtma = 0.6f;
    private int puan = 200;
    private ScoreController scoreController;
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
                scoreController.ScoreArttir(puan);
                AudioSource.PlayClipAtPoint(olumSesi,transform.position);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreController = GameObject.Find("Score").GetComponent<ScoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        float mermiatmaolasiligi = Time.deltaTime * saniyeBasiMermiAtma;
        if (Random.value < mermiatmaolasiligi)
        {
            AtesEtme();
        }
    }
    void AtesEtme()
    {
        Vector3 baslangicPozisyonu = transform.position;
        GameObject dusmanMermi = Instantiate(mermi, baslangicPozisyonu + new Vector3(0, -0.8f, 0f), Quaternion.identity) as GameObject;
        dusmanMermi.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -mermiHizi);
        AudioSource.PlayClipAtPoint(atesSesi,transform.position);
    }
}
