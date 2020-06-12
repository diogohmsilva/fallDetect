<?php
	
	
	$host="";	
	$user="";	
	$password="";	
	$dbname = $user; 
	$connection = new PDO("mysql:host=" . $host. ";dbname=" . $dbname, $user, $password, array(PDO::ATTR_ERRMODE => PDO::ERRMODE_WARNING));

	$id = $_POST['id'];

	$sql = "UPDATE patient set isOK = true WHERE id = " . $id;
	
	$result = $connection->query($sql);
	
	$connection = null;
	
	

?>

