using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;      //얘도
    public float startValue;    //json으로 해도됨
    public float maxValue;
    public float passiveValue;  //변동값
    public Image uiBar;

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }


    float GetPercentage()   //fill 바 적용하기위해서
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value , maxValue);      //두 수중 작은값 반환( 현재+value가 maxValue보다 커지면 max를 반환)
    }

    public void Subtract(float value) 
    {
        curValue = Mathf.Max(curValue - value , 0);     //얘는 반대
    }
}
