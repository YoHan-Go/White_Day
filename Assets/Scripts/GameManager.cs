using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public Animator talkPanel;
    public Animator portraitAnim;
    public Sprite prevPortrait;
    public Image portraitImg;
    public TypeEffect talk;
    public Text questText;
    public Text nametext;
    public QuestManager questManager;
    public GameObject scanObject;
    public GameObject menuSet;
    public GameObject player;
    public bool isAction;
    public int talkIndex; 

    void Start()
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
        Debug.Log(questManager.CheckQuest());
    }
    
    void Update()
    {
        //서브 메뉴
        if(Input.GetButtonDown("Cancel")){
            if(menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }
    }
    
    public void Action(GameObject scanObj)
    {
        //오브젝트 액션 관련 코딩
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNpc)
    {
        //talkmanager에서 가져오는 대화
        int questTalkIndex = 0;
        string talkData = "";

        if(talk.isAnim){
            talk.SetMsg("");
            return;
        }
        else{
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id+ questTalkIndex, talkIndex);
        }
        
       
        if(talkData == null){
            isAction = false;
            talkIndex = 0;
            questText.text = questManager.CheckQuest(id);
            //Debug.Log(questManager.CheckQuest(id));
            return;
        }

            
        if(isNpc){
            talk.SetMsg(talkData.Split(':')[0]);

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);

            if(prevPortrait != portraitImg.sprite){
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
                  
        }
        else{
            talk.SetMsg(talkData);

            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;
    }

    public void GameSave()
    {
        //게임 저장 (플레이어 x,y위치, 퀘스트 진행상태)
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QustId", questManager.questId);
        PlayerPrefs.SetInt("QustActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        //게임 불러오기 (플레이어 x,y위치, 퀘스트 진행상태)
        if(!PlayerPrefs.HasKey("PlayerX"))
            return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QustId");
        int questActionIndex = PlayerPrefs.GetInt("QustActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();

    }

    public void GameExit()
    {
        //게임 끄기
        Application.Quit();
    }
}
