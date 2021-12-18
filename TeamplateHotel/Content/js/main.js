$(".carousel").carousel({
    interval: 4000
});

(function($) {
    var flatRetinaLogo = function() {
        var retina = window.devicePixelRatio > 1 ? true : false;
        var $logo = $('#logo img');
        var $logo_retina = $logo.data('retina');

        if (retina && $logo_retina) {
            $logo.attr({
                src: $logo.data('retina'),
                width: $logo.data('width'),
                height: $logo.data('height')
            });
        }
    };

    var goTop = function() {
        $(window).scroll(function() {
            if ($(this).scrollTop() > 800) {
                $('.go-top').addClass('show');
            } else {
                $('.go-top').removeClass('show');
            }
        });

        $('.go-top').on('click', function() {
            $("html, body").animate({ scrollTop: 0 }, 1000, 'easeInOutExpo');
            return false;
        });
    };

    var parallax = function() {
        if ($().parallax && isMobile.any() === null) {
            $('.parallax1').parallax("50%", 1);
            $('.parallax2').parallax("50%", 0.7);
        }
    };

    var removePreloader = function() {
        $(window).on("load", function() {
            $(".loader").fadeOut();
            $("#loading-overlay").delay(500).fadeOut('slow', function() {
                $(this).remove();
            });
        });
    };

    var searchIcon = function() {
        $(document).on('click', function(e) {
            var clickID = e.target.id;
            if ((clickID !== 'input-search')) {
                $('.header-search-form').removeClass('show');
            }
        });

        $('.header-search-icon').on('click', function(event) {
            event.stopPropagation();
        });

        $('.header-search-form').on('click', function(event) {
            event.stopPropagation();
        });

        $('.header-search-icon').on('click', function(event) {
            if (!$('.header-search-form').hasClass("show")) {
                $('.header-search-form').addClass('show');
                event.preventDefault();
            } else
                $('.header-search-form').removeClass('show');
            event.preventDefault();

        });

    };

    var headerFixed = function() {
        if ($('body').hasClass('header_sticky')) {
            var nav = $('#header');

            if (nav.length) {
                var offsetTop = nav.offset().top,
                    headerHeight = nav.height(),
                    injectSpace = $('<div/>', {
                        height: headerHeight
                    }).insertAfter(nav);

                $(window).on('load scroll', function() {
                    if ($(window).scrollTop() > offsetTop) {
                        nav.addClass('is-fixed');
                        injectSpace.show();
                    } else {
                        nav.removeClass('is-fixed');
                        injectSpace.hide();
                    }

                    if ($(window).scrollTop() > 300) {
                        nav.addClass('is-small');
                    } else {
                        nav.removeClass('is-small');
                    }
                });
            }
        };
    };

    var responsiveMenu = function() {

        var menuType = 'desktop';
        $(window).on('load resize', function() {

            var currMenuType = 'desktop';



            if (matchMedia('only screen and (max-width: 991px)').matches) {

                currMenuType = 'mobile';

            }



            if (currMenuType !== menuType) {

                menuType = currMenuType;


                if (currMenuType === 'mobile') {

                    var $mobileMenu = $('#mainnav').attr('id', 'mainnav-mobi').hide();

                    var hasChildMenu = $('#mainnav-mobi').find('li:has(ul)');

                    $('#header #site-header-inner').after($mobileMenu);

                    hasChildMenu.children('ul').hide();

                    hasChildMenu.children('a').after('<span class="btn-submenu"></span>');

                    $('.btn-menu').removeClass('active');

                } else {

                    var $desktopMenu = $('#mainnav-mobi').attr('id', 'mainnav').removeAttr('style');



                    $desktopMenu.find('.submenu').removeAttr('style');

                    $('#header').find('.nav-wrap').append($desktopMenu);

                    $('.btn-submenu').remove();

                }

            }

        });


        $('.mobile-button').on('click', function() {

            $('#mainnav-mobi').slideToggle(300);

            $(this).toggleClass('active');

        });



        $(document).on('click', '#mainnav-mobi li .btn-submenu', function(e) {

            $(this).toggleClass('active').next('ul').slideToggle(300);

            e.stopImmediatePropagation()

        });

    };

    $(".block").click(function() {
        var activeTab = $(this).attr("href"); //Find the target via the href
        if ($(activeTab).is(":visible")) {
            $(activeTab).slideUp();
            $(this).removeClass("active");
        } else {
            $(".block").removeClass("active"); //Remove any "active" class
            $(this).addClass("active");
            $(".tab").hide();
            $(activeTab).fadeIn();
        }
        return false;
    });
    $('#demo').owlCarousel({
        margin: 10,
        responsiveClass: true,
        items: 3,
        autoplay: true,
        dots: true,
        responsive: {
            0: {
                items: 1,
                nav: false
            },
            600: {
                items: 2,
                nav: false
            },
            1000: {
                items: 3,
                nav: false,
            }
        }
    })

    $('#hhh').owlCarousel({
        margin: 10,
        responsiveClass: true,
        items: 2,
        autoplay: true,
        dots: false,
        responsive: {
            0: {
                items: 1,
                nav: false
            },
            600: {
                items: 2,
                nav: false
            },
            1000: {
                items: 2,
                nav: false,
            }
        }
    })

    // Dom Ready
    $(function() {
        removePreloader();
        goTop();
        parallax();
        flatRetinaLogo();
        searchIcon();
        headerFixed();
        responsiveMenu();
    });
})(jQuery)