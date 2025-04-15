using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Timer : SingleInstance<Timer>
{
    private class TimedEvent
    {
        public string Tag;
        public float TimeToExecute;
        public Callback Method;
    }

    private List<TimedEvent> events;
    public delegate void Callback();

    private void Awake()
    {
        //events = new List<TimedEvent>();
    }

    private void Update()
    {
        if (events == null || events.Count == 0) return;

        for (int i = 0; i < events.Count; i++)
        {
            var timedEvent = events[i];
            if (timedEvent.TimeToExecute <= Time.time)
            {
                timedEvent.Method();
                events.Remove(timedEvent);
            }
        }
    }

    public void Add(Callback method, float inSeconds, string tag = "")
    {
        if (events == null) {
            events = new List<TimedEvent>();
        }

        events.Add(new TimedEvent
        {
            Tag = tag,
            Method = method,
            TimeToExecute = Time.time + inSeconds
        });
    }

    public void Dismiss(Callback method)
    {
        var timedEvent = events?.Where(x => x.Method == method).FirstOrDefault();
        if (timedEvent != null)
            events.Remove(timedEvent);
    }

    public void Dismiss(string tag)
    {
        if (string.IsNullOrEmpty(tag))
            return;

        var timedEvent = events?.Where(x => x.Tag == tag).FirstOrDefault();
        if (timedEvent != null)
            events.Remove(timedEvent);
    }

    public bool IsAdded(string tagPrefix) {
        TimedEvent timedEvent = events.Find(e =>  e.Tag.Contains(tagPrefix));
        if (timedEvent != null) {
            return true;
        }

        return false;
    }

}
