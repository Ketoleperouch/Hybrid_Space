using UnityEngine;

public class TouchBehaviour : MonoBehaviour {

    private Camera m_Camera;
    private GameObject m_HitObject;

    private void Start()
    {
        m_Camera = GetComponent<Camera>();
    }

    void Update ()
    {
        var hit = new RaycastHit();
        for (int i = 0; i < Input.touchCount; i++)
        {
            Ray ray = m_Camera.ScreenPointToRay(Input.GetTouch(i).position);
            if (Physics.Raycast(ray, out hit))
            {
                var hitObject = hit.transform.gameObject;
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    m_HitObject = hitObject;
                    hitObject.SendMessage("OnTouchDown");
                }
                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    if (m_HitObject == hitObject)
                    {
                        hitObject.SendMessage("OnTouchUpAsButton");
                    }
                    hitObject.SendMessage("OnTouchUp");
                    m_HitObject = null;
                }
            }
        }
	}
}
