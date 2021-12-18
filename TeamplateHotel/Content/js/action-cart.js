function LoadCount() {
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '/Getlstcart',
        success: function (data) {
            if (data.totalp > 0) {
                $('#spnCart').text(data.totalp);

            } else {
                $('#spnCart').html('0');
                //$('.lstproduct').css('display' , 'none')
                //$("#lstproduct").html('<li class="single-cart-item"> <p>Cart Emty !</p> </li>');


                //lstproduct1.html();

            }

        },
        error: function (data) {
            alert(data);
        }
    });
}

$('.divAddCart').on('click', function () {
    var pID = $(this).data('id');

    var pQuan = $('#newQuantity').val();
    //alert(pQuan);

    console.log(pID);
    $.ajax({
        method: "POST",
        url: '/ThemVaoGio',
        data: JSON.stringify({
            'sanPhamID': pID,
            'newQuantity': pQuan,
        }),
        dataType: 'JSON',
        contentType: "application/json",
        success: function (data) {
            if (data.status) {
                $('#spnCart').text(data.totalp);
                Swal.fire({
                    position: 'top-end',
                    type: 'success',
                    title: '<h5><b> Product has been saved to cart </b></h5>',
                    showConfirmButton: false,
                    width: '300px',
                    timer: 1500
                });
            }

        },
        error: function (data) {
            alert("false", data);
        }
    });
});


$('#clickshowcart').on('click', function () {
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '/Getlstcart',
        success: function (data) {
            if (data.totalp > 0) {
                $('#lstproduct').css('height', '25em')
            }

            console.log("Data + ", data);

            var lstproduct = $("#lstproduct");
            var result = "";

            lstproduct.html('');

            //data.data.forEach((item, index) => {
            //    result += '<b> 1 : </b>' + item.SanPhamID + '<br/>' +

            //        '<b> 2 :</b>' + item.TenSanPham + '<br/>' +

            //        '<b> 3 :</b>' + item.SoLuong + '<br/>' +

            //        '<b> 4 :</b>' + item.DonGia + '<hr/>';
            //});

            data.data.forEach((item, index) => {
                result +=
                    '<li class="single-cart-item">' +
                    ' <div class="cart-img">' +
                    '<a href="#"><img src=" ' + item.Hinh + ' " alt="">' + '</a>' +
                    '</div>' +
                    '<div class="cart-content">' +
                    '<h5 class="product-name">' + ' <a href="#">' + item.TenSanPham + '</a>' + '</h5>' +
                    '<span class="product-quantity">' + item.SoLuong + ' x ' + '</span>' +
                    '<span class="product-price">' + '$ ' + item.DonGia + '</span>' +
                    '</div>' +
                    '<div class="cart-item-remove">' +
                    ' <a title="Remove" onclick="DeletePro(this)" data-id="' + item.SanPhamID + ' " > <i class="fa fa-trash"></i></a>' +
                    '</div>' +
                    '</li>';
            });

            //$.each(data, function (id, item) {

            //    result += '<b> 1 : </b>' + item.SanPhamID + '<br/>' +

            //        '<b> 2 :</b>' + item.TenSanPham + '<br/>' +

            //        '<b> 3 :</b>' + item.SoLuong + '<br/>' +

            //        '<b> 4 :</b>' + item.DonGia + '<hr/>';

            //});
            console.log(result);
            lstproduct.html(result);
            $('.header-cart-dropdown').toggleClass('showcart');


        },
        error: function (data) {
            alert(data);
        }
    });
});

function DeletePro(e) {
    //debugger;
    //var idpr = $('#deletepr').data('value');
    var id = $(e).data('id');

    console.log("ID seclect + ", id);

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: '/XoaKhoiGio?sanPhamID=' + id,
                type: 'GET',
                success: function (response) {
                    if (response.status) {
                        Swal.fire(
                            'Deleted!',
                            'Your file has been deleted.',
                            'success'
                        );
                        timer: 1500;
                        location.reload();
                    };

                    if (e !== null) {
                        $(e).closest('tr').remove();
                    }
                }
            });
        } else {
            Swal.fire("Your imaginary file is safe!");
        }
    });
}