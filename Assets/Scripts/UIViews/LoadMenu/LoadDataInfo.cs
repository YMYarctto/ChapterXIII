using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadDataInfo : MonoBehaviour,IPointerClickHandler
{
    public int index;

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
        GetData();
    }

    public void GetData()
    {
        SaveData_SO data = DataManager.instance.GetSaveData(index);
        if (data.isInit)
        {
            day.text = $"第{data.Day}天";
            time.text = data.SaveTime;
            money.text = data.TotalMoney.ToString();
            return;
        }
        day.text = "新建存档";
        time.gameObject.SetActive(false);
        money.gameObject.SetActive(false);
        line.SetActive(false);
        delete.SetActive(false);
    }

    public void DeleteData()
    {
        DataManager.instance.GetSaveData(index).DeleteFile();
        GetData();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DataManager.instance.LoadSaveData(index);
        UIManager.instance.GetUIView<LoadingInit>("LoadingInit").ChangeScene("PharmacyScene","MainScene",()=>{
            GameController.StartGameAction.Invoke();
        });
    }
}
