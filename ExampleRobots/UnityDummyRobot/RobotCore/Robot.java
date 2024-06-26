package RobotCore;

import ArmFiles.Arm;
import org.UnityRobotServer.RobotNetwork;

public class Robot {
    private RobotNetwork network = RobotNetwork.getInstance();

    private Arm fred;

    public Robot(){
        network.startNetwork();

        fred = new Arm();

        //fred.base.rotate(45);
        fred.base.spin(25);
        fred.upperArm.rotate(45);
        fred.lowerArm.rotate(15);
        fred.claw.rotate(-45);

        System.out.println(fred.base.getAngle());

        network.stopNetwork();
    }
    
}
