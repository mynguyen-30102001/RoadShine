$(document).ready(function () {
	var myEle = document.querySelector(".scroll-pos");
	if(myEle){
		var offset = $(".scroll-pos").offset();
		var top = offset.top;
		$('body, html').animate({scrollTop: top},  'slow');
	}

	function fix_hieght () { 
		var vh = window.innerHeight * 0.01;
		// Then we set the value in the --vh custom property to the root of the document
		document.documentElement.style.setProperty('--vh', `${vh}px`);
	}
	fix_hieght();

	// We listen to the resize event
	window.addEventListener('resize', function () {
		// We execute the same script as before
		fix_hieght();
	});

	$(window).scroll(function () { 
		var h_win =  $(window).innerHeight();
		$(".overview").height(h_win);
	});



	// main-menu
	/*$(".menu__item").click(function (e) {
      e.preventDefault();
      $(this).parents(".menu-parents").addClass("romove");
      $(this).find(".menu-child").addClass("active");
   });*/

	$(".nav-item").click(function () {

		$(this).parent(".nav-menu").find(".nav-item").removeClass("active");
		$(this).addClass("active");


	});
	function chang_size() {
		var size_left = $(".over-left").width();
		var w_cen_menu = $(".main-menu_wp").width();
		$(".menu__list").css("left", size_left);
		$(".menu__list").css("width", w_cen_menu);
		$(".menu-child").css("width", size_left);
	}

	chang_size();

	$(function isMobile() {
		$(window).resize(chang_size).trigger('resize');
	});


	// faq-page
	$(".faq-btn").click(function (e) {
		e.preventDefault();
		var coll = $(this).closest(".card-faq").find(".coll-body");
		if (coll.hasClass("show")) {
			$(this).closest(".card-faq").removeClass("open");
		}
		else {
			$(this).closest(".accordion").find(".card-faq").removeClass("open");
			$(this).closest(".card-faq").addClass("open");

		}

	});

	$(".table:not(.table_info) input[type='text']").attr("placeholder", "...");

	$(".article-slide").owlCarousel({
		items: 1,
		loop: true,
		dots: false,
		nav: true,
		autoplay: true,
		autoplayTimeout: 5000,
		autoplaySpeed: 800,
		smartSpeed: 800,
		navText: ['<i class="fas fa-chevron-left"></i>', '<i class="fas fa-chevron-right"></i>'],
	})


	$('.list-offer').owlCarousel({
		margin: 15,
		nav: true,
		autoplayTimeout: 3000,
		autoplaySpeed: 800,
		dots: false,
		responsiveClass: true,
		navText: ['<i class="fas fa-chevron-left"></i>', '<i class="fas fa-chevron-right"></i>'],
		responsive: {
			0: {
				loop: true,
				items: 1,
				autoplay: true,
			},
			600: {
				loop: true,
				items: 2,
				autoplay: true,
			},
			1000: {
				loop: false,
				items: 3,
				autoplay: false,
			}
		}
	})
	$('.list-image').owlCarousel({
		loop: true,
		margin: 0,
		items: 1,
		nav: false,
		dots: true,
		responsiveClass: true,
		responsive: {
			0: {
				items: 1,
			},
			600: {
				items: 1,
			},
			1000: {
				items: 1,
			}
		}
	})

	$(".room-slide").owlCarousel({
		items: 1,
		loop: true,
		margin: 8,
		//mouseDrag: false,
		nav: true,
		dots: false,
		responsiveClass: true,
		autoplayTimeout: 3000,
		autoplaySpeed: 800,
		smartSpeed: 1000,
		navSpeed: 1000,		
		navText: [
			'<i class="fas fa-chevron-left"></i>',
			'<i class="fas fa-chevron-right"></i>',
		],
		responsive: {
			0: {
				stagePadding: 0,
				autoplay: true,
			},
			767: {
				stagePadding: 100,
				autoplay: true,
			},
			1024: {
				stagePadding: 200,
			},
			1200: {
				stagePadding: 250,
			}
		},
	});
	
/*-----*/


$(".drop-button").click(function (e) {
	e.preventDefault();
	$(this).parent(".nav-order").find(".order-dropdown").toggle();
});

$(".order-link").click(function (e) {
	e.preventDefault();
	var get_text = $(this).text();

	$(this).closest(".nav-order").find(".drop-filter").text(get_text);
	$(this).closest(".order-dropdown").hide();
});

var $menu1 = $('.nav-order');
$(document).mouseup(e => {
	if (!$menu1.is(e.target) // if the target of the click isn't the container...
		&&
		$menu1.has(e.target).length === 0) // ... nor a descendant of the container
	{
		$(".order-dropdown").hide();
	}
});


//
$('.tooltipLink').hover(function () {
	var title = $(this).attr('data-tooltip');
	$(this).data('tipText', title);
	if (title == '') { } else {
		$('<p class="tiphover"></p>').fadeIn(200).text(title).appendTo('body');
	}
}, function () {
	$(this).attr('data-tooltip', $(this).data('tipText'));
	$('.tiphover').fadeOut(200);
}).mousemove(function (e) {
	var mousex = e.pageX;
	var mousey = e.pageY;
	$('.tiphover').css({
		top: mousey,
		left: mousex
	})
});

$(function swatch_se() {
	if ($(window).width() > 767) {
		$(".swatch").click(function (e) {
			e.preventDefault();
			var $this = $(this);
			$(this).closest('.no-bullet ').find('.swatch').not($this).removeClass("open");
			if ($(this).hasClass("open")) {
				$(this).removeClass("open");
			}
			else {
				$(this).addClass("open");
			}
		});
	}
});
	


/* order datepicker*/

	
$("#dateStart").daterangepicker({
  singleDatePicker: true,
  autoApply: true,
  minDate: moment(),
/*	locale: {
      format: 'DD/MM/YYYY'
    }*/
});

$("#dateEnd").daterangepicker({
  singleDatePicker: true,
  autoApply: true,
  minDate: moment(),
  startDate: moment().add(1, "days"),
	
});

$("#dateStart").on("apply.daterangepicker", function (e, picker) {
  var mindate = $("#dateStart").val();

  $("#dateEnd").daterangepicker({
    minDate: mindate,
    singleDatePicker: true,
    autoApply: true,
  });
});
	

/*******/
$('.mater-slide').owlCarousel({
		nav: true,
		autoplayTimeout: 3000,
		autoplaySpeed: 800,
		dots: false,
		responsiveClass: true,
		navText: ['<i class="fas fa-chevron-left"></i>', '<i class="fas fa-chevron-right"></i>'],
		responsive: {
			0: {
				loop: true,
				items: 1,
				autoplay: true,
			},
			600: {
				loop: true,
				items: 2,
				autoplay: true,
			},
			1000: {
				loop: false,
				items: 3,
				autoplay: false,
			}
		}
	})
	
/*-----*/
	  //Scroll event

	function backTop() {
		if ($(window).scrollTop() > 500) {
			$('.back-top-wp').fadeIn('slow');
		} else {
			$('.back-top-wp').fadeOut('slow');
		}
	}
	
	backTop();

  $(window).scroll(function(){
   backTop();
  });
  
  //Click event
  $('.back-top-wp').click(function () {
    $("html, body").animate({ scrollTop: "0" },  500);
  });
})

