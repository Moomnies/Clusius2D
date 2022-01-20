using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfile : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the Player Settings Menu.")]
    GameObject _PlayerSettingsMenu;

    public float totalExp;
    public float totalPlant;
    [SerializeField] float nextLevelExp;
    public float PreviusLevelExp;
    [SerializeField] float CurrentLevelExp;
    public float PlayerLevel;
    public string namePlayer;
    string[] firstName = { "cute","long","big","tiny","red","blue","fast","rocky","mad","happy"};
    string[] secondName = { "sad","strong","lazy","toasted", "disrupted", "super","sandy","round","yellow","cool","normal"};
    string[] thirdName = { "dragon","box","farmer","scarecrow","panda","pumkin","egg","chicken","dude","girl","horse","dog"};
    [SerializeField] TMPro.TextMeshProUGUI levelText;
    [SerializeField] TMPro.TextMeshProUGUI ExpText;
    [SerializeField] TMPro.TextMeshProUGUI PlantText;
    [SerializeField] TMPro.TextMeshProUGUI nameText;
    [SerializeField] Slider sliderExp;

    private void Start()
    {
        LoadPlayer();
        nameText.text = namePlayer;
        earnExp(0);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        GameDataPlayer data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            namePlayer = data.namePlayer;
            PreviusLevelExp = data.PreviusLevelExp;
            totalExp = data.totalExp;
            PlayerLevel = data.PlayerLevel;
            totalPlant = data.totalPlant;
        }
    }

    public void DeleteFiles()
    {
        SaveSystem.DeleteFiles();
    }

    public void OpenPlayerSettings()
    {
        if (_PlayerSettingsMenu != null && !_PlayerSettingsMenu.activeSelf)
        {
            _PlayerSettingsMenu.SetActive(true);
        }
    }

    public void ClosePlayerSettings()
    {
        if (_PlayerSettingsMenu != null && _PlayerSettingsMenu.activeSelf)
        {
            _PlayerSettingsMenu.SetActive(false);
        }
    }

    public void earnPlant(float amount)
    {
        totalPlant += amount;
        PlantText.text = totalPlant.ToString();
    }
    public void earnExp(float amount)
    {
        totalExp += amount;
        nextLevelExp = 100 * Mathf.Sqrt(PlayerLevel);
        CurrentLevelExp = (totalExp - PreviusLevelExp);

        if (totalExp > (PreviusLevelExp + nextLevelExp))
        {
            PreviusLevelExp = totalExp;
            PlayerLevel++;
        }
        sliderExp.maxValue = nextLevelExp;
        sliderExp.value = CurrentLevelExp;
        ExpText.text = ((int)CurrentLevelExp).ToString() + "/" + ((int)nextLevelExp).ToString();
        levelText.text = PlayerLevel.ToString();
    }

    public void changeName()
    {
        namePlayer = (firstName[(int)Random.Range(0, firstName.Length)] +" "+ secondName[(int)Random.Range(0, secondName.Length)] +" "+ thirdName[(int)Random.Range(0, thirdName.Length)]);
        nameText.text = namePlayer;
    }
}
 