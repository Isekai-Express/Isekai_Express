using System;
using System.Collections;
using System.Collections.Generic;

public class BlackBoard
{
    private Dictionary<Type, IDictionary> _blackBoardDictionary = new Dictionary<Type, IDictionary>();

    public void SetData<T>(string keyName, T value)
    {
        if (!_blackBoardDictionary.ContainsKey(typeof(T)))
        {
            _blackBoardDictionary.Add(typeof(T), new Dictionary<string, T>());
        }

        var dic = _blackBoardDictionary[typeof(T)];
        if (dic.Contains(keyName))
        {
            dic[keyName] = value;
        }
        else
        {
            dic.Add(keyName, value);
        }
    }

    public T GetData<T>(string keyName)
    {
        if (!_blackBoardDictionary.ContainsKey(typeof(T)))
        {
            return default(T);
        }

        var dic = _blackBoardDictionary[typeof(T)];
        if (!dic.Contains(keyName))
        {
            return default(T);
        }

        return (T)dic[keyName];
    }
}