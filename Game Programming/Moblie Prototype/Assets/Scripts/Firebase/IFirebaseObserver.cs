using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFirebaseObserver
{
    public void OnDatabaseDataLoaded();
}

public abstract class IFirebaseSubject : MonoBehaviour
{
    private List<IFirebaseObserver> updateObservers = new List<IFirebaseObserver>();

    public void RegisterObserver(IFirebaseObserver updateObserver)
    {
        updateObservers.Add(updateObserver);
    }

    public void DatabaseDataLoaded()
    {
        foreach(var observer in updateObservers)
        {
            observer.OnDatabaseDataLoaded();
        }
    }
}
