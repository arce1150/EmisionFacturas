function ParametrosSistema() {
    this.TIPOOPERACION_CATALOGO51 = 51;
    this.TIPODOCUMENTOIDENTIDAD_CATALOGO6 = 6;
    this.TIPOMONEDA_CATALOGO2 = 2;
    this.UNIDADMEDIDA_CATALOGO3 = 3;
    this.TIPOSDETRIBUTO_CATALOGO5 = 5;
    this.TIPOCALCULOISC_CATALOGO8 = 8;
    this.TIPOAFECTACIONIGV_CATALOGO7 = 7;
    this.TIPOPRODUCTO_CATALOGO100 = 100;
}
String.Format = function (b) {
    var a = arguments;
    return b.replace(/(\{\{\d\}\}|\{\d\})/g, function (b) {
        if (b.substring(0, 2) === "{{") return b;
        var c = parseInt(b.match(/\d/)[0]);
        return a[c + 1];
    });
};
function toDate(dateStr) {
    var parts = dateStr.split("/");
    return new Date(parts[2], parts[1] - 1, parts[0]);
}
function ValidarDecimal(e, input) { 
    var key = e.keyCode ? e.keyCode : e.which; 
    if (key == 8) return true;
    if (key > 47 && key < 58) {
        if (input.value === "") return true;
        var existePto = (/[.]/).test(input.value);
        if (existePto === false) {
            regexp = /.[0-9]{10}$/;
        }
        else {
            regexp = /.[0-9]{2}$/;
        }
        return !(regexp.test(input.value));
    }
    if (key == 46) {
        if (input.value === "") return false;
        regexp = /^[0-9]+$/;
        return regexp.test(input.value);
    }
    return false;
}
function IsNumber(b) {
    var a = document.all ? b.keyCode : b.which;
    if (a <= 13 || a >= 48 && a <= 57) {
        b.returnValue = true;
        return true;
    }
    else {
        b.returnValue = false;
        return false;
    }
}
function ajax(url, tipoMetodo, tipoRpta, metodo, parametrosUrl) {

    var xhr = new XMLHttpRequest();
    xhr.open(tipoMetodo, url, true);
    //if (tipoMetodo == "post") xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded; charset=utf-8");
    if (tipoMetodo == "post") {
        xhr.setRequestHeader("Content-type", "application/json; charset=utf-8");
        //xhr.setRequestHeader('Content-Length', parametrosUrl.length);
    }
    xhr.responseType = tipoRpta;
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            if (tipoRpta == "" || tipoRpta == "text") {
                metodo(xhr.responseText, true);
            }
            else {
                metodo(xhr.response);
            }
        }
    }
    if (tipoMetodo == "post") {
        xhr.send(parametrosUrl);
    } else {
        xhr.send();
    }
}
var waitingDialog = waitingDialog || (function ($) {
    'use strict';

    // Creating modal dialog's DOM
    var $dialog = $(
        '<div class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
        '<div class="modal-dialog modal-m" style="border: 0;padding: 0;">' +
        '<div id="modal-procesando" class="modal-content">' +
        '<div class="modal-header"><h3 style="margin:0;"></h3></div>' +
        '<div class="modal-body">' +
        '<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar" style="width: 100%"></div></div>' +
        '</div>' +
        '</div></div></div>');

    return { 
        show: function (message, options) { 
            if (typeof options === 'undefined') {
                options = {};
            }
            if (typeof message === 'undefined') {
                message = 'Procesando...';
            }
            var settings = $.extend({
                dialogSize: 'm',
                progressType: '',
                onHide: null  
            }, options);
            $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
            $dialog.find('.progress-bar').attr('class', 'progress-bar');
            if (settings.progressType) {
                $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
            }
            $dialog.find('h3').text(message);
            if (typeof settings.onHide === 'function') {
                $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                    settings.onHide.call($dialog);
                });
            }
            $dialog.modal('show');
        },
        hide: function () {
            $dialog.modal('hide');
            $(".modal-backdrop").remove();
            $("body").removeAttr("style");
        }
    };

})(jQuery);