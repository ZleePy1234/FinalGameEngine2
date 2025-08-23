using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public Transform playerTransform;
    public int health;
    public bool hasCamera;
    public bool hasGun;
    public bool hasKey;
    public bool hasFlower;
    public int reserveAmmo;
    public int currentAmmo;
    public int cameraTape;

    public DialogueScript dialogueElster;
    public DialogueScript dialogueKolibri;
    public DialogueScript dialogueAra;
    public int dialogueElsterLine;
    public int dialogueKolibriLine;
    public int dialogueAraLine;

    public KeyCode saveKey = KeyCode.F5;
    public KeyCode loadKey = KeyCode.F6;

    private PlayerMainScript playerMainScript;
    private PlayerEquipmentScript playerEquipmentScript;

    public void Awake()
    {
        playerMainScript = GameObject.Find("Player").GetComponent<PlayerMainScript>();
        playerEquipmentScript = GameObject.Find("Player").GetComponent<PlayerEquipmentScript>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(saveKey))
        {
            if(playerEquipmentScript.cameraTape <= 0)
            {
                Debug.LogWarning("Cannot save game, no camera tape available.");
                return;
            }
            playerEquipmentScript.cameraTape--;
            Debug.Log("Camera tape used, remaining: " + playerEquipmentScript.cameraTape);
            SaveGame();
        }
        if (Input.GetKeyDown(loadKey))
        {
            LoadGame();
        }
        if (playerMainScript != null && playerEquipmentScript != null)
        {
            health = playerMainScript.currentHealth;
            hasCamera = playerEquipmentScript.pickedUpCamera;
            hasGun = playerEquipmentScript.pickedUpGun;
            hasKey = playerEquipmentScript.pickedUpCard;
            hasFlower = playerEquipmentScript.pickedUpFlower;
            reserveAmmo = playerEquipmentScript.reserveAmmo;
            currentAmmo = playerEquipmentScript.currentAmmo;
            cameraTape = playerEquipmentScript.cameraTape;
            dialogueElsterLine = dialogueElster.dialogueCount;
            dialogueKolibriLine = dialogueKolibri.dialogueCount;
            dialogueAraLine = dialogueAra.dialogueCount;
        }

    }
    public void SaveGame()
    {
        PlayerData playerData = new PlayerData();
        playerData.position = new float[] { playerTransform.position.x, playerTransform.position.y, playerTransform.position.z };
        playerData.health = health;
        playerData.hasCamera = hasCamera;
        playerData.hasGun = hasGun;
        playerData.hasKey = hasKey;
        playerData.hasFlower = hasFlower;
        playerData.reserveAmmo = reserveAmmo;
        playerData.currentAmmo = currentAmmo;
        playerData.cameraTape = cameraTape;
        playerData.dialogueElsterLine = dialogueElsterLine;
        playerData.dialogueKolibriLine = dialogueKolibriLine;
        playerData.dialogueAraLine = dialogueAraLine;

        string json = JsonUtility.ToJson(playerData);
        string filePath = Application.persistentDataPath + "/playerData.json";
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log("Game saved to " + filePath);
        Debug.Log(json);
    }
    public void LoadGame()
    {
        string filePath = Application.persistentDataPath + "/playerData.json";
        if (!System.IO.File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found at " + filePath);
            return;
        }
        string json = System.IO.File.ReadAllText(filePath);
        PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("Loading game from " + filePath);

        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        if (playerData != null)
        {
            playerTransform.position = new Vector3(loadedData.position[0], loadedData.position[1], loadedData.position[2]);
            health = loadedData.health;
            hasCamera = loadedData.hasCamera;
            hasGun = loadedData.hasGun;
            hasKey = loadedData.hasKey;
            hasFlower = loadedData.hasFlower;
            reserveAmmo = loadedData.reserveAmmo;
            currentAmmo = loadedData.currentAmmo;
            cameraTape = loadedData.cameraTape;
            dialogueElsterLine = loadedData.dialogueElsterLine;
            dialogueKolibriLine = loadedData.dialogueKolibriLine;
            dialogueAraLine = loadedData.dialogueAraLine;

            // Update the player's state based on loaded data
            if (playerMainScript != null)
            {
                playerMainScript.currentHealth = health;
                playerTransform.position = new Vector3(playerData.position[0], playerData.position[1], playerData.position[2]);
                playerEquipmentScript.pickedUpCamera = hasCamera;
                playerEquipmentScript.pickedUpGun = hasGun;
                playerEquipmentScript.pickedUpCard = hasKey;
                playerEquipmentScript.pickedUpFlower = hasFlower;
                playerEquipmentScript.reserveAmmo = reserveAmmo;
                playerEquipmentScript.currentAmmo = currentAmmo;
                playerEquipmentScript.cameraTape = cameraTape;
                dialogueElster.dialogueCount = dialogueElsterLine;
                dialogueKolibri.dialogueCount = dialogueKolibriLine;
                dialogueAra.dialogueCount = dialogueAraLine;
            }
                
        }
    }
}
