using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlantScript : MonoBehaviour
{
    public DialogueBoxScript dialogueBox;
    PlayerMainScript playerMain;
    public PlayerEquipmentScript playerEquipment;

    bool isNear = false;

    void Awake()
    {
        playerMain = GameObject.Find("Player").GetComponent<PlayerMainScript>();
        playerEquipment = GameObject.Find("Player").GetComponent<PlayerEquipmentScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isNear)
        {
            TriggerDialogue();
        }
    }

    void TriggerDialogue()
    {
        playerEquipment.pickedUpFlower = true;
        RemovePrompt();
        Destroy(gameObject);
    }
        void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            if (playerEquipment.pickedUpCard)
            {
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
        dialogueBox.startPrompt.text = "I need the keycard to retrieve the flower...";

    }
    void PromptEnterConversationReady()
    {
        dialogueBox.startPrompt.GameObject().SetActive(true);
        dialogueBox.startPrompt.text = "I remember our promise now Ariane. (F to take the flower)";

    }
    
    void RemovePrompt()
    {
        dialogueBox.startPrompt.GameObject().SetActive(false);
    }
}
