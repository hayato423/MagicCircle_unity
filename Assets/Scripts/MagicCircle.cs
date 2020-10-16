using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    private Texture2D texture;
    private bool moving;
    private float angle;
    private float scale;
    private float rotateAngle = 360.0f;
    private float addNum = 2.0f;
    public GameObject LargeFlameMagic;
    public GameObject EnergyExplosion;
    public GameObject IceSpear;
    private float[] NormalizedParameter;
    private bool end;
    // Start is called before the first frame update
    void Start()
    {
        texture = new Texture2D(1, 1);
        angle = 0;
        scale = 0.0f;
        end = false;
        transform.localScale = new Vector3(0, 1, 0);
        NormalizedParameter = new float[3];
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            //transform.Rotate(new Vector3(0, addNum, 0));
            transform.localScale = new Vector3(scale, 1, scale);
            scale += addNum / rotateAngle;
            angle += addNum;
            if(angle > rotateAngle)
            {
                moving = false;
                int index = 0;
                float max = NormalizedParameter.Max();
                for(int i = 0; i < 3; i++)
                {
                    if (NormalizedParameter[i] == max)
                    {
                        index = i;
                        break;
                    }
                }                
                switch (index)
                {
                    case 0:
                        IceSpear.GetComponent<IceSpear>().PlayAnimation(NormalizedParameter);
                        break;
                    case 1:
                        LargeFlameMagic.GetComponent<LargeFlames>().PlayAnimation(NormalizedParameter);
                        break;
                    case 2:
                        EnergyExplosion.GetComponent<EnergyExplosion>().PlayAnimation(NormalizedParameter);
                        break;
                    default:
                        break;
                }                                                
            }
        }
        if (end)
        {
            transform.localScale = new Vector3(scale, 1, scale);
            scale -= 0.02f;
            if(scale <= 0.0f)
            {
                end = false;
            }
        }
        
    }

    public void Activate(int[] parameter,string base64)
    {
        angle = 0;
        scale = 0.0f;
        transform.localScale = new Vector3(0, 1, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        NormalizedParameter = NormalizeParam(parameter);
        ChangeTexture(base64);
        moving = true;
    }

    public void ChangeTexture(string data)
    {
        byte[] bytes = System.Convert.FromBase64String(data);
        texture.LoadImage(bytes);
        GetComponent<Renderer>().material.mainTexture = texture;
    }

    private float[] NormalizeParam(int[] parameter)
    {
        float[] nparam = new float[3];
        float maxVal = (float)parameter.Max();
        for(int i = 0; i < parameter.Count(); ++i)
        {
            nparam[i] = (float)parameter[i] / maxVal;
        }
        return nparam;
    }

    public void Invisible()
    {
        end = true;
    }
}
