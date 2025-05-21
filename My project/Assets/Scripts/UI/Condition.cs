using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;      //�굵
    public float startValue;    //json���� �ص���
    public float maxValue;
    public float passiveValue;  //������
    public Image uiBar;

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }


    float GetPercentage()   //fill �� �����ϱ����ؼ�
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value , maxValue);      //�� ���� ������ ��ȯ( ����+value�� maxValue���� Ŀ���� max�� ��ȯ)
    }

    public void Subtract(float value) 
    {
        curValue = Mathf.Max(curValue - value , 0);     //��� �ݴ�
    }
}
