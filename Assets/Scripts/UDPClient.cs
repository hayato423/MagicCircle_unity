using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Socket;

public class UDPClient : MonoBehaviour
{

    private UDP client;
    private string rcvMsg = "";
    private string pRcvMsg = "";

    // Start is called before the first frame update
    void Start()
    {
        client = new UDP();
        client.init(5555, 8000, 3000);
        client.start_receive();
    }

    // Update is called once per frame
    void Update()
    {
        rcvMsg = client.rcvMsg;        
        if(rcvMsg != pRcvMsg)
        {
            Debug.Log(rcvMsg);
            pRcvMsg = rcvMsg;
        }
    }
}
