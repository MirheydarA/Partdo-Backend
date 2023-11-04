$(document).ready(function () {
    $('.addToCart').on('click', function () {
        var id = $(this).data('id');

        $.ajax({
            type: 'GET',
            url: '/basket/add',
            data: {
                id: id
            },
            success: function (data) {
                // Handle success response here
                toastr.success('Product added successfully', '', { positionClass: 'toast-bottom-right' });
            },
            error: function (data) {
                // Handle error response here
                toastr.error('Error: ' + error, '', { positionClass: 'toast-bottom-right' });
            }
        });
    });
});