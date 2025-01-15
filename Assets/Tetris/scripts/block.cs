using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    GameObject othersGO;
    public pole pole;
    public int ochko;
    bool imAStay;
    public bool islive=true;
    
    // Start is called before the first frame update
    void Start()
    {
        pole=GameObject. Find("pole").GetComponent<pole>();
        ochko=pole.ochko;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other){
        if(!islive){
            return;
        }
        if (imAStay){
            return;
        }
        othersGO=other.gameObject;
        if(ochko!=pole.ochko){
            transform.parent=pole.transform;
            imAStay=true;
            gameObject.tag="Block";
        }else{
        if(othersGO.tag=="Past"||(othersGO.tag=="Block"&&Mathf.Approximately(othersGO.transform.position.x,transform.position.x)&&othersGO.transform.position.y<transform.position.y)){
            pole.isstay=true;
        }
        if(othersGO.tag=="Lefted"){
            pole.noleft=true;
        }
        if(othersGO.tag=="Righted"){
            pole.noright=true;
        }
        if((othersGO.tag=="Block"&&Mathf.Approximately(othersGO.transform.position.y,transform.position.y))){
            if(othersGO.transform.position.x>transform.position.x){
                pole.noright=true;
            }else{
                pole.noleft=true;
            }
        }
        }
    }
    void OnTriggerExit(Collider other){
        if (imAStay){return;}
        othersGO=other.gameObject;
        Debug.Log(othersGO.tag);
        if(othersGO.tag=="Past"||othersGO.tag=="Block"){
            pole.isstay=false;
        }
        
    }
}
