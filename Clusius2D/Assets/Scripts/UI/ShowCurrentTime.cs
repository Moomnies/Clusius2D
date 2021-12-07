using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShowCurrentTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    void Update()
    {
        DateTime dt = DateTime.Now;
        timer.text = dt.ToString("HH:mm");
    }
}
