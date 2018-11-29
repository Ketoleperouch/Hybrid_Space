using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour {

    public Text timerText;
    public float timer;

    [SerializeField] private GameObject[] m_Cameras;
    public enum PlayMode { VR, Physical }

    public static PlayMode playMode;

    private bool countingTimer = true;

    private void Awake()
    {
        m_Cameras[0].SetActive(true);
        m_Cameras[1].SetActive(false);
        playMode = PlayMode.VR;
    }

    private void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_Cameras[0].SetActive(true);
            m_Cameras[1].SetActive(false);
            playMode = PlayMode.VR;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_Cameras[1].SetActive(true);
            m_Cameras[0].SetActive(false);
            playMode = PlayMode.Physical;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }

        SetCursor();

        CountTimer();
    }

    private void SetCursor()
    {
        if (playMode == PlayMode.Physical)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        Cursor.visible = playMode == PlayMode.Physical;
    }

    private void CountTimer()
    {
        if (countingTimer)
        {
            timer -= Time.deltaTime;
            string secs = "";
            float adjustableTimer = timer;
            int minutes = 0;
            while (adjustableTimer - 60 > 0)
            {
                adjustableTimer -= 60;
                minutes++;
            }
            secs = Mathf.Round(adjustableTimer).ToString();
            if (adjustableTimer <= 9.4999f)
            {
                secs = secs.Insert(0, "0");
            }
            timerText.text = "0" + minutes.ToString() + ":" + secs;
        }
    }
}
