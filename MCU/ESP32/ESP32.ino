#include "esp_http_client.h"
#include "esp_camera.h"
#include <WiFi.h>
#include "Arduino.h"
#include <esp_http_server.h>
#include <HTTPClient.h>

#include "soc/soc.h"
#include "soc/rtc_cntl_reg.h"

//Enter WiFI Credentials
const char* ssid = "";
const char* password = "";

int patientID = 2;
bool hasPhotoDir = false;


#define PWDN_GPIO_NUM     32
#define RESET_GPIO_NUM    -1
#define XCLK_GPIO_NUM      0
#define SIOD_GPIO_NUM     26
#define SIOC_GPIO_NUM     27
#define LED_BUILTIN 4
#define Y9_GPIO_NUM       35
#define Y8_GPIO_NUM       34
#define Y7_GPIO_NUM       39
#define Y6_GPIO_NUM       36
#define Y5_GPIO_NUM       21
#define Y4_GPIO_NUM       19
#define Y3_GPIO_NUM       18
#define Y2_GPIO_NUM        5
#define VSYNC_GPIO_NUM    25
#define HREF_GPIO_NUM     23
#define PCLK_GPIO_NUM     22

bool internet_connected = false;
bool hasFallen = false;

unsigned long current_millis, last_capture_millis;
unsigned long  capture_interval = 250; //250 -> 4 frames/second


/*static httpd_handle_t server = NULL;

esp_err_t fall_handler(httpd_req_t *req)
{    
  const char resp[] = "Stream started";
  httpd_resp_send(req, resp, strlen(resp));
  Serial.println(resp);
  hasFallen = true;
  return ESP_OK;
}

esp_err_t rescue_handler(httpd_req_t *req)
{  
   const char resp[] = "Stream stopped";
   httpd_resp_send(req, resp, strlen(resp));
   Serial.println(resp);
   hasFallen = false;
   return ESP_OK;
}

httpd_uri_t handleFall = {
    .uri      = "/hasFallen",
    .method   = HTTP_GET,
    .handler  = fall_handler,
    .user_ctx = NULL
};

httpd_uri_t handleRescue = {
    .uri      = "/Rescued",
    .method   = HTTP_GET,
    .handler  = rescue_handler,
    .user_ctx = NULL
};*/


//httpd_handle_t start_webserver(void)
//{
    /* Generate default configuration */
//    httpd_config_t config = HTTPD_DEFAULT_CONFIG();

    /* Empty handle to esp_http_server */
//    httpd_handle_t server = NULL;

    /* Start the httpd server */
//    if (httpd_start(&server, &config) == ESP_OK) {
        /* Register URI handlers */
//        httpd_register_uri_handler(server, &handleFall);
//        httpd_register_uri_handler(server, &handleRescue);
//    }
    /* If server failed to start, handle will be NULL */
//    return server;
//}

/*void postIP(){
  
  if(WiFi.status()== WL_CONNECTED){

    esp_http_client_handle_t http_client;
  
    esp_http_client_config_t config_client = {0};
    config_client.url = "http://web.tecnico.ulisboa.pt/ist178685/FallDetection/updateIP.php";
    config_client.event_handler = _http_event_handler;
    config_client.method = HTTP_METHOD_POST;  
    http_client = esp_http_client_init(&config_client);


    String str = "id="+String(patientID)+"&camIP="+WiFi.localIP().toString();
      
    esp_http_client_set_post_field(http_client, str.c_str(), str.length());
    esp_http_client_set_header(http_client, "Content-Type", "application/x-www-form-urlencoded");
    esp_err_t err = esp_http_client_perform(http_client);  
    esp_http_client_cleanup(http_client);
  }
}*/

bool getStatus(String s){

    if(s.equals("0")){
      return false;
      }else{
        return true;
        }
  }

bool checkStatus(){

  if(WiFi.status()== WL_CONNECTED){
    HTTPClient http;

    http.begin("http://web.tecnico.ulisboa.pt/ist178685/FallDetection/checkPatientStatus.php?id=2"); 
    int httpCode = http.GET();
    String payload = http.getString();
    Serial.println(httpCode);
    Serial.println(payload);

    return getStatus(payload);

  }
  
  }




static esp_err_t take_send_photo()
{  

  camera_fb_t * fb = NULL;
  esp_err_t res = ESP_OK;

  fb = esp_camera_fb_get();
  if (!fb) {
    Serial.println("Camera capture failed");
    return ESP_FAIL;
  }  
  
  esp_http_client_handle_t http_client;
  
  esp_http_client_config_t config_client = {0};
  char url[100];
  sprintf(url, "http://web.tecnico.ulisboa.pt/ist178685/FallDetection/photos/%d/postPhotos.php", patientID);
  Serial.println(url);
  config_client.url = url;
  config_client.event_handler = _http_event_handler;
  config_client.method = HTTP_METHOD_POST;  
  http_client = esp_http_client_init(&config_client);
  
  esp_http_client_set_post_field(http_client, (const char *)fb->buf, fb->len);
  esp_http_client_set_header(http_client, "Content-Type", "image/jpeg");
  esp_http_client_set_header(http_client, "Content-Disposition", "name=\"fieldName\"; filename=\"filename.jpg\"\n");
  esp_err_t err = esp_http_client_perform(http_client); 
  
  if (err == ESP_OK) {
    Serial.print("esp_http_client_get_status_code: ");
    Serial.println(esp_http_client_get_status_code(http_client));
  }
  
  esp_http_client_cleanup(http_client);
  esp_camera_fb_return(fb);
}


bool init_wifi()
{  
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED ) {
    delay(500);
    Serial.print(".");
  }
  return true;
}



esp_err_t _http_event_handler(esp_http_client_event_t *evt)
{
  switch (evt->event_id) {
    case HTTP_EVENT_ERROR:
      //Serial.println("HTTP_EVENT_ERROR");
      break;
    case HTTP_EVENT_ON_CONNECTED:
      //Serial.println("HTTP_EVENT_ON_CONNECTED");
      break;
    case HTTP_EVENT_HEADER_SENT:
      //Serial.println("HTTP_EVENT_HEADER_SENT");
      break;
    case HTTP_EVENT_ON_HEADER:
      //Serial.println();
      //Serial.printf("HTTP_EVENT_ON_HEADER, key=%s, value=%s", evt->header_key, evt->header_value);
      break;
    case HTTP_EVENT_ON_DATA:
      //Serial.println();
      //Serial.printf("HTTP_EVENT_ON_DATA, len=%d", evt->data_len);
      if (!esp_http_client_is_chunked_response(evt->client)) {
        // Write out data
        printf("%.*s", evt->data_len, (char*)evt->data);
      }
      break;
    case HTTP_EVENT_ON_FINISH:
      //Serial.println("");
      //Serial.println("HTTP_EVENT_ON_FINISH");
      break;
    case HTTP_EVENT_DISCONNECTED:
      //Serial.println("HTTP_EVENT_DISCONNECTED");
      break;
  }
  return ESP_OK;
}




void createPhotoDir(){
  
  
  esp_http_client_handle_t http_client;
  
  esp_http_client_config_t config_client = {0};
  config_client.url = "http://web.tecnico.ulisboa.pt/ist178685/FallDetection/photos/createPhotoDir.php";
  config_client.event_handler = _http_event_handler;
  config_client.method = HTTP_METHOD_POST;  
  http_client = esp_http_client_init(&config_client);

  char id[10];
  sprintf(id, "id=%d", patientID);
  
  esp_http_client_set_post_field(http_client, id, strlen(id));
  esp_http_client_set_header(http_client, "Content-Type", "application/x-www-form-urlencoded");
  esp_err_t err = esp_http_client_perform(http_client); 
  
  if (err == ESP_OK) {
    Serial.print("esp_http_client_get_status_code: ");
    Serial.println(esp_http_client_get_status_code(http_client));
  }
    
  esp_http_client_cleanup(http_client);
  }

void setup()
{
  WRITE_PERI_REG(RTC_CNTL_BROWN_OUT_REG, 0);
  Serial.begin(115200);

  if (init_wifi()) { // Connected to WiFi
    internet_connected = true;
    Serial.println("Internet connected");
  }

  camera_config_t config;
  config.ledc_channel = LEDC_CHANNEL_0;
  config.ledc_timer = LEDC_TIMER_0;
  config.pin_d0 = Y2_GPIO_NUM;
  config.pin_d1 = Y3_GPIO_NUM;
  config.pin_d2 = Y4_GPIO_NUM;
  config.pin_d3 = Y5_GPIO_NUM;
  config.pin_d4 = Y6_GPIO_NUM;
  config.pin_d5 = Y7_GPIO_NUM;
  config.pin_d6 = Y8_GPIO_NUM;
  config.pin_d7 = Y9_GPIO_NUM;
  config.pin_xclk = XCLK_GPIO_NUM;
  config.pin_pclk = PCLK_GPIO_NUM;
  config.pin_vsync = VSYNC_GPIO_NUM;
  config.pin_href = HREF_GPIO_NUM;
  config.pin_sscb_sda = SIOD_GPIO_NUM;
  config.pin_sscb_scl = SIOC_GPIO_NUM;
  config.pin_pwdn = PWDN_GPIO_NUM;
  config.pin_reset = RESET_GPIO_NUM;
  config.xclk_freq_hz = 20000000;
  config.pixel_format = PIXFORMAT_JPEG; 
  
  if(psramFound()){
    config.frame_size = FRAMESIZE_UXGA; // FRAMESIZE_ + QVGA|CIF|VGA|SVGA|XGA|SXGA|UXGA
    config.jpeg_quality = 10;
    config.fb_count = 2;
  } else {
    config.frame_size = FRAMESIZE_SVGA;
    config.jpeg_quality = 12;
    config.fb_count = 1;
  }
  
  // Init Camera
  esp_err_t err = esp_camera_init(&config);
  if (err != ESP_OK) {
    Serial.printf("Camera init failed with error 0x%x", err);
    return;
  }
  
  //server = start_webserver();
  //postIP();
  //Serial.println(WiFi.localIP());
  
}

void loop()
{ 
  if(hasFallen){
    
    hasFallen = !checkStatus(); 
    
    if(!hasPhotoDir){
      createPhotoDir();
      delay(1000);
      hasPhotoDir = true;
      }
    current_millis = millis();
  
  if (current_millis - last_capture_millis > capture_interval) {
    last_capture_millis = millis();
    take_send_photo();
    }
  
}else{
  
  hasFallen = !checkStatus();  
  delay(5000);
  }
}
