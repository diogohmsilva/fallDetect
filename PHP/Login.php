<?php
	
	
	$host="";	
	$user="";	
	$password="";	
	$dbname = $user; 
	$connection = new PDO("mysql:host=" . $host. ";dbname=" . $dbname, $user, $password, array(PDO::ATTR_ERRMODE => PDO::ERRMODE_WARNING));

	$email = $_POST['email'];
	$pass = $_POST['password'];

	$sql = "Select id, password from responsible where email= '$email'";
	
	$result = $connection->query($sql);
	
	
	foreach($result as $row) {   	 	
    		echo("id= ".$row['id']." password= ".$row['password']. " "); 
	}

	$connection = null;
	
	

?>

