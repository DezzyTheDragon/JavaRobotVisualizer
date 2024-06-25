package org.UnityRobotServer;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
import org.json.*;

//Class that handles the robot server
public class RobotNetwork {
    private static RobotNetwork networkInstance;

    private int port = 55555;  //Port randomly chosen, can be changed as long as it maches in the Unity Client
    ServerSocket serverSocket;
    Socket clientSocket;
    PrintWriter out;
    BufferedReader in;

    // private enum RobotMessageType{
    //     CMD,
    //     MOTOR
    // }

    private RobotNetwork(){
        //getInstance();
        //startNetwork();
    }

    //Returns the single network instance
    public static RobotNetwork getInstance(){
        if(networkInstance == null){
            networkInstance = new RobotNetwork();
        }
        return networkInstance;
    }

    //This allows for the changing of the port the robot network uses.
    //  NOTE: Idealy this should only really be used if there is a problem with the default port (process already bound to port)
    public void setPort(int port){
        this.port = port;
    }

    //Format motor data and send to Unity
    public void bufferMotorData(int motorID, int dataType, double data){
        //Format the message using json
        String motorJson = String.format("{\"msg_type\": 1, \"motorID\": %d, \"actionType\": %d, \"motorData\": %e}", motorID, dataType, data);

        pushData(motorJson);
    }

    //Send data to Unity
    public void pushData(String json){
        System.out.println(json);
        out.println(json);
    }

    //Receive data from 
    public JSONObject pollData(){
        //System.out.println("Waiting to read from network");
        //String msg = "";
        JSONObject data = null;
        try{
            String msg = in.readLine();
            data = new JSONObject(msg);
            System.out.println(msg);
        }catch(IOException e){
            e.printStackTrace();
        }
        //System.out.println("Recived: " + msg);
        //TODO: consider creating some sort of subscriber that will handle updating values without needing a direct call
        return data;
        //return null;
    }

    //Begins the TCP robot network and opening a port
    //Throws an exception if the robot server fails to start
    public void startNetwork(){
        //String greeting = "";
        System.out.println("Starting robot network");
        try{
            serverSocket = new ServerSocket(port);
            clientSocket = serverSocket.accept();
            out = new PrintWriter(clientSocket.getOutputStream(), true);
            in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
            //greeting = in.readLine();
            
        }catch(IOException e){
            e.printStackTrace();
        }
        System.out.println("Network started and connected to Unity");
        // out.println("Hello Unity");
        // try{
        //     String greeting = in.readLine();
        //     System.out.println(greeting);
        // }catch(IOException e){
        //     e.printStackTrace();
        // }
    }

    //Closes the port and any streams
    //Throws an exeption if there is an issue closing
    public void stopNetwork(){
        try{
            in.close();
            out.close();
            clientSocket.close();
            serverSocket.close();
        }catch(IOException e){
            e.getStackTrace();
        }
    }
}
