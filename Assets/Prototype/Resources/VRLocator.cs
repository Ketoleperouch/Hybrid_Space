using UnityEngine;

public class VRLocator : MonoBehaviour {

    public bool canWarp { get; private set; }

    [SerializeField] private Color m_ValidColor = new Color(0, 2.3f, 4.0f);
    [SerializeField] private Color m_InvalidColor = new Color(3.8f, 0.15f, 0);
    [SerializeField] private Vector3 m_ZoneBounds;
    [SerializeField] private Vector3 m_ZoneBoundsOffset;
    [SerializeField] private LayerMask m_ZoneCollision;

    private MeshRenderer[] m_LocatorStrips;

    private void Start()
    {
        m_LocatorStrips = GetComponentsInChildren<MeshRenderer>();
    }

    private void Update()
    {
        Collider[] boxHits = Physics.OverlapBox(transform.position + m_ZoneBoundsOffset, m_ZoneBounds / 2, transform.rotation, m_ZoneCollision);

        canWarp = boxHits.Length <= 0;

        foreach (MeshRenderer strip in m_LocatorStrips)
        {
            strip.materials[0].SetColor("_EmissionColor", canWarp ? m_ValidColor : m_InvalidColor);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Matrix4x4 cubeTransform = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

        Gizmos.matrix *= cubeTransform;

        Gizmos.color = canWarp ? m_ValidColor : m_InvalidColor;
        Gizmos.DrawWireCube(m_ZoneBoundsOffset, m_ZoneBounds);

        Gizmos.matrix = oldGizmosMatrix;
    }
}
