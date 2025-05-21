using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Altitude : MonoBehaviour
{
    public TextMeshProUGUI altitudeText;
    private Transform playerTransform;
    private void Start()
    {
        altitudeText = transform.Find("Altitude").GetComponent<TextMeshProUGUI>();
        playerTransform = CharacterManager.Instance.Player.transform;
    }

    void Update()
    {
        altitudeText.text = ToInt(playerTransform.transform.position.y).ToString();
    }
    int ToInt(float value)
    {
        //���� �ݿø��� �Ϸ������� ������ ���ٰ� �Ǵ�
        return (int)value;
    }
}
