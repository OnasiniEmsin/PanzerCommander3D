using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pole : MonoBehaviour
{
    public GameObject blockprefab,Ish,Lsh,Ssh,Zsh,Osh,Tsh,CANVAS;
    public int[] shakllar,joylar,oj,ij,lj,sj,zj;//  S,Z,L,O,I
    int[] blockjoy;
    public int ochko;
    public GameObject myblock,GameOverImage;
    public bool isstay,noright,noleft;
    int myshakl,nextshakl;
    int numshakl;
    byte road=0;
    public finish[] lines;
    // Start is called before the first frame update
    void Start()
    {
        numshakl=Random.Range(0,4);
        nextshakl=shakllar[numshakl];
        setshakl();
        
    }

    // Update is called once per frame
    void Update()
    {
        isMoreBlocks();
        if(myblock==null){
            myblock=Instantiate(blockprefab,transform.position,transform.rotation);
            //myblock.transform.parent=CANVAS.transform;
            //myblock.transform.localScale=Vector3.one;
            
            myblock.transform.position=transform.position;StartCoroutine(tushish());
        }
        if(Input.GetKeyDown(KeyCode.A)){
            if(!noleft){
            myblock.transform.Translate(-1,0,0);
            noright=false;
            }else{
                noleft=false;
            }
        }
        if(Input.GetKeyDown(KeyCode.D)){
            if(!noright){
            myblock.transform.Translate(1,0,0);
            noleft=false;
            }else{
                noright=false;
            }
        }
        if(Input.GetKeyDown(KeyCode.S)){
            if(isstay==false){
            myblock.transform.Translate(0,-1,0);
            road++;
            }
        }
        //noright=false;
        //noleft=false;
    }
    void setshakl(){
        
        myshakl=nextshakl;
        numshakl=Random.Range(0,6);
        nextshakl=shakllar[numshakl];
        switch(myshakl){
            case 0:blockprefab=Osh;blockjoy=oj; break;
            case 1:blockprefab=Ish;blockjoy=ij;break;
            case 7:blockprefab=Lsh;blockjoy=lj;break;
            case 5:blockprefab=Ssh;blockjoy=sj;break;
            case 2:blockprefab=Zsh;blockjoy=zj;break;
            case 4:blockprefab=Tsh;blockjoy=zj;break;
        }
        noleft=noright=false;

    }
    IEnumerator tushish(){
        while(true){
            if(isstay){
                if(road<=1){
                    GameOverImage.SetActive(true);
                    StopCoroutine(tushish());
                    break;
                }
                Debug.Log("bitti");
                myblock=null;ochko++;
                road=0;
                setshakl();
                isstay=false;
                break;
            }else{
                myblock.transform.Translate(0,-1f,0);
                road++;
            }
            yield return new WaitForSeconds(1f);
        }
    }
    void isMoreBlocks(){
        for(byte i=0;i<lines.Length;i++){
            if(lines[i].boom){
                lines[i].boom=false;
                for(i=i;i<lines.Length;i++){
                    lines[i].reSort();
                }
            }
        }
    }
}
