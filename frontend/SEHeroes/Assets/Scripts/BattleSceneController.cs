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
    private string character = ProgramStateController.characterType;
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
    private int playerAttack;
    private int playerHit;
    private float playerHeal;
    private float playerCri;
    private int playerCrtHit;
    public Image[] playerhealthImages;
    public Image[] enemyhealthImages;
    public Sprite[] healthSprites;
    private int startHearts = 5;
    private Dictionary<string, object> characterDict;
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
    public TextMeshProUGUI vic;
    public TextMeshProUGUI fai;
    public Image[] starsImage;
    public Sprite[] starsSprites;
    void Start()
    {
        ProgramStateController.viewState();
        StartCoroutine(APIController.GetStoryModeQuesAPI());
        //StartCoroutine(GetQuesAPI());
        initializeCharacter();
        initializeHP();
        playerMaxHP = 20;
        enemyMaxHP = 10;

        playerPerHealth = playerMaxHP / 5;
        enemyPerHealth = enemyMaxHP / 5;

        playerHP = playerMaxHP;
        enemyHP = enemyMaxHP;

        checkHealthAmount(playerhealthImages);
        checkHealthAmount(enemyhealthImages);

        UpdateHearts(playerhealthImages, true);
        UpdateHearts(enemyhealthImages, false);
        resultCanvas.gameObject.SetActive(false);
        UpdateBG(world);
        enemy.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/"+world+"MonsterController/"+level+world+"Monst");
        if(character.Equals("Warrior"))
            player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/RedWarrior");
        else if(character.Equals("Magician"))
            player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Magician");
        else if(character.Equals("Bowman"))
            player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Bowman");
        else if(character.Equals("Swordman"))
            player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Swordman");

    }

    void initializeCharacter(){
        switch(ProgramStateController.characterType){
            case "Warrior":
            Debug.Log("1");
            characterDict = BattleSceneCharacterController.warrior;
            break;
            case "Magician":
             Debug.Log("2");
            characterDict = BattleSceneCharacterController.magician;
            break;
            case "Bowman":
             Debug.Log("3");
            characterDict = BattleSceneCharacterController.bowman;
            break;
            case "Swordman":
             Debug.Log("4");
            characterDict = BattleSceneCharacterController.swordman;
            break;
        }
        foreach (KeyValuePair<string, object> kvp in BattleSceneCharacterController.warrior)
            Debug.Log ("Key = {0} + Value = {1}"+ kvp.Key + kvp.Value);
        playerAttack = (int)characterDict["Damage"];
        playerHit = (int)characterDict["Hit"];
        playerCri = (float)characterDict["Crit"];
        playerHeal = (float)characterDict["HealChance"];
        playerCrtHit = (int)characterDict["CritDamage"];
    }
    void initializeHP(){
        playerMaxHP = 20;
        enemyMaxHP = 10;

        playerPerHealth = playerMaxHP / 5;
        enemyPerHealth = enemyMaxHP / 5;

        playerHP = playerMaxHP;
        enemyHP = enemyMaxHP;

        checkHealthAmount(playerhealthImages);
        checkHealthAmount(enemyhealthImages);

        UpdateHearts(playerhealthImages, true);
        UpdateHearts(enemyhealthImages, false);
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
                    player.Play("CorrectAttack");
                    float randValue=UnityEngine.Random.value;
                    Debug.Log(randValue);
                    if(randValue<playerCri)
                        UpdateHP(false, playerCrtHit*(-1));
                    else
                        UpdateHP(false,playerAttack*(-1));
                    if(UnityEngine.Random.value<playerHeal)
                    {
                        UpdateHP(true,1);//add health if random val smaller that heal percentage
                    }
                }
                else
                {
                    //Animation
                    enemy.Play(level.ToLower() + "Attack");
                    player.Play("Hit");
                    UpdateHP(true, playerHit*(-1));//TODO
                }

                if (playerHP <= 0)
                {
                    //Die animatin
                     player.Play("Hit");
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
                Debug.Log("The next answer is: " + correctAnswerIndex);
                questionCounter++;
                userAnswer = 0;
            
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
        StartCoroutine(APIController.PostStoryModeResult(marks));
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