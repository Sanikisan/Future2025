using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickRoButton : MonoBehaviour
{
    public int KapiIdx = 0;
    SerectKapi DKapi;
    KapiDicData KapiData;
    KapiDictionary KapiDic;

    public GameObject m_camera;
    // �R���g���[���[����
    ControllerState m_State;

    void Start()
    {
        // �R���g���[���[
        m_State = m_camera.GetComponent<ControllerState>();
        DKapi = new SerectKapi();
        KapiDic = new KapiDictionary();
    }

    void Update()
    {
        if (DKapi == null)
            DKapi = FindObjectOfType<SerectKapi>();
        if (KapiData == null)
            KapiData = FindObjectOfType<KapiDicData>();
        if (KapiDic == null)
            KapiDic = FindObjectOfType<KapiDictionary>();

        //�y�[�W�ړ�
        //if (m_State.GetButtonL())
        //{
        //    KapiDic.NextPage();
        //}
        //if (m_State.GetButtonR())
        //{
        //    KapiDic.PreviousPage();
        //}
        ////�����~��
        //if (m_State.GetButtonX())
        //{
        //    ASortKapi();
        //}
        //�o�^(�f�o�b�O�p�Ȃ̂ŃR���g���[���Ή��s�K�v)
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            RegistrationKapi();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            DestroyKapi();
        }

    }

    //�폜(��{����Ăт������ƂȂ�)
    public void DestroyKapi()
    {
        KapiData.DestroyKapi();
        KapiDic.UpdatePage();
    }

    //�o�^(Discord�ɓo�^�̎d��������)
    public void RegistrationKapi()
    {
        int num = 0;
        num = DKapi.GetKapiNumber();

        KapiData.MarkAsKapi(num);
        if (KapiDic.imageSlots != null)
        {
            KapiDic.UpdatePage();
        }
        else
        {
            Debug.LogError("UpdateKapi���Ăяo���O��imageSlots��null�ł��B");
        }
    }

    //����
    public void ASortKapi()
    {
        KapiData.SortKapi(0);
        KapiDic.ImageSlotsSort(true);
        KapiDic.UpdatePage();
    }

    //�~��
    public void DSortKapi()
    {
        KapiData.SortKapi(2);
        KapiDic.UpdatePage();
    }
}
