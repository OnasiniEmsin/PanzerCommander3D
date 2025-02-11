using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawner : MonoBehaviour
{
	public GameManager gm;
	GameObject go;
	int i=0,level=0;
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
		if(i>=gm.tankilar[level].Length){
			if(level==6){
			i=gm.tankilar[level].Length-1;
			}else{
				i=0;level++;
			}
		}
		if(i<0){
			if(level==0){
			i=0;
			}else{
				i=0;level--;
			}
		}
		PlayerPrefs.SetInt("numoftank", i);
		PlayerPrefs.SetInt("numofLevel", level);
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
