using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Text statusText;
    public GameObject skeleton;
    public GameObject obstacleSet;
    public float accelerationSpeed;
    public float initialVelocity;
    public float levelUpVelocityIncrement;
    private Rigidbody rb;
    private float targetVelocity = 0.0f;
    private int nextObstacleIndex;

	// Use this for initialization
	void Start () {
        statusText.text = "Ready?";
        ShowStatus();
        rb = GetComponent<Rigidbody>();
        nextObstacleIndex = 0;
        Invoke("StartMotion", 1);
	}
	
	// FixedUpdate is called once per physics frame
	void FixedUpdate () {
        int accelerationDirection = System.Math.Sign(targetVelocity - rb.velocity.magnitude);
        rb.AddForce(new Vector3(0.0f, 0.0f, accelerationDirection * accelerationSpeed));

        // test for collision
        /*RaycastHit hit;
        bool collisionIncoming = false;
        for (int i = 0; i < skeleton.transform.childCount && !collisionIncoming; i++)
        {
            LineRenderer lineRenderer = skeleton.transform.GetChild(i).GetComponent<LineRenderer>();
            Vector3[] positions = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(positions);
            Debug.Log(positions[0]);
            //float z = GetNextObstacle().transform.position.z;
            if (positions.Length > 0)
            {
               // positions[0].z = z;
            }
            for (int j = 0; j < positions.Length - 1; j++)
            {
                //positions[j + 1].z = z;
                //Debug.Log(positions[j].ToString() + positions[j + 1].ToString());
                if (Physics.Raycast(positions[j], positions[j + 1] - positions[j], out hit))
                {
                    //Debug.Log(hit.collider.gameObject);
                        collisionIncoming = true;
                        break;
                }
            }
        }
        if (collisionIncoming)
        {
            statusText.text = "FAIL";
            ShowStatus();
        }*/
	}

    private void OnCollisionEnter(Collision collision)
    {
        collision.collider.enabled = false;
    }

    void StartMotion ()
    {
        statusText.text = "GO!";
        Invoke("HideStatus", 1);
        targetVelocity = initialVelocity;
    }

    void ShowStatus()
    {
        statusText.gameObject.SetActive(true);
    }

    void HideStatus ()
    {
        statusText.gameObject.SetActive(false);
    }

    void LevelUp ()
    {
        statusText.text = "Level up!";
        ShowStatus();
        Invoke("HideStatus", 1);
        targetVelocity += levelUpVelocityIncrement;
    }

    GameObject GetNextObstacle ()
    {
        GameObject nextObstacle = obstacleSet.transform.GetChild(nextObstacleIndex).gameObject;
        return nextObstacle;
    }
}
