# Java Robot Visualizer
A visualizer made for students learning Java in the context of writing code and allows them to see what their code is doing.

The project is made of two parts. The Java robot classes that creates a server and handles the comunication, and the Unity project that launches the robot, connects to it and takes the given data and visualizes it.

Uses TCP to comunicate and sends messages in the form of json. Json is used because its easy to debug and works with no issue on both Java and C#.