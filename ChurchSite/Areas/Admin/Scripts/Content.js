function Edit(Id) {
    $.ajax({
        url: "/Content/GetArticle/" + Id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            debugger
            $('#Id').val(result.Id);
            $('#Title').val(result.Title);
            $('#Description').val(result.Description);
            $('#PublishedBy').val(result.PublishedBy);
            $('#ImageEdit').attr("src","data:image/gif;base64,"+result.ImageString);
            $('#CategoryID').val(result.CategoryID);
            if (result.IsFeatured == true)
                $('#IsFeatured').attr("checked","checked");
            else
                $('#IsFeatured').remove("checked");
            GetSelectedSubCategory(Id);
        },
        error: function (errormessage) {
            var message = errormessage.responseText;
        }
    });
}
function GetSelectedSubCategory(Id) {
    $.ajax({
        url: "/Content/GetSelectedSubCategory/" + Id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            html = "";
            $.each(result, function (index, row) {
                html += "<option value='"+row.Value+"' selected='"+row.Selected+"'>"+row.Text+" </option>"
            });
            $("#SubCategoryEdit").html(html);
        },
        error: function (errormessage) {
            var message = errormessage.responseText;
        }
    });
}

const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
})
function Prompt(ID) {
    Swal.fire({
        title: 'Confirmation',
        text: "Are you sure, you want to delete this?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Content/DeleteArticle/" + ID,
                type: "Post",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Deleted successfully',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    window.location.reload(true);
                },
                error: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Delete Failed',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
        }
        else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Deactivation cancelled :)',
                'error'
            )
        }
    })
}

function PromptArticleContent(ID) {
    Swal.fire({
        title: 'Confirmation',
        text: "Are you sure, you want to remove this?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Content/DeleteArticleContent/" + ID,
                type: "Post",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Deleted successfully',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    window.location.reload(true);
                },
                error: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Delete Failed',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
        }
        else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Removal cancelled :)',
                'error'
            )
        }
    })
}
function PromptPrayer(ID) {
    Swal.fire({
        title: 'Confirmation',
        text: "Are you sure, you want to remove this?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Content/DeletePrayer/" + ID,
                type: "Post",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Deleted successfully',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    window.location.reload(true);
                },
                error: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Delete Failed',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
        }
        else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Removal cancelled :)',
                'error'
            )
        }
    })
}

function PromptAnnouncement(ID) {
    Swal.fire({
        title: 'Confirmation',
        text: "Are you sure, you want to remove this?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Content/AnnouncementPrayer/" + ID,
                type: "Post",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Deleted successfully',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    window.location.reload(true);
                },
                error: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Delete Failed',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
        }
        else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Removal cancelled :)',
                'error'
            )
        }
    })
}

function PromptSacrament(ID) {
    Swal.fire({
        title: 'Confirmation',
        text: "Are you sure, you want to remove this?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Content/DeleteSacrament/" + ID,
                type: "Post",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Deleted successfully',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    window.location.reload(true);
                },
                error: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Delete Failed',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
        }
        else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Removal cancelled :)',
                'error'
            )
        }
    })
}
function PromptOrganisation(ID) {
    Swal.fire({
        title: 'Confirmation',
        text: "Are you sure, you want to remove this?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Content/DeleteOrganisation/" + ID,
                type: "Post",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Deleted successfully',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    window.location.reload(true);
                },
                error: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Delete Failed',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
        }
        else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Removal cancelled :)',
                'error'
            )
        }
    })
}

function PromptGallery(ID) {
    Swal.fire({
        title: 'Confirmation',
        text: "Are you sure, you want to remove this?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Content/DeleteGallery/" + ID,
                type: "Post",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Deleted successfully',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    window.location.reload(true);
                },
                error: function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Delete Failed',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
        }
        else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Removal cancelled :)',
                'error'
            )
        }
    })
}
