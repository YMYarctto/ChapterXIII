using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Events;

public class LoadingInit : UIView
{
    bool startLoading;
    float value;
    Image image;
    Color color;

    string loadScene;
    string unloadScene;
    UnityAction actionAfterSceneLoad;

    void Awake()
    {
        UIManager.instance.AddUIView("LoadingInit",this);
    }

    void FixedUpdate()
    {
        if(startLoading)
        {
            color.a+=value*Time.fixedDeltaTime;
            image.color=color;
            if(color.a<=0)
            {
                color.a=0;
                image.color=color;
                Debug.Log(color.a);
                startLoading=false;
                gameObject.SetActive(false);
                actionAfterSceneLoad?.Invoke();
            }
            if(color.a>=1)
            {
                color.a=1;
                image.color=color;
                Debug.Log(color.a);
                startLoading=false;
                StartCoroutine(ChangeScene());
            }
        }
    }

    public void ChangeScene(string load,string unload,UnityAction action)
    {
        Enable();
        loadScene=load;
        unloadScene=unload;
        actionAfterSceneLoad=action;
    }

    public void UnloadScene(string unload,UnityAction action)
    {
        ChangeScene("",unload,action);
    }

    IEnumerator ChangeScene()
    {
        var op=SceneManager.UnloadSceneAsync(unloadScene);
        while (!op.isDone)//如果没有完成
        {
            yield return null;
        }
        if(loadScene!="")
        {
            op=SceneManager.LoadSceneAsync(loadScene,LoadSceneMode.Additive);
            while (!op.isDone)//如果没有完成
            {
                yield return null;
            }
        }
        yield return new WaitForEndOfFrame();
        Disable();
    }

    public override void Init()
    {
        image=GetComponent<Image>();
        color=image.color;
        startLoading=false;
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("LoadingInit");
    }

    public override void Enable()
    {
        value=0.6f;
        startLoading=true;
        gameObject.SetActive(true);
    }

    public override void Disable()
    {
        value=-0.6f;
        startLoading=true;
    }
}
