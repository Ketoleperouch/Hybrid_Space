using UnityEngine;
using UnityEngine.Events;

public class ActivatingTrackableHandler : DefaultTrackableEventHandler
{
    public UnityEvent[] onTrackingFound;
    public UnityEvent[] onTrackingLost;

    public int playIndex = 0;

    protected override void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);

        foreach (var component in rendererComponents)
        {
            if (!component.CompareTag("KeepDisabled"))
                component.enabled = true;
        }

        onTrackingFound[playIndex].Invoke();
    }

    protected override void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);

        foreach (var component in rendererComponents)
            component.enabled = false;

        onTrackingLost[playIndex].Invoke();
    }

    public void SetIndex(int index)
    {
        playIndex = index;
    }
}
