using UnityEngine;
using System.Collections;
public class KeyboardInput : MonoBehaviour {

    public bool up, down, left, right = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        KeyInputCheck();
	}

    void KeyInputCheck()
    {
        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
    }
}
