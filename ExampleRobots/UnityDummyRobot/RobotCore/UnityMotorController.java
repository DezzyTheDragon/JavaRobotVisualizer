package RobotCore;

//A simple controller that simplifies sending motor data to the Unity Client
public class UnityMotorController {
    private int motorID = -1;
    private RobotNetwork network = RobotNetwork.getInstance();

    private enum motorDataType{
        ROTATION, 
        CONST_SPEED
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
}
