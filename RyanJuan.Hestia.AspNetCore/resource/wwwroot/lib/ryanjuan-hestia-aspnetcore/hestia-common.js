"use strict";

class HestiaCommon {
    static isUndefined(value) {
        return typeof value === "undefined";
    }

    static isNull(value) {
        return value === null;
    }

    static isFunction(value) {
        return typeof value === "function";
    }

    static isNullOrUndefined(value) {
        return HestiaCommon.isNull(value) || HestiaCommon.isUndefined(value);
    }

    static isNullOrUndefinedOrEmpty(value) {
        return HestiaCommon.isNullOrUndefined(value) || value === "";
    }

    static createStyleInstance(id, content = null) {
        return new HestiaCommon.#styleInstance(id, content);
    }

    static #styleInstance = class {
        constructor(id, content = null) {
            this.#id = id;
            let style = document.head.querySelector(`style#${id}`);
            if (HestiaCommon.isNullOrUndefined(style)) {
                style = document.createElement("style");
                style.id = id;
                document.head.appendChild(style);
            }
            if (!HestiaCommon.isNullOrUndefined(content)) {
                style.textContent = content;
            }
            this.#style = style;
        }

        #id = undefined;
        #style = undefined;

        get id() { return this.#id; }

        setContent(content) {
            this.#style.textContent = content;
        }
    }

    static #domParser = new DOMParser();

    static parseHtmlString(html) {
        return HestiaCommon.#domParser.parseFromString(html, "text/html").body.firstChild;
    }

    static chunkArray(arr, size) {
        if (arr.length > size) {
            return [arr.slice(0, size), ...HestiaCommon.chunkArray(arr.slice(size), size)];
        }
        return [arr];
    }

    static scrollIntoView(element) {
        if (HestiaCommon.isFunction(element.scrollIntoViewIfNeeded)) {
            element.scrollIntoViewIfNeeded();
        } else {
            element.scrollIntoView({ behavior: "smooth" });
        }
    }
}
