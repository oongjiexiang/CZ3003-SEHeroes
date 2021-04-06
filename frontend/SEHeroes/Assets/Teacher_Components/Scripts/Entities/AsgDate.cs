using System.Collections;
using System.Collections.Generic;
using System;
using SimpleJSON;
using System.Linq;
using UnityEngine;

[Serializable]
public class AsgDate{
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;
    public int second;
    public AsgDate(JSONNode time){
        year = time["year"];
        month = time["month"];
        day = time["day"];
        hour = time["hour"];
        minute = time["minute"];
        second = time["second"];
    }
    public AsgDate(DateTime time){
        year = time.Year;
        month = time.Month + 1;
        day = time.Day;
        hour = time.Hour;
        minute = time.Minute;
        second = time.Second;
        Debug.Log(month);
        Debug.Log(day);
    }
    public AsgDate(){
        year = 2022;
        month = 3;
        day = 30;
        hour = 10;
        minute = 22;
        second = 00;
    }
    public string printTime(){
        return String.Format("{0}/{1}/{2} {3:d2}:{4:d2}", year, month, day, hour, minute);
    }
    public string printOnlyDate(){
        return String.Format("{0}/{1}/{2}", year, month, day);
    }
    public string printOnlyTime(){
        return String.Format("{0:d2}:{1:d2}", hour, minute);
    }
    public DateTime time(){
        // Debug.Log(year);
        // Debug.Log(month);
        // Debug.Log(day);
        // Debug.Log(hour);
        // Debug.Log(minute);
        return DateTime.Parse(String.Format("{0}/{1}/{2} {3}:{4}:00", year, month, day, hour, minute));
    }
}