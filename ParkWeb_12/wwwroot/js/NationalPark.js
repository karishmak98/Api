﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "NationalPark/GetAll",
            "type": "GET",
            "datatype":"json"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            { "data": "state", "width": "40%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                             <div class="text-center">
                             <a href="NationalPark/Upsert/${data}" class="btn btn-info">
                                 <i class="fas fa-edit"></i>
                              </a>
                             <a class="btn btn-danger" onclick=Delete("NationalPark/Delete/${data}")>
                                 <i class="fas fa-trash-alt"></i>
                             </a>
                             </div>
                    `;
                }
            }
        ]
    })
}


function Delete(url) {
    swal({
        title: "Want to delete data?",
        text: "Delete Information!!!",
        buttons: true,
        icon: "warning",
        dangerModel: true,
    }).then((willdelete) => {
        if (willdelete) {
            $.ajax({
                url: url,
                type: "Delete",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }

    })
}