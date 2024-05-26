package ArmFiles;

public class Arm {
    public Joint base;
    public Joint upperArm;
    public Joint lowerArm;
    public Joint wrist;

    public Arm(){
        base = new Joint("base", 0, Constants.Base.leftLimit, Constants.Base.rightLimit);
        upperArm = new Joint("upperArm", 1, Constants.UpperArm.lowerLimit, Constants.UpperArm.upperLimit);
        lowerArm = new Joint("lowerArm", 2, Constants.LowerArm.lowerLimit, Constants.LowerArm.upperLimit);
        wrist = new Joint("wrist", 3, Constants.Wrist.lowerLimit, Constants.Wrist.upperLimit);
    }
    
}
