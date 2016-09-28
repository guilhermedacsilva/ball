using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = new Vector3(0, 6, -8);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = offset + player.transform.position;
	}
}
