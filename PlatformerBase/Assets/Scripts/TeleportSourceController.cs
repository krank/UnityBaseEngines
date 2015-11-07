using UnityEngine;
using System.Collections;

public class TeleportSourceController : MonoBehaviour {

    public GameObject teleportTarget;

	// Use this for initialization
	void Start () {
        if (teleportTarget == null)
        {
            Debug.Log("Warning! Teleport source lacks target!");
        }

	}

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.transform.position = teleportTarget.transform.position;
        }
    }
}
