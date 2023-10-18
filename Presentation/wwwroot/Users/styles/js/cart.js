//$(document).on('click', '.addToCart', function () {
//    var id = $(this).data('id');
//    $.ajax({
//        method: "POST",
//        url: "/basket/add",
//        data: {
//            id: id
//        },
//        success: function (res) {
//            //console.log(res)
//        },
//        error: function (err) {
//            //alert(err.responseText)
//        }
//    })
//});


$(document).on('click', '.increaseCount', function (e) {
    //e.preventDefault();

    //var increaseCount = $(this);

    var id = $(this).data('id');

    $.ajax({
        method: "GET",
        url: "/basket/increase/",
        data: {
            id: id
        },
        success: function (res) {
            window.location.reload()
            //console.log("clicked")
            //var countElement = $(increaseCount).parent().siblings().eq(3);
            //var stockElement = $(increaseCount).parent().siblings().eq(4);
            //console.log(stockElement);
            //var count = parseInt(countElement.html());
            //var stock = parseInt(stockElement.html());

            //if (count < stock) {
            //    count++;
            //    countElement.html(count);
            //}
        },
        error: function (err) {
            alert(err.responseText)
        }
    })
})

$(document).on('click', '.decreaseCount', function (e) {
    //e.preventDefault();
    var decreaseCount = $(this);

    var id = $(this).data('id');

    $.ajax({
        method: "GET",
        url: "/basket/decrease",
        data: {
            id: id
        },
        success: function (res) {
            var countElement = $(decreaseCount).parent().siblings().eq(3);
            var count = parseInt(countElement.html());

            if (count > 0) {
                count--;
                countElement.html(count);
            }
            window.location.reload()
        },
        error: function (err) {
            alert(err.responseText)
        }
    })
})