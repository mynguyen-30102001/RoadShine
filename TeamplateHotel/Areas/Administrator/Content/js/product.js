
$("#create").click(function (e) {
    var editors = $("textarea");
    $(this).attr('disabled', 'disabled');
    $("#Description").val(CKEDITOR.instances['Description'].getData());
    if (editors.length) {
        editors.each(function () {
            var instance = CKEDITOR.instances[$(this).attr("id")];
            if (instance && $(this).attr("id") !== 'Description') {
                instance.destroy(true);
                $(this).val(instance.getData());
                $(this).hide();
            }
        });
    }
    var form = $("#productfrom").serialize();
 
    $.ajax({
        type: 'POST',
        url: "/admin/Product/Create",
        data: form,
        dataType: 'json',
        success: function (data) {
            if (data.success === true) {
                bootbox.alert("Thêm thông tin thành công", function () {
                    console.log("success");
                    window.location = "/admin/Product/Index";

                });
            } else {
                console.log(form);
                bootbox.alert("Lỗi");
                $(this).removeAttr('disabled');
            }
        },
        error: function () {
            bootbox.alert("Lỗi");
            $(this).removeAttr('disabled');
        }
    });
});


$("#update").click(function (e) {
    var editors = $("textarea");
    $(this).attr('disabled', 'disabled');
    $("#Description").val(CKEDITOR.instances['Description'].getData());
    if (editors.length) {
        editors.each(function () {
            var instance = CKEDITOR.instances[$(this).attr("id")];
            if (instance && $(this).attr("id") !== 'Description') {
                instance.destroy(true);
                $(this).val(instance.getData());
                $(this).hide();
            }
        });
    }
    var form = $("#productfrom").serialize();
 
    $.ajax({
        type: 'POST',
        url: "/admin/Product/Update",
        data: form,
        dataType: 'json',
        success: function (data) {
            if (data.success === true) {
                bootbox.alert("Sửa thông tin thành công", function () {
                    window.location = "/admin/Product/Index";

                });
            } else {
                bootbox.alert("Lỗi");
                $(this).removeAttr('disabled');
            }
        },
        error: function () {
            bootbox.alert("Lỗi");
            $(this).removeAttr('disabled');
        }
    });
});