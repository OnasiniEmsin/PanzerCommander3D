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
        tank.isbot=true;
        string nick=DateTime.Now.Second.ToString()+i.ToString();
        tank.gameObject.name=nick;
    }
}
