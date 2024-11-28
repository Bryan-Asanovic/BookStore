var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#producttable').DataTable({
        'language': {
            'url': '//cdn.datatables.net/plug-ins/2.1.8/i18n/nl-NL.json'
        },
        'ajax': {
            'url': '/admin/product/GetAll'
        },
        'columns': [
            { 'data': 'title', 'width': '30%' },
            { 'data': 'isbn', 'width': '15%' },
            { 'data': 'price', 'width': '10%' },
            { 'data': 'author', 'width': '25%' },
            { 'data': 'category.name', 'width': '20%' },
            {
                'data': 'id',
                'render': function (data) {
                    return `
                        <div class="m-25 btn-group" role="group">
                        <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-primary mx-2 w-25">
                        <i class="bi bi-pencil-square mx-2"></i> Wijzig</a>
                        <a onclick="Delete('/admin/product/Delete/'+${data})"
                            class="btn btn-danger w-25"> <i class="bi bi-trash-fill mx-2"></i> Wis</a>
                        </div>
                    `
                }
            }
        ]
    });
}

function Delete(url) {
    swal.fire({
        title: 'Zeker weten?',
        text: 'Deze actie kan niet ongedaan gemaakt worden',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ja, verwijder maar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message); 1
                    }
                }
            });
        }
    });
}