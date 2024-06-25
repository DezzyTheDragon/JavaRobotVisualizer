package org.UnityRobotServer;
import org.json.*;

//A simple controller that simplifies sending motor data to the Unity Client
public class UnityMotorController {
    private int motorID = -1;
    private RobotNetwork network = RobotNetwork.getInstance();

    private enum motorDataType{
        ROTATION, 
        CONST_SPEED,
        ENCODER_REQUEST
    }

    //Preffered constructor
    public UnityMotorController(int id){
        motorID = id;
    }

    //Set motor angle to the specified angle
    public void setRotation(double angle){
        if(motorID != -1){
            network.bufferMotorData(motorID, motorDataType.ROTATION.ordinal(), angle);
        }
        else{
            System.out.println("Invalid motor id of -1");
        }
    }

    //Set the motor to rotate at a constant speed
    public void setSpeed(double speed){
        if(motorID != -1){
            network.bufferMotorData(motorID, motorDataType.CONST_SPEED.ordinal(), speed);
        }
        else{
            System.out.println("Invalid motor id of -1");
        }
    }

    //Get the angle of the Unity Motor
    public double getAngle(){
        //System.out.println("Sending Encoder Request");
        //Sensor value request
        network.bufferMotorData(motorID, motorDataType.ENCODER_REQUEST.ordinal(), 0);

        //System.out.println("Get Encoder Response");
        //Sensor value response
        JSONObject unityMsg = network.pollData();
        if(unityMsg == null){
            System.out.println("JSON message is null");
            return -999;
        }
        double encoderValue = 0;
        //Message Validation
        if(unityMsg.getInt("msg_type") == 2 && unityMsg.getInt("sensorID") == motorID){
            encoderValue = unityMsg.getFloat("sensorData");
        }
        else{
            System.out.println(String.format("Motor %d sensor error: Sensor data contains incorrect ID values", motorID));
        }
        return encoderValue;
    }
}
