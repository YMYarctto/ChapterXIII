using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETag;
using EPotion;

public class Customer_Normal : MonoBehaviour
{
    public float Speed;

    float current_price;
    int current_potion_index = 0;
    List<PotionName> potionList = new();
    Status current_status=Status.Waiting;

    Transform waiting_area;
    GameObject request;

    //TODO 临时
    List<PotionName> potionListConst = new(){
        PotionName.安眠药,
        //PotionName.清热药,
    };

    void FixedUpdate()
    {
        if(current_status==Status.Running)
        {
            if(transform.localPosition.x>0){
                transform.localPosition-=new Vector3(Speed*Time.fixedDeltaTime,0,0);
            }else{
                transform.localPosition.Set(0,transform.localPosition.y,transform.localPosition.z);
                SetStatus(Status.Order);
            }
        }
        if(current_status==Status.Leaving)
        {
            if(transform.localPosition.x<0){
                transform.localPosition+=new Vector3(Speed*Time.fixedDeltaTime,0,0);
            }else{
                Destroy(gameObject);
            }
        }
    }

    public Customer_Normal Init()
    {
        System.Random ran = new();
        potionList.Add(potionListConst[ran.Next(potionListConst.Count)]);
        SetStatus(Status.Waiting);
        waiting_area=GameObject.Find("Customer_Waiting").transform;
        request = transform.Find("request").gameObject;
        request.SetActive(false);
        return this;
    }

    public void SetStatusRunning(){
        SetStatus(Status.Running);
    }

    public void GivePotion(Potion potion){
        PotionName potionName = potionList[current_potion_index];
        Efficacy tag=(Efficacy)(int)potionName;
        float price=PotionConst.GetPotionPrice(potionName);
        if(!potion.EfficacyList.Contains(tag)){
            price=0;
            //TODO 根据配方复杂度增加价值
        }
        if(potion.SideEffectList.Count>0){
            price-=price*potion.SideEffectList.Count/3;
        }
        current_price+=price>0?price:0;
        current_potion_index++;
        if(current_potion_index>=potionList.Count){
            SettlePrice();
        }
    }

    void SettlePrice(){
        //TODO
        Debug.Log("获得金钱："+current_price);
        transform.SetParent(waiting_area);
        SetStatus(Status.Leaving);
        EventManager.instance.Invoke("Customer/Leave");
    }

    void SetStatus(Status status){
        current_status=status;
        if(status==Status.Order)
            request.SetActive(true);
        if(status==Status.Leaving)
            request.SetActive(false);
    }

    enum Status{
        Waiting,
        Running,
        Order,
        Leaving,
    }
}
