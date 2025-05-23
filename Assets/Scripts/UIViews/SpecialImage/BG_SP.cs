using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BG_SP : MonoBehaviour
{
    GameObject[] gameObjects;

    void Awake()
    {
        gameObjects=new GameObject[transform.childCount];
        for(int i=0;i<transform.childCount;i++)
        {
            gameObjects[i]=transform.Find("bg_SP_"+(i+1)).gameObject;
            Image image=gameObjects[i].GetComponent<Image>();
            image.color=new Color(image.color.r,image.color.g,image.color.b,0);
            gameObjects[i].SetActive(false);
        }
    }

    void OnEnable()
    {
        EventManager.instance.AddListener("Game/SAN/OnChange",ChangeBG);
    }

    public void ChangeBG()
    {
        int i=GameController.SAN-1;
        for(int j=0;j<gameObjects.Length;j++)
        {
            if(j<i)
            {
                SetBGActive(gameObjects[j],false);
                continue;
            }
            SetBGActive(gameObjects[j],true);
        }
    }

    public void SetBGActive(GameObject obj,bool isActive)
    {
        if(obj.activeSelf==isActive)
        {
            return;
        }
        if(isActive)
        {
            StartCoroutine(SetBGActiveTrue(obj));
        }
        else
        {
            StartCoroutine(SetBGActiveFalse(obj));
        }
    }

    IEnumerator SetBGActiveTrue(GameObject obj)
    {
        obj.SetActive(true);
        Image image=obj.GetComponent<Image>();
        Color color=image.color;
        // color.a=0;
        // image.color=color;
        while(color.a<1)
        {
            color.a+=3f*Time.fixedDeltaTime;
            image.color=color;
            yield return new WaitForFixedUpdate();
        }
        color.a=1;
        image.color=color;
    }

    IEnumerator SetBGActiveFalse(GameObject obj)
    {
        Image image=obj.GetComponent<Image>();
        Color color=image.color;
        // color.a=1;
        // image.color=color;
        while(color.a>0)
        {
            color.a-=3f*Time.fixedDeltaTime;
            image.color=color;
            yield return new WaitForFixedUpdate();
        }
        color.a=0;
        image.color=color;
        obj.SetActive(false);
    }
}
