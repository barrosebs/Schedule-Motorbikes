// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function maskCNPJ(input) {
    const CPFNumber = input.value.replace(/\D/g, '');

    let formattedCPFNumber = '';
    for (let i = 0; i < CPFNumber.length; i++) {
        if (i === 2) {
            formattedCPFNumber += '.';
        } else if (i === 5) {
            formattedCPFNumber += '.';
        } else if (i === 8) {
            formattedCPFNumber += '/';
        } else if (i === 12) {
            formattedCPFNumber += '-';
        }
        formattedCPFNumber += CPFNumber[i];
    }

    input.value = formattedCPFNumber;
}

function capitalizeWords(str) {
    return str.replace(/\b\w/g, function (char) {
        return char.toUpperCase();
    });
}