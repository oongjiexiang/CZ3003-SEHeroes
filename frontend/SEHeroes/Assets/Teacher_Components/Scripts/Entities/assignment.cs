using System.Collections;
using System.Collections.Generic;
using System;
using SimpleJSON;
using System.Linq;

public class Assignment{
    public string assignmentName;
    public string assignmentId;
    public int tries;
    public AsgDate startDate;      // will change
    public AsgDate dueDate;        // will change
    public List<string> questions;  // represents list of questionId 
    public Assignment(){

    }
    public Assignment(JSONNode jsonAsg){
        assignmentName = jsonAsg["assignmentName"];
        tries = jsonAsg["tries"];
        startDate = new AsgDate(jsonAsg["startDate"]);
        dueDate = new AsgDate(jsonAsg["dueDate"]);
        questions = new List<string>();
        for(int j = 0; j < jsonAsg["questions"].Count; j++)
            questions.Add(jsonAsg["questions"][j]);
    }
}
