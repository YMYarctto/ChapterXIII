using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    List<GameObject> customer_perfab_list;
    Queue<GameObject> random_customer_list;

    Transform customer_panel;
    Dictionary<Transform,bool> customer_area;
    public static Transform waiting_area;

    Queue<GameObject> customer_inWaiting;

    void Awake()
    {
        customer_perfab_list=ResourceManager.instance.GetNormalCustomerList_v2();
        random_customer_list=RandomList(customer_perfab_list);
        
        customer_inWaiting=new();
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
        EventManager.instance?.RemoveListener("Customer/Leave");
        EventManager.instance?.RemoveListener("Customer/Create");
    }

    public void Refresh(){
        StartCoroutine(ERefresh());
    }

    IEnumerator ERefresh()
    {
        yield return new WaitForFixedUpdate();
        List<Transform> list=new(customer_area.Keys);
        for(int i=0;i<list.Count;i++)
        {
            if(list[i].childCount==0)
            {
                customer_area[list[i]]=false;
            }
        }
        for(Transform trans=GetAvailableArea();trans!=null&&customer_inWaiting.Count>0;)
        {
            CreateCustomer(customer_inWaiting.Dequeue(),trans);
            yield return new WaitForFixedUpdate();
        }
    }

    public void CreateCustomer(){
        if(random_customer_list.Count==0){
            random_customer_list=RandomList(customer_perfab_list);
        }
        GameObject customer_perfab=random_customer_list.Dequeue();
        Transform trans = GetAvailableArea();
        var customer = Instantiate(customer_perfab,waiting_area);
        customer.name = customer.name.Replace("(Clone)", "");
        GameController.CustomerTotal.Add();
        GameController.CustomerNormalTotal.Add();
        if(trans==null)
        {
            customer.SetActive(false);
            customer_inWaiting.Enqueue(customer);
            return;
        }
        CreateCustomer(customer,trans);
    }

    public void CreateCustomer(GameObject customer,Transform trans){
        customer.SetActive(true);
        customer.transform.SetParent(trans,true);
        customer.transform.localPosition=new(0,customer.transform.localPosition.y,customer.transform.localPosition.z);
        customer_area[trans]=true;
        customer.GetComponent<Customer_Normal>().Init();
        AudioManager.instance.PlayAudio("Customer","Customer/Come");
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

    Queue<GameObject> RandomList(List<GameObject> gameObject_list){
        List<GameObject> list=new(gameObject_list);
        GameObject temp;
        System.Random ran=new();
        int max=list.Count;
        for(int i=0;i<max-1;i++)
        {
            int j=ran.Next(i,max);
            if(j==i)continue;
            temp=list[i];
            list[i]=list[j];
            list[j]=temp;
        }
        return new Queue<GameObject>(list);
    }
}
