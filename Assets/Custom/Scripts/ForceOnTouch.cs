using UnityEngine;

public class ForceOnTouch : MonoBehaviour {

    public float force = 1f;

    private void Update()
    {
        Physics.gravity = transform.root.InverseTransformDirection(new Vector3(0, -9.81f, 0));
    }

    public void OnTouchDown()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, force), ForceMode.Impulse);
    }
}
