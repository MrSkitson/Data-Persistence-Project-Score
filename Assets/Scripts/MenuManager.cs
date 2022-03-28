using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string savedName;
    public string currentName = "Name";
    public int bestScore = 10;
    public TMP_InputField nameText;
     public Text BestScoreText;
   

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int bestScore;
    }
    

// save data json
public void SaveInput()
{
    SaveData data = new SaveData();
    data.name = savedName;
    data.bestScore = bestScore;
    string json = JsonUtility.ToJson(data);
    File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
}
// load data json
public void LoadInput()
{
    string path = Application.persistentDataPath + "/savefile.json";
    if (File.Exists(path))
    {
        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        savedName = data.name;
        bestScore = data.bestScore;
    }
}



    // Start is called before the first frame update
    void Start()
    {
       BestScoreText.text = "Best Score: " + MenuManager.Instance.savedName + ": " + MenuManager.Instance.bestScore;
    }
    // start new game from menu
    public void StartNew()
    {
        currentName = nameText.text;
        SceneManager.LoadScene(1);
    }
    // for working buuton Exit in Menu game
    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else 
        Application.Quit();
        #endif
        MenuManager.Instance.SaveInput();
    }
    
    public void GetInput(string name)
    {
        Debug.Log("You Entered " + name);
        
    }

    
}
