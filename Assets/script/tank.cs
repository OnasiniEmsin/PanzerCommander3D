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
    Transform CameraPosition,shellStartPos,bashnya;
    Rigidbody rb;
    bool ismoving,ismovingComplex,isonline;
    Image _imHp,reloadBar;short _hitpointcomplex;
    public float aaaa;
    HP hp;
    public int summaUrona=0;
    string qoralanma="";
    float vboytime;
    // Start is called before the first frame update
    void Start()
    {
        g = Physics.gravity.y;
        CameraPosition=new GameObject().transform;
        CameraPosition.position=transform.position+transform.up*10;
        CameraPosition.parent=transform;
        hpstring=health.ToString();
        rb=GetComponent<Rigidbody>();rb.centerOfMass=Vector3.up*-.9f;
        driver=GetComponent<AudioSource>();
        summaUrona=PlayerPrefs.GetInt("isonline");
        if(summaUrona==1){
        view=GetComponent<PhotonView>();
        isonline=true;
        }else{
            isonline=false;
        }
        summaUrona=0;
        _dushman=transform;
        setHP();
        dust=Instantiate(dust,shellStartPos.position,transform.rotation);
        dust.transform.parent=bashnya;
        engine=GameObject.Find(gameObject.name+"/Cube").GetComponent<AudioSource>();
        engine.volume=0.1f;
        engine.loop=true;
        engine.spatialBlend=1;
        kaseta=ifneedKaseta;baraban=ifneedBaraban;
        baraban--;
        summaUrona=PlayerPrefs.GetInt("isonline");
        
        
        
        StartCoroutine(reloading());
    }

    // Update is called once per frame

    void Update()
    {
	if(transform.position.y<-25){
		DestroyTheTank();
	}	
        if(isbot==false){
            if(isonline){
            if(view.IsMine){
            
            if(health<=0){
                view.RPC("DestroyTheTank",RpcTarget.All);
            }
            
            view.RPC("receive",RpcTarget.All);bashlig();
            }else{
                if(health<=0){
                DestroyTheTank();
            }
            }
            }else{
                
                receive();bashlig();
            }
            _Camera.transform.position=CameraPosition.position;
            CheckInput();
            
        }else{
            if(health<=0){
                DestroyTheTank();
            }
            bot();
            receive();
            bashlig();
        }
        HPCanvas.transform.rotation=Quaternion.LookRotation(-camera.transform.position+HPCanvas.transform.position,camera.transform.up);
        dir=-transform.position+_dushman.position;
        hp._ism.text=ism;
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
            mojjalgaol();
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
            if(isonline){
            
            view.RPC("fire",RpcTarget.All);
            }else{
                fire();
            }

        }
    }
    float timerel=0;
    public IEnumerator reloading(){
        while(true){
            yield return new WaitForSeconds(.2f);
            if(isReadyToFire==false){
            timerel+=.2f;
            aaaa=timerel;
            if(isbot==false){
            reloadBar.fillAmount=aaaa/timeOfFire;
            }
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
        shell=Instantiate(Realshell,shellStartPos.position,shellStartPos.rotation).GetComponent<shell>();
        rb.AddForce(transform.forward*-10000);
        Destroy(shell.gameObject,5);
        if(DamageOfShell>=250){
        if(Random.Range(0,50)==5){
            shell.uron=DamageOfShell*5;
        }else{
        shell.uron=DamageOfShell;
        }
        }
        shell.GetComponent<Rigidbody>().velocity=velocityOfShell*shellStartPos.forward;
        shell.mytank=GetComponent<tank>();
    }
    void fireshell(){
        shell1=Instantiate(Shell,shellStartPos.position,shellStartPos.rotation);
        Destroy(shell1.gameObject,5);
        dust.SetActive(false);
        dust.SetActive(true);
        shell1.GetComponent<Rigidbody>().velocity=velocityOfShell*shellStartPos.forward;
    }
    public void setDamaged(int uron){
        if(isonline){
        view.RPC("Damaged",RpcTarget.All,uron);
        }else{
            Damaged(uron);
        }
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
    void receive(){
        hp.jon=health;
        hp.jonum=_hitpointcomplex;
        text.text=health.ToString();
    }
    [PunRPC]
    void PD(int urru){
        summaUrona+=urru;
    }
    public void PlusDamage(int urrru){
        if(isonline){
        view.RPC("PD",RpcTarget.All,urrru);
        }else{
            PD(urrru);
        }
    }
    [PunRPC]
    void setHP(){
        bashnya=GameObject.Find(gameObject.name+"/bash").transform;
        HPCanvas=GameObject.Find(gameObject.name+"/hp");
        text=GameObject.Find(gameObject.name+"/hp/Text (TMP)").GetComponent<TMP_Text>();
        camera=GameObject.Find("gorcam/MC");
        _imHp=GameObject.Find(gameObject.name+"/hp/hp").GetComponent<Image>();
        _hitpointcomplex=(short)health;
        shellStartPos=GameObject.Find(gameObject.name+"/bash/pushuch").transform;
        reloadBar=GameObject.Find("Canvas/reload/reload").GetComponent<Image>();
        hp=HPCanvas.GetComponent<HP>();
        GameObject.Find(gameObject.name+"/hp/Tankname").GetComponent<TMP_Text>().text=ism;
        ism="BOT"+gameObject.name;
        if(isonline){
        if(view.IsMine){
            qoralanma=PlayerPrefs.GetString("ism");
            view.Owner.NickName=qoralanma;
            ism=qoralanma;
            Debug.Log("ism Tanlandi"+ism);
        }else{
            ism=view.Owner.NickName;
        }
        }else{
            if(isbot==false){
                _Camera.transform.parent=transform;
            }
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
        receive();
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
    private float g = Physics.gravity.y;float angleRadians;
    void mojjalgaol(){
        Vector3 fromToXZ=new Vector3(dir.x, 0f, dir.z);
        float x = fromToXZ.magnitude;
        float y = dir.y;
        float v2=velocityOfShell*velocityOfShell;
        angleRadians =Mathf.Sqrt(((v2*v2 - (2 * g * y))/ (g*g * x * x)) - 1);// Mathf.Atan((v2 + Mathf.Sqrt(v2*v2 - g*(g*x*x + 2*y*v2))) / g*x);
                     
        angleRadians=Mathf.Atan2(v2/g/x-angleRadians, 1) * Mathf.Rad2Deg;
        angleRadians=-90-angleRadians;
        shellStartPos.rotation=bashnya.rotation;
        shellStartPos.Rotate(angleRadians,0,0);
    }
    [PunRPC]
    public void UPN(){
            view.RPC("setIsm",RpcTarget.All,"");
            Debug.Log("nickqayta tiklandi"+ism);
    }


    public Transform _dushman;
    Vector3 dir;
    float burchak;
    void bot(){
        if(_dushman!=null){
            //dir.y=transform.position.y;
            burchak=Vector3.Angle(dir, transform.forward);
            if(burchak>3f){
                transform.Rotate(0,-Time.deltaTime*Turnspeed,0);ismovingf();
            }else{
                if(burchak<=-3){
                    transform.Rotate(0,Time.deltaTime*Turnspeed,0);ismovingf();
                }else{
                    transform.rotation=Quaternion.LookRotation(dir,transform.up);
                    checktank();
                }
            }
        }
    }
    float turnRate;
    bool isturning;
    void bashlig(){
            
        Vector3 dir2=new Vector3(dir.x,0,dir.z);
        burchak=Vector3.Angle(dir2, bashnya.forward);
        if(_dushman!=null){
        dir2=bashnya.InverseTransformPoint(_dushman.position);
        }
        dir2=dir2.normalized;
        burchak=dir2.x*burchak;
        if(burchak>=3f){
            bashnya.Rotate(0,Time.deltaTime*Turnspeed,0);
            
        }else{
            if(burchak<=-3){
                bashnya.Rotate(0,-Time.deltaTime*Turnspeed,0);
                
            }else{
                if(burchak>0.01f){
                    
                    bashnya.Rotate(0,Time.deltaTime*Turnspeed,0);
                }else{
                    if(burchak<-0.01f){
                        
                        bashnya.Rotate(0,-Time.deltaTime*Turnspeed,0);
                    }else{
                        
                    }
                }
            }
        }
    }
    
}
