using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;


public class AssignmentQuestionController : MonoBehaviour
{
    public bool isQuestion;
    public int answerNo;
    private TextMeshProUGUI textMesh;
    private string questionText;
    private JSONNode allAnswer;
    [SerializeField] Button choicebutton;
    public Button ChoiceButton { get { return choicebutton; } }

    // Start is called before the first frame update
    void Start()
    {
        if(!isQuestion)choicebutton.onClick.AddListener(onClickButton);
    }

    // Update is called once per frame
    void Update()
    {

        if(AssignmentBattleSceneController.userAnswer != 0 && !isQuestion)
        {
            choicebutton.interactable = false;
            if (AssignmentBattleSceneController.userAnswer == answerNo && AssignmentBattleSceneController.correctAnswerIndex != answerNo)
            {
                choicebutton.interactable = true;
                choicebutton.image.color = Color.red;
            }
            else if (AssignmentBattleSceneController.correctAnswerIndex == answerNo)
            {
                choicebutton.interactable = true;
                choicebutton.image.color=Color.green;

            }
        }
        if (APIController.APIdone == true && AssignmentBattleSceneController.q1Ready == true)
        {
            textMesh = gameObject.GetComponent<TextMeshProUGUI>();
            questionText = AssignmentBattleSceneController.question;
            allAnswer = AssignmentBattleSceneController.answer;
            if (answerNo > allAnswer.Count)
            {
                choicebutton.gameObject.SetActive(false);
            }
            else if (isQuestion)
                textMesh.text = questionText;
            else
            {
                textMesh.text = allAnswer[answerNo - 1];
            }

        }
        
        
    }

    public void onClickButton()
    {
        AssignmentBattleSceneController.userAnswer = answerNo;
        Debug.Log(answerNo);
    }
}
