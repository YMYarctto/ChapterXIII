using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInfoUI : UIView
{
    Transform title;
    Transform description;
    Transform _tag;
    Transform image;

    void Awake()
    {
        UIManager.instance.AddUIView("ItemInfoUI",this);
    }

    public void ChangeTitle(string str)
    {
        title.GetComponent<TMP_Text>().text = str;
    }

    public void ChangeDescription(string str)
    {
        description.GetComponent<TMP_Text>().text = str;
    }

    public void ChangeTag(string str)
    {
        _tag.GetComponent<TMP_Text>().text = str;
    }

    public override void Init()
    {
        EventManager.instance.AddListener<string>("UI/ItemInfo/ChangeTitle", ChangeTitle);
        EventManager.instance.AddListener<string>("UI/ItemInfo/ChangeDescription", ChangeDescription);
        EventManager.instance.AddListener<string>("UI/ItemInfo/ChangeTag", ChangeTag);
        EventManager.instance.AddListener("UI/ItemInfo/ShowUI",ShowUI);
        title = transform.Find("ItemInfo_title");
        description = transform.Find("ItemInfo_description");
        _tag = transform.Find("ItemInfo_tag");
        image = transform.Find("ItemInfo_image");
        image.gameObject.SetActive(false);
    }

    public override void OnUnload()
    {
        EventManager.instance?.RemoveListener<string>("UI/ItemInfo/ChangeTitle", ChangeTitle);
        EventManager.instance?.RemoveListener<string>("UI/ItemInfo/ChangeDescription", ChangeDescription);
        EventManager.instance?.RemoveListener<string>("UI/ItemInfo/ChangeTag", ChangeTag);
        EventManager.instance?.RemoveListener("UI/ItemInfo/ShowUI",ShowUI);
        UIManager.instance?.RemoveUIView("ItemInfoUI");
    }

    public override void Enable()
    {
        
    }

    public override void Disable()
    {
        
    }

    public void ShowUI(){
        image.gameObject.SetActive(true);
        EventManager.instance.Invoke("UI/ItemInfo/ChangeTitle");
        EventManager.instance.Invoke("UI/ItemInfo/ChangeDescription");
        EventManager.instance.Invoke("UI/ItemInfo/ChangeTag");
    }
}
