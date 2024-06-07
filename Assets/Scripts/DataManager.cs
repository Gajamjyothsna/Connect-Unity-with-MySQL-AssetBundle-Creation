using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class DataManager : MonoBehaviour
{
    [SerializeField] private GameObject StudentUIPage;
    [SerializeField] private GameObject SubjectUIPage;
    [SerializeField] private GameObject ChapterUIPage;
    [SerializeField] private GameObject TopicsUIPage;
    [SerializeField] private GameObject DataPrefab;
    [SerializeField] private RectTransform SubjectParentPosition;
    [SerializeField] private RectTransform ChapterParentPosition;
    [SerializeField] private RectTransform TopicParentPosition;

    [SerializeField] private GameObject UIDataPanel;
    [SerializeField] private GameObject AssetBundleLoadPanel;

    [SerializeField] private InputField studentInputField;
    private string studentId;
    private Text resultText;
    private DataItemView DataItemViewHandler;
    private GameObject PreviousInstantiatedAssetBundleObject;

    public void SubmitButtonClick()
    {
        studentId = studentInputField.text;
        StartCoroutine(GetStudentDetails());
    }

    IEnumerator GetStudentDetails()
    {
        string url = "http://localhost/StudentDataBaseProject/SubjectDetails.php";

        Debug.Log("Sending StudentId: " + studentId);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes($"studentId={studentId}");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("JSON Response: " + jsonResponse);

                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    Subject[] subjects = JsonFormatter.FromJson<Subject>(jsonResponse);

                    foreach(var item in subjects)
                    {
                        Debug.Log("ID" +  item.subject_id);
                        Debug.Log("Name" + item.subject_name);
                    }
                    DisplaySubjects(subjects);
                }
                else
                {
                    Debug.LogError("Empty JSON response");
                }
            }
        }
    }

    [System.Serializable]
    public class Subject
    {
        public int subject_id;
        public string subject_name;
    }
    void DisplaySubjects(Subject[] subjects)
    {
       SubjectUIPage.SetActive(true);
        StudentUIPage.SetActive(false);

        foreach (var item in subjects)
        {
            GameObject DataPrefabObject = Instantiate(DataPrefab, SubjectParentPosition);

            DataPrefabObject.GetComponent<DataItemView>().Init(item.subject_id.ToString(), item.subject_name);

            DataItemViewHandler = DataPrefabObject.GetComponent<DataItemView>();

            if(DataItemViewHandler != null)
            {
                DataItemViewHandler.onDataItemClick += ShowSubjectChapters;
            }

        }

    }

    [System.Serializable]
    public class Chapter
    {
        public int chapter_id;
        public string chapter_name;
    }

    void ShowSubjectChapters(string SubjectId)
    {
        Debug.Log("Subject Id" + SubjectId);
        StartCoroutine(GetSubjectDetails(SubjectId));
    }

    IEnumerator GetSubjectDetails(string subjectId)
    {

        string url = "http://localhost/StudentDataBaseProject/ChapterDetails.php";

        Debug.Log("Sending SubjectId: " + subjectId);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes($"subjectId={subjectId}");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("JSON Response: " + jsonResponse);

                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    Chapter[] chapters = JsonFormatter.FromJson<Chapter>(jsonResponse);

                    if (DataItemViewHandler != null)
                    {
                        DataItemViewHandler.onDataItemClick -= ShowSubjectChapters;
                    }
                    

                    foreach (var item in chapters)
                    {
                        Debug.Log("ID: " + item.chapter_id);
                        Debug.Log("Name: " + item.chapter_name);
                    }
                    DisplayChapters(chapters);
                }
                else
                {
                    Debug.LogError("Empty JSON response");
                }
            }

        }
    }

    void DisplayChapters(Chapter[] chapters)
    {
        StudentUIPage.SetActive(false);
        SubjectUIPage.SetActive(false) ;
        ChapterUIPage.SetActive(true);

        foreach (var item in chapters)
        {
            GameObject DataPrefabObject = Instantiate(DataPrefab, ChapterParentPosition);

            DataPrefabObject.GetComponent<DataItemView>().Init(item.chapter_id.ToString(), item.chapter_name);

            DataItemViewHandler = DataPrefabObject.GetComponent<DataItemView>();

            if (DataItemViewHandler != null)
            {
                DataItemViewHandler.onDataItemClick += ShowChapterTopics;
            }

        }

    }

    void ShowChapterTopics(string chapterId)
    {
        Debug.Log("Topic Id : " + chapterId);
        StartCoroutine(GetTopicDetails(chapterId));
    }

    IEnumerator GetTopicDetails(string chapterId)
    {
        string url = "http://localhost/StudentDataBaseProject/TopicDetails.php";
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes($"chapterId={chapterId}");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("JSON Response: " + jsonResponse);

                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    Topic[] topics = JsonFormatter.FromJson<Topic>(jsonResponse);

                    if (DataItemViewHandler != null)
                    {
                        DataItemViewHandler.onDataItemClick -= ShowChapterTopics;
                    }
                    DisplayTopics(topics);

                   
                }
                else
                {
                    Debug.LogError("Empty JSON response");
                }
            }
        }
    }

    private void DisplayTopics(Topic[] topics)
    {
        StudentUIPage.SetActive(false);
        SubjectUIPage.SetActive(false);
        ChapterUIPage.SetActive(false);
        TopicsUIPage.SetActive(true);

        foreach (var item in topics)
        {
            GameObject DataPrefabObject = Instantiate(DataPrefab, TopicParentPosition);

            DataPrefabObject.GetComponent<DataItemView>().Init(item.topic_id.ToString(), item.topic_name);

            DataItemViewHandler = DataPrefabObject.GetComponent<DataItemView>();

            DataPrefabObject.GetComponent<Button>().onClick.RemoveAllListeners();
            DataPrefabObject.GetComponent<Button>().onClick.AddListener(() => CreateAssetBundle(item.topic_id));
        }
    }

    private void CreateAssetBundle(int topicID)
    {
        Debug.Log("Topic Id" + topicID);
        StartCoroutine(DownloadAssetBundlesFromServer(topicID-1));
    }

    IEnumerator DownloadAssetBundlesFromServer(int index)
    {
        UIDataPanel.SetActive(false );
        AssetBundleLoadPanel.SetActive(true);

        GameObject go = null;

        string url = "";

        switch(index)
        {
            case 0:
                url = "https://drive.google.com/uc?export=download&id=1iRrpNkDadYedOgwBWzyuih4z-0XcK82s";
                Debug.Log("Case 0 Url");
            break;
            case 1:
                url = "https://drive.google.com/uc?export=download&id=1z9cEpwb9G6P7KJ8V56sVbx98lWRYbWv1";
                Debug.Log("Case 1 Url");
                break;
        }
        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error on the get request at :" + url + "Error is :" + www.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www); //Get content from url

                if (bundle != null)
                {
                    string[] assetNames = bundle.GetAllAssetNames();
                    if (assetNames.Length > 0)
                    {
                        go = bundle.LoadAsset<GameObject>(assetNames[0]);
                        bundle.Unload(false);
                    }
                    else
                    {
                        Debug.LogError("No assets found in the bundle.");
                    }
                }
                else
                {
                    Debug.LogError("Failed to load AssetBundle.");
                }

                yield return new WaitForEndOfFrame();
            }

            www.Dispose();
        }

        if (PreviousInstantiatedAssetBundleObject != null)
        {
            DestroyImmediate(PreviousInstantiatedAssetBundleObject);
        }

        if (go != null)
        {
            InstantiateGameObjectFromAssetBundle(go);
        }
        else
        {
            Debug.LogError("GameObject from AssetBundle is null.");
        }
    }

    private void InstantiateGameObjectFromAssetBundle(GameObject go)
    {
        if (go != null)
        {
            PreviousInstantiatedAssetBundleObject = Instantiate(go);
            PreviousInstantiatedAssetBundleObject.transform.position = Vector3.zero;
        }
        else
        {
            Debug.Log("Your Asset Bundle Object is null");
        }
    }

    [System.Serializable]
    public class Topic
    {
        public int topic_id;
        public string topic_name;
    }

    public void BackToUIScreen()
    {
        StudentUIPage.SetActive(true);
        SubjectUIPage.SetActive(false) ;
        TopicsUIPage.SetActive(false);
        ChapterUIPage.SetActive(false);

        AssetBundleLoadPanel.SetActive(false);
        UIDataPanel.SetActive(true) ;

        ClearInstantiatedObjects(SubjectParentPosition);
        ClearInstantiatedObjects(ChapterParentPosition);
        ClearInstantiatedObjects(TopicParentPosition);

    }

    private void ClearInstantiatedObjects(RectTransform ParentPanel)
    {
        while(ParentPanel.childCount > 0)
        {
            var child = ParentPanel.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }
}



public static class JsonFormatter
{
    public static T[] FromJson<T>(string json)
    {
        // Directly deserialize the array from the JSON string
        return JsonUtility.FromJson<Wrapper<T>>(WrapJsonArray(json)).data;
    }

    public static string ToJson<T>(T[] array)
    {
        return JsonUtility.ToJson(new Wrapper<T> { data = array });
    }


    private static string WrapJsonArray(string jsonArray)
    {
        return "{\"data\":" + jsonArray + "}";
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] data;
    }
}
