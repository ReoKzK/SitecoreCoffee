var CartSearch = function() {

    var ids = {
        form: "#cart-search-form",
        result: "#cart-search-result"
    };

    $(ids.form).submit(function(e) {
        e.preventDefault();

        var url = $(this).attr("action");
        var data = $(this).serialize();

        $.ajax({
            url: url,
            method: "POST",
            data: data,
            success: function (result) {
                $(ids.result).html(result);
            }
        });
    });
}

$(document).ready(function() {
    document.cartSearch = CartSearch();
});