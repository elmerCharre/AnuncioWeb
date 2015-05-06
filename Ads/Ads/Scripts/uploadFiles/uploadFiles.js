/**
 * This plugin has been developed to implement drag and drop functionality to preview the files to upload.
 * @param URL_TARGET  : Indicates the path in which the files will get uploaded
 * @param MAX_FILES  : Indicates the maximum number of files to upload
 * @param Extensions : Indicates the allowed file formats up : {'IMAGES', 'DOCUMENTS', 'ARCHIVES', 'AUDIO', 'VIDEO', '*'}
 * @author Elmer Charre Salazar <elmer.nyd@gmail.com>
 */

var setting;
var array_ObjectFile = new Array();
var object_dragging = "";
var idObjectForm = "";
var countObjectFiles = 0;
var currentFileSize = 0;
var pathFile = '';
var countFiles = 0;
var value = 0;

jQuery.fn.extend({
    DragDropTool: function (options) {
        setting = $.extend({
            method: "POST",
            filesRequired: true,
            fileExtensions: "*",
            allowedExtensions: true,
            fileName: "files[]",
            maxNumberFiles: 10,
            maxFileSize: -1,
            showDelete: true,
            returnType: 'json',
            uploadStr: "Upload",
            idObjectInputFile: "uploadFileJs_dragdrop",
            dragDropStr: "Drag & Drop Files",
            noFilesErrorStr: "is not allowed. No file selected.",
            sizeErrorStr: "is not allowed. Allowed Max size: ",
            numberErrorStr: "is not allowed. Allowed Max number: ",
            defaultExtensionStr: "all extensions",
            supportAPI: true,
        }, options);

        setting.supportAPI = (typeof FormData !== "undefined") ? true : false;
        setting.allowedExtensions = (setting.fileExtensions === '*') ? true : false;

        if (!setting.supportAPI) {
            $(this).text('');
            $(this).DinamicUpload();
            return;
        }

        return this.each(function () {
            object_dragging = $(this);
            //object_dragging.addClass("dragandrophandler").text(setting.dragDropStr);
            object_dragging.addClass("dragandrophandler").html("<span>" + setting.dragDropStr + "</span>" +
                "<input type='file' name='" + setting.fileName + "' id='" + setting.idObjectInputFile + "' multiple>" +
                "<div class='list_files_dragging'></div>");

            //object_dragging.before($('<div class="message-label">'));
            showMessageLabel(setting.fileExtensions);
            //create object input file type
            //var input_file = document.createElement('div');
            //$(input_file).css({'padding-top': '6px', display: 'block', height: '25px'});
            //input_file.innerHTML = '<input type="file" name="'+setting.fileName+'" id="'+setting.idObjectInputFile+'" multiple>';

            //object_dragging.before(input_file);
            setting.idObjectInputFile = '#' + setting.idObjectInputFile;
            //$(setting.idObjectInputFile).nicefileUpload();

            object_dragging.on("dragenter", function (e) {
                e.stopPropagation();
                e.preventDefault();
                object_dragging.css("border", "1px solid #B3B3B3");
            });

            object_dragging.on("dragover", function (e) {
                e.stopPropagation();
                e.preventDefault();
            });

            object_dragging.on("dragleave", function (e) {
                e.stopPropagation();
                e.preventDefault();
                object_dragging.css("border", "1px dashed #B3B3B3");
            });

            object_dragging.on("drop", function (e) {
                e.stopPropagation();
                e.preventDefault();
                object_dragging.css("border", "1px dashed #B3B3B3");
                var objClassFile = new classFile(object_dragging);
                objClassFile.handleFileUpload(e.originalEvent.dataTransfer.files, object_dragging);
            });

            $(document).on('dragenter', function (e) {
                e.stopPropagation();
                e.preventDefault();
            });

            $(document).on('dragover', function (e) {
                e.stopPropagation();
                e.preventDefault();
            });

            $(document).on('drop', function (e) {
                e.stopPropagation();
                e.preventDefault();
            });

            $(setting.idObjectInputFile).change(function (e) {
                e.stopPropagation();
                e.preventDefault();
                var objClassFile = new classFile(object_dragging);
                objClassFile.handleFileUpload(e.target.files, object_dragging);
            });
        });
    },
    DinamicUpload: function () {
        if (typeof $("form").prop('id') === 'undefined') $("form").prop('id', 'form_uploadFiles');
        idObjectForm = '#' + $("form").prop('id');
        object_dragging = $(this);
        $('.dragandrophandler').remove();
        object_dragging.before($('<div class="message-label">'));
        showMessageLabel(setting.fileExtensions);
        //create object input file type
        var input_file = document.createElement('div');
        $(input_file).css({ 'padding-top': '6px', display: 'block', 'margin-bottom': '8px' });
        input_file.innerHTML = '<input type="file" class="uploadInputFile" id="uploadInputFile_0" name="' + setting.fileName + '" onchange="setFileName(this, 0);">';
        input_file.innerHTML += '<a id="addInputFile" href="javascript:void(0)" onclick="return addInputFile();"></a>';
        object_dragging.before(input_file);
        $("#uploadInputFile_0").nicefileinput();
    }
});

function classFile(object_dragging) {
    this.setFileNameSize = function (file) {
        if (countObjectFiles < setting.maxNumberFiles) {
            countObjectFiles++;

            file.id = countObjectFiles;
            //if (countObjectFiles < 1)
            this.statusbar = $("<div class='status-upload' id=" + (countObjectFiles - 1) + " onhover='test(" + (countObjectFiles - 1) + ");'></div>");
            //alert(this.statusbar);
            this.image = $("<div class='status-image'><img class='status-image' id='img_preview" + countObjectFiles + "' src=''></div>").appendTo(this.statusbar);
            //this.filename = $("<div class='status-filename'></div>").appendTo(this.statusbar).html(file.name);
            //this.progress_bar = $("<div class='progress-bar'></div>").appendTo(this.statusbar);
            //this.progress = $("<div class='status-bar'></div>").appendTo(this.progress_bar);
            //this.size = $("<div class='status-size'></div>").appendTo(this.statusbar);
            //this.sizereal = $("<div style='display:none;'></div>").appendTo(this.statusbar).html(file.size);
            //if (setting.showDelete) this.abort = $("<div class='remove_data'>").appendTo(this.statusbar);
            //object_dragging.after(this.statusbar);
            this.statusbar.appendTo($(".list_files_dragging"));

            if (file.type.match('image.*')) {
                var reader = new FileReader();
                reader.onload = (function (file) {
                    return function (e) {
                        $('#img_preview' + file.id).css('background', 'transparent');
                        $('#img_preview' + file.id).prop('src', e.target.result);
                    };
                })(file);
                reader.readAsDataURL(file);
                delete reader;
            } else
                $('#img_preview' + file.id).prop('src', pathFile + "images/icon_file/" + get_icon_document(file.name));

            var length = file.size;
            currentFileSize = currentFileSize + length;
            length = parseFloat(length / 1024);
            length = (length > 1024) ? ((length / 1024).toFixed(2) + " MB") : (length.toFixed(2) + " KB");
            //this.size.html(length);
            array_ObjectFile.push(file);
            countFiles++;

            if (countObjectFiles > 0)
                object_dragging.css('height', 'auto');
        } else
            showMessageLabel(setting.numberErrorStr + " " + setting.maxNumberFiles);
    }

    this.handleFileUpload = function (files, object_dragging) {
        $.each(files, function (key, data) {
            //if (this.isFileTypeAllowed(data.name)) {
                if (!searchFile(data.name)) {
                    setFileNameSize(data);
                    setAbortFile(data, null);
                }
            //}
        });
        $(setting.idObjectInputFile).val('');
    }

    this.isFileTypeAllowed = function (fileName) {
            if (setting.fileExtensions === '*') return true;
            var fileExtensions = setting.fileExtensions.toLowerCase().replace(/ /g, '');
            fileExtensions = fileExtensions.split(",");
            var ext = fileName.split('.').pop().toLowerCase();
            return (setting.allowedExtensions) ? (($.inArray(ext, fileExtensions) > -1) ? true : false) : (($.inArray(ext, fileExtensions) > -1) ? false : true);
    }

    this.searchFile = function (fileName) {
            var result = false;
            $.each(array_ObjectFile, function (key, data) {
                if (data && fileName === data.name) {
                    result = true;
                    return false;
                }
            });
            return result;
    }

    this.setAbortFile = function (formData, jqxhr) {
            if (setting.showDelete) {
                //var sb = this.statusbar;
                //var size = this.sizereal;
                //this.abort.click(function() {
                //    if (jqxhr) jqxhr.abort();
                //    currentFileSize = currentFileSize - parseFloat(size.text());
                //    delete array_ObjectFile[sb.prop("id")];
                //    delete formData;
                //    sb.remove();
                //    countFiles--;
                //});
            }
    }

    this.setProgress = function (percent) {
            this.progress.text(percent + "%");
            this.progress.css({ width: percent + "%" });
    }
}

function setSerializeForm(formData, objClassFile) {

    var totalSize = (objClassFile) ? formData.size : currentFileSize;
    if (totalSize > setting.maxFileSize && setting.maxFileSize !== -1)
        showMessageLabel(setting.sizeErrorStr + ' ' + ((setting.maxFileSize / 1024) / 1024).toFixed(2) + ' MB');
    else {
        if (objClassFile) {
            formData = new Array(formData);
            objClassFile.progress_bar.css({ border: "1px solid", 'border-radius': '5px' });
        }
        var data_files = new FormData();
        //set the input files of the form by dragging
        $.each(formData, function (key, data) {
            if (data) data_files.append(setting.fileName, data);
        });
        alert(totalSize);
        data_files.append('totalSize', totalSize); alert(data_files['totalSize']);
        //set the values of form input
        $.each($(":file"), function (key, data) {
            data_files.append(data.name, data);
        });
        var formValues = $("form").serializeArray();
        $.each(formValues, function (key, data) {
            data_files.append(data.name, data.value);
        });
        //send files to server
        //sendFileToServer(data_files, objClassFile);
    }
}

!function (a) {
    a.fn.nicefileUpload = function () {
        return this.each(function () {
            var b = this;
            a(b).wrap(a("<div>").addClass("NFI_InputUpload").text(setting.uploadStr)),
            a(b).prop("title", setting.uploadStr);
        });
    };
}(jQuery);

!function (a) {
    a.fn.nicefileinput = function () {
        return this.each(function () {
            var b = this;
            a(b).after(a('<input type="text" class="upload_txtInputFile" id="txtInputFile0" disabled>')),
            a(b).wrap(a("<div>").addClass("NFI_InputFile").html(setting.uploadStr)),
            a(b).prop("title", setting.uploadStr);
        });
    };
}(jQuery);

function addInputFile() {
    countObjectFiles++;
    var newInput = document.createElement('div');
    $(newInput).css({ 'margin-left': 0, 'margin-top': '8px' });
    newInput.innerHTML = '<div class="NFI_InputFile">' + setting.uploadStr + '\n\
                          <input type="file" class="uploadInputFile" name="'+ setting.fileName + '" id="uploadInputFile_' + countObjectFiles + '" title="' + setting.uploadStr + '" onchange="setFileName(this, ' + countObjectFiles + ');"></div>\n\
                          <input type="text" class="upload_txtInputFile" id="txtInputFile'+ countObjectFiles + '" disabled>';
    object_dragging.append(newInput);
}

function setFileName(objInputUpload, item) {
    /*if (isFileTypeAllowed(objInputUpload.value)) {
        $('#txtInputFile' + item).val(objInputUpload.value.replace('C:\\fakepath\\', ''));
        countFiles++;
    } else {
        if ($('#txtInputFile' + item).val() && countFiles > 0) countFiles--;
        $('#txtInputFile' + item).val('');
        $('#type_file' + item).replaceWith($('#type_file' + item).clone(true));
    }*/
}

function showMessageLabel(message) {
    $('.message-label').prop('class', (setting.allowedExtensions || message === '*') ? 'message-label accept' : 'message-label no-accept');
    $('.message-label').text((message !== '*') ? message : setting.defaultExtensionStr);
    //setTimeout(function() { $('.message-label').text(setting.fileExtensions); }, 4000);
    setTimeout(function () { $('.message-label').text((setting.fileExtensions !== '*') ? setting.fileExtensions : setting.defaultExtensionStr); }, 4000);
}