using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class naturalanimation : MonoBehaviour
{
    public GameObject anims;
    public byte vaxt=100;
    float sanoq;
    void Start()
    {
        sanoq=vaxt;
        StartCoroutine("Sana");
    }

    // Update is called once per frame
    void Update()
    {
        if(sanoq<=0){
            sanoq=vaxt;
            anims.SetActive(false);
            anims.SetActive(true);
        }
    }
    IEnumerator Sana(){
        while (true){
            yield return new WaitForSeconds(5);
            sanoq-=5;
        }
    }
}
