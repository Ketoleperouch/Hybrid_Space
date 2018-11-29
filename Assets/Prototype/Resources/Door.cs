using UnityEngine;

public class Door : MonoBehaviour {

    public bool locked;

    [SerializeField] private Light[] m_StateLights;
    [SerializeField] private Color m_UnlockedStateColor = Color.green;
    [SerializeField] private Color m_LockedStateColor = Color.red;
    [SerializeField] private MeshRenderer m_UIElement;
    [SerializeField] private Texture2D m_LockedTexture;
    private Texture2D m_OriginalTexture;

    private void Start()
    {
        m_OriginalTexture = m_UIElement.material.GetTexture("_MainTex") as Texture2D;
    }

    private void Update()
    {
        foreach (Light lamp in m_StateLights)
        {
            lamp.color = Color.Lerp(lamp.color, locked ? m_LockedStateColor : m_UnlockedStateColor, Time.deltaTime * 3);
        }
        m_UIElement.material.SetTexture("_MainTex", locked ? m_LockedTexture : m_OriginalTexture);
    }

    public void Open()
    {
        //Preview function
        Destroy(gameObject);
    }
}
