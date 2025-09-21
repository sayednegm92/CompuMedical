"use strict";
function showSwalAlert(message, flag = 0) {
    debugger;
    if (flag == 1) {
        Swal.fire({
            title: 'Save Successfully!',
            text: message,
            icon: 'success',
            showConfirmButton: false,
            timer: 3000,
            position: 'center'
        });
    }
    else if (flag == 2) {
        Swal.fire({
            title: 'Warning!',
            text: message,
            icon: 'warning',
            showConfirmButton: false,
            timer: 5000,
            position: 'center'
        });
    }
    else if (flag == 3) {
        Swal.fire({
            title: 'Error',
            text: message,
            icon: 'error',
            showConfirmButton: false,
            timer: 3000,
            position: 'center'
        });
    }
};