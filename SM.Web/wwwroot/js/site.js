// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    var cnpjElements = document.querySelectorAll(".cnpj-mask");
    cnpjElements.forEach(function (element) {
        var cnpj = element.textContent;
        element.textContent = formatarCNPJ(cnpj);
    });
});

function formatarCNPJ(cnpj) {
    // Remove caracteres indesejados
    cnpj = cnpj.replace(/[^\d]/g, "");

    // Aplica a máscara
    return cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/, "$1.$2.$3/$4-$5");
}
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
function IsNumber(input) {
    const IsNumber = input.value.replace(/\D/g, '');
    input.value = IsNumber;
}
function capitalizeWords(str) {
    return str.replace(/\b\w/g, function (char) {
        return char.toUpperCase();
    });
}