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
async function apiRequest(method, url, data = null, token = null) {
    const options = {
        method: method,
        headers: {
            "Content-Type": "application/json",
        },
    };

    if (token) {
        options.headers["Authorization"] = `Bearer ${token}`;
    }

    if (data) {
        options.body = JSON.stringify(data);
    }

    try {
        const response = await fetch(url, options);

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        return await response.json();
    } catch (error) {
        throw error;
    }
}

function showSwalAlert(message=null, flag = 0) {
    if (flag == 1) {
        Swal.fire({
            title: 'Save Successfully!',
            text: message??'Saved Successfully!',
            icon: 'success',
            showConfirmButton: false,
            timer: 3000,
            position: 'center'
        });
    }
    else if (flag == 2) {
        Swal.fire({
            title: 'Warning!',
            text: message??'Something went wrong!',
            icon: 'warning',
            showConfirmButton: false,
            timer: 5000,
            position: 'center'
        });
    }
    else if (flag == 3) {
        Swal.fire({
            title: 'Error',
            text: message??'Something went Error!',
            icon: 'error',
            showConfirmButton: false,
            timer: 5000,
            position: 'center'
        });
    }
};