using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PropertyState
{
    none,
    fire,
    water,

}



[Serializable]
public class StatBase
{
    public string uniqueKey;                //����ũ Ű
    public float atk;                       //���ݷ�
    public int hp;                        //ü��
    public float sp;                        //����
    public float def;                       //����
    public float spdef;                     //��������
    public float attackSpeed;               //����
    public float propertyReinforcePower;    //�Ӽ���ȭ
    public float propertyResistPower;       //�Ӽ�����
    protected int propertyState;               //�Ӽ�����

    public PropertyState PropertyState { get { 
            if(propertyState >= 0)
            {
                return (PropertyState)propertyState;
            }
            else
            {
                return PropertyState.none;
            }
        } 
    }  //�Ӽ�����




}
