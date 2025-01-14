using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class TitleButtonNavigation : MonoBehaviour
{
    [SerializeField, Label("���߂���")]
    public Button m_StartButton;
    [SerializeField, Label("�}��")]
    public Button m_BookButton;
    [SerializeField, Label("�I��")]
    public Button m_EndButton;

    [SerializeField, Label("�I�����Ȃ�")]
    public Button m_OFFButton;
    [SerializeField, Label("�I������")]
    public Button m_ONButton;
    [SerializeField, Label("�}�Ӊ�ʂ̃{�^��")]
    public Button m_OpOFFBookButton;

    private List<Button> buttons; // �{�^�����X�g
    private int currentIndex = 0; // ���݂̃t�H�[�J�X�ʒu

    // �\���E��\���ɂ���p�l���̎Q��
    [SerializeField, Label("���C���̃p�l��")]
    public GameObject currentPanel;
    [SerializeField, Label("�I���p�̃p�l��")]
    public GameObject targetPanel;
    [SerializeField, Label("�}�ӂ̃p�l��")]
    public GameObject BookPanel;
    // �R���g���[���[����
    ControllerState m_State;

    private void OnEnable()
    {
        StartCoroutine(SelectButtonOnEnable());
    }

    private IEnumerator SelectButtonOnEnable()
    {
        yield return null; // �t���[���ҋ@
        m_StartButton.Select();
    }

    void Start()
    {
        // �R���g���[���[
        m_State = GetComponent<ControllerState>();

        //=============================================
        // �{�^���ɂ���
        // �{�^�����X�g��������
        buttons = new List<Button> { m_StartButton, m_BookButton, m_EndButton };

        // �ŏ��̃{�^���Ƀt�H�[�J�X�𓖂Ă�
        currentIndex = 0;
        buttons[currentIndex].Select();

        // �{�^���ɃN���b�N�C�x���g��ǉ�
        m_EndButton.onClick.AddListener(OnEndButtonClick);
        m_ONButton.onClick.AddListener(OnEnd_ONButtonClick);
        m_OFFButton.onClick.AddListener(OnEnd_OFFButtonClick);
        m_BookButton.onClick.AddListener(OpenBook);
        m_OpOFFBookButton.onClick.AddListener(CloseBook);
        //=============================================
    }

    void Update()
    {
        // ��L�[�������ꂽ�ꍇ
        if (m_State.GetButtonUp())
        {
            MoveFocus(-1); // ��Ɉړ�
        }
        // ���L�[�������ꂽ�ꍇ
        else if (m_State.GetButtonDown())
        {
            MoveFocus(1); // ���Ɉړ�
        }
        if (m_State.GetButtonMenu())
        {
            BookPanel.SetActive(false);

            // ���C���p�l����\��
            if (currentPanel != null)
            {
                currentPanel.SetActive(true);
            }

            // ���C���p�l���̍ŏ��̃{�^���Ƀt�H�[�J�X���ړ�
            if (m_StartButton != null)
            {
                m_StartButton.Select();
            }
        }
    }

    private void MoveFocus(int direction)
    {
        // �C���f�b�N�X���X�V���A���[�v������
        currentIndex = (currentIndex + direction + buttons.Count) % buttons.Count;

        // �V�����{�^���Ƀt�H�[�J�X���ړ�
        buttons[currentIndex].Select();
    }

    // �I���{�^������
    void OnEndButtonClick()
    {

        // ���݂̃p�l�����\��
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }

        // �^�[�Q�b�g�p�l����\��
        if (targetPanel != null)
        {
            targetPanel.SetActive(true);

            // �^�[�Q�b�g�p�l�����̍ŏ��̃{�^���Ƀt�H�[�J�X���ړ�
            Button targetButton = targetPanel.GetComponentInChildren<Button>();
            if (targetButton != null)
            {
                targetButton.Select();
            }
        }
    }

    void OnEnd_ONButtonClick()
    {
        // �Q�[�����I��
        Application.Quit();

        // Unity�G�f�B�^��Ŏ��s���Ă���ꍇ�͒�~������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif    
    }

    public void OnEnd_OFFButtonClick()
    {
        if (targetPanel != null)
        {
            targetPanel.SetActive(false);
        }

        if (currentPanel != null)
        {
            currentPanel.SetActive(true);

            // �^�[�Q�b�g�p�l�����̍ŏ��̃{�^���Ƀt�H�[�J�X���ړ�
            Button currentButton = currentPanel.GetComponentInChildren<Button>();
            if (currentButton != null)
            {
                currentButton.Select();
            }
        }
    }
    void OpenBook()
    {
        // currentPanel���\��
        currentPanel.SetActive(false);

        // Panel_Button_4��\��
        BookPanel.SetActive(true);
    }
    void CloseBook()
    {
        BookPanel.SetActive(false);

        currentPanel.SetActive(true);

    }
}
