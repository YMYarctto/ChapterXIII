using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontDesk_SP : MonoBehaviour
{
    GameObject[] gameObjects;

    void Awake()
    {
        gameObjects=new GameObject[transform.childCount];
        gameObjects[0]=transform.Find("SP_2").gameObject;
        gameObjects[0].SetActive(false);
        gameObjects[1]=transform.Find("SP_3").gameObject;
        gameObjects[1].SetActive(false);
    }

    void OnEnable()
    {
        EventManager.instance.AddListener("Game/SAN/OnChange",ChangeBG);
    }

    void OnDisable()
    {
        EventManager.instance?.RemoveListener("Game/SAN/OnChange");
    }

    public void ChangeBG()
    {
        int i=GameController.SAN-2;
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
        color.a=0;
        image.color=color;
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
        color.a=1;
        image.color=color;
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
