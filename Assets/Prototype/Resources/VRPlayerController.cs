using UnityEngine;

public class VRPlayerController : MonoBehaviour {

    [SerializeField] private float m_MaxWarpDistance = 10f;
    [SerializeField] private float m_TurnSpeed = 25f;
    [SerializeField] private float m_ClampAxis = 80f;
    [SerializeField] private bool m_InverseVerticalAxis;
    [SerializeField] private Transform m_Camera;
    [SerializeField] private LayerMask m_InteractableLayers;
    [SerializeField] private VRLocator m_Locator;

    private float m_CameraXRotation;
    private float m_YOffset;

    private void Start()
    {
        m_YOffset = transform.position.y;
        if (!m_Camera)
        {
            Debug.LogWarning("No camera assigned. Using first loaded camera instead.");
            m_Camera = FindObjectOfType<Camera>().transform;
        }
    }

    private void Update()
    {
        if (GameSystem.playMode != GameSystem.PlayMode.VR)
        {
            return;
        }
        CameraMovement();

        if (!InteractEnvironment())
        {
            WarpToLocation();
        }
    }

    private void CameraMovement()
    {
        //Horizontal
        float turn = Input.GetAxis("Mouse X") * m_TurnSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, turn, Space.World);

        //Vertical
        float vTurn = (m_InverseVerticalAxis ? 1 : -1) * Input.GetAxis("Mouse Y") * m_TurnSpeed * Time.deltaTime;
        m_CameraXRotation += vTurn;
        m_CameraXRotation = Mathf.Clamp(m_CameraXRotation, -m_ClampAxis, m_ClampAxis);

        Quaternion localRotation = Quaternion.Euler(m_CameraXRotation, 0, 0);
        m_Camera.localRotation = localRotation;
        
    }

    private bool InteractEnvironment()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(m_Camera.position, m_Camera.forward, out hit, m_MaxWarpDistance * 0.5f, m_InteractableLayers))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.GetComponent<Door>())
                {
                    Door d = hit.collider.GetComponent<Door>();
                    if (!d.locked)
                    {
                        d.Open();
                        return true;
                    }
                }
                if (hit.collider.GetComponent<Objective>())
                {
                    Objective o = hit.collider.GetComponent<Objective>();
                    o.PickUp();
                    return true;
                }
            }
        }
        return false;
    }

    private void WarpToLocation()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(m_Camera.position, m_Camera.forward, out hit, m_MaxWarpDistance, m_InteractableLayers))
        {
            m_Locator.gameObject.SetActive(true);
            m_Locator.transform.position = hit.point;
            m_Locator.transform.rotation = transform.rotation;
            if (m_Locator.canWarp && Input.GetMouseButtonDown(0))
            {
                transform.position = m_Locator.transform.position + Vector3.up * m_YOffset;
            }
        }
        else
        {
            m_Locator.gameObject.SetActive(false);
        }
    }
}
