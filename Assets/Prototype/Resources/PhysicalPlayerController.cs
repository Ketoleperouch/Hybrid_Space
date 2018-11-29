using System.Collections.Generic;
using UnityEngine;

public class PhysicalPlayerController : MonoBehaviour {

    [SerializeField] private int m_MaxAllowedLockedDoors;
    [SerializeField] private LayerMask m_InteractableLayer;
    [SerializeField] private List<Door> m_LockedDoors = new List<Door>();

    private Camera m_Camera;
    private int m_AmountDoorsLocked;

    private void Start()
    {
        m_Camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (GameSystem.playMode != GameSystem.PlayMode.Physical)
        {
            return;
        }
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * m_Camera.farClipPlane);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, m_Camera.farClipPlane, m_InteractableLayer))
        {
            if (hit.collider.CompareTag("Door") && Input.GetMouseButtonDown(0))
            {
                //Lock the door and add to the list of locked doors
                Door d = hit.collider.GetComponentInParent<Door>();
                if (d.locked)
                {
                    d.locked = false;
                    m_LockedDoors.Remove(d);
                    m_LockedDoors.TrimExcess();
                    return;
                }
                d.locked = true;
                m_LockedDoors.Add(d);
                //Check list if the amount of locked doors is within maximum range
                int currentlyLockedDoors = 0;
                for (int i = 0; i < m_LockedDoors.Count; i++)
                {
                    if (m_LockedDoors[i].locked)
                    {
                        currentlyLockedDoors++;
                    }
                }
                //If not, unlock doors until the maximum allowed amount is reached
                while (currentlyLockedDoors > m_MaxAllowedLockedDoors)
                {
                    Debug.Log("Maximum amount of locked doors reached.", m_LockedDoors[0]);
                    m_LockedDoors[0].locked = false;
                    m_LockedDoors.RemoveAt(0);
                    m_LockedDoors.TrimExcess();
                    currentlyLockedDoors--;
                }
            }
        }
    }
}
