using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScoreObserver : MonoBehaviour
{
    public abstract void OnNotify(float time, string name);
}

public abstract class ScoreNotifier : MonoBehaviour
{
    private List<ScoreObserver> scoreObservers = new List<ScoreObserver>();
    public void RegisterObserver(ScoreObserver scoreObserver)
    {
        scoreObservers.Add(scoreObserver);
    }

    public void Notify(float time, string name)
    {
        foreach(var observer in scoreObservers)
        {
            observer.OnNotify(time, name);
            Debug.Log("notified");
        }
    }
}
