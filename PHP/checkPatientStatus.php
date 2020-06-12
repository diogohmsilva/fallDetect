<?php
	
	
	$host="";	
	$user="";	
	$password="";	
	$dbname = $user;

 
	$connection = new PDO("mysql:host=" . $host. ";dbname=" . $dbname, $user, $password, array(PDO::ATTR_ERRMODE => PDO::ERRMODE_WARNING));

	$id = $_GET['id'];

	$sql = "select isOK from patient where id = " . $id;

	$result = $connection->query($sql);

	foreach($result as $r){
		echo $r['isOK'];
	}

        $connection = null;

?>
