using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public interface iDog
{
    void Movement();
    void HandleComms(bool enable);
    void Call();
    
    
}

public abstract class Dog : MonoBehaviour, iDog
{
    public abstract void Movement();
    public abstract void HandleComms(bool enable);
    public abstract void Call();
    public delegate void Recieve();


    //public static event UnityAction Recieve;
    //public static void Broadcast() => Recieve?.Invoke();
}
