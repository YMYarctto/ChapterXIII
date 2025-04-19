using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject CustomerPerfab;

    Transform customer_panel;
    Dictionary<Transform,bool> customer_area;
    Transform waiting_area;

    void Awake()
    {
        customer_panel=GameObject.Find("Customer_Panel").transform;
        waiting_area=customer_panel.Find("Customer_Waiting");
        string[] area={"Customer_area1","Customer_area2","Customer_area3"};
        customer_area=new();
        for(int i=0;i<area.Count();i++)
        {
            customer_area[customer_panel.Find(area[i])]=false;
        }
    }

    void OnEnable()
    {
        EventManager.instance.AddListener("Customer/Leave",Refresh);
        EventManager.instance.AddListener("Customer/Create",CreateCustomer);
    }

    void OnDisable()
    {
        EventManager.instance?.RemoveListener("Customer/Leave",Refresh);
        EventManager.instance?.RemoveListener("Customer/Create",CreateCustomer);
    }

    public void Refresh(){
        List<Transform> list=new(customer_area.Keys);
        for(int i=0;i<list.Count;i++)
        {
            if(list[i].childCount==0)
            {
                customer_area[list[i]]=false;
            }
        }
    }

    public void CreateCustomer(){
        Transform trans = GetAvailableArea();
        if(trans==null)
        {
            //TODO
            Debug.Log("客人已满");
            return;
        }
        var customer = Instantiate(CustomerPerfab,waiting_area);
        customer.transform.localPosition=new(0,customer.transform.localPosition.y,customer.transform.localPosition.z);
        customer.transform.SetParent(trans,true);
        customer_area[trans]=true;
        customer.GetComponent<Customer_Normal>().Init().SetStatusRunning();
    }

    Transform GetAvailableArea(){
        List<Transform> transList = new();
        //如果area未在使用，则添加
        foreach(var trans in customer_area){
            if(!trans.Value)
                transList.Add(trans.Key);
        }
        if(transList.Count==0){
            return null;
        }
        System.Random ran =new();
        return transList[ran.Next(transList.Count)];
    }

}
