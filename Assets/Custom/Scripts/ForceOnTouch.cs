using UnityEngine;

public class ForceOnTouch : MonoBehaviour {

    public float force = 1f;

    private void Update()
    {
        Physics.gravity = transform.root.InverseTransformDirection(new Vector3(0, -9.81f, 0));
    }

    public void OnTouchDown()
    {
        Debug.Log("Invoked OnTouchDown().");
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * force, ForceMode.Impulse);
    }
}
