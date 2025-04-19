using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class UIManager : MonoBehaviour
{
    [Header("启用Debug模式")]
    public bool DebugModel;
    private Dictionary<string,UIView> _UIs;

    private static UIManager _UIManager;
    public static UIManager instance
    {
        get
        {
            if (!_UIManager)
            {
                _UIManager = FindObjectOfType(typeof(UIManager)) as UIManager;
                if (!_UIManager)
                {
                    return null;
                }
                _UIManager.Init();
            }
            return _UIManager;
        }
    }
    void Init()
    {
        _UIs ??= new();
    }

    public void AddUIView(string url, UIView view)
    {
        if (_UIs.ContainsKey(url))
        {
            Debug.LogError($"UIManager:\n添加Url: \"{url}\" 失败,该Url已存在");
            return;
        }
        _UIs.Add(url, view);
        view.Init();

    }
    public void RemoveUIView(params string[] urls)
    {
        foreach(var u in urls){
            _UIs.Remove(u);
        }
    }
    public void EnableUIView(string url)
    {
        if (!_UIs.ContainsKey(url))
        {
            Debug.LogError($"UIManager:\n启用Url: \"{url}\" 失败,该Url不存在");
            return;
        }
        _UIs[url].Enable();
    }
    public void DisableUIView(string url)
    {
        if (!_UIs.ContainsKey(url))
        {
            Debug.LogError($"UIManager:\n禁用Url: \"{url}\" 失败,该Url不存在");
            return;
        }
        _UIs[url].Disable();
    }
}