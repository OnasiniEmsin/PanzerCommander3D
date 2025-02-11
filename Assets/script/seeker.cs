using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seeker : MonoBehaviour
{
    tank tank;
    // Start is called before the first frame update
    void Start()
    {
        tank=transform.parent.gameObject.GetComponent<tank>();
    }
    void OnTriggerEnter(Collider other){
        tank._dushman=other.transform;
    }
}
