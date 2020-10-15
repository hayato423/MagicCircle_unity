using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyExplosion : MonoBehaviour,IMagic
{
    public GameObject EnergyExplosionObj;
    private GameObject MagicCircle;
    private bool isActivating = false;
    private float count;
    private float mcount;
    // Start is called before the first frame update
    void Start()
    {
        MagicCircle = GameObject.Find("MagicCircle");
        count = 0;
        mcount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivating)
        {
            if (count > mcount)
            {
                Instantiate(EnergyExplosionObj, this.transform.position, Quaternion.identity);
                mcount += 0.5f ;
            }
            if (count > 4)
            {
                MagicCircle.GetComponent<MagicCircle>().Invisible();
                count = 0;
                isActivating = false;
            }
            count += Time.deltaTime;            
        }
    }

    public void PlayAnimation(float[] parameter) {
        float H, albedoS, albedoV, emisionS, emisionV;
        H = parameter[0] + parameter[1] + parameter[2];
        while (H > 1.0f)
            H -= 1.0f;
        albedoS = 0.58f;
        albedoV = 0.99f;
        Color color = Color.HSVToRGB(H, albedoS, albedoV);        
        emisionS = 0.60f;
        emisionV = 0.75f;
        Color emColor = Color.HSVToRGB(H + 0.1f, emisionS, emisionV);        

        ParticleSystem.MainModule mainModule = EnergyExplosionObj.GetComponent<ParticleSystem>().main;
        mainModule.startColor = color;
        GameObject embers = EnergyExplosionObj.transform.Find("Embers").gameObject;
        ParticleSystem.MainModule embers_par = embers.GetComponent<ParticleSystem>().main;
        embers_par.startColor = emColor;
        GameObject lightning = EnergyExplosionObj.transform.Find("Lightning").gameObject;
        float factor = Mathf.Pow(2, 2f);
        lightning.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", emColor);

        count = 0;
        mcount = 0;
        isActivating = true;        
    }
}
