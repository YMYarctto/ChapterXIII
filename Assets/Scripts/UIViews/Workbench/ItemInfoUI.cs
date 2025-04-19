using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInfoUI : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.instance.AddListener<string>("UI/ItemInfo/ChangeTitle", ChangeTitle);
        EventManager.instance.AddListener<string>("UI/ItemInfo/ChangeDescription", ChangeDescription);
        EventManager.instance.AddListener<string>("UI/ItemInfo/ChangeTag", ChangeTag);
    }

    void OnDisable()
    {
        EventManager.instance?.RemoveListener<string>("UI/ItemInfo/ChangeTitle", ChangeTitle);
        EventManager.instance?.RemoveListener<string>("UI/ItemInfo/ChangeDescription", ChangeDescription);
        EventManager.instance?.RemoveListener<string>("UI/ItemInfo/ChangeTag", ChangeTag);
    }

    public void ChangeTitle(string str)
    {
        gameObject.transform.Find("ItemInfo_title").GetComponent<TMP_Text>().text = str;
    }

    public void ChangeDescription(string str)
    {
        gameObject.transform.Find("ItemInfo_description").GetComponent<TMP_Text>().text = str;
    }

    public void ChangeTag(string str)
    {
        gameObject.transform.Find("ItemInfo_tag").GetComponent<TMP_Text>().text = str;
    }
}
