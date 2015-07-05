//$(function () {
//    $('#dragandrophandler').DragDropTool({
//        fileExtensions: "jpg, png, gif",
//        allowedExtensions: true,
//        maxFileSize: "",
//        uploadStr: 'upload',
//        dragDropStr: "Drag & Drop or Select Files"
//    });
//});

var Ads = {

    getModelosByMarca: function (element) {
        angular.element('#marca').scope().marca = $(element).val();
        angular.element('#marca').scope().getModelosByMarcaforJquery();
    },

    doAfterMessage: function () {
        $('.replySuccess').removeClass('hidden').fadeIn().html('Su mensaje ha sido enviado...');
        $('#formContact')[0].reset();
        setTimeout(function () { $('.replySuccess').addClass('hidden'); }, 3000)
    },

    initCarousel: function () {
        console.log('aki');
        $("#myCarousel").carousel("cycle");

        $(".item").click(function () {
            $("#myCarousel").carousel(1);
        });

        $(".left").click(function () {
            $("#myCarousel").carousel("prev");
        });
    }
    
};