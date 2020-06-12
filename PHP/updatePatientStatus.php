<?php
	
	
	$host="";	
	$user="";	
	$password="";	
	$dbname = $user;	
 
	$connection = new PDO("mysql:host=" . $host. ";dbname=" . $dbname, $user, $password, array(PDO::ATTR_ERRMODE => PDO::ERRMODE_WARNING));

	$id = $_GET['id'];

	$sql = "UPDATE patient set isOK = false WHERE id = " . $id;

	$connection->query($sql);

	$sql = "Select name, surname,address,responsible from patient where id=" .$id;
	
	$result = $connection ->query($sql);

	foreach($result as $info){
		$responsibleID = $info['responsible'];
		$name = $info['name'];
		$surname = $info['surname'];
		$address = $info['address'];
	}

	
	$sql = "Select accessToken from responsible where id=" .$responsibleID;		
	$result = $connection->query($sql);

	foreach($result as $info){
		$accessToken = $info['accessToken'];		
	}
	
	
	$body = "Patient $name $surname needs assistance at $address";	

	$message = array(
		'body' => "$body",
		'title' => "Emergency!",
	);
	
	echo json_encode($message);
	
	echo gettype(json_encode($message));
	

	$fields = array(
   		'to' => $accessToken,
    		'notification' => [
			'body' => "$body",
			'title' => "Emergency!",
		]			
	);


	$headers = array
	(	
		'Content-Type: application/json',
 		'Authorization: key=AAAAoDu-xxE:APA91bEKjiLj1bv_Fxoy2Vym5glkezp8buFBb54ytpdlC_yCN9s4K6K5ibkeXRSTNdBdxMwdasyRUnC8Qxw87xPwWCMebGLpRt2-n5noF6rf0OHU3sa7cLeTRIn5gsFaQpb67p4-BhPi'
		
	);
	

	$ch = curl_init();
	curl_setopt( $ch,CURLOPT_URL, 'https://fcm.googleapis.com/fcm/send' );
	curl_setopt( $ch,CURLOPT_POST, true );
	curl_setopt( $ch,CURLOPT_HTTPHEADER, $headers );
	curl_setopt( $ch,CURLOPT_RETURNTRANSFER, true );
	//curl_setopt( $ch,CURLOPT_SSL_VERIFYPEER, false );
	curl_setopt( $ch,CURLOPT_POSTFIELDS, json_encode( $fields ) );
	$result = curl_exec($ch );
	curl_close( $ch );
	echo $result;

        $connection = null;

?>
