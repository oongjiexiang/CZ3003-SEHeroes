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

//TODOOOO:GET CHAR ATTACK AND HIT IN BATTLESCNECHARCONT
public class AssignmentBattleSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    private string level = ProgramStateController.level;
    private string section = ProgramStateController.section;
    private string world = ProgramStateController.world;
    private string character = ProgramStateController.characterType;

    private string assID = ProgramStateController.assID;
    private int scoreCount=0;
    public static bool APIdone = false;
    public static int questionCounter = 0;
    public static List<JSONNode> allQA = new List<JSONNode>();
    public static string question;
    public static JSONNode answer;
    public static int correctAnswerIndex;
    public static int score;
    public static int userAnswer = 0;
    private static bool beingHandled = false;
    private static bool canStart = false;

    //private readonly string baseQuesAPIURL = "https://seheroes.herokuapp.com/assignmentQuestion?assignmentId=";
    private JSONNode currQuestion;

    [SerializeField] Button Abutton;
    public Button AButton { get { return Abutton; } }
    [SerializeField] Button Bbutton;
    public Button BButton { get { return Bbutton; } }
    [SerializeField] Button Cbutton;
    public Button CButton { get { return Cbutton; } }
    [SerializeField] Button Dbutton;
    public Button DButton { get { return Dbutton; } }
    public Animator enemy;
    public Animator player;
    public Canvas ForestBG;
    public Canvas VillageBG;
    public Canvas SnowlandBG;
    public Canvas DesertBG;
    public Canvas AshlandBG;
    public Canvas resultCanvas;

    public static bool q1Ready=false;
    public TextMeshProUGUI marksText;
    public TextMeshProUGUI quesCounter;
    public TextMeshProUGUI totalScore;

    private string[] worldArr= new string[] {"Planning","Design","Implementation","Testing","Maintanence"};
    void Start()
    {
        ProgramStateController.viewState();
        StartCoroutine(APIController.GetAssignmentQuesAPI());
        //StartCoroutine(GetQuesAPI());
        resultCanvas.gameObject.SetActive(false);
        UpdateBG(worldArr[UnityEngine.Random.Range(0, worldArr.Length)]);
        enemy.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/assignmentMonster");
        if(character.Equals("Warrior"))
                player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/RedWarrior");
            else if(character.Equals("Magician"))
                player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Magician");
            else if(character.Equals("Bowman"))
                player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Bowman");
            else if(character.Equals("Swordman"))
                player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Swordman");
    }

    // Update is called once per frame
    void Update()
    {
        if (APIdone == true && questionCounter == 0)
        {
            currQuestion = allQA.ElementAt(questionCounter);
            question = currQuestion["question"];
            answer = currQuestion["answer"];
            score = (int)currQuestion["score"];
            correctAnswerIndex = (int)currQuestion["correctAnswer"]+1;
            questionCounter++;
            quesCounter.SetText(questionCounter+"/"+allQA.Count);
            q1Ready=true;
        }

        if (userAnswer != 0 && questionCounter<allQA.Count)
        {
            if (!beingHandled)
            {
                StartCoroutine(HandleIt());
                beingHandled = true;
            }
            if (canStart)
            {
                if (correctAnswerIndex == userAnswer)
                {
                    enemy.Play("Hit");
                    player.Play("CorrectAttack");
                    scoreCount+=score;
                }
                else
                {
                     enemy.Play("Attack");
                     player.Play("Hit");
                }

                if (questionCounter == allQA.Count-1)
                {
                    resultCanvas.gameObject.SetActive(true);
                    totalScore.SetText(scoreCount.ToString());
                    APIController.PostAssignmentResult(ProgramStateController.assID,ProgramStateController.matricNo,scoreCount.ToString());
                }

                currQuestion = allQA.ElementAt(questionCounter);
                question = currQuestion["question"];
                answer = currQuestion["answer"];
                correctAnswerIndex = (int)currQuestion["correctAnswer"]+1;
                score = (int)currQuestion["score"];
                questionCounter++;
                quesCounter.SetText(questionCounter+"/"+allQA.Count);
                marksText.SetText("Marks: "+scoreCount.ToString());

                userAnswer = 0;
                Debug.Log(correctAnswerIndex);

                //update button to enable
                Abutton.interactable = true;
                Bbutton.interactable = true;
                Cbutton.interactable = true;
                Dbutton.interactable = true;
                Abutton.gameObject.SetActive(true);
                Bbutton.gameObject.SetActive(true);
                Cbutton.gameObject.SetActive(true);
                Dbutton.gameObject.SetActive(true);
                Abutton.image.color = Color.white;
                Bbutton.image.color = Color.white;
                Cbutton.image.color = Color.white;
                Dbutton.image.color = Color.white;

                canStart=false;
                beingHandled=false;
            }
        }


    }
    
    private IEnumerator HandleIt()
    {
        yield return new WaitForSeconds(2.0f);
        canStart = true;
    }

    void UpdateBG(string world)
    {
        if (world.Contains("Planning"))
        {
            ForestBG.gameObject.SetActive(true);
            DesertBG.gameObject.SetActive(false);
            VillageBG.gameObject.SetActive(false);
            AshlandBG.gameObject.SetActive(false);
            SnowlandBG.gameObject.SetActive(false);
        }
        else if (world.Contains("Design"))
        {
            ForestBG.gameObject.SetActive(false);
            DesertBG.gameObject.SetActive(false);
            VillageBG.gameObject.SetActive(true);
            AshlandBG.gameObject.SetActive(false);
            SnowlandBG.gameObject.SetActive(false);
        }
        else if (world.Contains("Implementation"))
        {
            ForestBG.gameObject.SetActive(false);
            DesertBG.gameObject.SetActive(false);
            VillageBG.gameObject.SetActive(false);
            AshlandBG.gameObject.SetActive(false);
            SnowlandBG.gameObject.SetActive(true);

        }
        else if (world.Contains("Testing"))
        {
            ForestBG.gameObject.SetActive(false);
            DesertBG.gameObject.SetActive(true);
            VillageBG.gameObject.SetActive(false);
            AshlandBG.gameObject.SetActive(false);
            SnowlandBG.gameObject.SetActive(false);
        }
        else if (world.Contains("Maintanence"))
        {
            ForestBG.gameObject.SetActive(false);
            DesertBG.gameObject.SetActive(false);
            VillageBG.gameObject.SetActive(false);
            AshlandBG.gameObject.SetActive(true);
            SnowlandBG.gameObject.SetActive(false);
        }
    }

}