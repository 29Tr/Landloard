﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public abstract class MonoSingleton<T>:MonoBehaviour
    where T:MonoBehaviour
{
    private static T m_instance;

    public static T Instance
    {
        get { return m_instance; }
        set { m_instance = value; }
    }

    public virtual void Awake()
    {
        m_instance = this as T;
    }
}