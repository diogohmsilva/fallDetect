<?php
	
	$id = $_POST['id'];
	
	if (!is_dir(strval($id))) {	
		mkdir(strval($id));
		copy('postPhotos.php', $id.'/postPhotos.php');
		echo "Directory created";
	}
	
	
		
?>
