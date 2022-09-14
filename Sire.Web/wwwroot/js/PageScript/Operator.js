function Delete() {
    swal({
        title: 'Confirmation',
        text: 'Are You Sure You Want To Delete Operator?',
        type: 'warning',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'Confirm',
        confirmButtonColor: '#8CD4F5',
        cancelButtonText: 'Cancel',
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (isConfirm) {

            if (isConfirm) {
                window.location.href = '/operator/';
            }
        }
    );
}