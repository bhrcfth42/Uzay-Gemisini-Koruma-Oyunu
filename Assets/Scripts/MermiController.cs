using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermiController : MonoBehaviour
{
    float verdigiZarar = 10f;
    public void CarptigindaYokOlma()
    {
        Destroy(gameObject);
    }
    public float ZararVerme()
    {
        return verdigiZarar;
    }
}
