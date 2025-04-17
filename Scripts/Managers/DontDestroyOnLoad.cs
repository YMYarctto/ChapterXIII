using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    [Header("DontDestroyOnLoad")]
    public bool Enable;

    void Awake() {
        if (Enable) {
            DontDestroyOnLoad(gameObject);
        }
    }
}