<?php
	
	
	$host="";	
	$user="";	
	$password="";	
	$dbname = $user; 
	$connection = new PDO("mysql:host=" . $host. ";dbname=" . $dbname, $user, $password, array(PDO::ATTR_ERRMODE => PDO::ERRMODE_WARNING));

	$email = $_GET['email'];
	
	$sql = "Select id from responsible where email = '$email'";
	
	$result = $connection->query($sql);

	foreach ($result as $row){
		$responsibleID = $row['id'];
	}

	$sql = "Select * from patient where responsible = ". $responsibleID;
	
	$result = $connection->query($sql);
	
	$rows = array();
	
	foreach($result as $row){
		$patient = array(
			'id' => $row['id'],
			'name' => $row['name'],
			'surname' => $row['surname'],
			'age' => $row['age'],
			'address' => $row['address'],
			'risk' => $row['risk'],
			'isOK' => $row['isOK'],
			'responsible' => $row['responsible'],
			'camIP' => $row['camIP']
		);
		$patients [] = $patient;
	}
	
	
	echo json_encode($patients);

	$connection = null;
	

?>

