using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public QuestManager questManager;
    public TalkManager talkManager;
    public Animator talkPanel;
    public Animator portraitAnim;
    public Image portraitImg;
    public Sprite prevPortrait;
    public Text nameText;
    public Text questText;
    public TypeEffect talk;
    public GameObject menuSet;
    public GameObject scanObject;
    public GameObject player;
    public bool isAction;
    public int talkIndex;

    void Start()
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
    }

    void Update()
    {
        // Sub Menu
        // 끄는 기능은 계속하기 버튼을 통해서도 가능 -> 스크립트에는 없음 (GameObject의 기본함수 SetActive는 인스펙터 창에서 할당 가능)
        if (Input.GetButtonDown("Cancel"))
        {
            // 이미 켜져있을 때 ESC 버튼을 누르면 꺼짐
            if (menuSet.activeSelf)
            {
                Time.timeScale = 1;
                menuSet.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                menuSet.SetActive(true);
            }
        }

    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        // 중요!
        // scanObject의 ObjData를 사용하고 싶다면 GetComponent로 한번 호출한 뒤 사용
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNPC, objData.ObjectName);

        // isAction에 따라 Panel의 애니메이션을 올렸다 내렸다 하기
        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNPC, string name)
    {
        int questTalkIndex;
        string talkData;

        if (talk.isAnim)
        {
            // 타이핑 애니메이션 도중이라면 다음 대화를 불러오면 안됨
            // 대신 빈값을 넣어서 함수 호출
            talk.SetMsg("");
            return;
        }
        else
        {
            // questTalkIndex값을 더해주어서 GetTalk에서 지정된 대화내용을 불러옴
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }

        // 대화가 끝났을 경우
        // talkData가 null인 경우 isAction을 false로 하고 대화 종료
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            // Quest 체크하기(상황에 따라 다음 퀘스트로 넘길지 말지 결정)
            questText.text = questManager.CheckQuest(id);
            return;
        }

        // Object name
        nameText.text = name;
        if (isNPC)
        {
            // Split을 통해 문자열을 나누게 되면 배열로 바뀌게 됨
            talk.SetMsg(talkData.Split(':')[0]);

            // Parse : 문자열을 해당 타입으로 변경하는 함수
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);

            // Animation Portrait
            // 이전 초상화와 달라질 경우 초상화이펙트 실행
            if (prevPortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doMove");
                prevPortrait = portraitImg.sprite;
            }
        }
        else
        {
            talk.SetMsg(talkData);

            portraitImg.color = new Color(1, 1, 1, 0);
        }
        // 대화중이라면 계속 isAction은 true
        isAction = true;
        // talkIndex 증가
        talkIndex++;
    }

    public void GameSave()
    {
        // PlayerPrefs : 간단한 데이터 저장을 위한 클래스
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        // PlayerX라는 키가 없는 경우(한번도 Save하지 않고 게임이 실행된 경우) 불러오기를 하지 않음
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex= PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
