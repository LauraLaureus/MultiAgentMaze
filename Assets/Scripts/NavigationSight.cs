using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavigationSight : MonoBehaviour {

    List<Vector3> rays;
    Rigidbody rb;
    QLearningMask mask;
    public float sightRadius;
    public float angularSpeed;
    public float speed;

    public Vector3 selectedDestination;

	// Use this for initialization
	void Start () {
        rays = new List<Vector3>();
        sightRadius = 3f;
        angularSpeed = 10f;
        speed = 5f;
        rb = this.gameObject.GetComponent<Rigidbody>();
        mask = this.gameObject.GetComponent<QLearningBrain>().getMask();
	}
	
	
	void FixedUpdate () {
        calculateRays();
        updateQLearningProcedure();
        discartShortRays();
        Vector3 resultRay = sumRays();
        resultRay = scaleRayTofitSightRadius(resultRay);
        resultRay.y = 0;

        selectedDestination = resultRay;
        //move(resultRay);
        //resetRays();
	}

    private void updateQLearningProcedure()
    {
        foreach (Vector3 ray in rays) {
            enableAllPointsTo(ray);
        }
    }

    private void enableAllPointsTo(Vector3 ray)
    {
        Vector3 intRay = floatRayToIntegerRay(ray);

    }

    private Vector3 floatRayToIntegerRay(Vector3 ray)
    {
        return new Vector3((int)ray.x, (int)ray.y, (int)ray.z);
    }

    public void resetRays()
    {
        rays.Clear();
    }

    private void move(Vector3 resultRay)
    {
        transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(resultRay, Vector3.up),
                angularSpeed * Time.deltaTime
        );
        rb.velocity = transform.forward * speed;
    }

    private Vector3 scaleRayTofitSightRadius(Vector3 resultRay)
    {
        if (resultRay.magnitude > sightRadius ) {
            float factor = sightRadius / resultRay.magnitude;
            resultRay *= factor;
        }
        return resultRay;
    }

    private Vector3 sumRays()
    {
        Vector3 result = new Vector3();
        foreach (Vector3 ray in rays) {
            result += ray;
        }
        return result;
    }

    private void discartShortRays()
    {
        for (int r = rays.Count - 1; r > 0; r-- )
        {
            if (rays[r].sqrMagnitude < sightRadius * sightRadius)
            {
                rays[r] = Vector3.zero;
            }
        }
    }

    private void calculateRays()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        rays.Add(hit.point - transform.position);

        Physics.Raycast(transform.position, transform.right, out hit);
        rays.Add(hit.point - transform.position);

        Physics.Raycast(transform.position, -transform.right, out hit);
        rays.Add(hit.point - transform.position);
    }
}
