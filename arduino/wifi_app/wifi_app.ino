#include <ESP8266HTTPClient.h>
#include <ESP8266WiFi.h>

float measurement[5];
int measurementDelay = 2500;
int wait = 1800000;
String  ExternalKey = "2106e356-4f23-4167-ac8f-d45290a20f9a";

void setup() {
 
  Serial.begin(115200);  //Serial connection
  WiFi.begin("nazwaWiFi", "haslo");   //WiFi connection
  while (WiFi.status() != WL_CONNECTED) {  //Wait for the WiFI connection completion
    delay(500);
    Serial.println("Waiting for connection");
 
  }
 
}
 
void loop() {
 
  measurement[0] = measurementTemp();
  delay(measurementDelay);
  measurement[1] = measurementTemp();
  delay(measurementDelay);
  measurement[2] = measurementTemp();
  delay(measurementDelay);
  measurement[3] = measurementTemp();
  delay(measurementDelay);
  measurement[4] = measurementTemp();

  float temperature = getTemp();
  Serial.print("Pomiar tęperatury:"); 
  Serial.println(temperature);  
  sendMessage(temperature);

  delay(wait);  
}


float measurementTemp()
{
  float temp; 
  temp = analogRead(0)*3.3/1024.0;
  temp = temp - 0.5;
  return temp / 0.01;
}

float getTemp()
{
  int min = 0, max = 0, i;
  float sum = 0.0;
    for(i=0;i<5;i++)
    {
      if(measurement[min]>measurement[i])
      min = i;
    }
    for(i=0;i<5;i++)
    {
      if(measurement[max]<measurement[i])
      max = i;
    }
    measurement[min] = 0.0;
    measurement[max] = 0.0;

    for(i=0;i<5;i++)
    {
      sum += measurement[i];
    }
    
    if(min == max)
      return sum/4;
    else
      return sum/3;
}
void sendMessage(float temp)
{
   if(WiFi.status()== WL_CONNECTED){ 
   HTTPClient http;
 
   http.begin("http://weatherstationwebapp.azurewebsites.net/Home/Temperature");
   http.addHeader("Content-Type", "application/x-www-form-urlencoded");
   String Message = "ExternalKey=" + ExternalKey + "&Temperature=" + temp;
   int httpCode = http.POST(Message);  
   String payload = http.getString();
   Serial.println("po wysłaniu:");
   Serial.println(httpCode);  
   //Serial.println(payload);  
   http.end();
 }else{
    Serial.println("Error in WiFi connection");   
 }
}
