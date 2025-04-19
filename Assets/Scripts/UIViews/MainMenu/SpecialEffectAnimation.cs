using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SpecialEffectAnimation : MonoBehaviour
{
    void Awake()
    {
        transform.position=Vector3.zero;
    }

    void FixedUpdate()
    {
        transform.position-=new Vector3(Screen.width*Time.fixedDeltaTime/10,Screen.height*Time.fixedDeltaTime/5,0);
        if(transform.position.x<=-Screen.width)
            transform.position=Vector3.zero;
    }
}
