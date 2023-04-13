using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> // T (or any letter) is a generic type, able to pass in most things. This will also be a base class that others can inherit from mombehavior
{
    private static T instance;
    public static T Instance {get {return instance;} }
    
    // assign this instance to a monobeavhior class that is assigned to this isntance
    protected virtual void Awake()
    {
        if (instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject); // if object exsist, destory this object
        }
        else
        {
            instance = (T)this; // cast this object as a generic type
        }

        DontDestroyOnLoad(gameObject);
    }
}
