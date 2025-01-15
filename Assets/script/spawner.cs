using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawner : MonoBehaviour
{
	public GameManager gm;
	GameObject go;
	int i=0;
	public TMP_Text ismtext;
    // Start is called before the first frame update
    void Start()
    {
        gm=GameObject.Find("gorcam").GetComponent<GameManager>();
	go=Instantiate(gm.go,transform.position,transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void nextt(){
		i++;
		pp();
	}
	public void prevt(){
		i--;
	        pp();
	}
	void pp(){
		if(i>=gm.tanks.Length){
			i=gm.tanks.Length-1;
		}
		if(i<0){
			i=0;
		}
		PlayerPrefs.SetInt("numoftank", i);
		PlayerPrefs.Save();
		Destroy(go);
		gm.Start();
		go=Instantiate(gm.go,transform.position,transform.rotation);
		Destroy(GameObject.Find(go.name+"/hp"));
	}
	public void SetIsm(){
		PlayerPrefs.SetString("ism", ismtext.text);
	}
}
