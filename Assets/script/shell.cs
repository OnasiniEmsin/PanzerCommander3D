using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell : MonoBehaviour
{
    public int uron=110;
    public GameObject pentsmoke;
    public tank mytank;
    public bool isExplosive;
    public oskolki oskolki;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
    void OnCollisionEnter(Collision other){
        Destroy(gameObject);
        if(other.gameObject.tag=="Armor"){
            tank tank =other.gameObject.GetComponent<tank>();
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
        Destroy(Instantiate(pentsmoke,transform.position,Quaternion.identity),3);
        if(isExplosive){
            oskolki=Instantiate(oskolki,transform.position,transform.rotation);
            oskolki.uron=uron/2;
            oskolki.mytank=mytank;
            oskolki.transform.localScale=new Vector3(uron/30,1,1);
        }
    }
}
