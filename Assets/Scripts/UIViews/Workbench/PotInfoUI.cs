using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotInfoUI : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.instance.AddListener<string>("UI/PotInfo/ChangeTitle", ChangeTitle);
        EventManager.instance.AddListener<string>("UI/PotInfo/ChangeEfficacyTag", ChangeEfficacyTag);
        EventManager.instance.AddListener<string>("UI/PotInfo/ChangeSideEffectTag", ChangeSideEffectTag);
    }

    void OnDisable()
    {
        EventManager.instance.RemoveListener<string>("UI/PotInfo/ChangeTitle", ChangeTitle);
        EventManager.instance.RemoveListener<string>("UI/PotInfo/ChangeEfficacyTag", ChangeEfficacyTag);
        EventManager.instance.RemoveListener<string>("UI/PotInfo/ChangeSideEffectTag", ChangeSideEffectTag);
    }

    public void ChangeTitle(string str)
    {
        gameObject.transform.Find("PotInfo_title").GetComponent<TMP_Text>().text = str;
    }

    public void ChangeEfficacyTag(string str)
    {
        gameObject.transform.Find("PotInfo_tag_efficacy").GetComponent<TMP_Text>().text = str;
    }

    public void ChangeSideEffectTag(string str)
    {
        gameObject.transform.Find("PotInfo_tag_sideEffect").GetComponent<TMP_Text>().text = str;
    }
}
