using UnityEngine;

using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class PhotonManager : MonoBehaviourPunCallbacks
{
    public string region="Asia",roomname;
    //[SerializeField] GameObject hetzer;
    public GameManager gm;
    public GameObject buttons,fon;
    public bool iscontinue;
    public Transform[] spawnpoints;
    public Vector3 position;
    bool iscreatedroom,isStarted;
    float sanoq=5f;
    int s=0,isonlineee;
    void Start()
    {
	if(iscontinue){
        isonlineee=PlayerPrefs.GetInt("isonline");
        if(isonlineee==0){
            StartCoroutine("sanoqc");
            offline();

        }else{
	    joinRoomBotton();
        StartCoroutine("sanoqc");
        }
	}else{
	    PhotonNetwork.ConnectUsingSettings();
	    PhotonNetwork.ConnectToRegion(region);
	}
    }

    public override void OnConnectedToMaster(){
        Debug.Log("OOOOO"+PhotonNetwork.CloudRegion);
        buttons.SetActive(true);
        if(PhotonNetwork.InLobby){
        PhotonNetwork.JoinLobby();
        
        }
    }
    public override void OnDisconnected(DisconnectCause cause){
        Debug.Log("ssss");
    }
    
    public void CreateRoomBotton(){
        RoomOptions rop=new RoomOptions();
        rop.MaxPlayers=8;
        PhotonNetwork.CreateRoom(roomname,rop,TypedLobby.Default);
        buttons.SetActive(false);
    }
    public void joinRoomBotton(){

        //PhotonNetwork.JoinRoom(roomname);
        PhotonNetwork.JoinRandomOrCreateRoom();
        
    }
    public override void OnCreatedRoom(){
        print("xona ochildi");
    }
    public override void OnJoinedRoom(){
        print("qo'shildiz");Sort();
        fon.SetActive(false);
        iscreatedroom=true;
        StopCoroutine("sanoqc");
    }
    void Sort(){
        s=PhotonNetwork.CurrentRoom.PlayerCount-1;
        position=spawnpoints[s].position;
        s++;
        gm.tank=PhotonNetwork.Instantiate(gm.go.name,position,Quaternion.identity).GetComponent<tank>();
        gm.Sort();
    }
    public void nextS(){
        PlayerPrefs.SetInt("isonline",1);
        PlayerPrefs.Save();
	SceneManager.LoadScene(1);
    }
    public void nextS1(){
        PlayerPrefs.SetInt("isonline",0);
        PlayerPrefs.Save();
	SceneManager.LoadScene(1);
    }
    void checkv(){
        if(iscreatedroom==false){
            if(isonlineee==0){
                offline();
                return;
            }
            if(isStarted==false){
                CreateRoomBotton();
                isStarted=true;
            }else{
                joinRoomBotton();roomname+="a";
                isStarted=false;
            }
        }
    }
    IEnumerator sanoqc(){
        
            yield return new WaitForSeconds(sanoq);
            checkv();
        
    }
    
    





    void offline(){
        s=0;
        print("offline");
        fon.SetActive(false);
        position=spawnpoints[s].position;
        s++;
        gm.tank=Instantiate(gm.go,position,Quaternion.identity).GetComponent<tank>();
        gm.Sort();
        iscreatedroom=true;
        StopCoroutine("sanoqc");
        gm.tank.isbot=false;
        while(s<8){
            position=spawnpoints[s].position;
            gm.nick=s.ToString();
            s++;
            gm.tank=Instantiate(gm.go,position,Quaternion.identity).GetComponent<tank>();
            gm.rechooset();
        }
    }
    void print(string str){
        Debug.Log(str);
    }
}
