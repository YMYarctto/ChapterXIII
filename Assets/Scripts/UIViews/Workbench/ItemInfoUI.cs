using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemInfoUI : UIView
{
    Transform title;
    Transform description;
    Transform _tag;
    Transform image;

    GameObject tag_prefab;
    List<GameObject> tag_list;
    Vector2[] tag_position;
    Vector2[] tag_position_v4;
    int tag_index = 0;

    void Awake()
    {
        UIManager.instance.AddUIView("ItemInfoUI", this);
    }

    public void ChangeTitle(string str)
    {
        title.GetComponent<TMP_Text>().text = str;
    }

    public void ChangeDescription(string str)
    {
        description.GetComponent<TMP_Text>().text = str;
    }

    public override void Init()
    {
        tag_prefab = ResourceManager.instance.GetGameObject(EResource.GameObjectName.tag);
        tag_list = new();
        
        title = transform.Find("ItemInfo_title");
        description = transform.Find("ItemInfo_description");
        _tag = transform.Find("ItemInfo_tag");
        image = transform.Find("ItemInfo_image");
        image.gameObject.SetActive(false);

        tag_position = new Vector2[6];
        tag_position_v4 = new Vector2[4];
        for (int i = 0; i < 6; i += 2)
        {
            tag_position[i] = new(-50, 115 - 25 * i);
            tag_position[i + 1] = new(50, 115 - 25 * i);
            if(i>=4)return;
            tag_position_v4[i]= new(-50, 90 - 25 * i);
            tag_position_v4[i+1]=new(50, 90 - 25 * i);
        }
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("ItemInfoUI");
    }

    public override void Enable()
    {

    }

    public override void Disable()
    {

    }

    public void ShowImage()
    {
        image.gameObject.SetActive(true);
    }

    /// <summary>
    /// 添加标签
    /// </summary>
    /// <param name="str">标签内容</param>
    /// <param name="color">标签颜色</param>
    /// <param name="is_offseted">是否被划掉</param>
    public void AddTag(string str, Color color, bool is_offseted)
    {
        if (tag_index >= 6) return;
        if(tag_index==4)ChangePositionVersion_v6();
        
        GameObject gameobj_tag = Instantiate(tag_prefab, _tag);
        gameobj_tag.transform.localPosition = tag_index>=4?tag_position[tag_index]:tag_position_v4[tag_index];
        gameobj_tag.GetComponent<Image>().color = color;
        gameobj_tag.transform.Find("tag_text").GetComponent<TMP_Text>().text = str;
        gameobj_tag.transform.Find("tag_image").gameObject.SetActive(is_offseted);
        tag_list.Add(gameobj_tag);
        tag_index++;
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

    void ChangePositionVersion_v6()
    {
        for (int i=0;i<tag_list.Count;i++)
        {
            tag_list[i].transform.localPosition=tag_position[i];
        }
    }

    public void RemoveAllTag()
    {
        foreach (var obj in tag_list)
        {
            Destroy(obj);
        }
        tag_index = 0;
        tag_list.Clear();
    }
}
