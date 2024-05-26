using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

//Container to deserialize the message type value
[System.Serializable]
public class RobotMessage
{
    public int msg_type;
}

//Container to deserialize motor values
[System.Serializable]
public class MotorMessage
{
    public int motorID;
    public int actionType;
    public float motorData;
}

public class RobotBehavior : MonoBehaviour
{
    [SerializeField]
    List<MotorController> motors = new List<MotorController>();
    int robotPort = 55555;

    public TMP_InputField directory;

    //private string testJar = "C:\\Users\\DestinyG\\Desktop\\UnityDummyRobot\\UnityDummyRobot.jar";
    private Process process;
    private NetworkStream networkStream;
    private bool DEBUG = true;

    // Start is called before the first frame update
    void Start()
    {
        //StartRobot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        readNetwork();
    }

    public void StartRobot() {
        ExecuteRobotJar();
        StartNetwork();
    }


    //Starts the selected robot jar
    void ExecuteRobotJar()
    {
        ProcessStartInfo processInfo;
        
        //processInfo = new ProcessStartInfo("cmd.exe", "/K java -jar " + testJar);
        processInfo = new ProcessStartInfo("cmd.exe", "/K java -jar " + directory.text);
        processInfo.CreateNoWindow = !DEBUG;
        processInfo.UseShellExecute = true;

        process = Process.Start(processInfo);
        //Can probably stop the robot with process.Close();
    }

    //Connects to the robot server
    void StartNetwork()
    {
        UnityEngine.Debug.Log("Connecting robot network");
        TcpClient tcpClient = new TcpClient("127.0.0.1", robotPort);
        networkStream = tcpClient.GetStream();
        UnityEngine.Debug.Log("Connected to robot");

        //DEBUG SEND
        //byte[] sendBuffer = System.Text.Encoding.UTF8.GetBytes("Hello Robot!");
        //networkStream.Write(sendBuffer, 0, sendBuffer.Length);
    }

    void readNetwork()
    {
        if (networkStream == null)
        {
            return;
        }
        byte[] buffer = new byte[1024];
        Int32 bytes = networkStream.Read(buffer, 0, buffer.Length);
        String[] robotMessages = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes).Split('\n');
        //String robotMessage = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes);
        if (bytes != 0)
        {
            //UnityEngine.Debug.Log("Received " + bytes + " bytes from robot");
            //UnityEngine.Debug.Log("Robot message:\n" + robotMessage);

            foreach(String robotMessage in robotMessages)
            {
                if(robotMessage.Length > 0)
                {
                    //UnityEngine.Debug.Log("Robot message:\n" + robotMessage);

                    RobotMessage msg = JsonUtility.FromJson<RobotMessage>(robotMessage);

                    switch (msg.msg_type)
                    {
                        case 0:
                            UnityEngine.Debug.Log("Command Message");
                            break;
                        case 1:
                            //UnityEngine.Debug.Log("Motor Message");
                            parseMotorMessage(robotMessage);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    void parseMotorMessage(String message) {

        MotorMessage msg = JsonUtility.FromJson<MotorMessage>(message);

        //UnityEngine.Debug.Log("Motor ID: " + msg.motorID + " | Type ID: " + msg.actionType + " | Data: " + msg.motorData);
        switch(msg.actionType)
        {
            case 0:
                motors[msg.motorID].setRotation(msg.motorData);
                break;
            case 1:
                break;
            default:
                UnityEngine.Debug.LogWarning("Robot sent invalid action");
                break;
        }
    }
    void parseCommandMessage(byte[] msgBuff, int size) { }
}
