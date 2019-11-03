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
        } else { 
            toastr["warning"]("Ha ocurrido un error en el sistema " + xhr.status);
            waitingDialog.hide();
        }
    }
    if (tipoMetodo == "post") {
        xhr.send(parametrosUrl);
    } else {
        xhr.send();
    }
}
function ValidacionFormularios() {
    this.RegVacio = /^\s*$/;
    this.FechaValida= /^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/g; 
    this.trim = function (IdInputText) {
        var o = document.getElementById(IdInputText).value
        return o.replace(/^\s+|\s+$/g, "");
    };
    this.ValidarFecha = function (inputId) {
        var flagerror = true;
        var o = document.getElementById(inputId).value;
        el = document.getElementById(inputId);
        el.classList.remove("error"); 
        if (this.RegVacio.test(o)) {
            flagerror = false;
            document.getElementById(inputId).className += " error";
            return flagerror;
        }
        if (!this.FechaValida.test(o)) {
            flagerror = false;
            document.getElementById(inputId).className += " error";
            return flagerror;
        }
        return flagerror;
    };
    this.EsComboRequerido = function (IdCombo) {
        var flagerror = true;
        var o = document.getElementById(IdCombo).value;
        if (document.getElementById(IdCombo).classList.length > 0) document.getElementById(IdCombo).classList.remove("error");
        if (o == "-1" || o == "0" || o == "" || typeof (o) == undefined || o == null || this.RegVacio.test(o)) {
            document.getElementById(IdCombo).className += " error";
            flagerror = false;
        }
        return flagerror;
    };
    this.EsCampoRequerido = function (IdInputText) {
        var flagerror = true;
        var o = document.getElementById(IdInputText);
        if (o.value == "" || typeof (o.value) == undefined || o.value == null || this.RegVacio.test(o.value)) {
            o.className += " error";
            flagerror = false;
        }
        return flagerror;
    };
    this.SubirArchivo = function (IdInputFile, Index) {
        var o = document.getElementById(IdInputFile);
        var extesion = [];
        var NombreArchivo = null;
        var n = 0;
        var tamanio = 0;
        if (o === null) o = document.getElementsByName(IdInputFile.name)[Index];
        if (o.classList.length > 0) o.classList.remove("error");
        if (o.value.length > 0) {
            NombreArchivo = o.value;
            extesion = NombreArchivo.split(".");
            n = extesion.length;
            tamanio = o.files[0].size;
            if (ARCHIVOSPERMITIDOS.split("|").indexOf(extesion[n - 1].toString().toLowerCase()) > 0) {
                o.value = "";
                o.classList.add("error");
                //bootbox.alert({
                //    message: "No se permite subir éste tipo de archivos",
                //    size: 'small'
                //});
                return false;
            }
            if (tamanio > UPLOADMAXIMO) {
                o.value = "";
                o.classList.add("error");
                //bootbox.alert({
                //    message: "El archivo excede los "+ (UPLOADMAXIMO/1024)+" mb",
                //    size: 'small'
                //});
                return false;
            }
            var Archivo = o.files[0];
            var formData = new FormData();
            formData.append("Archivo", Archivo);
            $.ajax({
                type: "post",
                url: BASE_URLHOME + "/SubirArchivos",
                data: formData,
                contentType: false,
                processData: false,
                async: false,
                success: function (response) {
                    var ObjArchivo = response.d;
                    document.getElementById(IdInputFile).setAttribute("data-value", ObjArchivo.HasArchivo);
                    return true;
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.log("Error en la funcion SubirArchivo : " + xhr.status.toString() + "\n" + thrownError.toString());
                }
            });
        }
        return true;

    };
    this.RemoverClassModal = function (IdDiv) {
        //var form = $("#" + IdDiv + " .form-control");
        var form = document.getElementById(IdDiv).getElementsByClassName("form-control");
        var NroElementos = form.length;
        var clase = null; var IdElemento = null;
        var el = null, hijos = null, len = 0, ind = 0, nextEl = null;
        for (var i = 0; i < NroElementos; i++) {
            try {
                clase = form[i].classList;
                if (typeof (form[i].id) !== undefined && form[i].id !== "") {
                    IdElemento = form[i].id;
                    el = document.getElementById(IdElemento);
                    el.classList.remove("error"); 
                    nextEl = el.parentElement.getElementsByTagName("span");
                    len = nextEl.length;
                    for (var x = 0; x < len; x++) {
                        if (nextEl[x].classList.contains("error")) {
                            nextEl[x].remove(); break;
                        }
                    }
                }
                else if (clase.length !== 0) {
                    form[i].classList.remove("error");
                }


            } catch (err) {
                //console.log("Elemento:" + i);
            }
        }
    };
    this.ImprimirMensajeError = function (ElemetoId, MensajeError) {
        var Tipoelemento = document.getElementById(ElemetoId);
        var ElementoPadre = null;
        var spanError = document.createElement("span");
        spanError.setAttribute("class", "error");
        spanError.innerHTML = MensajeError;
        if (Tipoelemento.type === "text" || Tipoelemento.type === "select-one" || Tipoelemento.type === "file"
            || Tipoelemento.type === "textarea" || Tipoelemento.type === "select-multiple") {
            //$("#" + ElemetoId).after("<span class='error'>" + MensajeError + "</span>"); 
            ElementoPadre = document.getElementById(ElemetoId).parentNode;
            ElementoPadre.appendChild(spanError);
        } else if (Tipoelemento.type === "radio") {
            //$("#" + ElemetoId).parent().after("<span class='error'>" + MensajeError + "</span>");
            ElementoPadre = document.getElementById(ElemetoId).parentNode;
            ElementoPadre.appendChild(spanError);
        }

    };
    this.ValidaLongitudCadena = function (InputText, min, max) {
        var flagerror = true;
        var o = document.getElementById(InputText);
        if ((o.value.length > max || o.value.length < min) || o.value === null || this.RegVacio.test(o.value)) {
            o.classList.add("error");
            flagerror = false;
        }
        return flagerror;
    };
    this.ArchivoPermitido = function (IdInputFile, Index) {
        var o = document.getElementById(IdInputFile);
        var extesion = [];
        var NombreArchivo = null;
        var n = 0;
        var flagerror = true;
        if (o === null) o = document.getElementsByName(IdInputFile.name)[Index];
        if (o.value.length > 0) {
            NombreArchivo = o.value;
            extesion = NombreArchivo.split(".");
            n = extesion.length;
            if (ARCHIVOSPERMITIDOS.split("|").indexOf(extesion[n - 1].toString().toLowerCase()) > 0) {
                o.value = "";
                flagerror = false;
            }
        }
        return flagerror;
    };
    this.TamanioPermitido = function (IdInputFile, Index) {
        var flagerror = true;
        var o = document.getElementById(IdInputFile);
        var tamanio = 0;
        if (o === null) o = document.getElementsByName(IdInputFile.name)[Index];
        if (o.value.length > 0) {
            tamanio = o.files[0].size;
            if (tamanio > UPLOADMAXIMO) {
                o.value = "";
                flagerror = false;
            }
        }
        return flagerror;
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