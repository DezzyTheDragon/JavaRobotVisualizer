package ArmFiles;

import RobotCore.UnityMotorController;

public class Joint {
    private String jointName;
    private UnityMotorController jointMotor;
    private double angle = 0;
    private double min, max;

    public Joint(String name, int motorID, double min, double max){
        jointName = name;
        jointMotor = new UnityMotorController(motorID);
        this.min = min;
        this.max = max;
    }

    public void rotate(double angle){
        this.angle += angle;

        if(this.angle >= max){
            this.angle = max;
        }
        else if(angle <= min){
            this.angle = min;
        }

        jointMotor.setRotation(this.angle);
    }

    public void spin(double speed){
        jointMotor.setSpeed(speed);
    }

    // public double getAngle(){
    //     return jointMotor.getAngle();
    // }

}
