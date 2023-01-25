using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; // 하나의 퀘스트에 순서를 부여하기 위한 변수
    public GameObject[] questObject;

    // QuestData는 구조체로 사용되므로 Dictionary에 넣어서 사용
    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("마을 사람들과 대화하기", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("루도의 동전 찾아주기", new int[] { 10000, 20000, 30000, 2000 }));
        questList.Add(30, new QuestData("퀘스트 올 클리어!", new int[] { 0 }));    // 더미 데이터
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        // Next Talk Target
        // 지금 만난 오브젝트의 id와 현재퀘스트에서 npc의 id가 같을 경우에만 퀘스트의 다음 대화순서로 넘기기
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        // Control Quest Object
        // questActionIndex가 증가한 뒤에(대화가 끝난 뒤에) Object가 보여야하므로 처음부터 호출하면 안됨
        ControlObject();

        // Talk Complete & Next Quest
        // 지금 퀘스트의 대화가 현재퀘스트에서 npc id의 개수와 동일하다면(모두 대화를 완료했다면) 다음 퀘스트로(현재 퀘스트 완료)
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        // Now Quest Name
        return questList[questId].questName;
    }

    // Overloading : 매개변수에 따라 같은 이름의 함수 사용
    public string CheckQuest()
    {
        // Now Quest Name
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    public void ControlObject()
    {
        switch (questId)
        {
            case 10:
                // 10번 퀘스트에서 2명과 대화가 종료되었을 경우
                if (questActionIndex == 2)
                {
                    questObject[0].SetActive(true);
                    questObject[1].SetActive(false);
                    questObject[2].SetActive(false);
                }
                break;
            case 20:
                // 불러오기를 했을 때 동전이 사라지는 버그 방지
                if (questActionIndex == 0)
                {
                    questObject[0].SetActive(true);
                    questObject[1].SetActive(false);
                    questObject[2].SetActive(false);
                }
                // 20번 퀘스트에서 동색 동전과 상호작용 했을 경우
                else if (questActionIndex == 1)
                {
                    questObject[0].SetActive(false);
                    questObject[1].SetActive(true);
                    questObject[2].SetActive(false);
                }
                // 20번 퀘스트에서 은색 동전과 상호작용 했을 경우
                else if (questActionIndex == 2)
                {
                    questObject[0].SetActive(false);
                    questObject[1].SetActive(false);
                    questObject[2].SetActive(true);
                }
                // 20번 퀘스트에서 금색 동전과 상호작용 했을 경우
                else if (questActionIndex == 3)
                {
                    questObject[0].SetActive(false);
                    questObject[1].SetActive(false);
                    questObject[2].SetActive(false);
                }
                break;
        }
    }
}
