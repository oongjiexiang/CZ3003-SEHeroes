using System.Collections;
using System.Collections.Generic;
using System;
using SimpleJSON;
using System.Linq;
using UnityEngine;

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
    public string printTime(){
        return String.Format("{0}/{1}/{2} {3:d2}:{4:d2}", year, month, day, hour, minute);
    }
    public DateTime time(){
        return DateTime.Parse(String.Format("{0}/{1}/{2} {3}:{4}:00", year, month, day, hour, minute));
    }
}