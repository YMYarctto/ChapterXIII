using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIView : MonoBehaviour
{
    public abstract void Init();
    public abstract void OnUnload();
    
    public virtual void Enable()=> gameObject.SetActive(true);
    public virtual void Disable()=> gameObject.SetActive(false);

    void OnDestroy(){OnUnload();}
    
}