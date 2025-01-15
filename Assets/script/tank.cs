using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;

public class tank : MonoBehaviour
{
    public bool isbot,isBaraban;
    public float VelocityForward=4,Turnspeed=5,timeOfFire=2;
    public float velocityOfShell=10,ifneedBarabanReloadTime;
    public int DamageOfShell=110,health=350;
    public byte ifneedKaseta,ifneedBaraban;
    public GameObject _Camera,Shell,Realshell,dust;
    public TMP_Text text;
    public string ism,hpstring;
    PhotonView view;
    bool isReadyToFire;
    public shell shell;
    public AudioSource engine;
    AudioSource driver;
    GameObject shell1,camera,HPCanvas;
    Transform CameraPosition,shellStartPos;
    Rigidbody rb;
    bool ismoving,ismovingComplex;
    Image _imHp,reloadBar;short _hitpointcomplex;
    public float aaaa;
    HP hp;
    public int summaUrona=0;
    string qoralanma="";
    float vboytime;
    // Start is called before the first frame update
    void Start()
    {
        CameraPosition=new GameObject().transform;
        CameraPosition.position=transform.position+transform.forward*20;
        CameraPosition.parent=transform;
        view=GetComponent<PhotonView>();
        hpstring=health.ToString();
        
        rb=GetComponent<Rigidbody>();rb.centerOfMass=Vector3.up*-.9f;
        driver=GetComponent<AudioSource>();
        setHP();
        dust=Instantiate(dust,shellStartPos.position,transform.rotation);
        dust.transform.parent=transform;
        engine=GameObject.Find(gameObject.name+"/Cube").GetComponent<AudioSource>();
        engine.volume=0.1f;
        engine.loop=true;
        engine.spatialBlend=1;
        kaseta=ifneedKaseta;baraban=ifneedBaraban;
        baraban--;
        StartCoroutine(reloading());
    }

    // Update is called once per frame

    void Update()
    {
	if(transform.position.y<-25){
		DestroyTheTank();
	}	
        if(isbot&&view.IsMine){
            
            if(health<=0){
                view.RPC("DestroyTheTank",RpcTarget.All);
            }
            _Camera.transform.position=CameraPosition.position;
            CheckInput();
        }
        hp._ism.text=ism;
        view.RPC("receive",RpcTarget.All,hpstring);
        if(ismovingComplex!=ismoving){
        if(ismoving){
            driver.Play();
            engine.Stop();
        }else{
            driver.Stop();
            engine.Play();
        }
        ismovingComplex=ismoving;
        }
    }
    
    void CheckInput(){
        ismoving=false;
        if(Input.GetKey(KeyCode.W)){
            transform.Translate(0,0,VelocityForward*Time.deltaTime);
            ismovingf();
        }
        if(Input.GetKey(KeyCode.S)){
            transform.Translate(0,0,VelocityForward*-Time.deltaTime);
            ismovingf();
        }
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(0,-Time.deltaTime*Turnspeed,0);ismovingf();
        }
        if(Input.GetKey(KeyCode.D)){
            transform.Rotate(0,Time.deltaTime*Turnspeed,0);ismovingf();
        }
        if(Input.GetKey(KeyCode.Space)){
            checktank();
        }
    }
    void ismovingf(){
        ismoving=true;
    }
    void movebot(){

    }
    public byte baraban=0,kaseta=0;
    bool casseteNowWorking=false;
    void checktank(){
        if(isReadyToFire){
            if(isBaraban==false){
                timerel=0;
            }else{
                kaseta--;
                if(kaseta==0||kaseta>100){
                        kaseta=ifneedKaseta;
                        casseteNowWorking=false;
                    if(baraban==0){
                        baraban=ifneedBaraban;
                        kaseta=ifneedKaseta;
                        timerel=0;
                    }else{
                        timerel=timeOfFire-ifneedBarabanReloadTime;
                    }
                    baraban-=1;
                }else{
                        casseteNowWorking=true;timerel=timeOfFire-ifneedBarabanReloadTime;
                }
                    
            }
            isReadyToFire=false;
            fire1();
            view.RPC("fire",RpcTarget.All);

        }
    }
    float timerel=0;
    public IEnumerator reloading(){
        while(true){
            yield return new WaitForSeconds(.2f);
            if(isReadyToFire==false){
            timerel+=.2f;
            aaaa=timerel;
            reloadBar.fillAmount=aaaa/timeOfFire;
            if(timeOfFire<=timerel){
                isReadyToFire=true;timerel=0;
                if(casseteNowWorking){
                    checktank();
                }
                
            }
            }
        }
    }
    void fire1(){
        shell=Instantiate(Realshell,shellStartPos.position+transform.forward*5+Vector3.up*-1,transform.rotation).GetComponent<shell>();
        Destroy(shell.gameObject,5);
        shell.uron=DamageOfShell;
        shell.GetComponent<Rigidbody>().velocity=velocityOfShell*transform.forward;
        shell.mytank=GetComponent<tank>();
    }
    void fireshell(){
        shell1=Instantiate(Shell,shellStartPos.position+transform.forward*2,transform.rotation);
        Destroy(shell1.gameObject,5);
        dust.SetActive(false);
        dust.SetActive(true);
        shell1.GetComponent<Rigidbody>().velocity=velocityOfShell*transform.forward;
    }
    public void setDamaged(int uron){
        view.RPC("Damaged",RpcTarget.All,uron);
    }

    public void deleteShell(){
        view.RPC("deleteshell",RpcTarget.All);
    }
    
    void checkReloading(){

    }
    [PunRPC]
    void deleteshell(){
        Destroy(shell1);
    }
    [PunRPC]
    void receive(string ism){
        hp.jon=health;
        hp.jonum=_hitpointcomplex;
        text.text=health.ToString();
        HPCanvas.transform.rotation=Quaternion.LookRotation(-camera.transform.position+HPCanvas.transform.position,camera.transform.up);
    }
    [PunRPC]
    void PD(int urru){
        summaUrona+=urru;
    }
    public void PlusDamage(int urrru){
        view.RPC("PD",RpcTarget.All,urrru);
    }
    [PunRPC]
    void setHP(){
        HPCanvas=GameObject.Find(gameObject.name+"/hp");
        text=GameObject.Find(gameObject.name+"/hp/Text (TMP)").GetComponent<TMP_Text>();
        camera=GameObject.Find("gorcam/MC");
        _imHp=GameObject.Find(gameObject.name+"/hp/hp").GetComponent<Image>();
        _hitpointcomplex=(short)health;
        shellStartPos=GameObject.Find(gameObject.name+"/pushuch").transform;
        reloadBar=GameObject.Find("Canvas/reload/reload").GetComponent<Image>();
        hp=HPCanvas.GetComponent<HP>();
        GameObject.Find(gameObject.name+"/hp/Tankname").GetComponent<TMP_Text>().text=ism;
        if(view.IsMine){
            qoralanma=PlayerPrefs.GetString("ism");
            view.Owner.NickName=qoralanma;
            ism=qoralanma;
            Debug.Log("ism Tanlandi"+ism);
        }else{
            ism=view.Owner.NickName;
        }
        
        StartCoroutine("Zaylop");
        
        //view.RPC("UPN",RpcTarget.All);
    }
    [PunRPC]
    void fire(){
        fireshell();
    }
    [PunRPC]
    void setIsm(string ism1){
        
        ism=view.Owner.NickName;    
    }
    [PunRPC]

    void Damaged(int daamage){
        health-=daamage;
        receive(ism);
        aaaa=health;
        aaaa=aaaa/_hitpointcomplex;
        _imHp.fillAmount=aaaa;
    }
    [PunRPC]
    void DestroyTheTank(){
        Destroy(gameObject);
    }

    IEnumerator Zaylop(){
        while(true){
            yield return new WaitForSeconds(5);
            if(ism==qoralanma){

            }
            ochir();
        }
    }
    void ochir(){
        hp._ism.text=ism;
        camera.GetComponent<Sheetmanager>().Adding(gameObject.name,GetComponent<tank>()); 
        Debug.Log("sheet"+ism);
        StopCoroutine("Zaylop");
    }
    [PunRPC]
    public void UPN(){
            view.RPC("setIsm",RpcTarget.All,"");
            Debug.Log("nickqayta tiklandi"+ism);
    }
    
}
