
$(document).ready(function () {
    $(':input[required]').each(function (index, element) {
        $("label[for='" + element.id + "']").html(function (_, html) {
            var parts = html.split(" ");
            if (parts.indexOf("*") == -1) {
                parts.push('<span class=co-red>', '*', '</span>');
            }
            return parts.join(" ");
        });
    });

    $(".select2").select2();

    toggleDisplayNone("DivCheque", false);
    toggleDisplayNone("OnlineTrans", false);
    toggleDisplayNone("JointAccountDropdown", false);

    $(".btn-outline-primary").click(function () {
        $(this[0]).attr('checked', true);
    });

});

function toggleDisplayNone(element, show) {
    $('.' + element).toggleClass('display-none', !show);
    $('.' + element).each(function () {
        $(this).find(show ? "input[notrequired]" : "input[required]").each(function () {
            $(this).removeAttr(show ? "notrequired" : "required");
            $(this).attr(show ? "required" : "notrequired", true);
        });
    });
}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}

function showPayment(control) {
    toggleDisplayNone("DivCheque", control.value == "2");
    toggleDisplayNone("OnlineTrans", control.value == "3");
}

var btn;
function validate(ctl, event) {
    btn = ctl;
    if (validateForm($(btn).closest('form').attr('id'))) {
        event.preventDefault();
        swal({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            type: 'warning',
            showCancelButton: true,
            confirmButtonClass: 'btn btn-success',
            cancelButtonClass: 'btn btn-danger m-l-10',
            confirmButtonText: 'Yes, complete it!'
        }).then(function (isconfirm) {
            if (isconfirm) {
                $(btn).closest('form').submit();
                swal(
                    'Done!',
                    'Transaction completed.'
                );
            }
        }).catch(swal.noop);
    }
}

function gridWarning(ctl, event, gridCallBack) {
    event.preventDefault();
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger m-l-10',
        confirmButtonText: 'Yes, complete it!'
    }).then(function (isconfirm) {
        if (isconfirm) {
            gridCallBack.call(this, $(ctl).attr('id'));
            
        }
    }).catch(swal.noop);
}

function validateForm(formName) {
    var isValid = true;
    $("#" + formName + " input").each(function () {
        if (!this.checkValidity()) {
            isValid = false;
        }
    });
    return isValid;
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}