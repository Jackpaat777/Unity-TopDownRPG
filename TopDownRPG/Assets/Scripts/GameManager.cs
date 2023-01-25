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
        // ���� ����� ����ϱ� ��ư�� ���ؼ��� ���� -> ��ũ��Ʈ���� ���� (GameObject�� �⺻�Լ� SetActive�� �ν����� â���� �Ҵ� ����)
        if (Input.GetButtonDown("Cancel"))
        {
            // �̹� �������� �� ESC ��ư�� ������ ����
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
        // �߿�!
        // scanObject�� ObjData�� ����ϰ� �ʹٸ� GetComponent�� �ѹ� ȣ���� �� ���
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNPC, objData.ObjectName);

        // isAction�� ���� Panel�� �ִϸ��̼��� �÷ȴ� ���ȴ� �ϱ�
        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNPC, string name)
    {
        int questTalkIndex;
        string talkData;

        if (talk.isAnim)
        {
            // Ÿ���� �ִϸ��̼� �����̶�� ���� ��ȭ�� �ҷ����� �ȵ�
            // ��� ���� �־ �Լ� ȣ��
            talk.SetMsg("");
            return;
        }
        else
        {
            // questTalkIndex���� �����־ GetTalk���� ������ ��ȭ������ �ҷ���
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }

        // ��ȭ�� ������ ���
        // talkData�� null�� ��� isAction�� false�� �ϰ� ��ȭ ����
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            // Quest üũ�ϱ�(��Ȳ�� ���� ���� ����Ʈ�� �ѱ��� ���� ����)
            questText.text = questManager.CheckQuest(id);
            return;
        }

        // Object name
        nameText.text = name;
        if (isNPC)
        {
            // Split�� ���� ���ڿ��� ������ �Ǹ� �迭�� �ٲ�� ��
            talk.SetMsg(talkData.Split(':')[0]);

            // Parse : ���ڿ��� �ش� Ÿ������ �����ϴ� �Լ�
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);

            // Animation Portrait
            // ���� �ʻ�ȭ�� �޶��� ��� �ʻ�ȭ����Ʈ ����
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
        // ��ȭ���̶�� ��� isAction�� true
        isAction = true;
        // talkIndex ����
        talkIndex++;
    }

    public void GameSave()
    {
        // PlayerPrefs : ������ ������ ������ ���� Ŭ����
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        // PlayerX��� Ű�� ���� ���(�ѹ��� Save���� �ʰ� ������ ����� ���) �ҷ����⸦ ���� ����
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
