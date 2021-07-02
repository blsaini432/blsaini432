(function($) {
	
   "use strict";	
	
	var mainwindow = $(window);



/* ==========================================================================
		Submenu Dropdown Toggle
   ========================================================================== */
   
	if($('.main-header li.dropdown ul').length){
		$('.main-header li.dropdown').append('<div class="dropdown-btn"><span class=""></span></div>');
		
		//Dropdown Button
		$('.main-header li.dropdown .dropdown-btn').on('click', function() {
			$(this).prev('ul').slideToggle(500);
		});
		
		
		//Disable dropdown parent link
		$('.navigation li.dropdown > a').on('click', function(e) {
			e.preventDefault();
		});
	}


/* ==========================================================================
		            Partner 
   ========================================================================== */

		// test-active
	$('.brand-active').owlCarousel({
		smartSpeed:1000,
		margin:0,
		nav:false,
		autoplay:true,
		autoplayTimeout:3000,
		loop:true,
		navText:['<i class="fa fa-caret-left"></i>','<i class="fa fa-caret-right"></i>'],
		responsive:{
			0:{
				items:1
			},
			450:{
				items:2
			},
			678:{
				items:3
			},
			1000:{
				items:4
			}
		}
	})
	
	
	
	
	
	
	
/* ==========================================================================
		            service slider
   ========================================================================== */
    $('.tour-carousel').owlCarousel({
        loop: true,
        rtl: true,
        margin: 10,
        nav: true,
        dots: false,
        autoplay: true,
        autoplayTimeout: 10000,
        autoplayHoverPause: false,
        autoplaySpeed: 2000,
        animateOut: '',
        animateIn: 'zoomIn',
        navText: [
            '<i class="fa fa-arrow-left"></i>',
            '<i class="fa fa-arrow-right"></i>'
        ],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            1000: {
                items: 3
            }
        }
    });
	
})(window.jQuery);
