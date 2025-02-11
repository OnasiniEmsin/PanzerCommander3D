using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oskolki : MonoBehaviour
{
    bool isisai=false;
    public int uron=110;
    public GameObject pentsmoke;
    public tank mytank;
    void Update(){
        if(isisai){
            Destroy(gameObject);
        }else{
            isisai=true;
        }
    }
    void OnTriggerStay(Collider other){
        if(other.gameObject.tag=="Armor"){
            tank tank =other.transform.parent.gameObject.GetComponent<tank>();
            if(tank.health<=uron*5/4){
                uron*=5;
                uron/=4;
            }else{
                uron=Random.Range(uron*3/4,uron*5/4);
            }
            

            tank.setDamaged(uron);
            //mytank.deleteShell();
            mytank.PlusDamage(uron);
        }
    }
}
