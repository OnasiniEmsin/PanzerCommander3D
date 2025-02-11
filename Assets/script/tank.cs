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
    public float velocityOfShell=10,ifneedBarabanReloadTime,ifneedKasetaReloadTime;
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
    GameObject shell1,camera,HPCanvas,hodovaya;
    Transform CameraPosition,shellStartPos,bashnya,tormoz;
    Rigidbody rb;
    bool ismoving,ismovingComplex,isonline;
    Image _imHp,reloadBar;short _hitpointcomplex;
    public float aaaa,ifneedturretlimit=361;
    HP hp;
    public int summaUrona=0;
    string qoralanma="";
    float vboytime;
    float kvf,kva;
    // Start is called before the first frame update
    void Start()
    {
        VelocityForward/=1.8f;
        Turnspeed/=2;
        kvf=VelocityForward;kva=Turnspeed;
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
        if(_dushman!=null){
        dir=-transform.position+_dushman.position;
        }else{
            dir=Vector3.zero;
            dir.y=0;
        }
        hp._ism.text=ism;
        if(ismovingComplex!=ismoving){
        if(ismoving){
            driver.Play();
            engine.Stop();
            hodovaya.SetActive(true);
        }else{
            driver.Stop();
            hodovaya.SetActive(false);
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
            Turnspeed=kva/2;
            //tracksf();
        }
        if(Input.GetKey(KeyCode.S)){
            transform.Translate(0,0,VelocityForward/2*-Time.deltaTime);
            ismovingf();
            Turnspeed=kva/2;
            //tracksb();
        }
        VelocityForward=kvf;
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(0,-Time.deltaTime*Turnspeed,0);ismovingf();
            VelocityForward=kvf/2;
            //tracksl();
        }
        if(Input.GetKey(KeyCode.D)){
            transform.Rotate(0,Time.deltaTime*Turnspeed,0);ismovingf();
            VelocityForward=kvf/2;
            //tracksr();
        }
        Turnspeed=kva;
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
                        casseteNowWorking=true;timerel=timeOfFire-ifneedKasetaReloadTime;
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
        rb.AddForce(bashnya.forward*-10*DamageOfShell);
        Destroy(shell.gameObject,5);
        if(DamageOfShell>=250){
        if(Random.Range(0,50)==5){
            shell.uron=DamageOfShell*5;
        }else{
        shell.uron=DamageOfShell;
        }
        }else{
            shell.uron=DamageOfShell;
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
        hodovaya=GameObject.Find(gameObject.name+"/hodovaya");
        hodovaya.SetActive(false);
        ismoving=false;
        /*rend=GameObject.Find(gameObject.name+"/sep").GetComponent<Renderer>();
        rend2=GameObject.Find(gameObject.name+"/sep1").GetComponent<Renderer>();*/
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
        if(_dushman!=null){
        dir=shellStartPos.position-_dushman.position;
        }
        Vector3 fromToXZ=new Vector3(dir.x, 0f, dir.z);
        float x = fromToXZ.magnitude;
        float y = dir.y;
        float v2=velocityOfShell*velocityOfShell;
        angleRadians =Mathf.Sqrt((v2*v2 - g*(g*x*x + 2*y*v2)));// Mathf.Atan((v2 + Mathf.Sqrt(v2*v2 - g*(g*x*x + 2*y*v2))) / g*x);
                     
        angleRadians=Mathf.Atan((v2-angleRadians)/x/g) * Mathf.Rad2Deg;
        angleRadians=angleRadians;
        
        shellStartPos.rotation=bashnya.rotation;
        shellStartPos.Rotate(angleRadians,0,0);
    }
    [PunRPC]
    public void UPN(){
            view.RPC("setIsm",RpcTarget.All,"");
            Debug.Log("nickqayta tiklandi"+ism);
    }


    public Transform _dushman;
    public Vector3 dir;
    float burchak;
    void bot(){
        if(_dushman!=null){
            burchak=Vector3.Angle(dir, transform.forward);
            transform.Translate(0,0,VelocityForward*Time.deltaTime);
            ismovingf();
            if(burchak>3f){
                transform.Rotate(0,-Time.deltaTime*Turnspeed,0);ismovingf();
            }else{
                if(burchak<=-3){
                    transform.Rotate(0,Time.deltaTime*Turnspeed,0);ismovingf();
                }else{
                    //transform.rotation=Quaternion.LookRotation(dir,transform.up);
                    
                }
            }
        }
    }
    float turnRate;
    bool isturning;
    float deltaAngleOfTurret=0,dt;
    void bashlig(){
            
        Vector3 dir2=new Vector3(dir.x,0,dir.z);
        burchak=Vector3.Angle(dir2, bashnya.forward);
        if(_dushman!=null){
        dir2=bashnya.InverseTransformPoint(_dushman.position);
        }
        dir2=dir2.normalized;
        burchak=dir2.x*burchak;
        dt=Time.deltaTime*kva;
        if(deltaAngleOfTurret>360){
            deltaAngleOfTurret-=360;
        }else{
            if(deltaAngleOfTurret<-360){
                deltaAngleOfTurret+=360;
            }
        }
        if(burchak>=3f){
            if(deltaAngleOfTurret>-ifneedturretlimit){
            bashnya.Rotate(0,dt,0);
            deltaAngleOfTurret-=dt;
            }
        }else{
            if(burchak<=-3){
                if(deltaAngleOfTurret<ifneedturretlimit){
                    bashnya.Rotate(0,-dt,0);
                    deltaAngleOfTurret+=dt;
                }
            }else{
                if(burchak>0.01f){
                    if(deltaAngleOfTurret>-ifneedturretlimit){
                        bashnya.Rotate(0,dt,0);
                        deltaAngleOfTurret-=dt;
                    }
                }else{
                    if(burchak<-0.01f){
                        if(deltaAngleOfTurret<ifneedturretlimit){
                            bashnya.Rotate(0,-dt,0);
                            deltaAngleOfTurret+=dt;
                        }
                    }else{
                        if(isbot){
                            if(_dushman!=null){
                            checktank();
                            }
                        }
                    }
                }
            }
        }
    }
    /*public Renderer rend,rend2;
    void tracksf(){
        float offset = 0;
        rend.material.SetTextureOffset("_BaseMap", new Vector2(0,offset));
        rend2.material.SetTextureOffset("_MainTex", new Vector2(0,offset));
    }
    void tracksb(){
        float offset = 0;
        rend.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
        rend2.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
    }

    void tracksl(){
        float offset = VelocityForward;
        rend.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
        rend2.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
    void tracksr(){
        float offset = VelocityForward*Time.deltaTime;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        rend2.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
    }*/
    
}
