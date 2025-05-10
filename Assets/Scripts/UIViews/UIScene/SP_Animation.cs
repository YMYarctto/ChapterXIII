using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Animation : MonoBehaviour
{
    [Header("SP类型")]public SP_Type type;
    [Header("切换贴图所需SAN值")]public int SANValue;
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void OnEnable()
    {
        EventManager.instance.AddListener("Game/SAN/OnChange", ChangeSPAnimation);
    }
    public void ChangeSPAnimation()
    {
        if(type==SP_Type.Customer)
        {
            switch(transform.parent.name)
            {
                case "Customer_area1":
                    SANValue=2;
                    break;
                case "Customer_area2":
                    SANValue=3;
                    break;
                case "Customer_area3":
                    SANValue=4;
                    break;
                default:
                    SANValue=0;
                    break;
            }
        }

        if(GameController.SAN<=SANValue)
        {
            animator.SetBool("LowSAN", true);
            return;
        }
        animator.SetBool("LowSAN", false);
    }

    public enum SP_Type
    {
        Default,
        Customer,
    }
}
