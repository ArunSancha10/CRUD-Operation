// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function handleDepartmentChange() {
    var selectedValue = document.getElementById('Department').value;
    var departmentIdInput = document.getElementById('DepartmentId');
    var departmentIdHidden = document.getElementById('DepartmentIdHidden');

    switch (selectedValue) {
        case 'Development':
            departmentIdInput.value = 'IT-01';
            departmentIdHidden.value = 'IT-01';
            break;
        case 'Support':
            departmentIdInput.value = 'IT-02';
            departmentIdHidden.value = 'IT-02';
            break;
        case 'Admin':
            departmentIdInput.value = 'IT-03';
            departmentIdHidden.value = 'IT-03';
            break;
        case 'QA':
            departmentIdInput.value = 'IT-04';
            departmentIdHidden.value = 'IT-04';
            break;
        default:
            departmentIdInput.value = '';
            departmentIdHidden.value = '';
    }
}