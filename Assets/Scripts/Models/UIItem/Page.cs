using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page
{
    public bool IsNew{get=>isNew;set=>isNew=value;}
    public int index{get=>page;}

    int page;
    bool isNew;

    Page(int _page,bool _isNew)
    {
        page = _page;
        isNew = _isNew;
    }

    public static void Add(int _page,bool _isNew)
    {
        UIManager.instance.GetUIView<MailContent>("MailContent").AddPage(new(_page,_isNew));
    }
}
