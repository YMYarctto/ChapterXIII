using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EventChain:EventModel.EventChain
{
    public EventChain(UnityAction action):base(action){}
    public new EventChain AddUrl(params string[] url){base.AddUrl(url);return this;}
}
public class EventChain<T>:EventModel.EventChain<T>
{
    public EventChain(UnityAction<T> action):base(action){}
    public new EventChain<T> AddUrl(params string[] url){base.AddUrl(url);return this;}
}

namespace EventModel{
    public class EventChain
    {
        public List<string> UrlList { get => _url; }
        public UnityAction Action { get => _action; }
        List<string> _url;
        UnityAction _action;
        public EventChain(UnityAction action)
        {
            _url = new();
            _action = action;
        }
        public EventChain AddUrl(params string[] url)
        {
            foreach (var u in url)
            {   
                _url.Add(u);
            }
            return this;
        }
    }
    public class EventChain<T>
    {
        public List<string> UrlList { get => _url; }
        public UnityAction<T> Action { get => _action; }
        List<string> _url;
        UnityAction<T> _action;
        public EventChain(UnityAction<T> action)
        {
            _url = new();
            _action = action;
        }
        public void AddUrl(params string[] url)
        {
            foreach (var u in url)
            {
                _url.Add(u);
            }
        }
    }
    class EventList
    {
        Dictionary<Type,UnityEvent<object>> _event;
        Dictionary<Type,object> _param;
        public EventList(){
            _event=new();
            _param=new();
        }
        public void AddListener(UnityAction listener){
            Type type=typeof(NullParam);
            if(!_event.ContainsKey(type)){
                AddType(type);
            }
            UnityAction<object> _listener = (object obj) =>
            {
                listener();
            };
            _event[type].AddListener(_listener);
        }
        public void AddListener<T>(UnityAction<T> listener){
            Type type=typeof(T);
            if(!_event.ContainsKey(type)){
                AddType(type);
            }
            UnityAction<object> _listener = (object obj) =>
            {
                if (obj is T t)
                    listener(t);
                else
                    Debug.LogError("EventManager: 添加事件失败");
            };
            _event[type].AddListener(_listener);
        }
        public void RemoveListener(UnityAction listener){
            Type type=typeof(NullParam);
            if(!_event.ContainsKey(type)){
                Debug.LogWarning("EventManager: 该url下没有该类型事件");
                return;
            }
            UnityAction<object> _listener = (object obj) =>
            {
                listener();
            };
            _event[type].RemoveListener(_listener);
        }
        public void RemoveListener<T>(UnityAction<T> listener){
            Type type=typeof(T);
            if(!_event.ContainsKey(type)){
                Debug.LogWarning("EventManager: 该url下没有该类型事件");
                return;
            }
            UnityAction<object> _listener = (object obj) =>
            {
                if (obj is T t)
                    listener(t);
            };
            _event[type].RemoveListener(_listener);
        }

        public void InvokeAll(){
            foreach(var kv in _event){
                if(kv.Key==typeof(NullParam)){
                    kv.Value?.Invoke(new NullParam());
                }else if(_param.ContainsKey(kv.Key)){
                    kv.Value?.Invoke(_param[kv.Key]);
                }else{
                    Debug.LogError($"EventManager:\nType: {kv.Key} 未设置预设参数\n");
                }
            }
        }

        public void SetInvokeParam<T>(T param){
            Type type=typeof(T);
            _param[type] = param;
        }

        public void RemoveAllListeners(){
            foreach(var kv in _event){
                kv.Value.RemoveAllListeners();
            }
            _event.Clear();
            _param.Clear();
        }
        private void AddType(Type type){
            _event[type]=new UnityEvent<object>();
        }

        override public string ToString(){
            string str="_event:\n";
            foreach(var kv in _event){
                str+=$"Type : {kv.Key}\n";
            }
            str+="\n_param:\n";
            foreach(var kv in _param){
                str+=$"Type : {kv.Key}\n";
            }
            return str;
        }
    }

    struct NullParam{}

}