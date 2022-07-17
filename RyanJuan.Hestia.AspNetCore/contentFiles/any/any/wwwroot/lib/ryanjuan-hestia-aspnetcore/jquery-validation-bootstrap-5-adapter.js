"use strict";

document.addEventListener("DOMContentLoaded", function () {
    if (window.jQuery &&
        window.jQuery.validator &&
        window.jQuery.validator.unobtrusive &&
        window.bootstrap &&
        window.bootstrap.Tooltip &&
        window.bootstrap.Tooltip.VERSION &&
        window.bootstrap.Tooltip.VERSION.startsWith("5")) {
        window.jQuery.validator.setDefaults({
            errorClass: "",
            validClass: "",
            highlight: function (element, errorClass, validClass) {
                window.jQuery(element).addClass("is-invalid").removeClass("is-valid");
                window.jQuery(element.form).find(`[data-valmsg-for=${element.id}]`).addClass("invalid-feedback");
            },
            unhighlight: function (element, errorClass, validClass) {
                window.jQuery(element).addClass("is-valid").removeClass("is-invalid");
                window.jQuery(element.form).find(`[data-valmsg-for=${element.id}]`).removeClass("invalid-feedback");
            },
        });
    }
});
