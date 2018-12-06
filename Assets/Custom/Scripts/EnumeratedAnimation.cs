using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class EnumeratedAnimation : MonoBehaviour {

    public bool isPlaying { get; set; }
    public bool isPaused { get; set; }
    public bool isWaitingForTouch { get; set; }
    public bool isAutoPaused { get; set; }
    public bool isTracking { get; set; }
    public bool complete { get; set; }

    public int playsOnProgress;
    public Animation[] animations;

    [SerializeField] float playingTime;

    private int playingAnimation;

	public void Play()
    {
        if (playsOnProgress != StateBehaviour.me.progress || complete)
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
        if (!isTracking || complete)
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
        public enum AnimationMode { None, Popup_Default, Appear, Popdown_Default, Disappear, PlayAnimation}

        public float playTime;
        public GameObject target;
        public AnimationMode animationMode;
        public string animationName;
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
                case AnimationMode.PlayAnimation:
                    Animator animator = target.GetComponent<Animator>();
                    if (!animator.enabled)
                    {
                        animator.enabled = true;
                    }
                    target.GetComponent<Animator>().Play(animationName, 0);
                    break;
                case AnimationMode.None:
                    //For example when only events should run.
                    break;
                default:
                    //Appear
                    goto case AnimationMode.None;
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
