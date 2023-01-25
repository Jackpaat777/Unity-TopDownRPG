using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public GameObject EndCursor;
    public int CharPerSeconds;
    public bool isAnim;

    Text msgText;
    AudioSource audioSource;
    string targetMsg;
    int index;
    float interval;

    void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    // 메세지 보이기
    public void SetMsg(string msg)
    {
        // 애니메이션 도중 다음 키를 누를 경우
        if (isAnim)
        {
            // 대화를 모두 출력한 뒤 Invoke 종료, EffectEnd
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        // 애니메이션이 나오는 상황이 아닐 경우
        else
        {
            // 메세지 이펙트 실행
            targetMsg = msg;
            EffectStart();
        }
    }

    // 이펙트 시작 시에는 공백
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        // CPS : 1초에 보여주는 Char 개수 -> ( 1 / CPS ) : 하나의 Char이 보이는 시간 
        // interval 확실한 소숫값을 사용하기 위한 변수 -> 그냥 Invoke에 1 / CPS를 하는 것 보다 더 적용이 잘 됨
        interval = 1.0f / CharPerSeconds;

        isAnim = true;

        Invoke("Effecting", interval);
    }
    // 이펙트 도중에는 글자 하나씩 보여줌
    void Effecting()
    {
        // 모든 텍스트를 다 읽은 뒤에는 End로 이동
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];

        // Sound
        // 공백과 마침표를 제외하고 사운드 출력
        if (targetMsg[index] != ' ' || targetMsg[index] != '.' || targetMsg[index] != '?' || targetMsg[index] != '!')
            audioSource.Play();

        index++;

        // Recursive
        Invoke("Effecting", interval);
    }
    // 이펙트가 끝난 뒤에는 EndCursor 보여주기
    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true);
    }
}
