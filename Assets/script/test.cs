using UnityEngine;
using System.Collections.Generic;


public class test : MonoBehaviour {

    

    public GameObject testMouse;

    // Use this for initialization
    void Start() {
        testMouse.SetActive(false);
    }

    bool click = false;
    void Update() {
        if (Input.GetMouseButtonDown(2)) {
            click = true;
            testMouse.SetActive(click);
        }
        if (Input.GetMouseButtonUp(2)) {
            click = false;
            testMouse.SetActive(click);
        }

    }


       
}
