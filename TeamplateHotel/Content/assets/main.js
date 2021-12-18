/**
 * 通用模块
 *
 */
var $win = $(window),
    $doc = $(document),
    $body = $('body', $doc),
    jHead = $('.sidenav', $body),
    winW = $win.width();

$(window).resize(function() {
    winW = $win.width();
})


/**
 * 图片加载
 */
$(function() {
    if (!$.fn.lazyload) return;
    $("img.lazy", $body).lazyload({
        effect: "fadeIn",
        threshold: 200,
        failure_limit: 0
    });
});

// // 语言版本下拉
// $(function() {
//     var $nav_li = $(".header .lang");
//     $nav_li.hover(function() {
//         $(this).find('ul').stop(true, true).slideDown(200);
//     }, function() {
//         $(this).find('ul').slideUp(200);
//     });
// });
//语言下拉
$(function() {
    if (winW < 768) return;
    $(".language-box").on("mouseenter", function() {
        $(this).children("ul").stop().slideDown(200);
    }).on("mouseleave", function() {
        $(this).children("ul").stop().slideUp(200);
    });
});


// $(".story-nav>ul>li").hover(function() {
//     var par = $(this);
//     if (par.attr("class") == "on") {
//         $(".story-nav>ul>li").addClass("on").children('.level-2').slideUp();
//         par.children('.level-2').slideDown();
//         par.removeClass("on").addClass("current").siblings().removeClass('current');
//     } else {
//         par.children('.level-2').slideUp();
//         par.addClass("on").removeClass("current");
//     }

// });

// 飞来飞去
$(function() {
    if ($win.width() > 992) {
        if (!(/msie [6|7|8|9]/i.test(navigator.userAgent))) {
            new WOW().init();
        };
    }

});



// 导航滑动
$(function() {
    var jCate = $('.category ul', $body);
    var wrap = $('.category');
    var wrapWidth = wrap.width();
    if (jCate.width() < wrap.width()) {
        jCate.width("100%");
    } else {
        jCate.on('click', 'a', function(event) {
            event.preventDefault();
            event.stopPropagation();
            console.log(this.href);
            location.href = this.href;
        });

        jCate.on('touchstart', function(event) {
            jCate.addClass('touchstart');
            var touch = event.originalEvent.targetTouches[0];
            var data = {
                'touchX': touch.pageX,
                'width': jCate.width(),
                'left': parseInt(jCate.css('left')),
                'wwidth': wrapWidth
            };

            if (data.width < data.wwidth) {
                return true;
            }

            jCate.on('touchmove', data, touchMove);
            jCate.on('touchend', touchEnd);
        });
    }


    //  TAB 滑动
    var touchMove = function(event) {
        event.preventDefault();

        var touch = event.originalEvent.targetTouches[0];
        var touchX = touch.pageX;

        var incr = touchX - event.data.touchX;
        var left = event.data.left + incr;

        if (left > 0) {
            left = 0;
        } else if ((event.data.wwidth - left) > event.data.width) {
            left = event.data.wwidth - event.data.width;
        }

        jCate.css('left', left);
    };

    var touchEnd = function(event) {
        jCate.removeClass('touchstart');
        jCate.off('touchmove', touchMove);
        jCate.off('touchend', touchEnd);
    };


    /**
     * init-pos
     */
    (function() {
        var width = jCate.find('.active').width();
        var offset = jCate.find('.active').offset();

        var winWidth = $win.width();
        var cateWidth = jCate.width();

        // ($win - jCate) < left < 0
        if (offset && cateWidth > winWidth) {
            var left = winWidth / 2 - (offset.left + width / 2);
            left = Math.min(left, 0);
            left = Math.max(left, winWidth - cateWidth);
            jCate.css('left', left);
        }
    })();
});




//头部导航
$(function() {
    //折叠导航
    var oset;
    $(".nav-collapse").click(function(e) {
        if (e && e.stopPropagation) {
            e.stopPropagation();
        } else {
            window.event.cancelBubble = true;
        }
        $(".nav-collapse").toggleClass("active");

        $(".nav").stop().fadeToggle().toggleClass("fade-out");
        $("body").toggleClass("fixed");
        $(".nav").removeClass("left-100 left-200");

        if (!$(this).hasClass("active")) {
            $(".nav").hide();
            $(".nav-list1").find("li").removeClass("act");
        } else {
            clearTimeout(oset);
            $(".nav-list1 li").each(function(index, val) {
                var me = $(this);
                var num = $(this).index()
                oset = setTimeout(function() {
                    me.addClass("act");
                }, (index * 15))
            })
        }
    })

});

$(function() {

    var list1 = $(".nav-list1"),
        list2 = $(".nav-list2"),
        list3 = $(".nav-list3");
    list1.on("click", ".more", function(event) {
        if (winW > 1199) return;
        event.stopPropagation();
        event.preventDefault();
        $(".nav").addClass("left-100");
        var ostr = "";
        ostr = $(this).children(".nav-list2").html();
        ohref = $(this).children("a").clone(true);
        $(".nav-2 .content ul").html(ostr).children("li").has(".nav-list3").addClass("more");
        $(".nav-2 h2").html(ohref);

    })
    $(".nav2-list2").on("click", "li", function(event) {
        if (winW > 1199) return;
        event.stopPropagation();
        if ($(this).hasClass("more")) {
            event.preventDefault();
            $(".nav").addClass("left-200");
            var ostr = "";
            ostr = $(this).children(".nav-list3").html();
            ohref = $(this).children("h4").children("a").clone(true);
            console.log($(this).children("h4 a"));
            $(".nav-3 .content ul").html(ostr);
            $(".nav-3 h2").html(ohref);
        }
    })

    list1.on("mouseenter", ".more", function(event) {

        if (winW < 1200) return;
        var me2 = $(this).children(".nav-list2");
        me2
            .stop().slideDown("fast")
            .children("li").has(".nav-list3").addClass("more");

    }).on("mouseleave", ".more", function(event) {

        if (winW < 1200) return;
        $(this).children(".nav-list2").stop().slideUp("fast");

    })

    list2.on("mouseenter", "li", function(event) {
        if (winW < 1200) return;
        console.log($(this).children(".nav-list3"))
        // $(this).children(".nav-list3").stop().slideDown("fast");
    }).on("mouseleave", "li", function(event) {
        if (winW < 1200) return;
        // $(this).children(".nav-list3").stop().slideUp("fast");
    })

    $(".back-btn2").click(function() {
        $(".nav").removeClass("left-100");
    })
    $(".back-btn3").click(function() {
        $(".nav").removeClass("left-200");
    })
    //查找按钮
    $(".find").click(function(e) {
        if (e && e.stopPropagation) {
            //W3C取消冒泡事件
            e.stopPropagation();
        } else {
            //IE取消冒泡事件
            window.event.cancelBubble = true;
        }
        $(".search-lg").stop().slideToggle();
        if (winW < 1200) {
            $("#btn").removeClass("active");
        }
    })

    $(".search-icon").click(function() {
        if (winW < 1200) {
            $(".search-xs").addClass("show")
        }
    })
    $(".nav").click(function(e) {
        if (!$(e.target).hasClass("input-text") && !$(e.target).hasClass("search-icon")) {
            console.log(e.target);
            $(".search-xs").removeClass("show")
        }
    })

    $(".close-btn").click(function() {
        $(".search-lg").slideUp("fast");
    })


});


//产品详情
$(document).ready(function() {

    var sync1 = $("#sync1");
    var sync2 = $("#sync2");

    sync1.owlCarousel({
        autoPlay: true,
        lazyLoad: true,

        singleItem: true,
        slideSpeed: 1000,
        navigation: true,
        pagination: false,
        afterAction: syncPosition,
        responsiveRefreshRate: 200,
        beforeInit: function() {
            if (winW < 768) {
                $(".banner").find("img").each(function(i, item) {
                    $(this).attr("src", $(this).attr("data-xssrc"))
                    $(this).attr("data-src", $(this).attr("data-xssrc"))
                })
            }
        },
    });

    sync2.owlCarousel({
        autoPlay: true,
        lazyLoad: true,
        items: 3,
        itemsDesktop: [1200, 3],
        itemsDesktopSmall: [991, 3],
        itemsTablet: [767, 3],
        itemsMobile: [480, 3],
        pagination: false,
        responsiveRefreshRate: 100,
        afterInit: function(el) {
            el.find(".owl-item").eq(0).addClass("synced");
        }
    });

    function syncPosition(el) {
        var current = this.currentItem;
        sync2
            .find(".owl-item")
            .removeClass("synced")
            .eq(current)
            .addClass("synced")
        if (sync2.data("owlCarousel") !== undefined) {
            center(current)
        }

    }

    sync2.on('click', '.owl-item', function(e) {
        e.preventDefault();
        var number = $(this).data("owlItem");
        sync1.trigger("owl.goTo", number);
    });

    function center(number) {
        var sync2visible = sync2.data("owlCarousel").owl.visibleItems;

        var num = number;
        var found = false;
        for (var i in sync2visible) {
            if (num === sync2visible[i]) {
                var found = true;
            }
        }

        if (found === false) {
            if (num > sync2visible[sync2visible.length - 1]) {
                sync2.trigger("owl.goTo", num - sync2visible.length + 2)
            } else {
                if (num - 1 === -1) {
                    num = 0;
                }
                sync2.trigger("owl.goTo", num);
            }
        } else if (num === sync2visible[sync2visible.length - 1]) {
            sync2.trigger("owl.goTo", sync2visible[1])
        } else if (num === sync2visible[0]) {
            sync2.trigger("owl.goTo", num - 1)
        }
    }

});
//全屏banner

/*
 *  @author HSM
 *  @email
 *  @qq
 *   @lastdate 2018年1月19日09:52:40
 * 插件功能 全屏banner图
 */

(function($) {
    var pluginName = 'fullBanner', //定义插件名
        //插件的参数默认值
        defaults = {
            imgW: 1920, //图片宽度
            imgH: 950, //图片高度
            removeHeader: false, //是否需要去除头部的高度
            headerEl: ".header", //如果需要去除头部  按照$()索引
        };

    //... 插件主体功能代码 ...
    $.fn[pluginName] = function(options) {

        var settings = $.extend({}, defaults, options); //将默认值,参数值合并到setting

        //主体代码开始

        var $this = $(this),
            item = $this.find(".owl-item"),
            imgs = $this.find("img"),
            W = $(window),
            header = null,
            headerH = 0,
            winW = W.width(),
            winH = W.height(),
            banW = function() {
                return winW;
            },
            banH = function() {
                return winH - headerH;
            };

        //根据原始图片宽高比例 等比例缩放
        function cal() {
            //判断是否需要出去header的高度
            if (settings.removeHeader) {
                header = $(settings.headerEl);
                if (header) {
                    headerH = header.height();
                }
            }
            $this.css("marginTop", headerH)
            item.height(banH());
            if (settings.imgW / settings.imgH >= banW() / banH()) {
                imgs.css({
                    "height": banH(),
                    "width": banH() / (settings.imgH / settings.imgW),
                    "marginLeft": -(banH() / (settings.imgH / settings.imgW)) / 2,
                    "marginTop": -banH() / 2
                })
            } else {
                imgs.css({
                    "height": banW() / (settings.imgW / settings.imgH),
                    "width": banW(),
                    "marginLeft": -banW() / 2,
                    "marginTop": -(banW() / (settings.imgW / settings.imgH)) / 2
                })

            }
        }
        cal();
        var oset = null;
        W.on("resize", function() {
            clearTimeout(oset);
            oset = setTimeout(function() {
                winW = W.width();
                winH = W.height();
                cal();
            }, 300);
        })

    }

})(jQuery);

$(function() {

    (function() {
        if (winW > 991) {
            $(".index-banner").fullBanner({
                imgW: 1920, //图片宽度
                imgH: 960, //图片高度
                removeHeader: false, //是否需要去除头部的高度
                // headerEl: "header", //如果需要去除头部  按照$()索引
            });
        } else {
            $(".index-banner").fullBanner({
                imgW: 600, //图片宽度
                imgH: 960, //图片高度
                removeHeader: true, //是否需要去除头部的高度
                // headerEl: "header", //如果需要去除头部  按照$()索引
            });
        }
    })();

});




var oSwiper = new Swiper('#o-c', {
    direction: 'vertical',
    mousewheelControl: true,
    pagination: '.swiper-pagination',
    paginationClickable: true,
    mousewheelReleaseOnEdges: true,
    prevButton: '.swiper-button-prev',
    nextButton: '.swiper-button-next',
    speed: 1000,


    // 如果需要分页器

    slidesPerView: 'auto',
    onSlideChangeStart: function(swiper) {
        if (swiper.activeIndex == 0) {
            $('.sidenav').removeClass('side');
        } else {
            $('.sidenav').addClass('side');
        }
        for (i = 0; i < swiper.slides.length; i++) {
            slide = swiper.slides.eq(i);
            slide.removeClass('ani-slide').addClass('ani-slideout');
        }

        slide = swiper.slides.eq(swiper.activeIndex);
        slide.removeClass('ani-slideout').addClass('ani-slide');
    },
    onSlideChangeEnd: function(swiper) {

        $('.counter').countUp(600);

    },
    onTransitionEnd: function(swiper) {
        if (swiper.progress == 1) {
            swiper.activeIndex = swiper.slides.length - 1
        }
    },
    onSetTransition: function(swiper) {
        if (swiper.activeIndex == 6) {
            swiper.params.onlyExternal = true;
            swiper.disableMousewheelControl();
        } else {
            swiper.params.onlyExternal = false;
            swiper.enableMousewheelControl();
        }

    }

})

var iSwiper = new Swiper('#i-c1', {
    scrollbar: '.swiper-scrollbar',
    direction: 'vertical',
    slidesPerView: 'auto',
    freeMode: true,
    freeModeMomentum: false,
    mousewheelControl: true,
    mousewheelSensitivity: 0.5,
    onSetTransition: function(swiper, translate) {
        //translate 一直为0，不可直接用
        nowTranslate = swiper.translate;
        if (typeof(beforeTranslate) == "undefined") { beforeTranslate = 0 };
        slideHeight = swiper.slides[0].scrollHeight;
        swiperHeight = swiper.height
        if (nowTranslate > -2 && nowTranslate > beforeTranslate) { oSwiper.slideTo(5); }
        if (slideHeight - swiperHeight + nowTranslate < 2 && nowTranslate < beforeTranslate) { oSwiper.slideTo(7); }
        beforeTranslate = nowTranslate;
    },
    onTouchEnd: function(swiper) {
        if (swiper.translate > 0) { oSwiper.slideTo(0); }
        if (swiper.translate < (swiper.height - swiper.slides[0].scrollHeight)) { oSwiper.slideTo(7); }
    }

});





//产品详情
$(document).ready(function() {

    var sync1 = $("#sync3");
    var sync2 = $("#sync4");

    sync1.owlCarousel({
        autoPlay: false,
        lazyLoad: true,

        singleItem: true,
        slideSpeed: 1000,
        navigation: true,
        pagination: false,
        afterAction: syncPosition,
        responsiveRefreshRate: 200,
    });

    sync2.owlCarousel({
        autoPlay: false,
        lazyLoad: true,
        items: 5,
        itemsDesktop: [1200, 5],
        itemsDesktopSmall: [991, 5],
        itemsTablet: [767, 5],
        itemsMobile: [480, 4],
        pagination: false,
        responsiveRefreshRate: 100,
        afterInit: function(el) {
            el.find(".owl-item").eq(0).addClass("synced");
        }
    });
    if ($.fn.imagezoom && winW > 991) {
        $('.wrap-product-show .product-show .album .sync1 img').imagezoom({
            offset: 10,
            xzoom: 300,
            yzoom: 300
        });
    }

    function syncPosition(el) {
        var current = this.currentItem;
        sync2
            .find(".owl-item")
            .removeClass("synced")
            .eq(current)
            .addClass("synced")
        if (sync2.data("owlCarousel") !== undefined) {
            center(current)
        }

    }

    sync2.on('click', '.owl-item', function(e) {
        e.preventDefault();
        var number = $(this).data("owlItem");
        sync1.trigger("owl.goTo", number);
    });

    function center(number) {
        var sync2visible = sync2.data("owlCarousel").owl.visibleItems;

        var num = number;
        var found = false;
        for (var i in sync2visible) {
            if (num === sync2visible[i]) {
                var found = true;
            }
        }

        if (found === false) {
            if (num > sync2visible[sync2visible.length - 1]) {
                sync2.trigger("owl.goTo", num - sync2visible.length + 2)
            } else {
                if (num - 1 === -1) {
                    num = 0;
                }
                sync2.trigger("owl.goTo", num);
            }
        } else if (num === sync2visible[sync2visible.length - 1]) {
            sync2.trigger("owl.goTo", sync2visible[1])
        } else if (num === sync2visible[0]) {
            sync2.trigger("owl.goTo", num - 1)
        }
    }

});

$(function() {

    if ($('.dowebok').length == 0) return;

    $('.dowebok').liMarquee({
        direction: 'up',
        scrollamount: 50,
        drag: false
    });
});

$(function() {
    $(".btn-relate").on("click", function() {
        $("html,body").animate({
            scrollTop: $("#relate").offset().top - 100
        }, 500)
    })
});

//var myEle = document.querySelector(".wrap-message");
//if (myEle) {
//    var offset = $(".wrap-message").offset();
//    var top = offset.top;
//    $('body, html').animate({ scrollTop: top }, 'slow');
//}
// 手机底部
$(".footer .foot ul li .title-foot").click(function() {
    if ($win.width() < 991) {
        var par = $(this).parent().parent();
        if (par.attr("class") == "on") {
            $(".footer .foot ul li .title-foot").parent().parent().addClass("on").find('.info-down').slideUp();
            par.find('.info-down').slideDown();
            par.removeClass("on").addClass("current").siblings().removeClass('current');
        } else {
            par.find('.info-down').slideUp();
            par.addClass("on").removeClass("current");
        }
    }

});

// 表格
$(function() {
    var oTable = $("table");
    if (oTable.length !== 0) {
        var oTr = oTable.find('tr'),
            oTd = oTable.find('td');

        oTable.wrap("<div class='table-box'></div>");

        oTr.attr("style", "");

        oTd.each(function(index) {
            if (typeof($(this).attr("style")) !== "undefined") {

                if ($(this).attr("style").indexOf("text-align: center") >= 0) {
                    $(this).attr("style", "text-align: center");
                } else {
                    $(this).attr("style", "");
                }

            }
        })

    }
});

$(function() {
    if (!placeholderSupport()) { // 判断浏览器是否支持 placeholder
        $('[placeholder]').focus(function() {
            var input = $(this);
            if (input.val() == input.attr('placeholder')) {
                input.val('');
                input.removeClass('placeholder');
            }
        }).blur(function() {
            var input = $(this);
            if (input.val() == '' || input.val() == input.attr('placeholder')) {
                input.addClass('placeholder');
                input.val(input.attr('placeholder'));
            }
        }).blur();
    };
})

function placeholderSupport() {
    return 'placeholder' in document.createElement('input');
}



/**
 * Google Maps
 */
$(function () {
    var map;
    window.initGoogleMap = function () {
        var node = $('#google-map').data();

        map = new google.maps.Map(document.getElementById('google-map'), {
            center: new google.maps.LatLng(35.986154, 120.19494),
            zoom: 12
        });

        var marker = new google.maps.Marker({
            title: node.title,
            position: map.getCenter(),
            map: map
        });

        var infowindow = new google.maps.InfoWindow();
        infowindow.setContent('<b>' + node.company + '</b><br>Add: ' + node.address + '');

        marker.addListener('click', function () {
            map.setZoom(18);
            map.setCenter(marker.getPosition());
            infowindow.open(map, marker);
        });
    }
});
