using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

public class SpeechToTextNotes : MonoBehaviour
{
    private static string URL = "https://api.eu-de.speech-to-text.watson.cloud.ibm.com/instances/6a081172-4f2d-4d66-ba14-e8bd40a0aa46";
    private static string API_KEY = "svDe-IRuO-gCwV1jZtpjlPfGZ-fqenQnG2Rdx9yR19E-";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Request to WatsonApi
    void convertSpeechToText()
    {
        HttpWebRequest request =
          (HttpWebRequest)WebRequest.Create(URL+"&apikey="+API_KEY);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
    }
}
