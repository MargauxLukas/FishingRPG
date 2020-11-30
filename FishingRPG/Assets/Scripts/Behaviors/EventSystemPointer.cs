using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemPointer : MonoBehaviour
{
    public static EventSystemPointer instance;
    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }
}
