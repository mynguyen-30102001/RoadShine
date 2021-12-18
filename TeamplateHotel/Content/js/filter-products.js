
var homeconfig = {
    pageSize: 9,
    pageIndex: 1,
};



var searchController = {
    init: function () {
        searchController.loadDataSelect({});
        //searchController.loadDataSelect2();

        searchController.registerEvent();
    },
    registerEvent: function () {
    },

    loadDataSelect: function (filter) {
         //if () {
        //    console.log('!!!!');
        //}
        var category = filter['categ'] !== 'undefined' ? filter['categ'] : null;
        var sort = filter['sort'] !== 'undefined' ? filter['sort'] : null;
        var language = $('#Language').val();
        var menuid = $('#MenuID').val();
        var data = { 'Language': language, 'Menuid': menuid, 'CategoryListId': category, 'sort': sort };
        $.ajax({
            url: "/filter-product",
            method: "POST",
            data: JSON.stringify({
                'filter': data,
                minximunPrice: filter['min'],
                maximunPrice: filter['max'],
                page: homeconfig.pageIndex,
                pageSize: homeconfig.pageSize
            }),
            dataType: 'JSON',
            contentType: "application/json",

            success: function (response) {
                if (response.status) {
                    $("#loader").css("display", "none");
                    var data = response.data;
                    var listcount = response.total;
                    //$("#counttour").html(listcount);
                    var html = '';
                     $.each(data,
                         function (i, item) {
                            html += '<div class="col-lg-4 col-md-6 col-sm-6">';
                            html += '<div class="single-grid-product mb-30">';
                            html += '<div class="product-image">';
                            html += '<div class="product-label">';
                            //html += '<span class="sale">-20%</span>';
                            html += '<span class="new">New</span>';
                            html += '</div>';
                            html += '<a href="/' + item.MenuAlias + '/' + item.ID + '/' + item.Alias +' ">';
                            html += '<img src="'+ item.Image +'" class="img-fluid" alt="">';
                            html += '<img src="' + item.Image2 +'" class="img-fluid" alt="">';
                            html += '</a>';
                            html += '<div class="product-action d-flex justify-content-between">';
                             html += ' <a class="product-btn divAddCart"  onclick="Addtocart(this)" data-id="' + item.ID +'" >@GetLanguage.Language(Request.Cookies["LanguageID"].Value, "AddtoCart")</a>';
                            html += ' <ul class="d-flex">';
                            html += ' <li><a href="#"><i class="ion-android-favorite-outline"></i></a></li>';
                            html += ' </ul>';
                            html += ' </div>';
                            html += '</div>';
                            html += '<div class="product-content">';
                            html += ' <div class="product-category-rating">';
                             html += '<span class="category"><a href="/">' + item.Title+'</a></span>';
                            html += '<span class="rating">';
                            html += ' <i class="ion-android-star active"></i>';
                            html += ' <i class="ion-android-star active"></i>';
                            html += ' <i class="ion-android-star active"></i>';
                            html += ' <i class="ion-android-star active"></i>';
                            html += ' <i class="ion-android-star active"></i>';
                            html += ' </span>';
                            html += '</div>';
                            html += '<h3 class="title"> <a href="#"> ' + item.NameProduct +' </a></h3>';
                            html += '<p class="product-price">';
                            html += '<span class="discounted-price">$' + item.Price +'</span>';
                            html += ' <span class="main-price discounted">$ ' + item.PricePromo +'</span>';
                            html += '</p>';
                            html += '</div>';
                            html += '</div>';
                            html += '</div>';
                            return html;

                        });
                    $('#databin').html(html);

                    //searchController.paging(response.total,
                    //    function () {
                    //        searchController.loadDataSelect();
                    //    });
                    //searchController.registerEvent();
                }
            }
        });
    },
    //paging: function (totalRow, callBack) {
    //    var totalPage = Math.ceil(totalRow / homeconfig.pageSize);
    //    $('#pagination').twbsPagination({
    //        totalPages: totalPage,
            
    //        visiblePages: 3,
    //        onPageClick: function (event, page) {
    //            homeconfig.pageIndex = page;
    //            setTimeout(callBack, 10);
    //        }
    //    });
    //}
}
searchController.init();
//function getfilter(themes) {
//    var filter = [];
//    $('.' + themes + ':checked').each(function () {
//        filter.push($(this).val());
//    });
//    return filter;
//}