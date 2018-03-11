using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;

public class SocketIO : MonoBehaviour {

    public string serverURL = "http://143.215.108.220:5000";

    public float kinectPointScaleFactor;

    public GameObject head;
    public GameObject neck;
    public GameObject leftShoulder;
    public GameObject rightShoulder;
    public GameObject leftElbow;
    public GameObject rightElbow;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftKnee;
    public GameObject rightKnee;
    public GameObject leftFoot;
    public GameObject rightFoot;
    public GameObject hip;

    private Socket socket;
    private string kinectData;
    private int tt = 0;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("starting up :D");
        SocketOpen();
	}

    void OnDestroy()
    {
        Debug.Log("destruction >:)");
        SocketClose();
    }

    void OnApplicationQuit()
    {
        OnDestroy();
    }

    void SocketOpen ()
    {
        if (socket == null)
        {
            Debug.Log("hello");
            socket = IO.Socket(serverURL);
            socket.On("kinect-broadcast", (data) =>
            {
                Debug.Log("got kinect data " + (++tt).ToString());
                kinectData = data.ToString();
            });
            socket.On(Socket.EVENT_DISCONNECT, () =>
            {
                Debug.Log("got disconnected :(");
                kinectData = null;
            });
        }
    }

    void SocketClose ()
    {
        if (socket != null)
        {
            Debug.Log("bye bye");
            socket.Disconnect();
            socket = null;
        }
    }

	// Update is called once per frame
	void Update () {
        if (kinectData == null)
        {
            return;
        }
        string[] points = kinectData.Split(';');
        for (int i = 0; i < 13; i++)
        {
            string[] components = points[i].Split(',');
            float x = kinectPointScaleFactor * float.Parse(components[0]);
            float y = kinectPointScaleFactor * float.Parse(components[1]);
            float z;

            switch (i)
            {
                case 0:
                    z = head.transform.position.z;
                    head.transform.position = new Vector3(x, y, z);
                    break;
                case 1:
                    z = neck.transform.position.z;
                    neck.transform.position = new Vector3(x, y, z);
                    break;
                case 2:
                    z = leftShoulder.transform.position.z;
                    leftShoulder.transform.position = new Vector3(x, y, z);
                    break;
                case 3:
                    z = rightShoulder.transform.position.z;
                    rightShoulder.transform.position = new Vector3(x, y, z);
                    break;
                case 4:
                    z = leftElbow.transform.position.z;
                    leftElbow.transform.position = new Vector3(x, y, z);
                    break;
                case 5:
                    z = rightElbow.transform.position.z;
                    rightElbow.transform.position = new Vector3(x, y, z);
                    break;
                case 6:
                    z = leftHand.transform.position.z;
                    leftHand.transform.position = new Vector3(x, y, z);
                    break;
                case 7:
                    z = rightHand.transform.position.z;
                    rightHand.transform.position = new Vector3(x, y, z);
                    break;
                case 8:
                    z = leftKnee.transform.position.z;
                    leftKnee.transform.position = new Vector3(x, y, z);
                    break;
                case 9:
                    z = rightKnee.transform.position.z;
                    rightKnee.transform.position = new Vector3(x, y, z);
                    break;
                case 10:
                    z = leftFoot.transform.position.z;
                    leftFoot.transform.position = new Vector3(x, y, z);
                    break;
                case 11:
                    z = rightFoot.transform.position.z;
                    rightFoot.transform.position = new Vector3(x, y, z);
                    break;
                case 12:
                    z = hip.transform.position.z;
                    hip.transform.position = new Vector3(x, y, z);
                    break;
            }
        }
        kinectData = null;
    }
}
