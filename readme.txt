This project goal is to detect and alert that someone has suffered a fall.
It combines two MCUs, the ESP8266 and the ESP32-CAM, where the first is used to detect the fall, together with a LSM303D accelerometer.
This system was supported by a mobile application developed with Xamarin Forms.

When a fall is detected:
- the ESP32 will capture and post photos to a server that can later be seen in a browser as a time-lapse through a specific url
- the App will receive a notification regarding the situation (through Firebase Cloud Messaging)

A more detailed explanation of the system in general is given in the pdf Project Report.
