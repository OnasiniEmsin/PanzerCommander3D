using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public int jon=100,jon1=100,jonum=100;
    public TMP_Text hptext,deltahptext,_ism;
    public Image deltaDamage;
    bool tegdi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(jon!=jon1){
        
            deltahptext.text=(jon-jon1).ToString();
            deltahptext.gameObject.SetActive(true);
            StartCoroutine("sanoq");
            tegdi=true;
        }
        if(!tegdi){
            StopCoroutine("sanoq");
        }
        
    }
    IEnumerator sanoq(){
        while (true){
            yield return new WaitForSeconds(3);
            deltahptext.gameObject.SetActive(false);
            float a=jon;
            a/=jonum;
            deltaDamage.fillAmount=a;
            jon1=jon;
            tegdi=false;
        }
    }
}
