using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EventModel;

public class EventManager : MonoBehaviour
{
    [Header("启用Debug模式")]
    public bool DebugModel;
    private Dictionary<string,EventList> _events;

    private static EventManager _eventManager;
    public static EventManager instance
    {
        get
        {
            if (!_eventManager)
            {
                _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!_eventManager)
                    return null;
                _eventManager.Init();
            }
            return _eventManager;
        }
    }
    void Awake()
    {
        _=instance;
    }
    void Init()
    {
        _events ??= new();
    }
    void OnDisable(){
        RemoveAllListeners();
    }
    public void AddListener(string url, UnityAction listener)
    {
        if (!_events.ContainsKey(url))
        {
            EventList eventList = new EventList();
            eventList.AddListener(listener);
            _events.Add(url, eventList);
            return;
        }
        _events[url].AddListener(listener);
    }
    public void AddListener<T>(string url, UnityAction<T> listener)
    {
        if (!_events.ContainsKey(url))
        {
            EventList eventList = new EventList();
            eventList.AddListener(listener);
            _events.Add(url, eventList);
            return;
        }
        _events[url].AddListener(listener);
    }
    public void RemoveListener(string url, UnityAction listener)
    {
        if(!_events.ContainsKey(url)){
            Debug.LogError($"\"{url}\"不存在");
            return;
        }
        _events[url].RemoveListener(listener);
    }
    public void RemoveListener<T>(string url, UnityAction<T> listener)
    {
        if(!_events.ContainsKey(url)){
            Debug.LogError($"\"{url}\"不存在");
            return;
        }
        _events[url].RemoveListener(listener);
    }
    public void Invoke(EventModel.EventChain eventChain)
    {
        eventChain.Action.Invoke();
        foreach(var url in eventChain.UrlList){
            Invoke(url);
        }
    }
    public void Invoke<T>(EventModel.EventChain<T> eventChain,T param)
    {
        eventChain.Action.Invoke(param);
        foreach(var url in eventChain.UrlList){
            Invoke(url);
        }

    }
    public void Invoke(string url)
    {
        if (!_events.ContainsKey(url))
        {
            Debug.LogError($"\"{url}\"不存在");
            return;
        }
        DebugLog($"url: \"{url}\"\n{_events[url]}");
        _events[url].InvokeAll();
    }

    public void SetInvokeParam<T>(string url, T param)
    {
        if (!_events.ContainsKey(url))
        {
            EventList eventList = new EventList();
            eventList.SetInvokeParam(param);
            _events.Add(url, eventList);
            return;
        }
        _events[url].SetInvokeParam(param);
    }
    public void RemoveAllListeners()
    {
        foreach (var kv in _events)
        {
            kv.Value.RemoveAllListeners();
        }
        _events.Clear();
    }

    private void DebugLog(string str){
        if(DebugModel){
            Debug.Log(str);
        }
    }

}