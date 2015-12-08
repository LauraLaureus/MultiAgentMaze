using UnityEngine;
using System.Collections;

public class QLearningBrain : MonoBehaviour {

    IASensor friendSensor;
    NavigationSight sightSensor;

    QLearningMask mask;


    // Use this for initialization
	void Start () {
	    //TODO create mask and access the only instance of QLearningMatrix
        friendSensor = gameObject.GetComponent<IASensor>();
        sightSensor = gameObject.GetComponent<NavigationSight>();
        mask = new QLearningMask();
	}
	
	void FixedUpdate () {
	    
	}

    public void mergeMasks(QLearningMask othermask){
        mask.merge(othermask);
    }

    public QLearningMask getMask() {
        return this.mask;
    }
}
