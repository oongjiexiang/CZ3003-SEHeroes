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
using Photon.Pun;
using Photon.Realtime;

//TODOOOO:GET CHAR ATTACK AND HIT IN BATTLESCNECHARCONT
public class FriendBattleSceneController : MonoBehaviourPun
{
    // Start is called before the first frame update
    private string level = FriendRoomController.challengeLevel;
    private string section = FriendRoomController.challengeSection;
    private string world = FriendRoomController.challengeWorld;
    public static bool APIdone = false;
    public static bool q1Ready = false;
    public static int questionCounter = 0;
    public static List<JSONNode> allQA = new List<JSONNode>();
    public static string question;
    public static JSONNode answer;
    public static int correctAnswerIndex;
    public GameObject playerprefab;
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
    private GameObject playerPre;
    private GameObject enemyPre;
    private Animator playerAnimator;
    private Animator enemyAnimator;
    private string character;
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
    public Canvas ForestBG;
    public Canvas VillageBG;
    public Canvas SnowlandBG;
    public Canvas DesertBG;
    public Canvas AshlandBG;
    public Canvas resultCanvas;
    public TextMeshProUGUI vic;
    public TextMeshProUGUI fai;
    public TextMeshProUGUI statusText;
    public Canvas waitingpanel;

    public TextMeshProUGUI player1matric;
    public TextMeshProUGUI player2matric;
    PhotonView pv;
    private bool p2notset=true;
    private bool p2ready=false;

    void Start()
    {
        // if (FriendRoomController.playerID == 1)
        // {
        //     ExitGames.Client.Photon.Hashtable p2info = new ExitGames.Client.Photon.Hashtable();
        //     p2info.Add("p2matric", FriendRoomController.player2matric);
        //     p2info.Add("p2character", FriendRoomController.player2char);
        //     if (PhotonNetwork.CurrentRoom.SetCustomProperties(p2info))
        //     {
        //         Debug.Log("Successfully set p2 info");
        //     }
        //     else Debug.Log("Failed set Roominfo");
        // }
        pv = GetComponent<PhotonView>();
        Debug.Log(FriendRoomController.player1char + " " + FriendRoomController.player2char);
        ProgramStateController.viewState();
        StartCoroutine(APIController.GetFriendStoryModeQuesAPI());
        initializeCharacter();
        initializeHP();
        resultCanvas.gameObject.SetActive(false);
        waitingpanel.gameObject.SetActive(false);
        UpdateBG(world);

        player1matric.text = FriendRoomController.player1matric;
        player2matric.text = FriendRoomController.player2matric;
    }

    void initializeCharacter()
    {
        if (FriendRoomController.playerID == 1)
        {
            playerPre = Instantiate(playerprefab, ProgramStateController.player1Pos, new Quaternion(0, 0, 0, 0));
            enemyPre = Instantiate(playerprefab, ProgramStateController.player2Pos, new Quaternion(0, 180, 0, 0));
            character = FriendRoomController.player1char;
        }
        else
        {
            playerPre = Instantiate(playerprefab, ProgramStateController.player2Pos, new Quaternion(0, 180, 0, 0));
            enemyPre = Instantiate(playerprefab, ProgramStateController.player1Pos, new Quaternion(0, 0, 0, 0));
            character = FriendRoomController.player2char;
        }
        playerAnimator = playerPre.GetComponent<Animator>();
        if (character.Equals("Warrior"))
            playerAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/RedWarrior");
        else if (character.Equals("Magician"))
            playerAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Magician");
        else if (character.Equals("Bowman"))
            playerAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Bowman");
        else if (character.Equals("Swordman"))
            playerAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Swordman");

        enemyAnimator = enemyPre.GetComponent<Animator>();
        if (character.Equals("Warrior"))
            enemyAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/RedWarrior");
        else if (character.Equals("Magician"))
            enemyAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Magician");
        else if (character.Equals("Bowman"))
            enemyAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Bowman");
        else if (character.Equals("Swordman"))
            enemyAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Swordman");

        playerAttack = 2;
        playerHit = 2;
        playerCri = 0f;
        playerHeal = 0f;
        playerCrtHit = 0;
    }
    void initializeHP()
    {
        playerMaxHP = 20;
        enemyMaxHP = 20;

        playerPerHealth = playerMaxHP / 5;
        enemyPerHealth = enemyMaxHP / 5;

        playerHP = playerMaxHP;
        enemyHP = enemyMaxHP;

        ExitGames.Client.Photon.Hashtable Roominfo = new ExitGames.Client.Photon.Hashtable();
        Roominfo.Add("player2mat",null);
        Roominfo.Add("player2char",null);
        Roominfo.Add("player1HP", 20);
        Roominfo.Add("player2HP", 20);
        Roominfo.Add("player1waiting", false);
        Roominfo.Add("player2waiting", false);
        if (PhotonNetwork.CurrentRoom.SetCustomProperties(Roominfo))
        {
            Debug.Log("Successfully set HP info");
        }
        else Debug.Log("Failed set Roominfo");

        if (FriendRoomController.playerID == 2)
        {
            Image[] temp = playerhealthImages;
            playerhealthImages = enemyhealthImages;
            enemyhealthImages = temp;
        }
        checkHealthAmount(playerhealthImages);
        checkHealthAmount(enemyhealthImages);

        UpdateHearts(playerhealthImages, true);
        UpdateHearts(enemyhealthImages, false);
    }

    // Update is called once per frame
    void Update()
    {
        if(FriendRoomController.playerID == 2 && p2notset&& (!p2ready)){
            Debug.Log("Enter ready p2"); 
            ExitGames.Client.Photon.Hashtable Roominfo = new ExitGames.Client.Photon.Hashtable();
            Roominfo.Add("player2mat",FriendRoomController.player2matric);
            Roominfo.Add("player2char",FriendRoomController.player2char);
            Roominfo.Add("player1HP", (int)PhotonNetwork.CurrentRoom.CustomProperties["player1HP"]);
            Roominfo.Add("player2HP", (int)PhotonNetwork.CurrentRoom.CustomProperties["player2HP"]);
            Roominfo.Add("player1waiting", (bool)PhotonNetwork.CurrentRoom.CustomProperties["player1waiting"]);
            Roominfo.Add("player2waiting", (bool)PhotonNetwork.CurrentRoom.CustomProperties["player2waiting"]);
            if (PhotonNetwork.CurrentRoom.SetCustomProperties(Roominfo))
            {
                Debug.Log("Successfully set Room2 info");
            }
            else Debug.Log("Failed set Roominfo");  
            p2ready=true;
        }else{
            Debug.Log(p2ready.ToString()+p2notset.ToString());
        }
        if(p2ready && FriendRoomController.playerID==1 &&p2notset)
        {
            Debug.Log("Enter set p2"); 
            FriendRoomController.player2char=PhotonNetwork.CurrentRoom.CustomProperties["player2char"].ToString();
            Debug.Log("Enter setting p2, "+ PhotonNetwork.CurrentRoom.CustomProperties["player2char"].ToString()+" "+PhotonNetwork.CurrentRoom.CustomProperties["player2mat"].ToString());
            FriendRoomController.player2matric=PhotonNetwork.CurrentRoom.CustomProperties["player2mat"].ToString();
            p2notset=false;
            ExitGames.Client.Photon.Hashtable Roominfo = new ExitGames.Client.Photon.Hashtable();
            Roominfo.Add("player2mat",FriendRoomController.player2matric);
            Roominfo.Add("player2char",FriendRoomController.player2char);
            Roominfo.Add("player1HP", (int)PhotonNetwork.CurrentRoom.CustomProperties["player1HP"]);
            Roominfo.Add("player2HP", (int)PhotonNetwork.CurrentRoom.CustomProperties["player2HP"]);
            Roominfo.Add("player1waiting", (bool)PhotonNetwork.CurrentRoom.CustomProperties["player1waiting"]);
            Roominfo.Add("player2waiting", (bool)PhotonNetwork.CurrentRoom.CustomProperties["player2waiting"]);
            if (PhotonNetwork.CurrentRoom.SetCustomProperties(Roominfo))
            {
                Debug.Log("Successfully set Room2 info");
            }
            else Debug.Log("Failed set Roominfo");  
        }else{
            Debug.Log(p2ready.ToString()+p2notset.ToString());
        }
        if (playerHP != (FriendRoomController.playerID == 1 ? (int)PhotonNetwork.CurrentRoom.CustomProperties["player1HP"] : (int)PhotonNetwork.CurrentRoom.CustomProperties["player2HP"]))
        {
            playerHP = (FriendRoomController.playerID == 1 ? (int)PhotonNetwork.CurrentRoom.CustomProperties["player1HP"] : (int)PhotonNetwork.CurrentRoom.CustomProperties["player2HP"]);
            UpdateHearts(playerhealthImages, true);
            if (playerHP <= 0)
            {
                //Die animatin
                playerAnimator.Play("Hit");
                //show u loss
                resultCanvas.gameObject.SetActive(true);
                vic.gameObject.SetActive(false);
                statusText.text = "You Lost...";
            }
        }
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["player1waiting"] && (bool)PhotonNetwork.CurrentRoom.CustomProperties["player2waiting"])
        {
            resultCanvas.gameObject.SetActive(true);
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["player1HP"] < (int)PhotonNetwork.CurrentRoom.CustomProperties["player2HP"])
            {
                if (FriendRoomController.playerID == 1)
                {
                    vic.gameObject.SetActive(false);
                    statusText.text = "You Lost...";
                }
                else
                {
                    fai.gameObject.SetActive(false);
                    statusText.text = "You Win!";
                }
            }
            else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["player1HP"] > (int)PhotonNetwork.CurrentRoom.CustomProperties["player2HP"])
            {
                if (FriendRoomController.playerID == 2)
                {
                    vic.gameObject.SetActive(false);
                    statusText.text = "You Lost...";
                }
                else
                {
                    fai.gameObject.SetActive(false);
                    statusText.text = "You Win!";
                }
            }
            else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["player1HP"] == (int)PhotonNetwork.CurrentRoom.CustomProperties["player2HP"])
            {
                vic.text = "DRAWN";
                fai.gameObject.SetActive(false);
                statusText.text = "Same score!";
            }
        }
        if (APIdone == true && questionCounter == 0)
        {
            UpdateQA();
            q1Ready = true;
        }

        if (userAnswer != 0 && questionCounter < allQA.Count)
        {
            if (!beingHandled)
            {
                beingHandled = true;
                StartCoroutine(HandleIt());
            }
            if (canStart)
            {
                if (correctAnswerIndex == userAnswer)
                {
                    //Animation
                    enemyAnimator.Play("Hit");
                    playerAnimator.GetComponent<Animator>().Play("CorrectAttack");
                    float randValue = UnityEngine.Random.value;
                    Debug.Log(randValue);
                    if (randValue < playerCri)
                        UpdateHP(false, playerCrtHit * (-1));
                    else
                        UpdateHP(false, playerAttack * (-1));
                    if (UnityEngine.Random.value < playerHeal)
                    {
                        UpdateHP(true, 1);//add health if random val smaller that heal percentage
                    }
                }
                else
                {
                    //Animation
                    enemyAnimator.Play("CorrectAttack");
                    playerAnimator.Play("Hit");
                }

                if (enemyHP <= 0)
                {
                    //Win ani
                    enemyAnimator.Play("Hit");
                    //show win
                    resultCanvas.gameObject.SetActive(true);
                    fai.gameObject.SetActive(false);
                }
                else if (questionCounter == allQA.Count - 1)
                {
                    if ((!(FriendRoomController.playerID == 1) && (bool)PhotonNetwork.CurrentRoom.CustomProperties["player1waiting"]) || ((FriendRoomController.playerID == 1) && (bool)PhotonNetwork.CurrentRoom.CustomProperties["player2waiting"]))
                    {
                        ExitGames.Client.Photon.Hashtable Roominfo1 = new ExitGames.Client.Photon.Hashtable();
                        Roominfo1.Add("player1HP", FriendRoomController.playerID == 1 ? playerHP : enemyHP);
                        Roominfo1.Add("player2HP", FriendRoomController.playerID == 2 ? playerHP : enemyHP);
                        Roominfo1.Add("player1waiting", questionCounter == allQA.Count - 1 && FriendRoomController.playerID == 1 ? true : (bool)PhotonNetwork.CurrentRoom.CustomProperties["player1waiting"]);
                        Roominfo1.Add("player2waiting", questionCounter == allQA.Count - 1 && FriendRoomController.playerID == 2 ? true : (bool)PhotonNetwork.CurrentRoom.CustomProperties["player2waiting"]);
                        if (PhotonNetwork.CurrentRoom.SetCustomProperties(Roominfo1))
                        {
                            Debug.Log("Successfully set HP info");
                        }
                        else Debug.Log("Failed set Roominfo");
                        resultCanvas.gameObject.SetActive(true);
                        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["player1HP"] < (int)PhotonNetwork.CurrentRoom.CustomProperties["player2HP"])
                        {
                            if (FriendRoomController.playerID == 1)
                            {
                                vic.gameObject.SetActive(false);
                                statusText.text = "You Lost...";
                            }
                            else
                            {
                                fai.gameObject.SetActive(false);
                                statusText.text = "You Win!";
                            }
                        }
                        else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["player1HP"] > (int)PhotonNetwork.CurrentRoom.CustomProperties["player2HP"])
                        {
                            if (FriendRoomController.playerID == 2)
                            {
                                vic.gameObject.SetActive(false);
                                statusText.text = "You Lost...";
                            }
                            else
                            {
                                fai.gameObject.SetActive(false);
                                statusText.text = "You Win!";
                            }
                        }
                        else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["player1HP"] == (int)PhotonNetwork.CurrentRoom.CustomProperties["player2HP"])
                        {
                            vic.text = "DRAWN";
                            fai.gameObject.SetActive(false);
                            statusText.text = "Same score!";
                        }
                    }
                    else
                    {
                        waitingpanel.gameObject.SetActive(true);
                        ExitGames.Client.Photon.Hashtable Roominfo1 = new ExitGames.Client.Photon.Hashtable();
                        Roominfo1.Add("player1HP", FriendRoomController.playerID == 1 ? playerHP : enemyHP);
                        Roominfo1.Add("player2HP", FriendRoomController.playerID == 2 ? playerHP : enemyHP);
                        Roominfo1.Add("player1waiting", questionCounter == allQA.Count - 1 && FriendRoomController.playerID == 1 ? true : (bool)PhotonNetwork.CurrentRoom.CustomProperties["player1waiting"]);
                        Roominfo1.Add("player2waiting", questionCounter == allQA.Count - 1 && FriendRoomController.playerID == 2 ? true : (bool)PhotonNetwork.CurrentRoom.CustomProperties["player2waiting"]);
                        if (PhotonNetwork.CurrentRoom.SetCustomProperties(Roominfo1))
                        {
                            Debug.Log("Successfully set HP info in waiting");
                        }
                        else Debug.Log("Failed set Roominfo");
                    }
                }

                ExitGames.Client.Photon.Hashtable Roominfo = new ExitGames.Client.Photon.Hashtable();
                
                Roominfo.Add("player1HP", FriendRoomController.playerID == 1 ? playerHP : enemyHP);
                Roominfo.Add("player2HP", FriendRoomController.playerID == 2 ? playerHP : enemyHP);
                Roominfo.Add("player1waiting", questionCounter == allQA.Count - 1 && FriendRoomController.playerID == 1 ? true : (bool)PhotonNetwork.CurrentRoom.CustomProperties["player1waiting"]);
                Roominfo.Add("player2waiting", questionCounter == allQA.Count - 1 && FriendRoomController.playerID == 2 ? true : (bool)PhotonNetwork.CurrentRoom.CustomProperties["player2waiting"]);
                if (PhotonNetwork.CurrentRoom.SetCustomProperties(Roominfo))
                {
                    Debug.Log("Successfully set HP info");
                }
                else Debug.Log("Failed set Roominfo");

                if (questionCounter < allQA.Count - 1) UpdateQA();
                userAnswer = 0;

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
                canStart = false;
                beingHandled = false;
            }
        }



    }

    public void UpdateQA()
    {
        Debug.Log("Enter QA update " + questionCounter.ToString());
        currQuestion = allQA.ElementAt(questionCounter);
        question = currQuestion["question"];
        answer = currQuestion["answer"];
        correctAnswerIndex = (int)currQuestion["correctAnswer"] + 1;
        Debug.Log(correctAnswerIndex);
        questionCounter++;
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