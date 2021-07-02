/*! Stellar.js v0.6.2  */
jQuery(document).ready(function() {
					jQuery('#id-testimonial').slick({
						dots: true,
					  centerMode: true,
					  autoplay: true,
					  centerPadding: '0px',
					  slidesToShow: 1,
					  responsive: [
						{
						  breakpoint: 768,
						  settings: {
							arrows: true,
							centerMode: true,
							centerPadding: '0px',
							slidesToShow: 1
						  }
						},
						{
						  breakpoint: 480,
						  settings: {
							dots: false,
							arrows: true,
							centerMode: true,
							centerPadding: '0px',
							slidesToShow: 1
						  }
						}
					  ]
					});
});


/*! Mobile Toggle Button */

	jQuery(document).ready(function() {
		jQuery(".open-off-canvas" ).click(function() {
		  jQuery( "#mobile_menu").toggleClass("mobile_bar_visible", 'fast');
		  jQuery(".body-innerwrapper").toggleClass("body-innerwrapper-canvas", 'fast');
		  jQuery("body").toggleClass("body-offcanvas", 'fast');
		  jQuery(".offcanvas-cover").toggleClass("cover-visible", 'fast');
		});
		
		jQuery("#toggle_button").click(function() {
		  jQuery( "#mobile_menu" ).toggleClass("mobile_bar_visible", 'fast');
		  jQuery(".body-innerwrapper").toggleClass("body-innerwrapper-canvas", 'fast');
		  jQuery("body").toggleClass("body-offcanvas", 'fast');
		  jQuery(".offcanvas-cover").toggleClass("cover-visible", 'fast');
		});
		
		jQuery(".offcanvas-cover").click(function() {
		  jQuery( "#mobile_menu" ).toggleClass("mobile_bar_visible", 'fast');
		  jQuery(".body-innerwrapper").toggleClass("body-innerwrapper-canvas", 'fast');
		  jQuery("body").toggleClass("body-offcanvas", 'fast');
		  jQuery(".offcanvas-cover").toggleClass("cover-visible", 'fast');
		});

	});	
	
	

	
	
	jQuery(function(){		
				jQuery('#ph-camera-slideshow').camera({
					alignment: 'topCenter',
					autoAdvance: true,
					mobileAutoAdvance: true, 
					slideOn: 'random',	
					thumbnails: false,
					time: 10000,
					transPeriod: 600,
					cols: 10,
					rows: 10,
					slicedCols: 10,	
					slicedRows: 10,
					fx: 'simpleFade',
					gridDifference: 250,
					height: '46%',
					minHeight: '500px',
					imagePath: '/quadra/templates/quadra/images/',	
					hover: true,
					loader: 'pie',
					barDirection: 'leftToRight',
					barPosition: 'bottom',	
					pieDiameter: 38,
					piePosition: 'rightTop',
					loaderColor: '#ffc800', 
					loaderBgColor: '#000000', 
					loaderOpacity: 1.0,
					loaderPadding: 2,
					loaderStroke: 2,
					navigation: false,
					playPause: false,
					navigationHover: false,
					mobileNavHover: false,
					opacityOnGrid: false,
					pagination: true,
					pauseOnClick: false,
					portrait: false				});
			});
	
	

			function toggle_visibility(id) {
		var e = document.getElementById(id);
		if(e.style.display == 'block')
		e.style.display = 'none';
		else
		e.style.display = 'block';
		}
		
		

		
			jQuery(window).on("scroll touchmove", function () {
		jQuery('header#top-handler').toggleClass('dark', jQuery(document).scrollTop() > 1);		jQuery('.jump-to-top').toggleClass('visible', jQuery(document).scrollTop() > jQuery(window).height() / 4);
	});
	
	

	
	
	