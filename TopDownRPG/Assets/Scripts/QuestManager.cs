using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; // �ϳ��� ����Ʈ�� ������ �ο��ϱ� ���� ����
    public GameObject[] questObject;

    // QuestData�� ����ü�� ���ǹǷ� Dictionary�� �־ ���
    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("���� ������ ��ȭ�ϱ�", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("�絵�� ���� ã���ֱ�", new int[] { 10000, 20000, 30000, 2000 }));
        questList.Add(30, new QuestData("����Ʈ �� Ŭ����!", new int[] { 0 }));    // ���� ������
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        // Next Talk Target
        // ���� ���� ������Ʈ�� id�� ��������Ʈ���� npc�� id�� ���� ��쿡�� ����Ʈ�� ���� ��ȭ������ �ѱ��
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        // Control Quest Object
        // questActionIndex�� ������ �ڿ�(��ȭ�� ���� �ڿ�) Object�� �������ϹǷ� ó������ ȣ���ϸ� �ȵ�
        ControlObject();

        // Talk Complete & Next Quest
        // ���� ����Ʈ�� ��ȭ�� ��������Ʈ���� npc id�� ������ �����ϴٸ�(��� ��ȭ�� �Ϸ��ߴٸ�) ���� ����Ʈ��(���� ����Ʈ �Ϸ�)
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        // Now Quest Name
        return questList[questId].questName;
    }

    // Overloading : �Ű������� ���� ���� �̸��� �Լ� ���
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
                // 10�� ����Ʈ���� 2��� ��ȭ�� ����Ǿ��� ���
                if (questActionIndex == 2)
                {
                    questObject[0].SetActive(true);
                    questObject[1].SetActive(false);
                    questObject[2].SetActive(false);
                }
                break;
            case 20:
                // �ҷ����⸦ ���� �� ������ ������� ���� ����
                if (questActionIndex == 0)
                {
                    questObject[0].SetActive(true);
                    questObject[1].SetActive(false);
                    questObject[2].SetActive(false);
                }
                // 20�� ����Ʈ���� ���� ������ ��ȣ�ۿ� ���� ���
                else if (questActionIndex == 1)
                {
                    questObject[0].SetActive(false);
                    questObject[1].SetActive(true);
                    questObject[2].SetActive(false);
                }
                // 20�� ����Ʈ���� ���� ������ ��ȣ�ۿ� ���� ���
                else if (questActionIndex == 2)
                {
                    questObject[0].SetActive(false);
                    questObject[1].SetActive(false);
                    questObject[2].SetActive(true);
                }
                // 20�� ����Ʈ���� �ݻ� ������ ��ȣ�ۿ� ���� ���
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
