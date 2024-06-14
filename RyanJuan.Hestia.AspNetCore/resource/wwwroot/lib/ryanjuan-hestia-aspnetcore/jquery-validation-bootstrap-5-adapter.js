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
                highlightInvalid(element);
            },
            unhighlight: function (element, errorClass, validClass) {
                const elementDOM = window.jQuery(element);
                elementDOM.addClass("is-valid").removeClass("is-invalid");
                if (elementDOM.is(":radio")) {
                    elementDOM.parent().addClass("is-valid").removeClass("is-invalid");
                }
                window.jQuery(element.form).find(`[data-valmsg-for=${element.id}]`).removeClass("invalid-feedback");
            },
        });
        window.jQuery(".input-validation-error").each((i, element) => {
            highlightInvalid(element);
        });
        window
            .jQuery(".input-group:has(.field-validation-valid), .input-group:has(.field-validation-error)")
            .addClass("has-validation");
    }

    function highlightInvalid(element) {
        const elementDOM = window.jQuery(element);
        elementDOM.addClass("is-invalid").removeClass("is-valid");
        if (elementDOM.is(":radio")) {
            elementDOM.parent().addClass("is-invalid").removeClass("is-valid");
        }
        window.jQuery(element.form).find(`[data-valmsg-for=${element.id}]`).addClass("invalid-feedback");
    }
});
