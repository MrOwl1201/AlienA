using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactionUI;
    public TextMeshProUGUI dialogueText;
    private bool isInRange = false;
    private int currentDialogueIndex = 0;
    private string[] dialogues = {
        "Chào người tiên phong",
        "Bạn đã tiêu diệt thủ lĩnh của hành tinh này",
        "Khiến đối thủ của chúng ta không có lý do để ngăn cản chúng ta",
        "Bây giờ chúng ta sẽ bắt đầu xâm chiếm hành tinh này",
        "Bạn đã hoàn thành nhiệm vụ bây giờ bạn hãy nghỉ ngơi",
        "Các phần thưởng và huân chương dành cho bạn đang ở trụ sở",
        "Hãy đến đấy và nhận nhứng thứ xứng đáng với công lao của bạn",
        "Hãy nhấn vào nút phía trên",
    };

    private void Start()
    {
        Time.timeScale = 1f;
        interactionUI.SetActive(false);
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.R))
        {

                ShowDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            interactionUI.SetActive(false);
        }
    }

    private void ShowDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            interactionUI.SetActive(false);
        }
    }
}
