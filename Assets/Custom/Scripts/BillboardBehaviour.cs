using UnityEngine;

public class BillboardBehaviour : MonoBehaviour {

    private Transform m_Camera;

    private void Start()
    {
        m_Camera = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(m_Camera);
    }
}
