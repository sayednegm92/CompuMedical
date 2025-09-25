"use strict";

function getCookie(name) {
    let cookieArr = document.cookie.split(";");
    for (let i = 0; i < cookieArr.length; i++) {
        let cookiePair = cookieArr[i].split("=");
        if (name.trim() === cookiePair[0].trim()) {
            return decodeURIComponent(cookiePair[1]);
        }
    }
    return null;
};
function apiRequest(method, url, data = null,dataType = 'json') {
    debugger;
    return new Promise((resolve, reject) => {
        let token = getCookie("AuthToken");
        if (!token) {
            showSwalAlert('Authentication token not found. Please log in again.', 3);
            reject("No token");
            return;
        }

        $.ajax({
            type: method,
            url: url,
            data:data==null? null: JSON.stringify(data),
            contentType: "application/json",
            headers: {
                "Authorization": "Bearer " + token
            },
            dataType: dataType,
            success: function (res) {
                if (res) {
                    resolve(res);
                    return;
                } 
                else {
                    let msg = res?.message ?? 'No data found';
                    showSwalAlert(msg, 2);
                    reject(msg);
                }
            },
            error: function (xhr, status, error) {
                let errors = xhr.responseText + ', ' + error;
                showSwalAlert(errors, 3);
                reject(errors);
            }
        });
    });
}

function showSwalAlert(message = null, flag = 0) {
    if (flag == 1) {
        Swal.fire({
            title: 'Save Successfully!',
            text: message ?? 'Saved Successfully!',
            icon: 'success',
            showConfirmButton: false,
            timer: 3000,
            position: 'center'
        });
    }
    else if (flag == 2) {
        Swal.fire({
            title: 'Warning!',
            text: message ?? 'Something went wrong!',
            icon: 'warning',
            showConfirmButton: false,
            timer: 5000,
            position: 'center'
        });
    }
    else if (flag == 3) {
        Swal.fire({
            title: 'Error',
            text: message ?? 'Something went Error!',
            icon: 'error',
            showConfirmButton: false,
            timer: 5000,
            position: 'center'
        });
    }
};