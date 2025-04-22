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
    public float Speed;
    Order_SO order_SO;
    GameObject order_perfab;

    float current_price;
    int current_potion_index = 0;
    List<PotionName> potionList = new();
    List<GameObject> order_obj = new();
    Status current_status = Status.Waiting;

    Transform waiting_area;
    GameObject request;

    void FixedUpdate()
    {
        if (current_status == Status.Running)
        {
            if (transform.localPosition.x > 0)
            {
                transform.localPosition -= new Vector3(Speed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                transform.localPosition.Set(0, transform.localPosition.y, transform.localPosition.z);
                SetStatus(Status.Order);
            }
        }
        if (current_status == Status.Leaving)
        {
            if (transform.localPosition.x < 0)
            {
                transform.localPosition += new Vector3(Speed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public Customer_Normal Init()
    {
        order_SO=DataManager.instance.Order;
        order_perfab=ResourceManager.instance.GetGameObject(GameObjectName.Order);

        System.Random ran = new();
        int order_count = order_SO.RandomOrderCount();
        SetStatus(Status.Waiting);
        waiting_area = GameObject.Find("Customer_Waiting").transform;
        request = transform.Find("request").gameObject;
        request.SetActive(false);
        for(int i=0;i<order_count;i++)
        {
            potionList.Add(order_SO.PotionRange[ran.Next(order_SO.PotionRange.Count)]);
            CreateOrder(potionList[i]);
        }
        ChangeUI(0);
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
        order_obj.Add(obj);
    }

    void ChangeUI(int index){
        for(int i=index;i<order_obj.Count;i++){
            GameObject obj=order_obj[i];
            obj.transform.localPosition=OrderConst.PositonConst[order_obj.Count-1][i];
            if(i==index)
            {
                obj.transform.Find("potion").localScale=OrderConst.PositonStruct[OrderConst.PositionScale.Focus].Scale;
                obj.transform.Find("order_text").GetComponent<TMP_Text>().fontSize=OrderConst.PositonStruct[OrderConst.PositionScale.Focus].FontSize;
            }
        }
    }

    void SettlePrice()
    {
        //TODO
        Debug.Log("获得金钱：" + current_price);
        transform.SetParent(waiting_area);
        SetStatus(Status.Leaving);
        EventManager.instance.Invoke("Customer/Leave");
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
