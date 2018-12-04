using UnityEngine;
using UnityEngine.Events;

public class EnumeratedAnimation : MonoBehaviour {

    public bool isPlaying { get; set; }
    public bool isPaused { get; set; }
    public bool isWaitingForTouch { get; set; }
    public bool isAutoPaused { get; set; }
    public bool isTracking { get; set; }

    public int playsOnProgress;
    public Animation[] animations;

    [SerializeField] float playingTime;

    private int playingAnimation;

	public void Play()
    {
        if (playsOnProgress != StateBehaviour.me.progress)
        {
            return;
        }
        if (!isPlaying)
        {
            isPlaying = true;
        }
    }

    public void Pause(bool value)
    {
        if (!value)
        {
            isAutoPaused = false;
        }
        isPaused = value;
    }

    public void Stop()
    {
        isPlaying = false;
        playingTime = 0;
        foreach (Animation anim in animations)
        {
            anim.target.GetComponent<Renderer>().enabled = false;
        }
    }

    private void Update()
    {
        if (!isTracking)
        {
            return;
        }
        if (isWaitingForTouch && isAutoPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animations[playingAnimation].skipped = true;
                isWaitingForTouch = false;
                isPaused = false;
                isAutoPaused = false;
            }
            return;
        }
        if (!isPlaying || isPaused || isAutoPaused)
        {
            return;
        }
        playingTime += Time.deltaTime;
        for (int i = 0; i < animations.Length; i++)
        {
            if (playingTime >= animations[i].playTime)
            {
                playingAnimation = i;
                animations[i].Play(this);
            }
        }
    }

    [System.Serializable]
    public class Animation
    {
        public enum AnimationMode { Popup_Default, Appear, Popdown_Default, Disappear}

        public float playTime;
        public GameObject target;
        public AnimationMode animationMode;
        public UnityEvent events;
        public bool autoPause;

        public bool skipped { get; set; }

        public void Play(EnumeratedAnimation root)
        {
            events.Invoke();
            switch (animationMode)
            {
                case AnimationMode.Popup_Default:
                    //Target object expands and tilts upwards. For future scripting.
                    goto case AnimationMode.Appear;
                case AnimationMode.Appear:
                    //Target object appears.
                    target.GetComponent<Renderer>().enabled = true;
                    break;
                case AnimationMode.Popdown_Default:
                    //Target object shrinks and tilts downwards. For future scripting.
                    goto case AnimationMode.Disappear;
                case AnimationMode.Disappear:
                    //Target object disappears.
                    target.GetComponent<Renderer>().enabled = false;
                    break;
                default:
                    //Appear
                    goto case AnimationMode.Appear;
            }
            if (autoPause && !skipped)
            {
                root.isWaitingForTouch = true;
                root.isAutoPaused = true;
                root.Pause(true);
            }
        }
    }
}
