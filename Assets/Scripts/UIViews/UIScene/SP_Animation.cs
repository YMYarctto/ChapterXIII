using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Animation : MonoBehaviour
{
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
    void OnDisable()
    {
        EventManager.instance?.RemoveListener("Game/SAN/OnChange");
    }
    public void ChangeSPAnimation()
    {
        if(GameController.SAN<=SANValue)
        {
            animator.SetBool("LowSAN", true);
            return;
        }
        animator.SetBool("LowSAN", false);
    }
}
