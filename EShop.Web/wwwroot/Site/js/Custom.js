// var swal = require("sweetalert2");

// function ShowMessage(title, text, theme) {
//     window.createNotification({
//         closeOnClick: true,
//         displayCloseButton: false,
//         positionClass: 'nfc-bottom-right',
//         showDuration: 4000,
//         theme: theme != '' ? theme : 'success'
//     })({
//         title: title != '' ? title : 'اعلان',
//         message: decodeURI(text)
//     });
// }
function ShowMessage(title, text, theme) {
    Swal.fire({
        title: title || 'اعلان',
        text: text || '',
        icon: theme || 'success',
        confirmButtonText: 'باشه',
        confirmButtonColor: '#8b6f47'
    });
}