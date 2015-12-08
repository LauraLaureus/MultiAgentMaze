using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IASensor : MonoBehaviour {

    List<GameObject> friends;
    public float sensorRadius;

    void Start() {
        friends = new List<GameObject>();
        sensorRadius = 4f;
    }

	// Update is called once per frame
	void FixedUpdate () {

        RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, sensorRadius, Vector3.forward);
        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject.tag == "IA") {
                if (friends.Contains(hit.collider.gameObject)) continue;
                else {
                    GameObject friend = hit.collider.gameObject;
                    this.gameObject.GetComponent<QLearningBrain>().mergeMasks(friend.GetComponent<QLearningBrain>().getMask());
                    friends.Add(friend);
                }
            }
        }
	}
}
