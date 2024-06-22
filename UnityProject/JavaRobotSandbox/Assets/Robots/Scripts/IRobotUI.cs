using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRobotUI
{
    void closeRobotTerminal();
    void startRobotServer();
    void setRobotServerPort(int port);
}
