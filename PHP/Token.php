<?php
	
	
	$host="";	
	$user="";	
	$password="";	
	$dbname = $user; 
	$connection = new PDO("mysql:host=" . $host. ";dbname=" . $dbname, $user, $password, array(PDO::ATTR_ERRMODE => PDO::ERRMODE_WARNING));

	

	$id = $_POST['id'];
	$token = $_POST['token'];

	

	$sql = "UPDATE responsible set accessToken = '$token' WHERE id = " . $id;

	echo("Query: " . $sql . "\n");
	
	$result = $connection->query($sql);

	$connection = null;
	
	
	
	

?>

