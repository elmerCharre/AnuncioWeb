/**
 * This plugin has been developed to implement drag and drop functionality to preview the files to upload.
 * @param URL_TARGET  : Indicates the path in which the files will get uploaded
 * @param MAX_FILES  : Indicates the maximum number of files to upload
 * @param Extensions : Indicates the allowed file formats up : {'IMAGES', 'DOCUMENTS', 'ARCHIVES', 'AUDIO', 'VIDEO', '*'}
 * @author Elmer Charre Salazar <elmer.nyd@gmail.com>
 */

$(document).ready(function() {
    browser_support_api = (typeof FormData !== "undefined") ? true : false;
    
    //$(':submit').click(function(e) {
    //    e.preventDefault();
    //    $(window).scrollTop(0);
    //    alert(countFiles);
    //    if (setting.filesRequired) {
    //        if (countFiles > 0) {
    //            //validate required form objects
    //            $.each(setting.objectRequired, function(idObject, errorStr) {
    //                idObject = '#' + idObject;
    //                if (!$(idObject).val() && $(idObject).length > 0) {
    //                    //showMessageLabel(errorStr);
    //                    $(idObject).attr('class', 'objectRequired');
    //                    //$(idObject).after($("<label class='required-input'><img src='"+pathFile+"/images/required.png' title='"+errorStr+"'></label>"));
    //                    $(idObject).after($("<img src='"+pathFile+"/images/required.png' title='"+errorStr+"'>"));
    //                    $(idObject).focus();
    //                    //sendData = false;
    //                    //return false;
    //                }
    //            });
    //            //if (sendData) submitData();
    //        } else showMessageLabel(setting.noFilesErrorStr);
    //    }
    //});
});

var setting;
var browser_support_api = true;
var array_ObjectFile = new Array();
var object_dragging = "";
var idObjectForm = "";
var countObjectFiles = 0;
var currentFileSize = 0;
var pathFile = '';
var countFiles = 0;
var value = 0;

jQuery.fn.extend({
    DragDropTool: function(options) {
        setting = $.extend({
            url: "",
            method: "POST",
            filesRequired: true,
            fileExtensions: "*",
            allowedExtensions: true,
            fileName: "files[]",
            maxNumberFiles: 100,
            maxFileSize: -1,
            autoSubmit: true,
            showSend: true,
            showDelete: true,
            returnType: 'json',
            uploadStr: "Upload",
            idObjectInputFile: "uploadFileJs_dragdrop",
            dragDropStr: "Drag & Drop Files",
            noFilesErrorStr: "is not allowed. No file selected.",
            sizeErrorStr: "is not allowed. Allowed Max size: ",
            numberErrorStr: "is not allowed. Allowed Max number: ",
            defaultExtensionStr: "all extensions",
            objectRequired: {}
        }, options);
        
        if (setting.fileExtensions === '*') setting.allowedExtensions = true;
        if (setting.autoSubmit) setting.showSend = false;

        if(!browser_support_api) {
            $(this).text('');
            $(this).DinamicUpload();
            return;
        }
        
        return this.each(function() {
            object_dragging = $(this);
            object_dragging.addClass("dragandrophandler").text(setting.dragDropStr);
            object_dragging.before($('<div class="message-label">'));
            showMessageLabel(setting.fileExtensions);
            //create object input file type
            var input_file = document.createElement('div');
            $(input_file).css({'padding-top': '6px', display: 'block', height: '39px'});
            input_file.innerHTML = '<input type="file" name="'+setting.fileName+'" id="'+setting.idObjectInputFile+'" multiple>';
            object_dragging.before(input_file);
            setting.idObjectInputFile = '#' + setting.idObjectInputFile;
            $(setting.idObjectInputFile).nicefileUpload();

            object_dragging.on("dragenter", function(e) {
                e.stopPropagation();
                e.preventDefault();
                object_dragging.css("border", "1px solid #B3B3B3");
            });

            object_dragging.on("dragover", function(e) {
                e.stopPropagation();
                e.preventDefault();
            });

            object_dragging.on("dragleave", function(e) {
                e.stopPropagation();
                e.preventDefault();
                object_dragging.css("border", "1px dashed #B3B3B3");
            });

            object_dragging.on("drop", function(e) {
                e.stopPropagation();
                e.preventDefault();
                object_dragging.css("border", "1px dashed #B3B3B3");
                handleFileUpload(e.originalEvent.dataTransfer.files, object_dragging);
            });

            $(document).on('dragenter', function(e) {
                e.stopPropagation();
                e.preventDefault();
            });

            $(document).on('dragover', function(e) {
                e.stopPropagation();
                e.preventDefault();
            });

            $(document).on('drop', function(e) {
                e.stopPropagation();
                e.preventDefault();
            });

            $(setting.idObjectInputFile).change(function(e) {
                e.stopPropagation();
                e.preventDefault();
                handleFileUpload(e.target.files, object_dragging);
            });
        });
    },
    DinamicUpload: function() {
        if (typeof $("form").attr('id') === 'undefined') $("form").attr('id', 'form_uploadFiles');
        idObjectForm = '#' + $("form").attr('id');
        object_dragging = $(this);
        $('.dragandrophandler').remove();
        object_dragging.before($('<div class="message-label">'));
        showMessageLabel(setting.fileExtensions);
        //create object input file type
        var input_file = document.createElement('div');
        $(input_file).css({'padding-top':'6px', display:'block', 'margin-bottom':'8px', height:'40px'});
        input_file.innerHTML = '<input type="file" class="uploadInputFile" id="uploadInputFile_0" name="'+setting.fileName+'" onchange="setFileName(this, 0);">';
        input_file.innerHTML += '<a id="addInputFile" href="javascript:void(0)" onclick="return addInputFile();"></a>';
        object_dragging.before(input_file);
        $("#uploadInputFile_0").nicefileinput();
    }
});

function handleFileUpload(files, object_dragging) {
    $.each(files, function(key, data) {
        if (isFileTypeAllowed(data.name)) {
            if (!searchFile(data.name)) {
                var objClassFile = new classFile(object_dragging);
                objClassFile.setFileNameSize(data);
                objClassFile.setAbortFile(data, null);
                //(setting.autoSubmit) ? setSerializeForm(data, objClassFile) : sendFile(data, objClassFile);
            }
        }
    });
    $(setting.idObjectInputFile).val('');
}

function classFile(object_dragging) {
    if (countObjectFiles < setting.maxNumberFiles) {
        countObjectFiles++;

        this.setFileNameSize = function(file) {
            file.id = countObjectFiles;
            this.statusbar = $("<div class='status-upload' id="+(countObjectFiles-1)+"></div>");
            this.image = $("<img class='status-image' id='img_preview"+countObjectFiles+"' src=''>").appendTo(this.statusbar);
            this.filename = $("<div class='status-filename'></div>").appendTo(this.statusbar).html(file.name);
            //this.progress_bar = $("<div class='progress-bar'></div>").appendTo(this.statusbar);
            //this.progress = $("<div class='status-bar'></div>").appendTo(this.progress_bar);
            this.size = $("<div class='status-size'></div>").appendTo(this.statusbar);
            this.sizereal = $("<div style='display:none;'></div>").appendTo(this.statusbar).html(file.size);
            if (setting.showSend) this.send = $("<div class='send_data'>").appendTo(this.statusbar);
            if (setting.showDelete) this.abort = $("<div class='remove_data'>").appendTo(this.statusbar);
            object_dragging.after(this.statusbar);
            if (file.type.match('image.*')) {
                var reader = new FileReader();
                reader.onload = (function(file) {
                    return function(e) {
                        $('#img_preview' + file.id).css('background', 'transparent');
                        $('#img_preview' + file.id).attr('src', e.target.result);
                    };
                })(file);
                reader.readAsDataURL(file);
                delete reader;
            } else
                $('#img_preview' + file.id).attr('src', pathFile + "images/icon_file/" + get_icon_document(file.name));
            
            var length = file.size;
            currentFileSize = currentFileSize + length;
            length = parseFloat(length/1024);
            length = (length > 1024) ? ((length/1024).toFixed(2) + " MB") : (length.toFixed(2) + " KB");
            this.size.html(length);
            array_ObjectFile.push(file);
            countFiles++;
        };
        this.setSendFile = function(formData, objClassFile) {
            if (setting.showSend) {
                this.send.click(function() {
                    setSerializeForm(formData, objClassFile);
                });
            }
        };
        this.setAbortFile = function(formData, jqxhr) {
            if (setting.showDelete) {
                var sb = this.statusbar;
                var size = this.sizereal;
                this.abort.click(function() {
                    if (jqxhr) jqxhr.abort();
                    currentFileSize = currentFileSize - parseFloat(size.text());
                    delete array_ObjectFile[sb.attr("id")];
                    delete formData;
                    sb.remove();
                    countFiles--;
                });
            }
        };
        this.setProgress = function(percent) {
            this.progress.text(percent + "%");
            this.progress.css({width:percent + "%"});
        };
    } else
        showMessageLabel(setting.numberErrorStr + " " + setting.maxNumberFiles);
}

function sendFile(formData, objClassFile) {
    objClassFile.setSendFile(formData, objClassFile);
}

function setSerializeForm(formData, objClassFile) {
    
    var totalSize = (objClassFile) ? formData.size : currentFileSize;
    if (totalSize > setting.maxFileSize && setting.maxFileSize !== -1)
        showMessageLabel(setting.sizeErrorStr + ' ' + ((setting.maxFileSize/1024)/1024).toFixed(2) + ' MB');
    else {
        if (objClassFile) {
            formData = new Array(formData);
            objClassFile.progress_bar.css({border: "1px solid", 'border-radius': '5px'});
            if (setting.showSend) objClassFile.send.remove();
        }
        var data_files = new FormData();
        //set the input files of the form by dragging
        $.each(formData, function(key, data) {
            if (data) data_files.append(setting.fileName, data);
        });
        
        data_files.append('totalSize', totalSize);
        //set the values of form input
        $.each($(":file"), function(key, data) {
            data_files.append(data.name, data);
        });
        var formValues = $("form").serializeArray();
        $.each(formValues, function(key, data) {
            data_files.append(data.name, data.value);
        });
        //send files to server
        //sendFileToServer(data_files, objClassFile);
    }
}

function sendFileToServer(formData, objClassFile) {
    value = 0;
    var jqXHR = $.ajax({
        xhr: function() {
            var xhrobj = $.ajaxSettings.xhr();
            if (xhrobj.upload) {
                xhrobj.upload.addEventListener('progress', function(event) {
                    value = Math.ceil((event.loaded || event.position)/event.total*100);
                    if (objClassFile) objClassFile.setProgress(value);
                    else {
                        $('.status-bar').css({width: value + '%'});
                        $('.status-bar').text(value + "%");
                    }
                }, false);
            }
            return xhrobj;
        },
        url: setting.url,
        type: setting.method,
        dataType: setting.returnType,
        contentType: false,
        processData: false,
        cache: false,
        data: formData,
        success: function(url) {
            delete formData;
            setting.maxFileSize -= 215;
            currentFileSize -= 215;
            countFiles--;
            if (objClassFile) {
                delete array_ObjectFile[objClassFile.statusbar.attr("id")];
                objClassFile.statusbar.remove();
            } else (validateURL(url)) ? (window.location.href = url) : window.location.reload();
        }
    });
    if (objClassFile) objClassFile.setAbortFile(formData, jqXHR);
}

function getFiles() {
    var data_files = new FormData();
    $.each(array_ObjectFile, function(key, data) {
        if (data) data_files.append(setting.fileName, data);
    });
    return data_files;
}

function searchFile(fileName) {
    var result = false;
    $.each(array_ObjectFile, function(key, data) {
        if (data && fileName === data.name) {
            result = true;
            return false;
        }
    });
    return result;
}

function validateURL(url) {
    return /http/i.test(url);
}

function preventSubmit() {
    showMessageLabel('');
    $('.status-bar').attr('class', '');
    $('.message-label').append($('<div class="status-bar"></div>'));
    $('.message-label').attr('class', 'status-progress');
    $(':submit').attr("disabled", true);
    $('.dragandrophandler').remove();
    $('img.remove_data').remove();
    $('img.send_data').remove();
    $('.NFI_InputUpload').remove();
}

function submitData() {
    if (!browser_support_api) SubmitForm();
    else {
        preventSubmit();
        setSerializeForm(array_ObjectFile, null);
    }
}

function SubmitForm() {
    preventSubmit();
    value = 0;
    var progressSimulator;
    $('#addInputFile').hide();
    $(idObjectForm).ajaxForm({
        type: setting.method,
        url: setting.url,
        dataType: setting.returnType,
        beforeSend: function() {
            $(".status-bar").css({width: "0%"});
            $(".status-bar").text("0%");
            progressSimulator = setInterval(function() { progressFallback(); }, 2000);
        },
        success: function(url) {
            clearInterval(progressSimulator);
            $(".status-bar").css({width: "100%"});
            $(".status-bar").text("100%");
            (validateURL(url)) ? (window.location.href = url) : window.location.reload();
        },
        error: function() {
            setTimeout('SubmitForm()', 300);
        }
    }).submit();
}

function progressFallback() {
    if(value < 97) {
        value += 1;
        $('.status-bar').css({width: value + '%'});
        $('.status-bar').text(value + "%");
    }
}

!function(a) {
    a.fn.nicefileUpload = function() {
        return this.each(function() {
            var b = this;
            a(b).wrap(a("<div>").addClass("NFI_InputUpload").text(setting.uploadStr)),
            a(b).attr("title", setting.uploadStr);
        });
    };
}(jQuery);

!function(a) {
    a.fn.nicefileinput = function() {
        return this.each(function() {
            var b = this;
            a(b).after(a('<input type="text" class="upload_txtInputFile" id="txtInputFile0" disabled>')),
            a(b).wrap(a("<div>").addClass("NFI_InputFile").html(setting.uploadStr)),
            a(b).attr("title", setting.uploadStr);
        });
    };
}(jQuery);

function addInputFile() {
    countObjectFiles++;
    var newInput = document.createElement('div');
    $(newInput).css({'margin-left':0, 'margin-top':'8px'});
    newInput.innerHTML = '<div class="NFI_InputFile">' + setting.uploadStr + '\n\
                          <input type="file" class="uploadInputFile" name="'+setting.fileName+'" id="uploadInputFile_'+countObjectFiles+'" title="'+setting.uploadStr+'" onchange="setFileName(this, '+countObjectFiles+');"></div>\n\
                          <input type="text" class="upload_txtInputFile" id="txtInputFile'+countObjectFiles+'" disabled>';
    object_dragging.append(newInput);
}

function setFileName(objInputUpload, item) {
    if (isFileTypeAllowed(objInputUpload.value)) {
        $('#txtInputFile' + item).val(objInputUpload.value.replace('C:\\fakepath\\', ''));
        countFiles++;
    } else {
        if ($('#txtInputFile' + item).val() && countFiles > 0) countFiles--;
        $('#txtInputFile' + item).val('');
        $('#type_file' + item).replaceWith($('#type_file' + item).clone(true));
    }
}

function showMessageLabel(message) {
    $('.message-label').attr('class', (setting.allowedExtensions || message === '*') ? 'message-label accept' : 'message-label no-accept');
    $('.message-label').text((message !== '*') ? message : setting.defaultExtensionStr);
    //setTimeout(function() { $('.message-label').text(setting.fileExtensions); }, 4000);
    setTimeout(function() { $('.message-label').text((setting.fileExtensions !== '*') ? setting.fileExtensions : setting.defaultExtensionStr); }, 4000);
}

function isFileTypeAllowed(fileName) {
    if (setting.fileExtensions === '*') return true;
    var fileExtensions = setting.fileExtensions.toLowerCase().replace(/ /g, '');
    fileExtensions = fileExtensions.split(",");
    var ext = fileName.split('.').pop().toLowerCase();
    return (setting.allowedExtensions) ? (($.inArray(ext, fileExtensions) > -1) ? true : false) : (($.inArray(ext, fileExtensions) > -1) ? false : true);
}

function get_icon_document(file) {
    file = (file.substring(file.lastIndexOf("."))).toLowerCase();
    switch (file) {
        case '.doc': case '.dot': case '.rtf': case '.mcw': case '.wps': case '.psw': case '.docm': case '.docx': case '.dotm':
        case '.dotx': case '.odt': 
            file = 'word-icon.png';
            break;
        case '.pdf':
            file = 'pdf-icon.png';
            break;
        case '.xls': case '.xlt': case '.pxl': case '.xlsx': case '.xlsm': case '.xlam': case '.xlsb': case '.xltm': case '.xltx':
            file = 'excel-icon.png';
            break;
        case '.zip': case '.tar': case '.rar': case '.gz': case '.jar': case '.cab':
            file = 'compress-icon.png';
            break;
        case '.htm': case '.html': case '.htx': case '.xml': case '.xsl': case '.xhtml': case '.dxhtml':
            file = 'html-icon.png';
            break;
        case '.js': case '.css': case '.java': case '.php': case '.phps': case '.asp': case '.cpp':
            file = 'web-icon.png';
            break;
        case '.mp3': case '.wav': case '.wma': case '.m4a': case '.aac': case '.ac3': case '.mka': case '.mpc': case '.ogg':
            file = 'audio-icon.png';
            break;
        case '.txt': case '.log':
            file = 'txt-icon.png';
            break;
        case '.swf': case '.fla':
            file = 'flash-icon.png';
            break;
        case '.ppt': case '.pps': case '.pptm': case '.pptx': case '.potm': case '.potx': case '.ppam': case '.ppsm': case '.ppsx':
            file = 'ppt-icon.png';
            break;
        default:
            file = 'def-icon.png';
            break;
    }
    return file;
}