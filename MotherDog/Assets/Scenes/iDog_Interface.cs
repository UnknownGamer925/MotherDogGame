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
    void Respond();
    static event UnityAction Recieve;
    public static void Broadcast() => Recieve?.Invoke();
}
