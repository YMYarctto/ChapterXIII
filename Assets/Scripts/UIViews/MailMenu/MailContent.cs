using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MailContent : UIView
{
    public bool HaveNewMail{get=>!pageList.All(i=>!i.IsNew);}

    MailPageCounter mailPageCounter;
    int current_page_index;
    List<GameObject> gameobjectList;
    List<Page> pageList;
    bool isInit=false;

    void Awake()
    {
        UIManager.instance.AddUIView("MailContent",this);
    }

    void OnEnable()
    {
        if(!isInit)return;
        gameobjectList[pageList[current_page_index].index].SetActive(true);
    }

    public override void Init()
    {
        gameobjectList = new();
        pageList = new();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject page=transform.Find("Page_"+i).gameObject;
            gameobjectList.Add(page);
            StartCoroutine(SetActiveFalse(page));
        }
        current_page_index = 0;
    }

    IEnumerator SetActiveFalse(GameObject obj)
    {
        yield return null;
        obj.SetActive(false);
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("MailContent");
    }

    public void NextPage(int index)
    {
        current_page_index+=index>0?1:-1;
        current_page_index=current_page_index<0?pageList.Count-1:current_page_index>=pageList.Count?0:current_page_index;
        for (int i = 0; i < pageList.Count; i++)
        {
            gameobjectList[pageList[i].index].SetActive(false);
        }
        GameObject currentPage=gameobjectList[pageList[current_page_index].index];
        currentPage.SetActive(true);
        pageList[current_page_index].IsNew=false;
        mailPageCounter.SetPage(current_page_index + 1, pageList.Count);
        RectTransform rt=GetComponent<RectTransform>();
        rt.sizeDelta=new Vector2(rt.sizeDelta.x,currentPage.GetComponent<RectTransform>().sizeDelta.y+300);
    }

    public void AddPage(Page page)
    {
        pageList.Add(page);
        if(pageList.Count<=1){
            page.IsNew=false;
        }
    }

    public void AddPageFinish()
    {
        mailPageCounter = UIManager.instance.GetUIView<MailPageCounter>("MailPageCounter");
        mailPageCounter.SetPage(current_page_index + 1, pageList.Count);
        isInit = true;
        RectTransform rt=GetComponent<RectTransform>();
        rt.sizeDelta=new Vector2(rt.sizeDelta.x,gameobjectList[pageList[current_page_index].index].GetComponent<RectTransform>().sizeDelta.y+300);
    }
}
