using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeFlames : MonoBehaviour, IMagic
{
    public GameObject FlameParticleObj;
    private bool IsActivating;
    private float radius;
    private float theta;
    // Start is called before the first frame update
    void Start()
    {        
        IsActivating = false;
        radius = 0.0f;
        theta = 0.0f;
        this.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsActivating == true)
        {
            Instantiate(FlameParticleObj, this.transform.position, Quaternion.identity);
            radius += 0.1f;
            if(radius > 0.5f)
            {
                IsActivating = false;
                radius = 0.0f;
                theta = 0.0f;
                this.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
    }

    public void PlayAnimation(float[] parameter)
    {
        IsActivating = true;
    }
}
