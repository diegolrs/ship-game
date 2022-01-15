using System.Collections.Generic;
using UnityEngine;

public interface IObserver<T>
{
    void OnNotified(T notifier);
}

public class ObserverNotifier<T> : MonoBehaviour
{
    List<IObserver<T>> _listeners;

    public void AddListener(IObserver<T> listener)
    {
        if(_listeners == null)
            _listeners = new List<IObserver<T>>();

        if(listener != null && !(_listeners.Contains(listener)))
            _listeners.Add(listener);
    }

    public void RemoveListener(IObserver<T> listener)
    {
        if(_listeners == null || listener == null)
            return;

        if(_listeners.Contains(listener))
            _listeners.Remove(listener);
    }

    public void NotifyListeners()
    {
        if(_listeners == null)
            return;

        T notifier = GetComponent<T>();

        foreach(var listener in _listeners)
            listener.OnNotified(notifier);
    }
}