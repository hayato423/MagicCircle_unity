using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeFlames : MonoBehaviour, IMagic
{
    public GameObject FlameParticleObj;
    private GameObject MagicCircle;
    private bool IsActivating;
    private float radius;
    private float degree;
    // Start is called before the first frame update
    void Start()
    {        
        IsActivating = false;
        radius = 0.0f;
        degree = 0.0f;
        this.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        MagicCircle = GameObject.Find("MagicCircle");
    }

    // Update is called once per frame
    void Update()
    {
        if(IsActivating == true)
        {            
            float radian = degree * (Mathf.PI / 180.0f);
            Vector3 newPos = new Vector3(radius * Mathf.Cos(radian), 0.0f, radius * Mathf.Sin(radian));
            this.transform.position = newPos;
            if (degree % 10 == 0)
            {
                Instantiate(FlameParticleObj, this.transform.position, Quaternion.identity);
            }
            radius += 0.02f;
            degree += 2.0f;
            if(radius > 15.0f)
            {
                IsActivating = false;
                radius = 0.0f;
                degree = 0.0f;
                this.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                MagicCircle.GetComponent<MagicCircle>().Invisible();
            }
        }
    }

    public void PlayAnimation(float[] parameter)
    {
        float H, albedoS, albedoV,emisionS,emisionV;
        H = parameter[0] + parameter[1] + parameter[2];
        while (H > 1.0f)
            H -= 1.0f;
        albedoS = 0.69f;
        albedoV = 0.99f;
        Color color = Color.HSVToRGB(H, albedoS, albedoV);        
        emisionS = 0.60f;
        emisionV = 0.99f;
        Color emColor = Color.HSVToRGB(H+0.1f, emisionS, emisionV);        
        FlameParticleObj.GetComponent<Renderer>().sharedMaterial.color = color;
        FlameParticleObj.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor",new Color(2.0f*emColor.r,2.0f*emColor.g,2.0f*emColor.b));
        GameObject embers = FlameParticleObj.transform.Find("FireEmbers").gameObject;
        ParticleSystem.MainModule embers_par = embers.GetComponent<ParticleSystem>().main;
        embers_par.startColor = emColor;
        IsActivating = true;
    }
}
