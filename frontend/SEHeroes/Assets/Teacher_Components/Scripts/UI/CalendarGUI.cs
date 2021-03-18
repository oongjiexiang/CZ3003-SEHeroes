using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarGUI : MonoBehaviour
{
    private string[] Months;


    void Start()
    {
        //fill an array with the names of the months
        Months = new string[12];
        System.DateTime iMonth = new System.DateTime(2009, 1, 1);
        for (int i = 0; i < 12; ++i) 
        {
            iMonth = new System.DateTime(2009, i + 1, 1);
            Months[i] = iMonth.ToString("MMMM");
        }
    }
    void OnGUI()
    {
        //show the months in a grid and select the current month
        GUILayout.SelectionGrid(System.DateTime.Now.Month - 1, Months, 3);
    }

}
