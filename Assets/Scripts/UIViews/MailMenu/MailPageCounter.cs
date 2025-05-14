using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MailPageCounter : UIView
{
    TMP_Text text;

    void Awake()
    {
        UIManager.instance.AddUIView("MailPageCounter",this);
    }

    public override void Init()
    {
        text=GetComponent<TMP_Text>();
        text.text = "0/0";
    }

    public override void OnUnload()
    {
        UIManager.instance?.RemoveUIView("MailPageCounter");
    }

    public void SetPage(int current_page_index,int total_page)
    {
        text.text = $"{current_page_index}/{total_page}";
    }
}
