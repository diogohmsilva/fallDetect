<?php
	
	
	$host="";	
	$user="";	
	$password="";	
	$dbname = $user;	 
	$connection = new PDO("mysql:host=" . $host. ";dbname=" . $dbname, $user, $password, array(PDO::ATTR_ERRMODE => PDO::ERRMODE_WARNING));

	$address = $_POST['address'];

	$sql = "Select id from patient where address = '".$address."'";
	
	$result = $connection->query($sql);
	
	
	foreach($result as $row) {   	 	
    		echo $row['id']; 
	}

	$connection = null;
	
	

?>

