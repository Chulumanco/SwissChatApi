const displaySwal = (data, message) => {

    if (data == false) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: message,
            showConfirmButton: true,
            //timer: 10000,
            confirmButtonText: 'Ok'

        }).then(function () {
            location.reload();
        });
    }
    else {
        Swal.fire({
            icon: 'success',
            title: 'Success',
            text: message,
            showConfirmButton: true,
            confirmButtonText: 'Ok'
            //timer: 10000
        }).then(function () {
            location.reload();
        });
    }
};