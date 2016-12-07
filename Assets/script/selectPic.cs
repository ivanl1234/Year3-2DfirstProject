using UnityEngine;
using System.Collections;

public class selectPic : MonoBehaviour {

    public GameObject go;
    Vector3 ts;
	
	// Update is called once per frame
	void Update () {
        ts = go.transform.position;
        transform.position = ts;
	}
}
