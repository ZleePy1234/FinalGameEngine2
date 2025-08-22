using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ItemPickup : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.F) && isNear)
        {
            PickupItem();
        }
    }
    void PickupItem()
    {
        playerEquipment.pickedUpCard = true;
        StartCoroutine(DestroyObject());
    }
    private IEnumerator DestroyObject()
    {
        GameObject pass = GetComponent<Transform>().GetChild(0).gameObject;
        dialogueBox.startPrompt.GameObject().SetActive(false);
        Destroy(pass);
        Destroy(gameObject);
        yield return new WaitForSeconds(0.1f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            PromptEnterConversationReady();
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

    void PromptEnterConversationReady()
    {
        dialogueBox.startPrompt.GameObject().SetActive(true);
        dialogueBox.startPrompt.text = "Press F to pick up Alina's Keycard";

    }
    
    void RemovePrompt()
    {
        dialogueBox.startPrompt.GameObject().SetActive(false);
    }
}
