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


public class FriendBattleQuestionController : MonoBehaviour
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

        if(FriendBattleSceneController.userAnswer != 0 && !isQuestion)
        {
            // Debug.Log(FriendBattleSceneController.userAnswer.ToString()+" Hellllllo");
            choicebutton.interactable = false;
            if (FriendBattleSceneController.userAnswer == answerNo && FriendBattleSceneController.correctAnswerIndex != answerNo)
            {
                choicebutton.interactable = true;
                choicebutton.image.color = Color.red;
            }
            else if (FriendBattleSceneController.correctAnswerIndex == answerNo)
            {
                choicebutton.interactable = true;
                choicebutton.image.color=Color.green;

            }
        }
        if (FriendBattleSceneController.APIdone == true && FriendBattleSceneController.q1Ready == true)
        {
            textMesh = gameObject.GetComponent<TextMeshProUGUI>();
            questionText = FriendBattleSceneController.question;
            allAnswer = FriendBattleSceneController.answer;

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
        FriendBattleSceneController.userAnswer = answerNo;
    }
}
