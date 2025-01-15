using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finish : MonoBehaviour
{
    public block block;
    public byte number=0;
    public bool boom;
    public List<block> blocks=new List<block>();
    //Raycast Ray;
    void Update(){
        change();
    }
    void change(){
        Debug.DrawRay(transform.position,Vector3.right,Color.red);
        RaycastHit[] hits=Physics.RaycastAll(transform.position,Vector3.right);
        if(hits.Length>0){
            blocks.Clear();
            for(byte i=0;i<hits.Length;i++){
                if(hits[i].collider.CompareTag("Block")){
                    blocks.Add(hits[i].collider.GetComponent<block>());
                }
            }
            if(blocks.Count>7){
                if(!boom){
                    boom=true;
                    deleteBkock();
                }
            }
        }
    }
    public void reSort(){
        if(blocks.Count>0){
            foreach(block bl in blocks){
                bl.transform.Translate(0,-1,0);
            }

        }
    }
    public void deleteBkock(){
        foreach(block bl in blocks){
            Destroy(bl.gameObject);
            bl.islive=false;
        }
        blocks.Clear();
    }

}
