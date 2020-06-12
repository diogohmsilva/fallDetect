#include <Wire.h>
#include <LSM303.h>
#include <math.h>
#include <ESP8266HTTPClient.h>
#include <ESP8266WiFi.h>

HTTPClient http;

int patientID = 2;

char id[10];


LSM303 accelerometer;

unsigned long T = 50; 

int sum = 0;

int iter = 0;
int avgTime = 5000; //ms
int Nreadings = avgTime/T;

int lastAVG = 30000;

int total;

bool falling = false;
bool hasFallen = false;

unsigned long fallTime = 3000; //miliseconds
unsigned long startFall, startTime;

static String camIP;
bool hasCamIP = false;

float lowThreshold = 0.5;
float highThreshold = 3.0;

/*String getCamIP(){

  itoa(patientID, id, 10);
  char address[100] = "http://web.tecnico.ulisboa.pt/ist178685/FallDetection/getCamIP.php?id=";
  strcat(address, id);
  Serial.println(address);
  http.begin(address);
  int httpCode = http.GET();
  
  camIP = http.getString();
  Serial.println(httpCode);
  Serial.println(camIP);
  http.end(); 
  hasCamIP = true;
  return camIP;
  
  }*/

bool checkIfRescued(){

  itoa(patientID, id, 10);
  char address[100] = "http://web.tecnico.ulisboa.pt/ist178685/FallDetection/checkPatientStatus.php?id=";
  strcat(address, id);
  
  http.begin(address);
  int httpCode = http.GET();
  
  String  patientStatus = http.getString(); 
  Serial.println(httpCode);
  Serial.println(patientStatus);
  http.end(); 

   if(patientStatus.equals("0")){
    return true;
    }else{
      //alertRescue();
      Serial.println("Patient escued");
      return false;   // has been rescued
      }
  
  }

/*void alertRescue(){

  Serial.println("Communicating rescue");
  char address[100] ;
  camIP.toCharArray(address, camIP.length()+1);
  strcat(address, "/Rescued");
  http.begin(address);
  int httpCode = http.GET();
  Serial.println(httpCode);
  http.end(); 
  
  }*/

void alertFall(){

  Serial.println("Alerting fall");
  hasFallen = true;
  char address[100] ;
  /*camIP = getCamIP();
  camIP.toCharArray(address, camIP.length()+1);
  strcat(address, "/hasFallen");
  Serial.print("Communicating fall to ");
  Serial.println(address);
  String str = "http://192.168.1.114/hasFallen";
  http.begin(str);
  int httpCode = http.GET();
  Serial.println(httpCode);
  Serial.println(http.getString());
  http.end(); */

  
  
  itoa(patientID, id, 10);
  strcpy(address, "http://web.tecnico.ulisboa.pt/ist178685/FallDetection/updatePatientStatus.php?id=");
  strcat(address, id);
  
  http.begin(address);
  int httpCode = http.GET();
  Serial.println(httpCode);
  http.end(); 
  
}

void receiveEvent(size_t howMany) {

  (void) howMany;
  while (1 < Wire.available()) { // loop through all but the last
    char c = Wire.read(); // receive byte as a character
    Serial.print(c);         // print the character
  }
  int x = Wire.read();    // receive byte as an integer
  Serial.println(x);         // print the integer
}


void setup() {
  Serial.begin(115200);
  Wire.begin();

  accelerometer.init();
  Wire.onReceive(receiveEvent); // register event
  accelerometer.enableDefault();

  WiFi.begin("MEO-F2BA66", "50AEFCA5C5");   //WiFi connection
  
  while (WiFi.status() != WL_CONNECTED) {
    delay(2000);
    Serial.println("Waiting for connection"); 
  }

  Serial.println("Connected");
  
}



void loop() {

  if(!hasFallen){
    startTime = millis();
    
    accelerometer.read();
  
    sum = abs(accelerometer.a.x)+ abs(accelerometer.a.y ) + abs(accelerometer.a.z);
  
    total = total + sum;
      
    if(iter == Nreadings){
      iter = 0;
      lastAVG = total / Nreadings;
      total = 0;
      }else{
        iter++;
        }
          
     if(sum < lowThreshold*lastAVG && !falling){
      startFall = millis();
        
      falling = true;
      }
     
     if(falling){
        if(millis() - startFall > fallTime){
          falling = false;
          }
        if(sum > highThreshold * lastAVG && falling){     
          alertFall();
        }
      }
  
  
    while(millis() - startTime < T){
          delay(5);
    }
  }else{
    startTime = millis();

    hasFallen = checkIfRescued();
    
    while(millis() - startTime < 2000){ //Check every 2 seconds if patient has been rescued
          delay(500);
    }
    
  }
  

}
