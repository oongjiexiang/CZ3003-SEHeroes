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


public class QuestionController : MonoBehaviour
{
    public bool isQuestion;
    public int answerNo;
    public bool answered = false;
    private TextMeshProUGUI textMesh;
    private string questionText;
    private JSONNode allAnswer;
    private int correctAnswerIndex;
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

        if(BattleSceneController.userAnswer != 0 && !isQuestion)
        {
            choicebutton.interactable = false;
            if (BattleSceneController.userAnswer == answerNo && BattleSceneController.correctAnswerIndex != answerNo)
            {
                choicebutton.interactable = true;
                choicebutton.image.color = Color.red;
            }
            else if (BattleSceneController.correctAnswerIndex == answerNo)
            {
                choicebutton.interactable = true;
                choicebutton.image.color=Color.green;

            }
        }
        if (BattleSceneController.APIdone == true && BattleSceneController.q1Ready == true)
        {
            textMesh = gameObject.GetComponent<TextMeshProUGUI>();
            questionText = BattleSceneController.question;
            allAnswer = BattleSceneController.answer;

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
        Debug.Log(answerNo);
        BattleSceneController.userAnswer = answerNo;
    }
}
