using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using EColor;

public class PotInfoUI : UIView
{
    Transform title;
    Transform efficacy_tag;
    Transform side_tag;
    Transform image;

    GameObject tag_prefab;
    List<GameObject> tag_list;
    Vector2[] tag_efficacy_position;
    Vector2[] tag_side_position;
    int tag_efficacy_index = 0;
    int tag_side_index = 0;

    void Awake()
    {
        UIManager.instance.AddUIView("PotInfoUI", this);
    }

    public void ChangeTitle(string str)
    {
        title.GetComponent<TMP_Text>().text = str;
    }

    /// <summary>
    /// 添加标签
    /// </summary>
    /// <param name="str">标签内容</param>
    /// <param name="color">标签颜色</param>
    /// <param name="is_offseted">是否被划掉</param>
    public void AddTag(string str, Color color, bool is_offseted)
    {
        if(color==TagColor.Efficacy){
            if (tag_efficacy_index >= 4) return;
            AddEfficacyTag(str,is_offseted);
        }else{
            if(tag_side_index>=4)return;
            AddSideTag(str,is_offseted);
        }
        
    }

    /// <summary>
    /// 添加标签
    /// </summary>
    /// <param name="str">标签内容</param>
    /// <param name="color"><标签颜色/param>
    public void AddTag(string str, Color color)
    {
        AddTag(str, color, false);
    }

    void AddEfficacyTag(string str,bool is_offseted){
        GameObject gameobj_tag = Instantiate(tag_prefab, efficacy_tag);
        gameobj_tag.transform.localPosition = tag_efficacy_position[tag_efficacy_index];
        gameobj_tag.GetComponent<Image>().color = TagColor.Efficacy;
        gameobj_tag.transform.Find("tag_text").GetComponent<TMP_Text>().text = str;
        gameobj_tag.transform.Find("tag_image").gameObject.SetActive(is_offseted);
        tag_list.Add(gameobj_tag);
        tag_efficacy_index++;
    }

    void AddSideTag(string str,bool is_offseted){
        GameObject gameobj_tag = Instantiate(tag_prefab, side_tag);
        gameobj_tag.transform.localPosition = tag_side_position[tag_side_index];
        gameobj_tag.GetComponent<Image>().color = TagColor.SideEffect;
        gameobj_tag.transform.Find("tag_text").GetComponent<TMP_Text>().text = str;
        gameobj_tag.transform.Find("tag_image").gameObject.SetActive(is_offseted);
        tag_list.Add(gameobj_tag);
        tag_side_index++;
    }


    public override void Init()
    {
        tag_prefab = ResourceManager.instance.GetGameObject(EResource.GameObjectName.tag);
        tag_list = new();

        title = transform.Find("PotInfo_title");
        efficacy_tag = transform.Find("PotInfo_tag_efficacy");
        side_tag = transform.Find("PotInfo_tag_sideEffect");
        image = transform.Find("PotInfo_image");
        image.gameObject.SetActive(false);

        tag_efficacy_position = new Vector2[4];
        tag_side_position = new Vector2[4];
        for (int i = 0; i < 4; i ++)
        {
            tag_efficacy_position[i] = new(-135+90*i, 0);
            tag_side_position[i] = new(-135+90*i, 25);
        }
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("PotInfoUI");
    }

    public void ShowImage()
    {
        image.gameObject.SetActive(true);
    }

    public void ClearUI()
    {
        RemoveAllTag();
        ChangeTitle("");
        image.gameObject.SetActive(false);
    }

    public void RemoveAllTag()
    {
        foreach (var obj in tag_list)
        {
            Destroy(obj);
        }
        tag_efficacy_index = 0;
        tag_side_index = 0;
        tag_list.Clear();
    }
}
