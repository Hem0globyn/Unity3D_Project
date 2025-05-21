using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition playercondition;

    public Item_Data itemData;
    public Action addItem;  //�κ��� ������

    public Transform dropItem;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        playercondition = GetComponent<PlayerCondition>();
    }
}
