/// <reference path="jquery-1.8.2.js" />
/// <reference path="jquery-ui-1.8.24.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
/// <reference path="knockout-2.2.0.debug.js" />
/// <reference path="modernizr-2.6.2.js" />

function updateSuccess(data) {
    $(".priceLoading").hide();
    var currencyrates = JSON.parse(data);
    

    $(".price").each(function () {
        var newvalue = $("#pricenew" + this.id);
        var oldCurrency = $("#oldCurrency" + this.id).html();
        if (oldCurrency) {
            var oldprice = parseFloat($(this).html());
            var rate = parseFloat(currencyrates[oldCurrency]);
            var newprice = oldprice * rate;
            newvalue.html(newprice.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
            $("#currencynew" + this.id).html($("#SelectedCurrency").val());
        }
    });
    $(".calculatedPrice").show();
}
function HideFieldsShowLoading() {
    $(".priceLocation").hide();
    $(".priceLoading").show();
    $(".calculatedPrice").hide();
}

function updatefailed() {
    $(".priceLocation").show();
    $(".priceLoading").hide();
    $(".calculatedPrice").hide();

}
