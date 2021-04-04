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

public class BattleSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    private string level = ProgramStateController.level;
    private string section = ProgramStateController.section;
    private string world = ProgramStateController.world;

    public static int questionCounter=0;
    public static List<JSONNode> allQA = new List<JSONNode>();
    public static string question;
    public static JSONNode answer;
    public static int correctAnswerIndex;
    public static string correctAnswer;
    public static int userAnswer = 0;
    // private static bool beingHandled=false;
    private static bool canStart = false;

    // private readonly string baseQuesAPIURL = "https://seheroes.herokuapp.com/storyModeQuestion?";
    private JSONNode currQuestion;

    private int playerHP;
    private int enemyHP;
    // private int maxHP = 20;
    private int maxHeart = 5;
    public Image[] playerhealthImages;
    public Image[] enemyhealthImages;
    public Sprite[] healthSprites;
    private int startHearts = 5;

    [SerializeField] Button Abutton;
    public Button AButton { get { return Abutton; } }
    [SerializeField] Button Bbutton;
    public Button BButton { get { return Bbutton; } }
    [SerializeField] Button Cbutton;
    public Button CButton { get { return Cbutton; } }
    [SerializeField] Button Dbutton;
    public Button DButton { get { return Dbutton; } }

    public Canvas ForestBG;
    public Canvas VillageBG;
    public Canvas SnowlandBG;
    public Canvas DesertBG;
    public Canvas AshlandBG;

    void Start()
    {
        StartCoroutine(APIController.GetStoryModeQuesAPI());
        allQA = APIController.allQA;
        // StartCoroutine(GetQuesAPI());
        playerHP = 20;
        enemyHP = 10;
        checkHealthAmount(playerhealthImages);
        checkHealthAmount(enemyhealthImages);
        UpdateHearts(playerhealthImages,true);
        UpdateHearts(enemyhealthImages,false);
        UpdateBG(world);
    }

    // Update is called once per frame
    void Update()
    {
        // if(APIdone == true && questionCounter == 0)
        if(APIController.APIdone == true && questionCounter == 0)
        {
            currQuestion = allQA.ElementAt(questionCounter);
            question = currQuestion["question"];
            answer = currQuestion["answer"];
            correctAnswer = currQuestion["correctAnswer"];

            for (int i = 0; i < answer.Count; i++)
            {
                if (answer[i] == correctAnswer) correctAnswerIndex = i + 1;
            }
            questionCounter++;
        }

        if(userAnswer != 0){
            StartCoroutine(HandleIt());
            if (canStart == true)
            {
                if (correctAnswerIndex == userAnswer)
                {
                    //Animation
                    //getEnemyHPLoss(character)
                    UpdateHP(false, -2);
                    //update HP icon
                }
                else
                {
                    //Animation
                    //getPlayerHPLoss(character)
                    UpdateHP(true, -2);
                    //update HP icon
                }


                if (playerHP <= 0)
                {
                    //Die animatin
                    //show u loss
                }
                else if (enemyHP <= 0)
                {
                    //Win ani
                    //show win
                }

                currQuestion = allQA.ElementAt(questionCounter);
                question = currQuestion["question"];
                answer = currQuestion["answer"];
                correctAnswer = currQuestion["correctAnswer"];

                for (int i = 0; i < answer.Count; i++)
                {
                    if (answer[i] == correctAnswer) correctAnswerIndex = i + 1;
                }
                questionCounter++;
                userAnswer = 0;


                //update button to enable
                Abutton.interactable = true;
                Bbutton.interactable = true;
                Cbutton.interactable = true;
                Dbutton.interactable = true;
                Abutton.image.color = Color.white;
                Bbutton.image.color = Color.white;
                Cbutton.image.color = Color.white;
                Dbutton.image.color = Color.white;

            }
        }
            
        
    }

    // IEnumerator GetQuesAPI()
    // {
    //     string QuesURL = baseQuesAPIURL + "world=" + world.Split(' ')[0] + "&section=" + section + "&level=" + level;
    //     UnityWebRequest quesRequest = UnityWebRequest.Get(QuesURL);
    //     yield return quesRequest.SendWebRequest();

    //     if (quesRequest.isNetworkError || quesRequest.isHttpError)
    //     {
    //         Debug.LogError(quesRequest.error);
    //         yield break;
    //     }

    //     JSONNode quesInfo = JSON.Parse(quesRequest.downloadHandler.text);
    //     Debug.Log(quesInfo[0]["level"]);
    //     Debug.Log(quesInfo[0]);
    //     for (int i = 0; i < quesInfo.Count; i++)
    //     {
    //         Debug.Log(quesInfo[0]);
    //         allQA.Add(quesInfo[i]);
    //     }
    //     APIdone = true;
    // }

    private IEnumerator HandleIt()
    {
        yield return new WaitForSeconds(2.0f);
        canStart = true;
    }

    void checkHealthAmount(Image[] healthImages)
    {
        for(int i = 0; i < maxHeart; i++)
        {
            if (startHearts <= i)
            {
                healthImages[i].enabled = false;
            }
            else
            {
                healthImages[i].enabled = true;
            }
        }
    }

    void UpdateHearts(Image[] healthImages,bool player)
    {
        bool empty = false;
        int i = 0;
        int HP;

        if(player == true)
        {
            HP = playerHP;
        }
        else
        {
            HP = enemyHP;
        }
        foreach(Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprites[0];
            }
            else
            {
                i++;
                if(HP >= i * 4)
                {
                    image.sprite = healthSprites[healthSprites.Length - 1];
                }
                else
                {
                    int currentHeartHealth = (int)(4 - 4 * i + HP);
                    int healthPerImage = 4 / (healthSprites.Length - 1);
                    int imageIndex = currentHeartHealth / healthPerImage;
                    image.sprite = healthSprites[imageIndex];
                    empty = true;
                }
            }
        }
    }

    void UpdateHP(bool player, int amount)
    {
        if (player == true)
        {
            playerHP += amount;
            playerHP = Mathf.Clamp(playerHP, 0, 20);
            UpdateHearts(playerhealthImages,true);
        }
        else
        {
            enemyHP += amount;
            enemyHP = Mathf.Clamp(enemyHP, 0, 10);
            UpdateHearts(enemyhealthImages,false);
        }

    }

    void UpdateBG(string world)
    {
        if(world.Contains("Forest")){
            ForestBG.gameObject.SetActive(true);
            DesertBG.gameObject.SetActive(false);
            VillageBG.gameObject.SetActive(false);
            AshlandBG.gameObject.SetActive(false);
            SnowlandBG.gameObject.SetActive(false);
        }
        else if(world.Contains("Village")){
            ForestBG.gameObject.SetActive(false);
            DesertBG.gameObject.SetActive(false);
            VillageBG.gameObject.SetActive(true);
            AshlandBG.gameObject.SetActive(false);
            SnowlandBG.gameObject.SetActive(false);
        }
        else if(world.Contains("Snowland")){
            ForestBG.gameObject.SetActive(false);
            DesertBG.gameObject.SetActive(false);
            VillageBG.gameObject.SetActive(false);
            AshlandBG.gameObject.SetActive(false);
            SnowlandBG.gameObject.SetActive(true);

        }
        else if(world.Contains("Desert")){
            ForestBG.gameObject.SetActive(false);
            DesertBG.gameObject.SetActive(true);
            VillageBG.gameObject.SetActive(false);
            AshlandBG.gameObject.SetActive(false);
            SnowlandBG.gameObject.SetActive(false);
        }
        else if(world.Contains("Ashland")){
            ForestBG.gameObject.SetActive(false);
            DesertBG.gameObject.SetActive(false);
            VillageBG.gameObject.SetActive(false);
            AshlandBG.gameObject.SetActive(true);
            SnowlandBG.gameObject.SetActive(false);
        }
    }

}
