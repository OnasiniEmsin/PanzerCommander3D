using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
public class GameManager : MonoBehaviour
{
    public GameObject[] tanks;
    public GameObject[] l4,l5,l6,l7,l8,l9,l10;
    public GameObject[][] tankilar;
    public GameObject go;
    public tank tank;
    public string nick;
    int i;

    // Start is called before the first frame update
    public void Start(){
        tankilar=new GameObject[7][];
        tankilar[0]=l4;tankilar[1]=l5;tankilar[2]=l6;tankilar[3]=l7;tankilar[4]=l8;tankilar[5]=l9;tankilar[6]=l10;
    i=PlayerPrefs.GetInt("numofLevel");
    tanks=tankilar[i];
    i=PlayerPrefs.GetInt("numoftank");
	
        go=tanks[i];
    }
    public void Start1(){
    i=PlayerPrefs.GetInt("numofLevel");
	Debug.Log(i);
	Debug.Log(i);
    tanks=tankilar[i];
    i=PlayerPrefs.GetInt("numoftank");
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
            i=UnityEngine. Random.Range(0,tanks.Length);
        }
    }
}
