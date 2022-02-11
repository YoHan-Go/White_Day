using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public GameObject EndCursor;
    public int CharPerSeconds;
    public bool isAnim;
    
    string targetMsg;
    AudioSource audioSource;
    Text msgText;
    int index;
    float interval;
    

    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }    

    public void SetMsg(string msg)
    {
        if(isAnim){
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        interval = 1.0f / CharPerSeconds;
        Debug.Log(interval);

        isAnim = true;
        Invoke("Effecting", interval);         //'1초 /CPS = 1글자가 나오는 딜레이'
    }
    
    void Effecting()
    {
        if(msgText.text == targetMsg){
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];
        
        //소리
        if(targetMsg[index] != ' ' || targetMsg[index] != '.')
            audioSource.Play();

        index++;

        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true);
    }
}
