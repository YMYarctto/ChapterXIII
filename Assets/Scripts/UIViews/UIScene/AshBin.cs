using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshBin : MonoBehaviour
{
    GameObject open;
    GameObject close;

    void Awake()
    {
        open=transform.Find("open").gameObject;
        close=transform.Find("close").gameObject;
        open.SetActive(false);
    }

    public void Open()
    {
        open.SetActive(true);
        close.SetActive(false);
    }

    public void Close()
    {
        open.SetActive(false);
        close.SetActive(true);
    }
}
