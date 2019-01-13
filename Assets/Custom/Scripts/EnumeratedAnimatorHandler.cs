using UnityEngine;

public class EnumeratedAnimatorHandler : MonoBehaviour {

    public EnumeratedAnimation[] enumeratedAnimations;
    public bool searchAutomatically;

    private void Start()
    {
        if (searchAutomatically)
        {
            enumeratedAnimations = GetComponents<EnumeratedAnimation>();
        }
    }

    public void Play(int index)
    {
        FindObjectOfType<CameraSettings>().SwitchAutofocus(true);
        FindObjectOfType<TapHandler>().enabled = false;
        enumeratedAnimations[index].Play();
    }

    public void Stop(int index)
    {
        enumeratedAnimations[index].Stop();
        FindObjectOfType<CameraSettings>().SwitchAutofocus(false);
        FindObjectOfType<TapHandler>().enabled = true;
    }

    public void Pause(int index)
    {
        enumeratedAnimations[index].Pause(true);
    }

    public void Resume(int index)
    {
        enumeratedAnimations[index].Pause(false);
    }

    public void Complete(int index)
    {
        enumeratedAnimations[index].complete = true;
    }
}
