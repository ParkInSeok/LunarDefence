using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonSelectUI : MonoBehaviour
{
    public RectTransform rectTransform;




    void Start()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }





}
