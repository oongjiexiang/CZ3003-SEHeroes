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


public class MultiPlayerQuestionController : MonoBehaviour
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

        if(MultiPlayerBattleSceneController.userAnswer != 0 && !isQuestion)
        {
            choicebutton.interactable = false;
            if (MultiPlayerBattleSceneController.userAnswer == answerNo && MultiPlayerBattleSceneController.correctAnswerIndex != answerNo)
            {
                choicebutton.interactable = true;
                choicebutton.image.color = Color.red;
            }
            else if (MultiPlayerBattleSceneController.correctAnswerIndex == answerNo)
            {
                choicebutton.interactable = true;
                choicebutton.image.color=Color.green;

            }
        }
        if (MultiPlayerBattleSceneController.APIdone == true && MultiPlayerBattleSceneController.q1Ready == true)
        {
            textMesh = gameObject.GetComponent<TextMeshProUGUI>();
            questionText = MultiPlayerBattleSceneController.question;
            allAnswer = MultiPlayerBattleSceneController.answer;

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
        MultiPlayerBattleSceneController.userAnswer = answerNo;
    }
}
