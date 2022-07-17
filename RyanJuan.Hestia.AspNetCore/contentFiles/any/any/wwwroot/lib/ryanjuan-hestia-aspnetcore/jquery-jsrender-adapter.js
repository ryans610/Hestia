"use strict";

document.addEventListener("DOMContentLoaded", function () {
    if (window.jQuery &&
        typeof window.jQuery().__proto__.render === "function" &&
        typeof window.jQuery().__proto__.renderToDOM !== "function") {
        window.jQuery().__proto__.renderToDOM = function (data) {
            return window.jQuery(this.render(data))[0];
        };
    }
});
