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
public class BattleSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    private string world = ProgramStateController.world;
    private string section = ProgramStateController.section;
    private string level = ProgramStateController.level;

    public static bool APIdone = false;
    public static bool q1Ready = false;
    public static int questionCounter = 0;
    public static List<JSONNode> allQA = new List<JSONNode>();
    public static string question;
    public static JSONNode answer;
    public static int correctAnswerIndex;
    //public static string correctAnswer;
    public static int userAnswer = 0;
    private static bool beingHandled = false;
    private static bool canStart = false;
    //private readonly string baseQuesAPIURL = "https://seheroes.herokuapp.com/storyModeQuestion?";
    private JSONNode currQuestion;

    private int playerHP;
    private int enemyHP;
    private int maxHeart = 5;
    private int playerPerHealth;
    private int enemyPerHealth;
    private int playerMaxHP;
    private int enemyMaxHP;
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
    public Animator enemy;
    public Canvas ForestBG;
    public Canvas VillageBG;
    public Canvas SnowlandBG;
    public Canvas DesertBG;
    public Canvas AshlandBG;
    public Canvas resultCanvas;
    public TextMeshProUGUI vic;
    public TextMeshProUGUI fai;
    public Image[] starsImage;
    public Sprite[] starsSprites;
    void Start()
    {
        ProgramStateController.viewState();
        StartCoroutine(APIController.GetStoryModeQuesAPI());
        //StartCoroutine(GetQuesAPI());
        playerMaxHP = 20;
        enemyMaxHP = 10;
        playerPerHealth = playerMaxHP / 5;
        enemyPerHealth = enemyMaxHP / 5;
        playerHP = playerMaxHP;
        enemyHP = enemyMaxHP;

        resultCanvas.gameObject.SetActive(false);
        checkHealthAmount(playerhealthImages);
        checkHealthAmount(enemyhealthImages);
        UpdateHearts(playerhealthImages, true);
        UpdateHearts(enemyhealthImages, false);
        UpdateBG(world);
        enemy.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/"+world+"MonsterController/"+level+world+"Monst");
    }

    // Update is called once per frame
    void Update()
    {
        if (APIdone == true && questionCounter == 0)
        {
            currQuestion = allQA.ElementAt(questionCounter);
            question = currQuestion["question"];
            answer = currQuestion["answer"];
            correctAnswerIndex = (int)currQuestion["correctAnswer"]+1;

            questionCounter++;
            q1Ready=true;
        }

        if (userAnswer != 0)
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
                    //Animation
                    enemy.Play(level.ToLower() + "Hit");
                    //getEnemyHPLoss(character)
                    UpdateHP(false, -2);//TODO
                    //update HP icon
                }
                else
                {
                    //Animation
                     enemy.Play(level.ToLower() + "Attack");
                    //getPlayerHPLoss(character)
                    UpdateHP(true, -2);//TODO
                    //update HP icon
                }

                if (playerHP <= 0)
                {
                    //Die animatin
                    //show u loss
                    resultCanvas.gameObject.SetActive(true);
                    vic.gameObject.SetActive(false);
                    UpdateStars(starsImage);
                }
                else if (enemyHP <= 0)
                {
                    //Win ani
                    enemy.Play(level.ToLower() + "Death");
                    //show win
                    resultCanvas.gameObject.SetActive(true);
                    fai.gameObject.SetActive(false);
                    UpdateStars(starsImage);
                }

                currQuestion = allQA.ElementAt(questionCounter);
                question = currQuestion["question"];
                answer = currQuestion["answer"];
                correctAnswerIndex = (int)currQuestion["correctAnswer"]+1;
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

                canStart=false;
                beingHandled=false;
            }
        }


    }
    // IEnumerator GetQuesAPI()
    // {
    //     string QuesURL = baseQuesAPIURL + "world=" + world + "&section=" + section + "&level=" + level;
    //     UnityWebRequest quesRequest = UnityWebRequest.Get(QuesURL);
    //     yield return quesRequest.SendWebRequest();

    //     if (quesRequest.isNetworkError || quesRequest.isHttpError)
    //     {
    //         Debug.LogError(quesRequest.error);
    //         yield break;
    //     }

    //     JSONNode quesInfo = JSON.Parse(quesRequest.downloadHandler.text);
    //     for (int i = 0; i < quesInfo.Count; i++)
    //     {
    //         Debug.Log(quesInfo[i]);
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
        for (int i = 0; i < maxHeart; i++)
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

    void UpdateHearts(Image[] healthImages, bool player)
    {
        bool empty = false;
        int i = 0;
        int HP;
        int perHealth;

        if (player == true)
        {
            HP = playerHP;
            perHealth = playerPerHealth;
        }
        else
        {
            HP = enemyHP;
            perHealth = enemyPerHealth;
        }
        foreach (Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprites[0];
            }
            else
            {
                i++;
                if (HP >= i * perHealth)
                {
                    image.sprite = healthSprites[healthSprites.Length - 1];
                }
                else
                {
                    int currentHeartHealth = (int)(perHealth - perHealth * i + HP);
                    int healthPerImage = playerPerHealth / (healthSprites.Length - 1);
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
            playerHP = Mathf.Clamp(playerHP, 0, playerMaxHP);
            UpdateHearts(playerhealthImages, true);
        }
        else
        {
            enemyHP += amount;
            enemyHP = Mathf.Clamp(enemyHP, 0, enemyMaxHP);
            UpdateHearts(enemyhealthImages, false);
        }

    }

    void UpdateStars(Image[] starsImg)
    {
        bool empty = false;
        int i = 0;
        int marks = Mathf.FloorToInt(playerHP / 5.0F);

        foreach (Image image in starsImg)
        {
            if (empty)
            {
                image.sprite = starsSprites[0];
            }
            else
            {
                i++;
                if (marks >= i)
                {
                    image.sprite = starsSprites[1];
                }
            }
        }
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