// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.getElementById('rolSelect').addEventListener('change', function () {
        var selectedRole = this.value;
        var montoInicialField = document.getElementById('montoInicialField');

        if (selectedRole === 'Cliente') {
            montoInicialField.style.display = 'block';
        } else {
            montoInicialField.style.display = 'none';
        }
    });