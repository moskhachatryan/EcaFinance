$(document).on('click', '.open-modal', function () {
    $('#dialogContent').load(this.href, function () {
        $('#dialogDiv').modal({
            backdrop: 'static',
            keyboard: false
        }, 'show').on('shown.bs.modal', function () {

        });
        bindForm(this);
    });
    return false;
});

function bindForm(dialog) {
}