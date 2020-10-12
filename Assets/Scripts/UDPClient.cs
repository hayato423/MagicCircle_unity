using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Socket;
using System;

public class UDPClient : MonoBehaviour
{

    private UDP client;
    private string rcvMsg = "ini";
    private string pRcvMsg = "ini";
    private GameObject MagicCircle;

    // Start is called before the first frame update
    void Start()
    {
        client = new UDP();
        client.init(5555, 8000, 3000);
        client.start_receive();
        MagicCircle = GameObject.Find("MagicCircle");
    }

    // Update is called once per frame
    void Update()
    {
        rcvMsg = client.rcvMsg;        
        if(rcvMsg != pRcvMsg)
        {            
            string[] msgArr = rcvMsg.Split(',');
            string base64 = msgArr[3];
            int[] parametar = new int[3] { int.Parse(msgArr[0]), int.Parse(msgArr[1]), int.Parse(msgArr[2]) };
            MagicCircle mc = MagicCircle.GetComponent<MagicCircle>();
            mc.ChangeTexture(base64);
            pRcvMsg = rcvMsg;
        }
    }
}
