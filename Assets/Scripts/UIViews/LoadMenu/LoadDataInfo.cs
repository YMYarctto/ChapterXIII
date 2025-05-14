using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadDataInfo : MonoBehaviour,IPointerClickHandler
{
    public int index;
    SaveData_SO data;

    TMP_Text day;
    TMP_Text time;
    TMP_Text money;
    GameObject line;
    GameObject delete;

    void Awake()
    {
        day = transform.Find("Day").GetComponent<TMP_Text>();
        time = transform.Find("Time").GetComponent<TMP_Text>();
        money = transform.Find("Money").GetComponent<TMP_Text>();
        line = transform.Find("Line").gameObject;
        delete = transform.Find("Delete").gameObject;
        if(index==0)delete.SetActive(false);
    }

    public void Init()
    {
        data = DataManager.instance.GetSaveData(index);
        GetData();
    }

    public void GetData()
    {
        if (data.isInit)
        {
            line.SetActive(true);
            if(index!=0)delete.SetActive(true);
            time.gameObject.SetActive(true);
            money.gameObject.SetActive(true);
            day.text = $"第{data.Day}天";
            time.text = data.SaveTime;
            money.text = data.TotalMoney.ToString();
            return;
        }
        time.gameObject.SetActive(false);
        money.gameObject.SetActive(false);
        line.SetActive(false);
        delete.SetActive(false);
        day.text = "新建存档";
    }

    public void DeleteData()
    {
        DataManager.instance.GetSaveData(index).DeleteFile();
        GetData();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(LoadMenu.enableType==EnableType.Load)
        {
            DataManager.instance.LoadSaveData(index);
            UIManager.instance.GetUIView<LoadingInit>("LoadingInit").ChangeScene("PharmacyScene","MainScene",()=>{
                UIManager.instance.GetUIView<LoadMenu>("LoadMenu").ForceDisable();
            },()=>{
                GameController.StartGameAction.Invoke();
            });
        }
        else if(LoadMenu.enableType==EnableType.Save)
        {
            data.LoadFromSO(DataManager.instance.DefaultSaveData);
            data.SaveToFile();
            GetData();
        }
    }
}
