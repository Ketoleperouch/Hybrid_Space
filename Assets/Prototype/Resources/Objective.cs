using UnityEngine;

public class Objective : MonoBehaviour {

	public void PickUp()
    {
        Debug.Log("VR Player wins!");
        Destroy(gameObject);
    }
}
