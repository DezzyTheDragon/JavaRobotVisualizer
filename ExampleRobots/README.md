# Example Robots
This contains example robots to show how to create a robot that will work with the visualizer.

The key component is the `RobotNetwork.java`, this class contains the code that manages the robot server and contains functions to send data to the Unity visualizer.
The `UnityMotorController.java` acts as a motor controller class.


Unity Robot Protocol

`msg_type` details what type the message is and hints at how to properly handle the following data

`msg_type`
| value | Description |
| ----- | ----------- |
| 0 | Message is a command |
| 1 | Message is a motor instruction |
| 2 | Sensor data |

Motor instructions have 3 values to them, `motorID`, `actionType`, and `motorData`. `motorID` needs to correspond to a motor ID on the target Unity robot. `actionType` details how the motor should behave, is it constantly rotating or is it turning to a specific angle. `motorData` is the value that the motor is trying to hit.

`{"msg_type": 1, "motorID": 0, "actionType": 0, "motorData": 90}`

`actionType`
| value | Description |
| ----- | ----------- |
| 0 | Set the specified motor to the given angle |
| 1 | Set the target speed of the specified motor |

Sensor data contains 3 values, `sensorID`, `sensorType`, and `sensorData`. `sensorID` is the id of the sensor. If the sensor is an encoder the ID matches the motor ID. `sensorType` is the type of sensor. `sensorData` is the raw value of what the sensor is reading.

`{"msg_type": 2, "sensorID": 0, "sensorType": 0, "sensorData": 90}`

`sensorType`
| value | Description |
| ----- | ----------- |
| 0 | encoder |
