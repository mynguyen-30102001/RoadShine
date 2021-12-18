//$(function () {
//    $('.form1').on('click', '.send1', function (event) {
//        event.preventDefault();
//        var jForm = $(event.delegateTarget), jThis = $(this);
//        if (jThis.hasClass('disabled')) {
//            alert('Please wait...');
//            return;
//        }

//        var reg_email = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;

//        var data = {
//            id: 'book', _spam: '{$session.project_spam}',
//            title: $('input[name="title"]', jForm).val().trim(),
//            address: $('input[name="address"]', jForm).val().trim(),
//            email: $('input[name="email"]', jForm).val().trim(),
//            tel: $('input[name="tel"]', jForm).val().trim(),
//            mobile: $('input[name="mobile"]', jForm).val().trim(),
//            country: $('input[name="country"]', jForm).val().trim(),
//            content: $('textarea[name="content"]', jForm).val().trim(),
//        };


//        if (!data.email || !reg_email.test(data.email)) {
//            alert('Please enter a valid email address');
//            // alert('请输入一个有效的邮箱地址');
//            $('input[name="email"]', jForm).focus();
//            return false;
//        } else if (!data.title) {
//            alert('The Name cannot be empty');
//            // alert('请输入留言主题');
//            $('input[name="title"]', jForm).focus();
//            return false;
//        } else if (!data.content) {
//            alert('The message cannot be empty');
//            // alert('请输入留言内容');
//            $('textarea[name="content"]', jForm).focus();
//            return false;
//        }


//        jThis.css({opacity: '.5'}).addClass('disabled');

//        //$.ajax({
//        //    'url' : 'api.php?c=post&f=save&_t='+(new Date().getTime()),
//        //    'data': jForm.serializeArray(),
//        //    'type' : 'post',
//        //    'dataType' : 'json',
//        //    'success' : function (rs) {
//        //        jThis.css({opacity: '1'}).removeClass('disabled');
//        //        if (rs.status == 'ok') {
//        //            alert('Your message has been posted, please wait patiently administrator audit, thank you for your submission');
//        //            // alert('您的留言已提交,请耐心等候管理员回复');
//        //            jForm.get(0).reset();
//        //        } else {
//        //            alert(rs.content);
//        //            return false;
//        //        }
//        //    }
//        //});

//                $.ajax({
//            'url' : '/send-contact',
//            'data': jForm.serializeArray(),
//            'type' : 'post',
//            'dataType' : 'json',
//            'success' : function (rs) {
//                jThis.css({opacity: '1'}).removeClass('disabled');
//                if (rs.status == 'ok') {
//                    alert('Your message has been posted, please wait patiently administrator audit, thank you for your submission');
//                    // alert('您的留言已提交,请耐心等候管理员回复');
//                    jForm.get(0).reset();
//                } else {
//                    alert(rs.content);
//                    return false;
//                }
//            }
//        });
//        return false;
//    });
//});


//$(function () {
//    $('.form2').on('click', '.send2', function (event) {
//        event.preventDefault();
//        var jForm = $(event.delegateTarget), jThis = $(this);
//        if (jThis.hasClass('disabled')) {
//            alert('Please wait...');
//            return;
//        }

//        var reg_email = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;

//        var data = {
//            id: 'book', _spam: '{$session.project_spam}',
//            title: $('input[name="title"]', jForm).val().trim(),
//            email: $('input[name="email"]', jForm).val().trim(),
//            content: $('textarea[name="content"]', jForm).val().trim(),
//        };


//        if (!data.email || !reg_email.test(data.email)) {
//            alert('Please enter a valid email address');
//            // alert('请输入一个有效的邮箱地址');
//            $('input[name="email"]', jForm).focus();
//            return false;
//        } else if (!data.title) {
//            alert('The Name cannot be empty');
//            // alert('请输入留言主题');
//            $('input[name="title"]', jForm).focus();
//            return false;
//        } else if (!data.content) {
//            alert('The message cannot be empty');
//            // alert('请输入留言内容');
//            $('textarea[name="content"]', jForm).focus();
//            return false;
//        }


//        jThis.css({opacity: '.5'}).addClass('disabled');

//        $.ajax({
//            'url' : 'api.php?c=post&f=save&_t='+(new Date().getTime()),
//            'data': jForm.serializeArray(),
//            'type' : 'post',
//            'dataType' : 'json',
//            'success' : function (rs) {
//                jThis.css({opacity: '1'}).removeClass('disabled');
//                if (rs.status == 'ok') {
//                    alert('Your message has been posted, please wait patiently administrator audit, thank you for your submission');
//                    // alert('您的留言已提交,请耐心等候管理员回复');
//                    jForm.get(0).reset();
//                } else {
//                    alert(rs.content);
//                    return false;
//                }
//            }
//        });
//        return false;
//    });
//});
