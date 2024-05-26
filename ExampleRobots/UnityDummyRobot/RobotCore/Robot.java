package RobotCore;

import ArmFiles.Arm;

public class Robot {
    private RobotNetwork network = RobotNetwork.getInstance();

    private Arm fred;

    public Robot(){
        network.startNetwork();

        fred = new Arm();

        fred.base.rotate(45);
        fred.upperArm.rotate(60);
        fred.lowerArm.rotate(15);

        network.stopNetwork();
    }
    
}
