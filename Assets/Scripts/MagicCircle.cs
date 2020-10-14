using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    private Texture2D texture;
    private bool moving;
    private float angle;
    private float scale;
    private float rotateAngle = 360.0f;
    private float addNum = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        texture = new Texture2D(1, 1);
        angle = 0;
        scale = 0.0f;
        transform.localScale = new Vector3(0, 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            //transform.Rotate(new Vector3(0, addNum, 0));
            transform.localScale = new Vector3(scale, 1, scale);
            scale += addNum / rotateAngle;
            angle+= addNum;
            if(angle > rotateAngle)
            {
                moving = false;
            }
        }
        
    }

    public void Activate(int[] parameter,string base64)
    {
        angle = 0;
        scale = 0.0f;
        transform.localScale = new Vector3(0, 1, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        ChangeTexture(base64);
        moving = true;
    }

    public void ChangeTexture(string data)
    {
        byte[] bytes = System.Convert.FromBase64String(data);
        texture.LoadImage(bytes);
        GetComponent<Renderer>().material.mainTexture = texture;
    }
}
