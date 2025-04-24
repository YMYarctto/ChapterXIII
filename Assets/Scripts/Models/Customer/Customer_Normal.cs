using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETag;
using EPotion;
using EResource;
using TMPro;
using UnityEngine.UI;

public class Customer_Normal : MonoBehaviour
{
    Color customer_color;
    float customer_a_speed=0.6f;

    OrderData_SO order_data;
    CustomerData_SO customer_data;
    GameObject order_perfab;

    float current_price;
    int current_potion_index = 0;
    List<PotionName> potionList = new();
    List<GameObject> order_obj = new();
    Status current_status = Status.Waiting;

    GameObject request;
    Collider2D collider_2d;
    PatienceBar patienceBar;

    float customer_waiting_time;
    float current_waiting_time;
    float waiting_time_scale;

    void FixedUpdate()
    {
        if (current_status == Status.Running)
        {
            if (customer_color.a<1)
            {
                customer_color.a=customer_color.a+customer_a_speed*Time.fixedDeltaTime>1?1:customer_color.a+customer_a_speed*Time.fixedDeltaTime;
                GetComponent<Image>().color=customer_color;
            }
            else
            {
                SetStatus(Status.Order);
            }
        }
        if(current_status==Status.Order)
        {
            current_waiting_time-=waiting_time_scale*Time.fixedDeltaTime;
            patienceBar.ChangeUI(current_waiting_time/customer_waiting_time);
            if(current_waiting_time<=0){
                SettlePrice();
                SetStatus(Status.Leaving);
            }
        }
        if (current_status == Status.Leaving)
        {
            if (customer_color.a>0)
            {
                customer_color.a=customer_color.a-customer_a_speed*Time.fixedDeltaTime<0?0:customer_color.a-customer_a_speed*Time.fixedDeltaTime;
                GetComponent<Image>().color=customer_color;
            }
            else
            {
                EventManager.instance.Invoke("Customer/Leave");
                Destroy(gameObject);
            }
        }
    }

    public Customer_Normal Init()
    {
        order_data=DataManager.instance.OrderData;
        order_perfab=ResourceManager.instance.GetGameObject(GameObjectName.Order);
        customer_data=DataManager.instance.CustomerData;
        customer_color=GetComponent<Image>().color;
        customer_color.a=0;
        GetComponent<Image>().color=customer_color;

        int order_count = order_data.RandomOrderCount();
        SetStatus(Status.Waiting);
        request = transform.Find("request").gameObject;
        patienceBar = request.transform.Find("patience_bar").GetComponent<PatienceBar>();
        collider_2d=request.GetComponent<BoxCollider2D>();
        collider_2d.enabled=false;
        request.SetActive(false);
        CreateOrderList(order_count);

        customer_waiting_time=customer_data.GetWaitingTime(potionList.Count);
        current_waiting_time=customer_waiting_time;
        waiting_time_scale=customer_data.OrderingTimeScale;
        return this;
    }

    public void SetStatusRunning()
    {
        SetStatus(Status.Running);
    }

    public void GivePotion(Potion potion)
    {
        PotionName potionName = potionList[current_potion_index]; 
        Efficacy tag = (Efficacy)(int)potionName;
        float price = PotionConst.GetPotionPrice(potionName);
        if (!potion.EfficacyList.Contains(tag))
        {
            price = 0;
            //TODO 根据配方复杂度增加价值
        }
        if (potion.SideEffectList.Count > 0)
        {
            price -= price * potion.SideEffectList.Count / 3;
        }
        current_price += price > 0 ? price : 0;
        order_obj[current_potion_index].SetActive(false);
        current_potion_index++;
        if (current_potion_index >= potionList.Count)
        {
            SettlePrice();
            return;
        }
        ChangeUI(current_potion_index);
    }

    public void Order_Recept(){
        collider_2d.enabled=true;
        waiting_time_scale=customer_data.WaitingTimeScale;
        ChangeUI(0);
    }

    public void Order_Refuse(){
        SettlePrice();
    }

    void CreateOrderList(int order_count){
        System.Random ran = new();
        for(int i=0;i<order_count;i++)
        {
            potionList.Add(order_data.PotionRange[ran.Next(order_data.PotionRange.Count)]);
            CreateOrder(potionList[i]);
        }
    }

    void CreateOrder(PotionName potionName){
        GameObject obj=Instantiate(order_perfab,request.transform);
        var sprites=ResourceManager.instance.GetPotionSprite(potionName);
        if(sprites!=null){
            var potion = obj.transform.Find("potion").Find("potion_image");
            potion.GetComponent<Image>().sprite = sprites.potion;
            potion.transform.Find("bottle_plug").GetComponent<Image>().sprite = sprites.bottle_plug;
            potion.transform.Find("bottle_plug").GetComponent<RectTransform>().sizeDelta=new Vector2(sprites.bottle_plug.bounds.size.x*100,sprites.bottle_plug.bounds.size.y*100);
        }
        obj.transform.Find("order_text").GetComponent<TMP_Text>().text=potionName.ToString();
        obj.SetActive(false);
        order_obj.Add(obj);
    }

    void ChangeUI(int index){
        for(int i=index;i<order_obj.Count;i++){
            GameObject obj=order_obj[i];
            obj.transform.localPosition=OrderConst.PositonConst[order_obj.Count-index-1][i-index];
            if(i==index)
            {
                obj.transform.Find("potion").localScale=OrderConst.PositonStruct[OrderConst.PositionScale.Focus].Scale;
                obj.transform.Find("order_text").GetComponent<TMP_Text>().fontSize=OrderConst.PositonStruct[OrderConst.PositionScale.Focus].FontSize;
            }
            obj.SetActive(true);
        }
    }

    void SettlePrice()
    {
        //TODO
        current_price*=customer_data.GetTipRate(current_waiting_time/customer_waiting_time);
        Debug.Log("获得金钱：" + current_price);
        SetStatus(Status.Leaving);
        transform.SetParent(transform.parent,true);
    }

    void SetStatus(Status status)
    {
        current_status = status;
        if (status == Status.Order)
            request.SetActive(true);
        if (status == Status.Leaving)
            request.SetActive(false);
    }

    enum Status
    {
        Waiting,
        Running,
        Order,
        Leaving,
    }
}
