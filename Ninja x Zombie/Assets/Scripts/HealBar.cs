using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealBar : MonoBehaviour
{
    [SerializeField]
    private float fillAmount;
    [SerializeField]
    private Image Heal;
    [SerializeField]
    private Text valueText;
    public float MaxValue { get; set; }
    public float Value
    {
        set
        {
            string[] tmp = valueText.text.Split(':');
            valueText.text = tmp[0] + ":" + value;
            fillAmount = Map(value,0,MaxValue,0,1);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();   
    }
    private void HandleBar()
    {
        if (fillAmount != Heal.fillAmount)
        Heal.fillAmount = fillAmount;
    }
    private float Map(float value, float inMin,float inMax, float outMin,float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
