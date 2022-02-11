using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        //대화
        talkData.Add(1000, new string[] {"NPC번호 1000에 대한 일반 메세지 창이다.:1", "NPC번호 1000에 대한 추가 일반 메세지 창이다.:2"});
        talkData.Add(2000, new string[] {"NPC번호 2000에 대한 일반 메세지 창이다.:1", "NPC번호 2000에 대한 추가 일반 메세지 창이다.:3"});        
        talkData.Add(3000, new string[] {"오브젝트 3000에 대한 메세지 창이다."});

        talkData.Add(10 + 1000, new string[] {"퀘스트 번호 10번 수행중일때 NPC번호 1000에게서 나타나는 메세지이다.:0",
                                                 "퀘스트 번호 10번 수행중일때 NPC번호 1000에게서 나타나는 추가 메세지이다.:1"});
        talkData.Add(11 + 1000, new string[] {"퀘스트번호 10번을 수행중인경우에 퀘스트를 받고나서 다시 말을 걸경우 나타나는 메세지이다.:3", });                                               
        talkData.Add(11 + 2000, new string[] {"퀘스트번호 10번을 수행중일때 NPC번호 1000과 대화한후 NPC번호 2000에게서 받을수 있는 메세지이다.:0",
                                                 "퀘스트번호 10번을 수행중일때 NPC번호 1000과 대화한후 NPC번호 2000에게서 받을수 있는 추가 메세지이다.:1",
                                                 "퀘스트번호 10번을 수행중일때 NPC번호 1000과 대화한후 NPC번호 2000에게서 받을수 있는 추가 메세지이다.:3"});

        talkData.Add(20 + 1000, new string[] {"퀘스트 번호 20번을 수행중일때 NPC번호 1000과 대화할 경우 나타나는 메세지이다.:0",
                                                 "퀘스트 번호 20번을 수행중일때 NPC번호 1000과 대화할 경우 나타나는 추가 메세지이다.:1",
                                                 "퀘스트 번호 20번을 수행중일때 NPC번호 1000과 대화할 경우 나타나는 추가 메세지이다.:3"});                                         
        talkData.Add(20 + 2000, new string[] {"퀘스트 번호 20번을 수행중일때 NPC번호 2000과 대화할 경우 나타나는 메세지이다.:0"});                                           
        talkData.Add(20 + 5000, new string[] {"오브젝트 번호 5000과 대화할때 나타나는 메세지이다.", });
        talkData.Add(21 + 2000, new string[] {"오브젝트 번호 5000과의 대화이후 NPC번호 2000과 대화할 경우 나타나는 메세지이다.:0",});
                                             
        //NPC 표정
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex)
    {

        if(!talkData.ContainsKey(id)){
            if(!talkData.ContainsKey(id - id % 10))       
                return GetTalk(id - id % 100, talkIndex);  
            else
                return GetTalk(id - id % 10, talkIndex);  
                       
        }

        if(talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
