<?php
	
	
	$host="";	
	$user="";	
	$password="";	
	$dbname = $user; 
	$connection = new PDO("mysql:host=" . $host. ";dbname=" . $dbname, $user, $password, array(PDO::ATTR_ERRMODE => PDO::ERRMODE_WARNING));

	$name = $_POST['name'];
	$surname = $_POST['surname'];
	$address = $_POST['address'];
	$age = $_POST['age'];
	$risk = $_POST['risk'];
	$email = $_POST['email'];

	
	$sql = "Select id from responsible where email = '$email'";
	
	$result = $connection->query($sql);

	foreach ($result as $row){
		$responsibleID = $row['id'];
	}
	
	$sql = "Insert into patient(name, surname, age, address, risk, responsible)  values ('$name','$surname',".$age.",'$address','$risk', ".$responsibleID." )";
	

	$result = $connection->query($sql);


	$connection = null;
	

?>

