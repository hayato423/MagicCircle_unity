using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpear : MonoBehaviour, IMagic
{
    public GameObject IceSpearObj;
    private GameObject MagicCircle;
    private bool IsActivating;
    private float radius;
    private float degree;
    private float height;
    // Start is called before the first frame update
    void Start()
    {
        IsActivating = false;
        radius = 10.0f;
        degree = 0.0f;
        height = 0.0f;
        this.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        MagicCircle = GameObject.Find("MagicCircle");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivating == true)
        {
            float radian = degree * (Mathf.PI / 180.0f);
            Vector3 newPos = new Vector3(radius * Mathf.Cos(radian), height, radius * Mathf.Sin(radian));
            this.transform.position = newPos;
            if (degree % 10 == 0)
            {
                Instantiate(IceSpearObj, this.transform.position, Quaternion.identity);
            }
            height += 0.04f;
            degree += 10.0f;
            radius -= 0.05f;
            if (height > 8.0f)
            {
                IsActivating = false;
                radius = 10.0f;
                degree = 0.0f;
                height = 0.0f;
                this.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                MagicCircle.GetComponent<MagicCircle>().Invisible();
            }
        }
    }
    
    public void PlayAnimation(float[] parameter)
    {
        float H, albedoS, albedoV, emisionS, emisionV;
        H = parameter[0] + parameter[1] + parameter[2];
        while (H > 1.0f)
            H -= 1.0f;
        albedoS = 0.52f;
        albedoV = 0.99f;
        Color color = Color.HSVToRGB(H, albedoS, albedoV);        
        emisionS = 0.83f;
        emisionV = 0.75f;
        Color emColor = Color.HSVToRGB(H + 0.1f, emisionS, emisionV);        

        IceSpearObj.GetComponent<Renderer>().sharedMaterial.color = emColor;        
        IceSpearObj.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor",new Color(4.0f*emColor.r,4.0f*emColor.g,4.0f*emColor.b));
        GameObject chill = IceSpearObj.transform.Find("Chill").gameObject;
        ParticleSystem.MainModule chillPar = chill.GetComponent<ParticleSystem>().main;
        chillPar.startColor = color;
        IsActivating = true;
    }
}
