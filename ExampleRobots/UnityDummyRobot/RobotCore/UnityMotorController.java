package RobotCore;

public class UnityMotorController {
    private int motorID = -1;
    private RobotNetwork network = RobotNetwork.getInstance();

    private enum motorDataType{
        ROTATION, 
        CONST_SPEED
    }

    public UnityMotorController(int id){
        motorID = id;
    }

    //Set motor angle to the specified angle
    public void setRotation(double angle){
        network.bufferMotorData(motorID, motorDataType.ROTATION.ordinal(), angle);
    }

    //Set the motor to rotate at a constant speed
    public void setSpeed(double speed){
        network.bufferMotorData(motorID, motorDataType.CONST_SPEED.ordinal(), speed);
    }
}
