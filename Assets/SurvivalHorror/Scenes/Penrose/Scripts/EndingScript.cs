using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    DialogueBoxScript dialogueBox;
    PlayerMainScript playerMain;
    public PlayerEquipmentScript playerEquipment;

    public bool isNear = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dialogueBox = GameObject.FindWithTag("DialogueBox").GetComponent<DialogueBoxScript>();
        playerMain = GameObject.Find("Player").GetComponent<PlayerMainScript>();
        playerEquipment = GameObject.Find("Player").GetComponent<PlayerEquipmentScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerEquipment.pickedUpFlower && isNear)
        {
            TriggerEnding();
        }
    }
    
    void TriggerEnding()
    {
        SceneManager.LoadScene("Win");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerEquipment.pickedUpFlower)
            {
                isNear = true;
                PromptEnterConversationReady();
            }
            else
            {
                PromptEnterConversationNotReady();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            RemovePrompt();
        }
    }

    void PromptEnterConversationNotReady()
    {
        dialogueBox.startPrompt.GameObject().SetActive(true);
        dialogueBox.startPrompt.text = "Press ⚠ to talk to ⚠⚠⚠ (I cant Remember, I need the flower...)";

    }
    void PromptEnterConversationReady()
    {
        dialogueBox.startPrompt.GameObject().SetActive(true);
        dialogueBox.startPrompt.text = "Remember our Promise (Press F to talk to Ariane)";

    }
    
    void RemovePrompt()
    {
        dialogueBox.startPrompt.GameObject().SetActive(false);
    }
}
