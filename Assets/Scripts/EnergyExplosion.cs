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
        Color color = new Color(parameter[0], parameter[1], parameter[2]);
        ParticleSystem.MainModule mainModule = EnergyExplosionObj.GetComponent<ParticleSystem>().main;
        GameObject embers = EnergyExplosionObj.transform.Find("Embers").gameObject;
        ParticleSystem.MainModule embers_par = embers.GetComponent<ParticleSystem>().main;
        embers_par.startColor = color;
        mainModule.startColor = color;
        GameObject lightning = EnergyExplosionObj.transform.Find("Lightning").gameObject;
        lightning.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", color);
        count = 0;
        mcount = 0;
        isActivating = true;        
    }
}
