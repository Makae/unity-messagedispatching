using UnityEngine;
using System.Collections;

public class UnityEventRedispatcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        MessageDispatcher md = MessageDispatcher.Instance();
        md.Update();
	}
}
