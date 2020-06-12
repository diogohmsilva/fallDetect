<!DOCTYPE html>
<html>
<body>
<div class="slider" id="main-slider">
	<div class="slider-wrapper">
		<?php	

		$id = $_GET['id'];
		$dirname = "photos/".$id;
		$images = glob($dirname."/*.jpg");

		foreach($images as $image) {
   			echo '<img src="'.$image.'" class="slide"/>';
		}
		?>

		

	</div>
</div>	
<style>
html,body {
	margin: 0;
	padding: 0;
}
.slider {
	width: 100%;
	margin: 2em auto;
	
}

.slider-wrapper {
	width: 60vh;
	height: 80vh;
	position: relative;
}

.slide {
	float: left;
	position: absolute;
	width: 100%;
	height: 100%;
	opacity: 0;
	transform: rotate(90deg);
	transition: opacity 0s linear;
}

.slider-wrapper > .slide:first-child {
	opacity: 1;
}

</style>

<script>
(function() {
	
	function Slideshow( element ) {
		this.el = document.querySelector( element );
		this.init();
	}
	
	Slideshow.prototype = {
		init: function() {
			this.wrapper = this.el.querySelector( ".slider-wrapper" );
			this.slides = this.el.querySelectorAll( ".slide" );
			this.previous = this.el.querySelector( ".slider-previous" );
			this.next = this.el.querySelector( ".slider-next" );
			this.index = 0;
			this.total = this.slides.length;
			this.timer = null;
			
			this.action();
			this.stopStart();	
		},
		_slideTo: function( slide ) {
			var currentSlide = this.slides[slide];
			currentSlide.style.opacity = 1;
			
			for( var i = 0; i < this.slides.length; i++ ) {
				var slide = this.slides[i];
				if( slide !== currentSlide ) {
					slide.style.opacity = 0;
				}
			}
		},
		action: function() {
			var self = this;
			self.timer = setInterval(function() {
				self.index++;
				if( self.index == self.slides.length ) {
					self.index = 0;
				}
				self._slideTo( self.index );
				
			}, 250);
		},
		stopStart: function() {
			var self = this;
			self.el.addEventListener( "mouseover", function() {
				clearInterval( self.timer );
				self.timer = null;
				
			}, false);
			self.el.addEventListener( "mouseout", function() {
				self.action();
				
			}, false);
		}
		
		
	};
	
	document.addEventListener( "DOMContentLoaded", function() {
		
		var slider = new Slideshow( "#main-slider" );
		
	});
	
	
})();

</script>

</body>
</html>