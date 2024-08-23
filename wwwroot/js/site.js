// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    if ($('#employeeTable tbody tr').length > 1) {
        $('#employeeTable').DataTable({
            stateSave: true,
            "bDestroy": true,
            stateDuration: 60,
            lengthMenu: [
                [5, 10, 15, 20, 25, 50, -1],
                [5, 10, 15, 20, 25, 50, 'All']
            ],
            language: {
                searchPlaceholder: "Type here to search.."
            },
            columnDefs: [
                {
                    targets: 0,
                    orderSequence: ['asc', 'desc']

                },
                {
                    targets: 1,
                    orderSequence: ['asc', 'desc']
                },
                {
                    targets: 2,
                    orderSequence: ['asc', 'desc']
                },
                {
                    targets: 3,
                    orderSequence: ['asc', 'desc']
                },
                {
                    targets: 4,
                    orderSequence: ['asc', 'desc']
                },
                {
                    targets: 5,
                    orderSequence: ['asc', 'desc']
                },
                {
                    targets: 6,
                    searchable: false,
                    orderable: false,
                }
            ],
            order: [[0, 'asc']]

        });
    }
})
document.addEventListener('DOMContentLoaded', function () {
    handleDepartmentChange();
    
});

function handleDepartmentChange() {
    var departmentSelect = document.getElementById('Department');
    var departmentIdInput = document.getElementById('DepartmentId');
    var departmentIdHidden = document.getElementById('DepartmentIdHidden');

    console.log(departmentSelect);
    console.log(departmentIdInput);
    console.log(departmentIdHidden);

    if (departmentSelect) {
        var selectedValue = departmentSelect.value;

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
    } else {
        console.error("Department element not found.");
    }
}

function deletePopup(employeeID, firstName, lastName) {
    document.getElementById('deleteMessage').textContent = `Do you really want to delete this record for ${firstName} ${lastName}?`;
    document.getElementById('deleteButton').href = `Employees/DeleteEmployees/${employeeID}`;

    var deleteModal = new bootstrap.Modal(document.getElementById('deletePopup'));
    deleteModal.show();
}

function viewPopup(employeeID, firstName, lastName, email, phoneNumber, Department, DepartmentID) {
    document.getElementById('viewPopupModalLabel').textContent = `Details for ${firstName} ${lastName}`;
    document.getElementById('viewEmail').textContent = `Email : ${email}`;
    document.getElementById('viewPhoneNumber').textContent = `Phone Number : ${phoneNumber}`;
    document.getElementById('viewDepartment').textContent = `Department : ${Department}`;
    document.getElementById('viewDepartmentID').textContent = `Department ID : ${DepartmentID}`;

    var myModal = new bootstrap.Modal(document.getElementById('viewPopupModal'));
    myModal.show();

}