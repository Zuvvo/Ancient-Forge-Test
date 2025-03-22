using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance => _instance ?? FindAnyObjectByType<T>();

    private static T _instance = null;

    public virtual void Awake()
    {
        _instance = GetComponent<T>();
    }
}
