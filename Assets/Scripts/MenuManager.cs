using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

public class MenuManager : MonoBehaviour
{
    [System.Serializable]
    class SaveData
    {
        public InputField input;
    }
public InputField input;
public static MenuManager Instance;

public void SaveInput()
{
    SaveData data = new SaveData();
    data.input = input;
    string json = JsonUtility.ToJson(data);
    File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
}

public void LoadInput()
{
    string path = Application.persistentDataPath + "/savefile.json";
    if (File.Exists(path))
    {
        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        input = data.input;
    }
}

void Awake()
{
    //input = GameObject.Find("InputField").GetComponent<InputField>();
    
    if (Instance != null)
    {
        Destroy(gameObject);
        return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
    LoadInput();
}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
