<?php
	$received=file_get_contents('php://input');
	
	$images = glob("*.jpg");	

	$now   = time();

	foreach($images as $image)
	{
  		echo $image;
		if (is_file($image)) {
   	   		if ($now - filemtime($image) >= 30 ) { // 30 seconds
     	   			unlink($image);
			}
		}
	}
	
	
	$fileToWrite = time().".jpg";
	file_put_contents($fileToWrite, $received);
		
?>
