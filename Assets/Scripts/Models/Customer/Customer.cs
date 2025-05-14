using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETag;
using EPotion;
using EResource;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using ECustomer;

public abstract class Customer : MonoBehaviour
{
    protected Customer_SO CurrentCustomer;

    Color customer_color;
    float customer_a_speed=0.6f;

    protected OrderData_SO order_data;
    protected CustomerData_SO customer_data;
    GameObject order_perfab;

    protected float current_price;
    protected int current_potion_index = 0;
    protected List<PotionName> potionList;
    protected List<GameObject> order_obj;
    Status current_status = Status.Waiting;

    GameObject request;
    Collider2D collider_2d;
    PatienceBar patienceBar;
    TMP_Text dialog;
    GameObject button;
    protected string dialog_str;

    protected float customer_waiting_time;
    protected float current_waiting_time;
    float waiting_time_scale;
    float SAN_time_Scale;

    bool isInit=false;

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
            SAN_time_Scale=1+0.05f*(6-GameController.SAN);
            current_waiting_time-=waiting_time_scale*SAN_time_Scale*Time.fixedDeltaTime;
            patienceBar.ChangeUI(current_waiting_time/customer_waiting_time);
            if(current_waiting_time<=0){
                SettleMoney();
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
                transform.SetParent(CustomerController.waiting_area,false);
                transform.localPosition=new Vector3(0,0,0);
                transform.localScale=new Vector3(0,0,0);
                EventManager.instance.Invoke("Customer/Leave");
            }
        }
    }

    public void Init()
    {
        StartCoroutine(init());
        StartCoroutine(SetStatusRunning());
    }

    IEnumerator init()
    {
        CurrentCustomer=ResourceManager.instance.GetCustomerSO(gameObject.name);
        order_data=DataManager.instance.OrderData;
        order_perfab=ResourceManager.instance.GetGameObject(GameObjectName.Order);
        customer_data=DataManager.instance.CustomerData;
        customer_color=GetComponent<Image>().color;
        customer_color.a=0;
        GetComponent<Image>().color=customer_color;

        potionList=new();
        order_obj=new();
        order_data.LoadData();
        int order_count = order_data.RandomOrderCount(DataManager.instance.DefaultSaveData.Stage);
        SetStatus(Status.Waiting);
        request = transform.Find("request").gameObject;
        patienceBar = request.transform.Find("patience_bar").GetComponent<PatienceBar>();
        collider_2d=request.GetComponent<BoxCollider2D>();
        collider_2d.enabled=false;
        dialog = request.transform.Find("dialog").GetComponent<TMP_Text>();
        dialog.text=CurrentCustomer.GetRandomDialog(CurrentCustomer.DialogOrder);
        button = request.transform.Find("button").gameObject;
        request.SetActive(false);
        CreateOrderList(order_count);

        customer_waiting_time=customer_data.GetWaitingTime(potionList.Count);
        current_waiting_time=customer_waiting_time;
        waiting_time_scale=customer_data.OrderingTimeScale;
        yield return null;
        isInit=true;
    }

    protected virtual void CreateOrderList(int order_count){
        System.Random ran = new();
        for(int i=0;i<order_count;i++)
        {
            var list = order_data.PotionRange.Intersect(CurrentCustomer.PotionRequest).ToList();
            potionList.Add(list[ran.Next(list.Count)]);
            CreateOrder(potionList[i]);
        }
    }

    public virtual void Order_Recept(){
        dialog.text="";
        dialog_str=CurrentCustomer.GetRandomDialog(CurrentCustomer.DialogSuccess);
        collider_2d.enabled=true;
        waiting_time_scale=customer_data.WaitingTimeScale;
        if(CurrentCustomer.Classification==CustomerClassification.SA)
        {
            GameController.CustomerSpecialRecepted.Add();
        }else
        {
            GameController.CustomerNormalRecepted.Add();
        }
        ChangeUI(0);
    }

    public virtual void Order_Refuse(){
        dialog_str=CurrentCustomer.GetRandomDialog(CurrentCustomer.DialogRefuse);
        GameController.CustomerRefused.Add();
        SettleMoney();
    }

    public abstract void GivePotion(Potion potion);

    protected void ChangeUI(int index){
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

    protected void CreateOrder(PotionName potionName){
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

    public virtual void SettleMoney()
    {
        if(current_status!=Status.Order)return;
        StartCoroutine(ESettleMoney());
    }

    IEnumerator ESettleMoney()
    {
        for(int i=0;i<order_obj.Count;i++)
        {
            order_obj[i].SetActive(false);
        }
        SetStatus(Status.Waiting);
        dialog.text=dialog_str;
        button.SetActive(false);
        GameController.AddMoney(current_price);
        yield return new WaitForSeconds(1.5f);
        SetStatus(Status.Leaving);
        transform.SetParent(transform.parent,true);
        GameController.CustomerLeave.Add();
    }

    IEnumerator SetStatusRunning()
    {
        yield return new WaitUntil(()=>isInit);
        SetStatus(Status.Running);
        GameController.CustomerTotal.Add();
        if(CurrentCustomer.Classification==CustomerClassification.SA)
        {
            GameController.CustomerSpecialTotal.Add();
        }
        else
        {
            GameController.CustomerNormalTotal.Add();
        }
    }

    protected void SetStatus(Status status)
    {
        current_status = status;
        if (status == Status.Order)
            request.SetActive(true);
        if (status == Status.Leaving)
            request.SetActive(false);
    }

    protected enum Status
    {
        Waiting,
        Running,
        Order,
        Leaving,
    }
}
