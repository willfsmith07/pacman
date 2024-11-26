using System;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    private bool inTunnel = false;

    private bool enteredTunnel = false;

    private string wichTunnel;

    private void Update()
    {
        tunnelCheck();
    }

    public void tunnelCheck()
    {
        if (transform.position.x < -15)
        {
            inTunnel = true;
            wichTunnel = "left";
        }
        else if (transform.position.x > 15)
        {
            inTunnel = true;
            wichTunnel = "right";
        }
        else
        {
            inTunnel = false;
        }

        if (inTunnel == true)
        {
            if (wichTunnel == "left")
            {
                transform.position = new Vector3(15, -0.5f, -1);
            }
            else if (wichTunnel == "right")
            {
                transform.position = new Vector3(-15, -0.5f, -1);
            }
        }
    }
}