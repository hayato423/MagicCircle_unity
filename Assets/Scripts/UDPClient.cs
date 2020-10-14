using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Socket;
using System;
using System.IO;

public class UDPClient : MonoBehaviour
{

    private UDP client;
    private string rcvMsg = "";    
    private GameObject MagicCircle;
    private List<string> msgList;  //受け取ったメッセージを格納するリスト
    private List<string> valList;  //メッセージを整形した値を格納するリスト
    private int size;  //メッセージの受け取る予定の数
    private int count;  //実際に受け取ったメッセージの数
    private MagicCircle mc;
    string lastMsg = "";

    // Start is called before the first frame update
    void Start()
    {
        client = new UDP();
        client.init(5555, 8000, 3000);
        client.start_receive();
        msgList = new List<string>();
        valList = new List<string>();
        MagicCircle = GameObject.Find("MagicCircle");
        mc = MagicCircle.GetComponent<MagicCircle>();
        count = 0;
        size = 100;
    }

    // Update is called once per frame
    void Update()
    {
        rcvMsg = client.rcvMsg;
        if(msgList.Contains(rcvMsg) == false && rcvMsg != lastMsg)
        {
            if(rcvMsg.Substring(0,1) == "s")
            {
                size = int.Parse(rcvMsg.Substring(1, 1));                
            }            
            msgList.Add(rcvMsg);
            lastMsg = rcvMsg;
            count++;
            if(count == size)
            {                
                foreach(string msg in msgList)
                {                    
                    if(msg.Substring(0,1) == "s")
                    {                        
                        valList.Add(msg.Substring(2, msg.Length - 2));
                    }
                    else
                    {
                        valList.Add(msg);
                    }
                }
                valList.Sort();
                string connected = "";
                foreach(string val in valList)
                {                    
                    connected += val.Substring(1, val.Length - 1).Trim();
                }                
                valList.Clear();
                msgList.Clear();
                size = 100;
                count = 0;

                string[] valArr = connected.Split(',');                
                int[] parameter = new int[3] { int.Parse(valArr[0]), int.Parse(valArr[1]), int.Parse(valArr[2]) };
                string base64 = valArr[3];
                mc.Activate(parameter, base64);                
            }
        }        
    }
}
