﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

//Define base robot behavior, starting the robot server and managing the communication to the robot server
public class RobotBehavior : MonoBehaviour, IRobotUI
{
    [SerializeField, Header("Robot Motors")]
    List<BaseMotorController> motors = new List<BaseMotorController>();
    //Arbitrary port that isn't reserved by windows, can be changed here or using UI, only requirement is the server uses the same port
    int robotPort = 55555;

    [Header("UI Elements")]
    public TMP_InputField directory;
    public Toggle consoleToggle;

    private Process process;
    private NetworkStream networkStream;

    private void FixedUpdate()
    {
        //Pull data from the robot network at a fixed rate
        readNetwork();
    }

    //Starts the robot server and connects to it
    public void startRobotServer() {
        ExecuteRobotJar();
        StartNetwork();
    }

    public void closeRobotTerminal()
    {
        if(process != null)
        {
            process.CloseMainWindow();
        }
    }

    public void setRobotServerPort(int port)
    {
        robotPort = port;
    }

    //Starts the selected robot jar
    void ExecuteRobotJar()
    {
        ProcessStartInfo processInfo;
        
        //processInfo = new ProcessStartInfo("cmd.exe", "/K java -jar " + testJar);
        processInfo = new ProcessStartInfo("cmd.exe", "/K java -jar " + directory.text);
        processInfo.CreateNoWindow = false;
        processInfo.UseShellExecute = true;

        if(consoleToggle != null)
        {
            if (!consoleToggle.isOn)
            {
                processInfo.WindowStyle = ProcessWindowStyle.Minimized;
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("No reference to UI Toggle");
        }
        
        process = Process.Start(processInfo);
    }

    //Connects to the robot server on localhost
    void StartNetwork()
    {
        UnityEngine.Debug.Log("Connecting robot network");
        TcpClient tcpClient = new TcpClient("127.0.0.1", robotPort);
        networkStream = tcpClient.GetStream();
        UnityEngine.Debug.Log("Connected to robot");
    }

    //Reads the robot network and parses the message type
    async void readNetwork()
    {
        if (networkStream == null)
        {
            return;
        }
        byte[] buffer = new byte[1024];
        //Reading from network blocks so it should be done asynchronously to avoid halting the entire unity client
        Int32 bytes = await networkStream.ReadAsync(buffer, 0, buffer.Length);
        //The messages can get buffered so multiple json can be sent at a time so it needs to be split
        String[] robotMessages = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes).Split('\n');
        if (bytes != 0)
        {
            //UnityEngine.Debug.Log("Received " + bytes + " bytes from robot");
            //UnityEngine.Debug.Log("Robot message:\n" + robotMessage);

            foreach(String robotMessage in robotMessages)
            {
                //Check to make sure the element is an actual json and not an empty string from the split
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

    //async void writeNetwork(string msg)
    void writeNetwork(string msg)
    {
        //UnityEngine.Debug.Log("Attempting to write: " + msg);
        byte[] sendBuffer = System.Text.Encoding.UTF8.GetBytes(msg);
        //UnityEngine.Debug.Log("Byte buffer: " + sendBuffer[0]);
        networkStream.Write(sendBuffer, 0, sendBuffer.Length);
        //Writing to the network blocks so it needs to be done asynchronously
        //await networkStream.WriteAsync(sendBuffer, 0, sendBuffer.Length);
        //UnityEngine.Debug.Log("Wrote Message to network");
    }

    //Takes the json from the robot network, calling the appropriate function on the specified motor and passing it the data.
    void parseMotorMessage(String message) {

        MotorMessage msg = JsonUtility.FromJson<MotorMessage>(message);

        //UnityEngine.Debug.Log("Motor ID: " + msg.motorID + " | Type ID: " + msg.actionType + " | Data: " + msg.motorData);
        switch(msg.actionType)
        {
            case 0:
                motors[msg.motorID].setRotation(msg.motorData);
                break;
            case 1:
                motors[msg.motorID].setSpeed(msg.motorData);
                break;
            case 2:
                float encoderValue = motors[msg.motorID].getEncoder();
                UnityEngine.Debug.Log($"Sending {encoderValue}");
                string response = $"{{\"msg_type\": 2, \"sensorID\": {msg.motorID}, \"sensorType\": 0, \"sensorData\": {encoderValue}}}\n";
                //string response = "{\"msg_type\": 2, \"sensorID\": -1, \"sensorType\": 0, \"sensorData\": 0}\n";    //NOTE: Remember to include '\n' in any string sent to robot server

                writeNetwork(response);
                break;
            default:
                UnityEngine.Debug.LogWarning("Robot sent invalid motor action");
                break;
        }
    }

    //TODO: Take the json and handle the command send from robot server
    void parseCommandMessage(byte[] msgBuff, int size) { }
}
