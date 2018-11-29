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
        enumeratedAnimations[index].Play();
    }

    public void Stop(int index)
    {
        enumeratedAnimations[index].Stop();
    }

    public void Pause(int index)
    {
        enumeratedAnimations[index].Pause(true);
    }

    public void Resume(int index)
    {
        enumeratedAnimations[index].Pause(false);
    }
}
