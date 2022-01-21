[System.Serializable]
public class GameDataPlayer {
    public string namePlayer;
    public float PreviusLevelExp;
    public float totalExp;
    public float PlayerLevel;
    public float totalPlant;
    

    public GameDataPlayer(PlayerProfile playerProfile) {
        namePlayer = playerProfile.namePlayer;
        PreviusLevelExp = playerProfile.PreviusLevelExp;
        totalExp = playerProfile.totalExp;
        PlayerLevel = playerProfile.PlayerLevel;
        totalPlant = playerProfile.totalPlant;
    }
}

[System.Serializable]
public class GameDataTutorial {
    public int CurrentButton;
    public bool tutorial;

    public GameDataTutorial(Scr_tutorial scr_Tutorial) {
        CurrentButton = scr_Tutorial.CurrentButton;
        tutorial = scr_Tutorial.tutorial;
    }
}

[System.Serializable]
public class GameDataInventory {

    public string[] itemIDs;

    public GameDataInventory(PlayerInventory playerInventory) {

        itemIDs = new string[PlayerInventory.GetPlayerInventory().GetSize];

        PlayerInventory inventory;

        if (playerInventory == null) {
            inventory = PlayerInventory.GetPlayerInventory();
        } else { inventory = playerInventory; }

        for (int i = 0; i < inventory.GetSize; i++) {

            if(inventory.GetItemInSlot(i) != null) {
                Item item = inventory.GetItemInSlot(i);
                itemIDs[i] = item.ItemID;
            }
        }
    }
}
