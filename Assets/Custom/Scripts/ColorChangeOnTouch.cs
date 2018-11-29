using UnityEngine;

public class ColorChangeOnTouch : MonoBehaviour {

    private MeshRenderer m_Renderer;

    private void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

    private void Update ()
    {
        if (Input.GetTouch(0).tapCount > 0)
        {
            m_Renderer.material.color = Random.ColorHSV();
        }
	}
}
