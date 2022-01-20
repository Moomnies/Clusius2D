[System.Serializable]
public class GameDataPlayer
{
    public string namePlayer;
    public float PreviusLevelExp;
    public float totalExp;
    public float PlayerLevel;
    public float totalPlant;



    public GameDataPlayer(PlayerProfile playerProfile)
    {
        namePlayer = playerProfile.namePlayer;
        PreviusLevelExp = playerProfile.PreviusLevelExp;
        totalExp = playerProfile.totalExp;
        PlayerLevel = playerProfile.PlayerLevel;
        totalPlant = playerProfile.totalPlant;
    }
}

[System.Serializable]
public class GameDataTutorial
{
    public int CurrentButton;
    public bool tutorial;

    public GameDataTutorial(Scr_tutorial scr_Tutorial)
    {
        CurrentButton = scr_Tutorial.CurrentButton;
        tutorial = scr_Tutorial.tutorial;
    }
}
