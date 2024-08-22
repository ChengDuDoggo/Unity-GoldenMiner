using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singletom<T> where T : new()
{
    private static T _instance;
    private static object mutex = new object();
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                lock (mutex)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
            }
            return _instance;
        }
    }
}
