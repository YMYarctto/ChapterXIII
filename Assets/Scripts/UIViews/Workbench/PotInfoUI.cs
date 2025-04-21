using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotInfoUI : UIView
{
    Transform title;
    Transform efficacy_tag;
    Transform side_tag;
    Transform image;

    void Awake()
    {
        UIManager.instance.AddUIView("PotInfoUI",this);
    }

    public void ChangeTitle(string str)
    {
        title.GetComponent<TMP_Text>().text = str;
    }

    public void ChangeEfficacyTag(string str)
    {
        efficacy_tag.GetComponent<TMP_Text>().text = str;
    }

    public void ChangeSideEffectTag(string str)
    {
        side_tag.GetComponent<TMP_Text>().text = str;
    }

    public override void Init()
    {
        EventManager.instance.AddListener<string>("UI/PotInfo/ChangeTitle", ChangeTitle);
        EventManager.instance.AddListener<string>("UI/PotInfo/ChangeEfficacyTag", ChangeEfficacyTag);
        EventManager.instance.AddListener<string>("UI/PotInfo/ChangeSideEffectTag", ChangeSideEffectTag);
        EventManager.instance.AddListener("UI/PotInfo/ShowUI",ShowUI);
        EventManager.instance.AddListener("UI/PotInfo/ClearUI",ClearUI);
        title = transform.Find("PotInfo_title");
        efficacy_tag = transform.Find("PotInfo_tag_efficacy");
        side_tag = transform.Find("PotInfo_tag_sideEffect");
        image = transform.Find("PotInfo_image");
        image.gameObject.SetActive(false);
    }

    public override void OnUnload()
    {
        EventManager.instance?.RemoveListener<string>("UI/PotInfo/ChangeTitle", ChangeTitle);
        EventManager.instance?.RemoveListener<string>("UI/PotInfo/ChangeEfficacyTag", ChangeEfficacyTag);
        EventManager.instance?.RemoveListener<string>("UI/PotInfo/ChangeSideEffectTag", ChangeSideEffectTag);
        EventManager.instance?.RemoveListener("UI/PotInfo/ShowUI",ShowUI);
        EventManager.instance?.RemoveListener("UI/PotInfo/ClearUI",ClearUI);
    }

    public void ShowUI(){
        image.gameObject.SetActive(true);
        EventManager.instance.Invoke("UI/PotInfo/ChangeTitle");
        EventManager.instance.Invoke("UI/PotInfo/ChangeEfficacyTag");
        EventManager.instance.Invoke("UI/PotInfo/ChangeSideEffectTag");
    }

    public void ClearUI(){
        image.gameObject.SetActive(false);
        ChangeTitle("");
        ChangeSideEffectTag("");
        ChangeEfficacyTag("");
    }
}
