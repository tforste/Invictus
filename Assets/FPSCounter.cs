using UnityEngine;
using System.Collections;
using System.IO;

public class FPScoutner : MonoBehaviour
{

    string username = "NULL";

    public float updateInterval = 0.5f;

    private float accum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval
    private string format; //The formatted string to be displayed

    public GUIStyle fpsStyle; //fps font style

    void Start()
    {
        //>>>Error Catching and initialization for Username
        try
        {
            username = File.ReadAllText(Application.persistentDataPath + "/username.txt");
        }
        catch (FileNotFoundException)
        {
            username = "Enter Username";
        }

        //>>>Initialization for FPS Counter
        timeleft = updateInterval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        //Interval ended - update GUI Text and start new interval
        if (timeleft <= 0.0)
        {
            //Display two fractional digits (f2 format)
            float fps = accum / frames;
            format = System.String.Format("{0:f2} FPS", fps);
            //guiText.text = format;

            if (fps < 30)
            {
                fpsStyle.normal.textColor = Color.yellow;
            }
            else
            {
                if (fps < 10)
                {
                    fpsStyle.normal.textColor = Color.red;
                }
                else
                {
                    fpsStyle.normal.textColor = Color.green;
                }
            }

            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }

    void OnGUI()
    {

        //>>>FPS LABEL<<<
        GUI.Label(new Rect(Screen.width - 100, 0, 100, 30), format, fpsStyle);

        //GUILayout.Label((1f / Time.deltaTime) + " FPS");


        //>>>USER NAME FIELD<<<
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("Username: ");
            GUI.changed = false;
            username = GUILayout.TextField(username);

            if (GUI.changed)
            {
                File.WriteAllText(Application.persistentDataPath + "/username.txt", username);
            }
        }
        GUILayout.EndHorizontal();

    }
}
