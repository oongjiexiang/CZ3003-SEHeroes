using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;
using UnityEngine.Networking;

// This class handles the general GET, POST, PUT and DELETE HTTP requests. Other classes will obtain data from this class
public class API_Connection
{
    private readonly string baseURL = "https://seheroes.herokuapp.com/";
    
    // Handles GET request
    public IEnumerator GetData(string apiEndpoint, Dictionary<string, string> queryParams, System.Action<string> callback = null)   //Verified: Working
    {
        string fullURL = baseURL + apiEndpoint;
        if(queryParams != null)
        {
            fullURL += "?";
            foreach(var param in queryParams)
            {
                fullURL += (param.Key + "=" + param.Value + "&");
            }
            fullURL += "\b";
        }
        UnityWebRequest response = UnityWebRequest.Get(fullURL);
        yield return response.SendWebRequest();
        
        if (response.isNetworkError || response.isHttpError)
        {
            yield break;
        }
        if(callback != null){
            callback(response.downloadHandler.text);
        }
    }

    // Handles POST request
    public IEnumerator PostData(string apiEndpoint, string json_toAdd, System.Action<string> callback = null){
        string fullURL = baseURL + apiEndpoint;

        var addRequest = new UnityWebRequest(fullURL, "POST");
        byte[] bytes_toAdd = System.Text.Encoding.UTF8.GetBytes(json_toAdd);
        addRequest.uploadHandler = (UploadHandler) new UploadHandlerRaw(bytes_toAdd);
        addRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        addRequest.SetRequestHeader("Content-Type", "application/json");
        yield return addRequest.SendWebRequest();
        if (addRequest.isNetworkError){
            Debug.Log(addRequest.error);
        }
        else{
            Debug.Log(addRequest.downloadHandler.text);
        }
        if(callback != null){
            callback(addRequest.downloadHandler.text);
        }
    }

    // Handles PUT Request
    public IEnumerator PutData(string apiEndpoint, string json_toModify, System.Action<string> callback = null)
    {   // PUT method: as extra compiler must be installed for dynamic type, pass json instead of whole object
        string fullURL = baseURL + apiEndpoint;
        UnityWebRequest putRequest;
        byte[] bytes_toModify = System.Text.Encoding.UTF8.GetBytes(json_toModify);

        //Using UnityWebRequest to do a put request to the database
        using (putRequest = UnityWebRequest.Put(fullURL, bytes_toModify))
        {
            putRequest.SetRequestHeader("Content-Type", "application/json");
            yield return putRequest.SendWebRequest();
            if (putRequest.isNetworkError)
            {
                Debug.Log(putRequest.error);
            }
            else
            {
                Debug.Log(putRequest.downloadHandler.text);
            }
        }
        if(callback != null){
            try{
                callback(putRequest.downloadHandler.text);
            }
            catch(Exception){}
        }
    }

    // Handles DELETE Request
    public IEnumerator DeleteData(string apiEndpoint, System.Action<string> callback = null){  // Verified: Working
        string fullURL = baseURL + apiEndpoint;
        UnityWebRequest response = UnityWebRequest.Delete(fullURL);
        yield return response.SendWebRequest();
        if (response.isNetworkError || response.isHttpError)
        {
            Debug.LogError(response.error);
            yield break;
        }
        if(callback != null){
            try{
                callback(response.downloadHandler.text);
            }
            catch(Exception){}
        }
    }
}
