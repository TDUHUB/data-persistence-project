using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat Instance;
    public string PlayerName;
    public int PlayerScore;
    public string PlayerProgress;
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
        //File.Delete(Application.persistentDataPath + "/savefile.json");
        LoadProgress();
    }
    [System.Serializable]
    class SaveData
    {
        public string PlayerProgress;
        public string PlayerName;
        public int PlayerScore;
    }

    public void SaveProgress()
    {
        SaveData data = new SaveData();
        PlayerProgress = $"Best Score: {PlayerName}: {PlayerScore.ToString()}";
        data.PlayerProgress = PlayerProgress;
        data.PlayerName = PlayerName;
        data.PlayerScore = PlayerScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Progress saved"+ json);
    }
    public void LoadProgress()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerProgress = data.PlayerProgress;
            PlayerScore = data.PlayerScore;
            Debug.Log("Progress loaded: "+ PlayerProgress);
        }
    }
}
