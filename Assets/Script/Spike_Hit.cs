using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Hit : MonoBehaviour
{
    GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if(col.gameObject.tag == "Player" && PlayerMove_Contoller.hitstate == true)
        {    
            obj =  col.gameObject;


            PlayerMove_Contoller.hitstate = false;
            PlayerMove_Contoller.hitstate_timer = 0;
            col.gameObject.GetComponent<PlayerMove_Contoller>().ReduceLife();
            obj.GetComponent<PlayerMove_Contoller>().Player_hit(Vector2.up);
        }
    }
}
