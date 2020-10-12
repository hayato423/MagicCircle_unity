using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    private Texture2D texture;
    // Start is called before the first frame update
    void Start()
    {
        texture = new Texture2D(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTexture(string data)
    {
        byte[] bytes = System.Convert.FromBase64String(data);
        texture.LoadImage(bytes);
        GetComponent<Renderer>().material.mainTexture = texture;
    }
}
