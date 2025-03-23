using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class LunarInputController 
{
    public Action keyaction = null;

    public void OnUpdate()
    {
        // �Է� ���� Ű�� �ƹ��͵� ���ٸ� ����
        if (Input.anyKey == false) return;

        // � Ű�� ���Դٸ�, keyaction���� �̺�Ʈ�� �߻������� ����. 
        if (keyaction != null)
        {
            keyaction.Invoke();

        }
    }




}
