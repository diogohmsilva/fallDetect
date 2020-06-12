<?php
	

	$host="";	
	$user="";	
	$password="";	
	$dbname = $user;

 
	$connection = new PDO("mysql:host=" . $host. ";dbname=" . $dbname, $user, $password, array(PDO::ATTR_ERRMODE => PDO::ERRMODE_WARNING));

	$IP = $_POST['camIP'];
	$id = $_POST['id'];

	echo $IP;

	$sql = "UPDATE patient set camIP = '".$IP."' WHERE id = $id";

	
	$connection->query($sql);
	
	$connection = null;

?>
