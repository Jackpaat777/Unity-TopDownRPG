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

    // �޼��� ���̱�
    public void SetMsg(string msg)
    {
        // �ִϸ��̼� ���� ���� Ű�� ���� ���
        if (isAnim)
        {
            // ��ȭ�� ��� ����� �� Invoke ����, EffectEnd
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        // �ִϸ��̼��� ������ ��Ȳ�� �ƴ� ���
        else
        {
            // �޼��� ����Ʈ ����
            targetMsg = msg;
            EffectStart();
        }
    }

    // ����Ʈ ���� �ÿ��� ����
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        // CPS : 1�ʿ� �����ִ� Char ���� -> ( 1 / CPS ) : �ϳ��� Char�� ���̴� �ð� 
        // interval Ȯ���� �Ҽ����� ����ϱ� ���� ���� -> �׳� Invoke�� 1 / CPS�� �ϴ� �� ���� �� ������ �� ��
        interval = 1.0f / CharPerSeconds;

        isAnim = true;

        Invoke("Effecting", interval);
    }
    // ����Ʈ ���߿��� ���� �ϳ��� ������
    void Effecting()
    {
        // ��� �ؽ�Ʈ�� �� ���� �ڿ��� End�� �̵�
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];

        // Sound
        // ����� ��ħǥ�� �����ϰ� ���� ���
        if (targetMsg[index] != ' ' || targetMsg[index] != '.' || targetMsg[index] != '?' || targetMsg[index] != '!')
            audioSource.Play();

        index++;

        // Recursive
        Invoke("Effecting", interval);
    }
    // ����Ʈ�� ���� �ڿ��� EndCursor �����ֱ�
    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true);
    }
}
