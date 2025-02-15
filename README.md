# Tekno MES API Demo

This demo API is meant to demonstrate how to communicate with an Allen-Bradley ControlLogix PLC leveraging the [libplctag.NET](https://github.com/libplctag/libplctag.NET) library. It provides two demo endpoints to read string and bool tag types, and it could be easily extended to add support for more data types.

## How to test the API?

- Clone this repository to your local.
- Open the solution in Visual Studio and run the project.
- Make sure that the CompactLogix device is reachable on the network, identify the IP address (gateway) and path.
- Use any API client such like Postman and make the following POST request:

![image](https://github.com/user-attachments/assets/5c9f32c1-24b2-4bdf-8d7a-6bbc999a4f0b)


