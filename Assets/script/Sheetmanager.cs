using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sheetmanager : MonoBehaviour
{
    public Dictionary<string, tank> dictio=new Dictionary<string, tank>();
    Dictionary<string, int> tanki=new Dictionary<string,int>();
    public TMP_Text[] texts;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("soniya");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator soniya(){
        while (true){
            yield return new WaitForSeconds(2);
            foreach (KeyValuePair<string,tank> Keys in dictio)
            {
                
                tanki[Keys.Value.ism]=Keys.Value.summaUrona;
            }
            byte a=0;
            /*foreach (int Keys in tanki.Values){
                texts[a].text=Keys.ToString();
                a++;
            }*/
            for(byte aa=a;a<texts.Length;a++){
                texts[a].text="";
            }
            a=(byte)(a-tanki.Count);
            foreach (KeyValuePair<string,int> Keys in tanki.OrderBy(key => key.Value)){
                texts[a].text=Keys.Key+" - "+Keys.Value.ToString();
                a++;
            }
            
        }
    }
    public void Adding(string a,tank tank){
        dictio.Add(a,tank);
        tanki.Add(tank.ism,0);
    }
}
