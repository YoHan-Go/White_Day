using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();        
    }


    void GenerateData()
    {
        //퀘스트 리스트(퀘스트 목록, 퀘스트 이름, 퀘스트 관련 NPC 및 오브젝트)
        questList.Add(10, new QuestData("마을 사람들과 대화하기.", new int[] {1000, 2000}));
        questList.Add(20, new QuestData("새로운 영상 지켜보기.", new int[] {5000, 2000}));
        questList.Add(30, new QuestData("제리 영상보기 완료!", new int[] {0}));
    }
    
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }
    public string CheckQuest(int id)
    {
        if(id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        ControlObject();

        if(questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        return questList[questId].questName;
    }
    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
    
    public void ControlObject()
    {
        //퀘스트 관련 오브젝트
        switch(questId) {
            case 10:
                if(questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 0)
                    questObject[0].SetActive(true);
                else if(questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
        }
    }
}
