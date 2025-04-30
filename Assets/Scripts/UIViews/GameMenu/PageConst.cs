using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PageConst
{
    public static int Page=0;

    public static void Init()
    {
        Page=0;
    }

    public static void ChangePage(int i)
    {
        Page=Page+i<0?0:Page+i>2?2:Page+i;
        switch(Page)
        {
            case 0:
                UIManager.instance.DisableUIView("Button_LastPage");
                UIManager.instance.EnableUIView("Reception");
                UIManager.instance.DisableUIView("Workbench");
                UIManager.instance.DisableUIView("ItemInfoUI");
                break;
            case 1:
                UIManager.instance.EnableUIView("Button_NextPage");
                UIManager.instance.EnableUIView("Button_LastPage");
                UIManager.instance.EnableUIView("Workbench");
                UIManager.instance.DisableUIView("Reception");
                UIManager.instance.DisableUIView("Warehouse");
                UIManager.instance.EnableUIView("ItemInfoUI");
                break;
            case 2:
                UIManager.instance.DisableUIView("Button_NextPage");
                UIManager.instance.DisableUIView("Workbench");
                UIManager.instance.EnableUIView("Warehouse");
                break;
        }
    }
}
