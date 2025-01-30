using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
public class GameManager : MonoBehaviour
{
    public GameObject[] tanks;
    public GameObject go;
    public tank tank;
    public string nick;
    int i;

    // Start is called before the first frame update
    public void Start(){
    i=PlayerPrefs.GetInt("numoftank");
	Debug.Log(i);
	Debug.Log(i);
        go=tanks[i];
    }
    public void Sort()
    {
        tank._Camera=gameObject;
        nick=DateTime.Now.Second.ToString()+i.ToString()+"player";
        tank.gameObject.name=nick;
        tank.isbot=false;
    }
    public void rechooset(){
        
        tank._Camera=gameObject;
        nick=nick+DateTime.Now.Second.ToString();
        tank.gameObject.name=nick;
        tank.isbot=true;
        go=tanks[i];
        if(i!=0){
            i=i-1+UnityEngine. Random.Range(0,3);
        }
    }
}
